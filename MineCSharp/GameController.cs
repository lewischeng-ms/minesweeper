using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp.Properties
{
    class GameController : IGameController
    {
        #region 数据成员

        // ProcessBlock方法参数的委托类型。
        private delegate void BlockHandler(int rowId, int colId);

        // 游戏视图。
        private IGameView view;

        // 雷区的后端数据。
        private BlockInfo[,] blocks;

        // 随机数生成器
        private Random random;

        // 是否结束游戏？
        private bool isGameEnd;

        // 挖开的方块数。
        private int diggedCount;

        // 剩余雷数。
        private int remainCount;

        // 已用时间。
        private int elapsedTime;

        // 临时变量：某方块周围有标记的方块个数。
        private int flaggedAroundCount;

        #endregion

        #region IGameController实现

        public GameController(IGameView view)
        {
            this.view = view;
            // 初始设定级别为中级。
            StaticData.Level = StaticData.Levels.Intermediate;
            random = new Random();
            CreateBlockInfos();
        }

        public void NewGame()
        {
            ResetMineField();
            ClearState();
            view.ResetUserInterfaceExceptMineField();
            view.UpdateTimeElapsedDisplay(elapsedTime);
            view.UpdateRemainMinesDisplay(remainCount);
        }

        public void TerminateGame()
        {
            isGameEnd = true;
            view.TurnOffTimer();
        }

        public void Cheat()
        {
            ProcessBlocks(StaticData.RowCount, StaticData.ColCount, ShowAllMinesCheatAux);
            remainCount = 0;
        }

        public void TimeTick()
        {
            elapsedTime++;
            view.UpdateTimeElapsedDisplay(elapsedTime);
        }

        public void ProcessMouseDown(int rowId, int colId, StaticData.MyMouseButtons buttons)
        {
            // 游戏结束，不响应事件。
            if (isGameEnd) return;
            // 获取响应方块信息
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            switch (buttons)
            {
                case StaticData.MyMouseButtons.Left:
                    switch (bi.CurrentState)
                    {
                        case BlockInfo.BlockStates.Flagged:
                            view.SetBlockImage(rowId, colId, Resources.FlaggedImage);
                            break;
                        case BlockInfo.BlockStates.Normal:
                            view.SetBlockImage(rowId, colId, Resources.NormalMouseDownImage);
                            break;
                        case BlockInfo.BlockStates.Questioned:
                            view.SetBlockImage(rowId, colId, Resources.QuestionedMouseDownImage);
                            break;
                    }
                    break;
                case StaticData.MyMouseButtons.Right:
                    // 在普通，标记，问号三个状态间顺序切换。
                    switch (bi.CurrentState)
                    {
                        case BlockInfo.BlockStates.Normal:
                            bi.CurrentState = BlockInfo.BlockStates.Flagged;
                            view.SetBlockImage(rowId, colId, Resources.FlaggedImage);
                            remainCount--;
                            break;
                        case BlockInfo.BlockStates.Flagged:
                            bi.CurrentState = BlockInfo.BlockStates.Questioned;
                            view.SetBlockImage(rowId, colId, Resources.QuestionedImage);
                            remainCount++;
                            break;
                        case BlockInfo.BlockStates.Questioned:
                            bi.CurrentState = BlockInfo.BlockStates.Normal;
                            view.SetBlockImage(rowId, colId, Resources.NormalImage);
                            break;
                    }
                    view.UpdateRemainMinesDisplay(remainCount);
                    break;
                case StaticData.MyMouseButtons.Both:
                    TryDigAround(rowId, colId);
                    break;
            }
        }

        public void ProcessMouseUp(int rowId, int colId, StaticData.MyMouseButtons buttons)
        {
            // 游戏结束，不响应事件。
            if (isGameEnd) return;
            // 获取响应方块信息
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            try
            {
                switch (buttons)
                {
                    case StaticData.MyMouseButtons.Left:
                    case StaticData.MyMouseButtons.Right:
                        switch (bi.CurrentState)
                        {
                            case BlockInfo.BlockStates.Digged:
                                return;
                            case BlockInfo.BlockStates.Flagged:
                                view.SetBlockImage(rowId, colId, Resources.FlaggedHighlightImage);
                                break;
                            case BlockInfo.BlockStates.Normal:
                            case BlockInfo.BlockStates.Questioned:
                                // 点开这个方块。
                                if (buttons == StaticData.MyMouseButtons.Left)
                                    DigIt(rowId, colId);
                                break;
                        }
                        break;
                    case StaticData.MyMouseButtons.Both:
                        // 尝试挖开周围方块。
                        DigAround(rowId, colId);
                        break;
                }
            }
            catch (GameEndException)
            { // 已捕捉到游戏结束异常。
                NewGame();
            }
        }

        public void ProcessMouseMove(int rowId, int colId)
        {
            // 游戏结束，不响应事件。
            if (isGameEnd) return;
            // 获取响应方块信息
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            switch (bi.CurrentState)
            {
                case BlockInfo.BlockStates.Digged:
                    // 已被点开，不响应鼠标移动事件。
                    break;
                case BlockInfo.BlockStates.Flagged:
                    view.SetBlockImage(rowId, colId, Resources.FlaggedHighlightImage);
                    break;
                case BlockInfo.BlockStates.Normal:
                    view.SetBlockImage(rowId, colId, Resources.NormalHighlightImage);
                    break;
                case BlockInfo.BlockStates.Questioned:
                    view.SetBlockImage(rowId, colId, Resources.QuestionedHighlightImage);
                    break;
            }
        }

        public void ProcessMouseLeave(int rowId, int colId)
        {
            // 游戏结束，不响应事件。
            if (isGameEnd) return;
            // 获取响应方块信息
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            switch (bi.CurrentState)
            {
                case BlockInfo.BlockStates.Digged:
                    break;
                case BlockInfo.BlockStates.Flagged:
                    view.SetBlockImage(rowId, colId, Resources.FlaggedImage);
                    break;
                case BlockInfo.BlockStates.Normal:
                    view.SetBlockImage(rowId, colId, Resources.NormalImage);
                    break;
                case BlockInfo.BlockStates.Questioned:
                    view.SetBlockImage(rowId, colId, Resources.QuestionedImage);
                    break;
            }
        }

        #endregion

        // 清空游戏动态数据。
        private void ClearState()
        {
            // 清空游戏动态数据。
            diggedCount = 0;
            remainCount = StaticData.MineCount;
            elapsedTime = 0;
            // 开始响应用户事件。
            isGameEnd = false;
        }

        // 获取指定处方块的信息。
        private BlockInfo GetCurrentInfo(int rowId, int colId)
        {
            return blocks[rowId, colId];
        }

        // 点开某方块。
        private void DigIt(int rowId, int colId)
        {
            // 第一次点开一个方块时，生成新雷区，打开计时器
            // 且新雷区保证该方块一定不是雷，即第一次点不到雷。
            if (diggedCount == 0)
            {
                GenerateNewField(rowId, colId);
                view.TurnOnTimer();
            }
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            // 已挖开或已标记的不能挖。
            if (bi.CurrentState == BlockInfo.BlockStates.Digged || bi.CurrentState == BlockInfo.BlockStates.Flagged)
                return;
            TestLose(rowId, colId);
            // 若点到雷是不可能到这里的。
            bi.CurrentState = BlockInfo.BlockStates.Digged;
            diggedCount++;
            switch (bi.Number)
            {
                case 0:
                    view.SetBlockImage(rowId, colId, Resources.DiggedImage);
                    DigAround(rowId, colId);
                    break;
                case 1:
                    view.SetBlockImage(rowId, colId, Resources.Num1);
                    break;
                case 2:
                    view.SetBlockImage(rowId, colId, Resources.Num2);
                    break;
                case 3:
                    view.SetBlockImage(rowId, colId, Resources.Num3);
                    break;
                case 4:
                    view.SetBlockImage(rowId, colId, Resources.Num4);
                    break;
                case 5:
                    view.SetBlockImage(rowId, colId, Resources.Num5);
                    break;
                case 6:
                    view.SetBlockImage(rowId, colId, Resources.Num6);
                    break;
                case 7:
                    view.SetBlockImage(rowId, colId, Resources.Num7);
                    break;
                case 8:
                    view.SetBlockImage(rowId, colId, Resources.Num8);
                    break;
            }
            // 判断是否胜利。
            TestWin();
        }

        // 判断是否赢了？若赢了，则抛出结束游戏异常。
        private void TestWin()
        {
            if (diggedCount == StaticData.SafeCount)
            {
                TerminateGame();
                ShowAllMinesWin();
                view.WinAddProc(elapsedTime);
                throw new GameEndException();
            }
        }

        // 判断点击该方块是否会输？若输了，则抛出结束游戏异常。
        private void TestLose(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            if (bi.IsMine)
            {
                TerminateGame();
                ShowAllMinesLose(rowId, colId);
                view.LoseAddProc(elapsedTime);
                throw new GameEndException();
            }
        }

        #region ProcessBlocks一族方法

        // 处理一定范围内所有方块。
        private void ProcessBlocks(int rowCount, int colCount, BlockHandler Func)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    Func(i, j);
                }
            }
        }

        #region CreateBlockInfo

        // 创建雷区所有方块的后端数据结构。
        private void CreateBlockInfos()
        {
            blocks = new BlockInfo[StaticData.MaxRowCount, StaticData.MaxColCount];
            ProcessBlocks(StaticData.MaxRowCount, StaticData.MaxColCount, CreateBlockInfoAux);
        }

        private void CreateBlockInfoAux(int rowId, int colId)
        {
            blocks[rowId, colId] = new BlockInfo();
        }

        #endregion

        #region NewGame

        private void ResetMineField()
        {
            // 重置雷区数据结构。
            ProcessBlocks(StaticData.MaxRowCount, StaticData.MaxColCount, NewGameAux);
        }

        private void NewGameAux(int rowId, int colId)
        {
            if (rowId < StaticData.RowCount && colId < StaticData.ColCount)
            {
                BlockInfo bi = GetCurrentInfo(rowId, colId);
                if (bi.CurrentState != BlockInfo.BlockStates.Normal)
                    view.SetBlockImage(rowId, colId, Resources.NormalImage);
                bi.CurrentState = BlockInfo.BlockStates.Normal;
                bi.Number = 0;
                bi.IsMine = false;
                view.SetBlockVisible(rowId, colId, true);
            }
            else
                view.SetBlockVisible(rowId, colId, false);
        }

        #endregion

        #region ShowAllMines

        private void ShowAllMinesLoseAux(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            if (bi.IsMine)
            {
                if (bi.CurrentState != BlockInfo.BlockStates.Flagged)
                {
                    bi.CurrentState = BlockInfo.BlockStates.MineShowed;
                    view.SetBlockImage(rowId, colId, Resources.UndiggedMineImage);
                }
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Flagged)
                view.SetBlockImage(rowId, colId, Resources.WrongMineImage);
        }

        private void ShowAllMinesWinAux(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            if (bi.IsMine)
            {
                bi.CurrentState = BlockInfo.BlockStates.Flagged;
                view.SetBlockImage(rowId, colId, Resources.FlaggedImage);
            }
        }

        private void ShowAllMinesCheatAux(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            if (bi.IsMine)
            {
                bi.CurrentState = BlockInfo.BlockStates.Flagged;
                view.SetBlockImage(rowId, colId, Resources.FlaggedImage);
            }
        }

        // 失败时显示出所有地雷。
        private void ShowAllMinesLose(int rowId, int colId)
        {
            ProcessBlocks(StaticData.RowCount, StaticData.ColCount, ShowAllMinesLoseAux);
            view.SetBlockImage(rowId, colId, Resources.DeadMineImage);
        }

        // 成功时显示出所有地雷。
        private void ShowAllMinesWin()
        {
            ProcessBlocks(StaticData.RowCount, StaticData.ColCount, ShowAllMinesWinAux);
        }

        #endregion

        #endregion

        #region ProcessAround族方法
        private void ProcessAround(int rowId, int colId, BlockHandler Func)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            // 上一行
            if (rowId >= 1 && rowId < StaticData.RowCount)
            {
                // 上一列
                if (colId >= 1 && colId < StaticData.ColCount)
                    Func(rowId - 1, colId - 1);
                // 本列
                Func(rowId - 1, colId);
                // 下一列
                if (colId >= 0 && colId < StaticData.ColCount - 1)
                    Func(rowId - 1, colId + 1);
            }
            // 本行
            // 上一列
            if (colId >= 1 && colId < StaticData.ColCount)
                Func(rowId, colId - 1);
            // 本列
            Func(rowId, colId);
            // 下一列
            if (colId >= 0 && colId < StaticData.ColCount - 1)
                Func(rowId, colId + 1);
            // 下一行
            if (rowId >= 0 && rowId < StaticData.RowCount - 1)
            {
                // 上一列
                if (colId >= 1 && colId < StaticData.ColCount)
                    Func(rowId + 1, colId - 1);
                // 本列
                Func(rowId + 1, colId);
                // 下一列
                if (colId >= 0 && colId < StaticData.ColCount - 1)
                    Func(rowId + 1, colId + 1);
            }
        }

        // 辅助函数，设置该方块的Number加1。
        private void GenerateNewFieldAux(int rowId, int colId)
        {
            blocks[rowId, colId].Number++;
        }

        // 产生新的雷区，并保证指定点不是雷。
        private void GenerateNewField(int rowIdNotMine, int colIdNotMine)
        {
            // 随机产生地雷。
            int currentMineCount = 0;
            int rnd;
            int max = StaticData.ColCount * StaticData.RowCount;
            while (currentMineCount < StaticData.MineCount)
            {
                rnd = random.Next(max);
                int rowId = rnd / StaticData.ColCount;
                int colId = rnd % StaticData.ColCount;
                // 保证(rowIdNotMine, colIdNotMine)的周围均不为雷。
                if (StaticData.Level == StaticData.Levels.SuperRP)
                {
                    if (rowId == rowIdNotMine && colId == colIdNotMine)
                        continue;
                }
                else if (Math.Abs(rowId - rowIdNotMine) <= 1 && Math.Abs(colId - colIdNotMine) <= 1)
                    continue;
                if (blocks[rowId, colId].IsMine == true)
                    continue;
                else
                {
                    blocks[rowId, colId].IsMine = true;
                    // 更新方块周围数据。
                    ProcessAround(rowId, colId, GenerateNewFieldAux);
                    currentMineCount++;
                }
            }
        }

        // 辅助函数，用于变换周围方块的背景。
        private void TryDigAroundAux(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            if (bi.CurrentState == BlockInfo.BlockStates.Normal)
            {
                view.SetBlockImage(rowId, colId, Resources.NormalMouseDownImage);
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Questioned)
            {
                view.SetBlockImage(rowId, colId, Resources.QuestionedMouseDownImage);
            }
        }

        // 当双键同时按下时调用，仅用于显示效果，没有具体逻辑。
        private void TryDigAround(int rowId, int colId)
        {
            ProcessAround(rowId, colId, TryDigAroundAux);
        }

        // 辅助函数，用于恢复周围方块背景及统计标记个数。
        private void DigAroundAux1(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            if (bi.CurrentState == BlockInfo.BlockStates.Normal)
            {
                view.SetBlockImage(rowId, colId, Resources.NormalImage);
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Questioned)
            {
                view.SetBlockImage(rowId, colId, Resources.QuestionedImage);
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Flagged)
                flaggedAroundCount++;
        }

        // 辅助函数，决定该方块如何被挖开。
        private void DigAroundAux2(int rowId, int colId)
        {
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            // 点开它。
            DigIt(rowId, colId);
        }

        // 当双键同时按下后释放时调用。
        private void DigAround(int rowId, int colId)
        {
            // 获得该方块周围已标记的方块个数。
            flaggedAroundCount = 0;
            ProcessAround(rowId, colId, DigAroundAux1);
            BlockInfo bi = GetCurrentInfo(rowId, colId);
            // 仅当该方块已被点开时可以挖开周围方块。
            if (bi.CurrentState == BlockInfo.BlockStates.Digged)
            {
                // 判断已标记个数与固有雷数是否相等。
                if (bi.Number == 0 || bi.Number == flaggedAroundCount)
                {
                    // 挖开周围方块。
                    ProcessAround(rowId, colId, DigAroundAux2);
                }
            }
        }

        #endregion
    }
}
