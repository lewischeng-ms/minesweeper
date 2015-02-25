using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using global::MineCSharp.Properties;

namespace MineCSharp
{
    public partial class MainForm : Form
    {
        // 雷区的图形前端。
        private PictureBox[,] pbBlocks;

        public MainForm()
        {
            InitializeComponent();
            CreateBlocks();
            RestartGame();
        }

        public void CreateBlocks()
        {
            pbBlocks = new PictureBox[GameData.MaxRowCount, GameData.MaxColCount];
            // 循环初始化每个图片框。
            for (int i = 0; i < GameData.MaxRowCount; i++)
            {
                for (int j = 0; j < GameData.MaxColCount; j++)
                {
                    pbBlocks[i, j] = new PictureBox();
                    // 设置区分值。
                    pbBlocks[i, j].Tag = new Position(i, j);
                    // 添加事件。
                    pbBlocks[i, j].MouseUp += new MouseEventHandler(Blocks_MouseUp);
                    pbBlocks[i, j].MouseDown += new MouseEventHandler(Blocks_MouseDown);
                    pbBlocks[i, j].MouseMove += new MouseEventHandler(Blocks_MouseMove);
                    pbBlocks[i, j].MouseLeave += new EventHandler(Blocks_MouseLeave);
                    // 设置外观。
                    pbBlocks[i, j].Width = GameData.BlockWidth;
                    pbBlocks[i, j].Height = GameData.BlockHeight;
                    pbBlocks[i, j].BorderStyle = BorderStyle.FixedSingle;
                    pbBlocks[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbBlocks[i, j].Image = Resources.NormalImage;
                    // 设置位置。
                    pbBlocks[i, j].Left = GameData.FieldLeft + j * GameData.BlockWidth;
                    pbBlocks[i, j].Top = GameData.FieldTop + i * GameData.BlockHeight;
                    // 设置可见性。
                    if (i < GameData.RowCount && j < GameData.ColCount)
                        pbBlocks[i, j].Visible = true;
                    else
                        pbBlocks[i, j].Visible = false;
                    // 添加控件。
                    Controls.Add(pbBlocks[i, j]);
                }
            }
        }

        public void RestartGame()
        {
            GameState.ClearState();
            MineField.GenerateNewField();
            AdjustFormSize();
            RenewBlocks();
            UpdateCaption();
        }

        public void RenewBlocks()
        {
            for (int i = 0; i < GameData.MaxRowCount; i++)
            {
                for (int j = 0; j < GameData.MaxColCount; j++)
                {
                    if (i < GameData.RowCount && j < GameData.ColCount)
                    {
                        pbBlocks[i, j].Visible = true;
                        pbBlocks[i, j].Image = Resources.NormalImage;
                    }
                    else
                        pbBlocks[i, j].Visible = false;
                }
            }
        }

        public void AdjustFormSize()
        {
            this.Width = GameData.FieldLeft + GameData.FieldRight + GameData.ColCount * GameData.BlockWidth;
            this.Height = GameData.FieldTop + GameData.FieldBottom + GameData.RowCount * GameData.BlockHeight;
        }

        private void Blocks_MouseMove(object sender, MouseEventArgs e)
        {
            if (GameState.IsGameEnd) return;
            PictureBox pb = sender as PictureBox;
            Position pos = pb.Tag as Position;
            BlockInfo bi = MineField.GetCurrentInfo(pos.RowId, pos.ColId);
            if (bi.CurrentState == BlockInfo.BlockStates.Digged)
                return;
            switch (bi.CurrentState)
            {
                case BlockInfo.BlockStates.Flagged:
                    pb.Image = Resources.FlaggedHighlightImage;
                    break;
                case BlockInfo.BlockStates.Normal:
                    pb.Image = Resources.NormalHighlightImage;
                    break;
                case BlockInfo.BlockStates.Questioned:
                    pb.Image = Resources.QuestionedHighlightImage;
                    break;
            }
        }

        private void Blocks_MouseLeave(object sender, EventArgs e)
        {
            if (GameState.IsGameEnd) return;
            PictureBox pb = sender as PictureBox;
            Position pos = pb.Tag as Position;
            BlockInfo bi = MineField.GetCurrentInfo(pos.RowId, pos.ColId);
            switch (bi.CurrentState)
            {
                case BlockInfo.BlockStates.Digged:
                    return;
                case BlockInfo.BlockStates.Flagged:
                    pb.Image = Resources.FlaggedImage;
                    break;
                case BlockInfo.BlockStates.Normal:
                    pb.Image = Resources.NormalImage;
                    break;
                case BlockInfo.BlockStates.Questioned:
                    pb.Image = Resources.QuestionedImage;
                    break;
            }
        }

        private void Blocks_MouseDown(object sender, MouseEventArgs e)
        {
            if (GameState.IsGameEnd) return;
            PictureBox pb = sender as PictureBox;
            Position pos = pb.Tag as Position;
            BlockInfo bi = MineField.GetCurrentInfo(pos.RowId, pos.ColId);
            if (e.Button == MouseButtons.Left)
            {
                GameState.IsLeftDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                GameState.IsRightDown = true;
            }
            if (GameState.IsLeftDown && GameState.IsRightDown)
                TestAround(pos.RowId, pos.ColId);
            if (bi.CurrentState == BlockInfo.BlockStates.Digged)
                return;
            if (e.Button == MouseButtons.Left)
            {
                switch (bi.CurrentState)
                {
                    case BlockInfo.BlockStates.Flagged:
                        pb.Image = Resources.FlaggedImage;
                        break;
                    case BlockInfo.BlockStates.Normal:
                        pb.Image = Resources.NormalMouseDownImage;
                        break;
                    case BlockInfo.BlockStates.Questioned:
                        pb.Image = Resources.QuestionedMouseDownImage;
                        break;
                }
            }
            else if (e.Button == MouseButtons.Right)
            { // 在普通，标记，问号三个状态间顺序切换。
                switch (bi.CurrentState)
                {
                    case BlockInfo.BlockStates.Normal:
                        bi.CurrentState = BlockInfo.BlockStates.Flagged;
                        pb.Image = Resources.FlaggedImage;
                        GameState.DecRemain();
                        break;
                    case BlockInfo.BlockStates.Flagged:
                        bi.CurrentState = BlockInfo.BlockStates.Questioned;
                        pb.Image = Resources.QuestionedImage;
                        GameState.IncRemain();
                        break;
                    case BlockInfo.BlockStates.Questioned:
                        bi.CurrentState = BlockInfo.BlockStates.Normal;
                        pb.Image = Resources.NormalImage;
                        break;
                }
                UpdateCaption();
            }
        }

        private void Blocks_MouseUp(object sender, MouseEventArgs e)
        {
            if (GameState.IsGameEnd) return;
            PictureBox pb = sender as PictureBox;
            Position pos = pb.Tag as Position;
            BlockInfo bi = MineField.GetCurrentInfo(pos.RowId, pos.ColId);
            try
            {
                if (GameState.IsLeftDown && GameState.IsRightDown)
                    RecoverAround(pos.RowId, pos.ColId);
                if (e.Button == MouseButtons.Left)
                    GameState.IsLeftDown = false;
                else if (e.Button == MouseButtons.Right)
                    GameState.IsRightDown = false;
                switch (bi.CurrentState)
                {
                    case BlockInfo.BlockStates.Digged:
                        return;
                    case BlockInfo.BlockStates.Flagged:
                        pb.Image = Resources.FlaggedHighlightImage;
                        break;
                    case BlockInfo.BlockStates.Normal:
                    case BlockInfo.BlockStates.Questioned:
                        // 点开这个方块。
                        if (e.Button == MouseButtons.Left)
                        {
                            if (!timer1.Enabled)
                                timer1.Enabled = true;
                            Dig(pos.RowId, pos.ColId);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Dig(int rowId, int colId)
        {
            if (rowId < 0 || colId < 0 || rowId >= GameData.RowCount || colId >= GameData.ColCount)
                return;
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            if (bi.CurrentState == BlockInfo.BlockStates.Flagged || bi.CurrentState == BlockInfo.BlockStates.Digged)
                return;
            if (bi.IsMine)
                Lose(rowId, colId);
            else
                DigNonMine(rowId, colId);
        }

        private void DigNonMineRec(int rowId, int colId)
        {
            if (rowId < 0 || colId < 0 || rowId >= GameData.RowCount || colId >= GameData.ColCount)
                return;
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            if (bi.IsMine || bi.CurrentState == BlockInfo.BlockStates.Digged || bi.CurrentState == BlockInfo.BlockStates.Flagged)
                return;
            DigNonMine(rowId, colId);
        }

        private void DigNonMine(int rowId, int colId)
        {
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            bi.CurrentState = BlockInfo.BlockStates.Digged;
            switch (bi.Number)
            {
                case 0:
                    pbBlocks[rowId, colId].Image = Resources.DiggedImage;
                    DigNonMineRec(rowId - 1, colId - 1);
                    DigNonMineRec(rowId - 1, colId);
                    DigNonMineRec(rowId - 1, colId + 1);
                    DigNonMineRec(rowId + 1, colId - 1);
                    DigNonMineRec(rowId + 1, colId);
                    DigNonMineRec(rowId + 1, colId + 1);
                    DigNonMineRec(rowId, colId - 1);
                    DigNonMineRec(rowId, colId + 1);
                    break;
                case 1:
                    pbBlocks[rowId, colId].Image = Resources.Num1;
                    break;
                case 2:
                    pbBlocks[rowId, colId].Image = Resources.Num2;
                    break;
                case 3:
                    pbBlocks[rowId, colId].Image = Resources.Num3;
                    break;
                case 4:
                    pbBlocks[rowId, colId].Image = Resources.Num4;
                    break;
                case 5:
                    pbBlocks[rowId, colId].Image = Resources.Num5;
                    break;
                case 6:
                    pbBlocks[rowId, colId].Image = Resources.Num6;
                    break;
                case 7:
                    pbBlocks[rowId, colId].Image = Resources.Num7;
                    break;
                case 8:
                    pbBlocks[rowId, colId].Image = Resources.Num8;
                    break;
            }
            GameState.IncDigged();
            TestWin();
        }

        private void TestWin()
        {
            if (GameState.DiggedCount == GameData.SafeCount)
            {
                GameState.IsGameEnd = true;
                timer1.Enabled = false;
                ShowAllMines(-1, -1, true);
                MessageBox.Show("你赢了，你太强力了！ 时间: " + GameState.ElapsedTime + "秒！");
                RestartGame();
                throw new Exception("Win");
            }
        }

        private void Lose(int rowId, int colId)
        {
            GameState.IsGameEnd = true;
            timer1.Enabled = false;
            ShowAllMines(rowId, colId, false);
            MessageBox.Show("输了吧？!哈哈~");
            RestartGame();
            throw new Exception("Lose");
        }

        private void ShowAllMines(int rowId, int colId, bool isWin)
        {
            for (int i = 0; i < GameData.RowCount; i++)
            {
                for (int j = 0; j < GameData.ColCount; j++)
                {
                    BlockInfo bi = MineField.GetCurrentInfo(i, j);
                    if (bi.IsMine)
                    {
                        if (isWin)
                        {
                            pbBlocks[i, j].Image = Resources.FlaggedImage;
                        }
                        else if (bi.CurrentState != BlockInfo.BlockStates.Flagged)
                            pbBlocks[i, j].Image = Resources.UndiggedMineImage;
                        continue;
                    }
                    if (bi.CurrentState == BlockInfo.BlockStates.Flagged)
                        pbBlocks[i, j].Image = Resources.WrongMineImage;
                }
            }
            if (!isWin)
                pbBlocks[rowId, colId].Image = Resources.DeadMineImage;
        }

        // TestAround的辅助方法，仅当该方块是问号或正常时更换按下背景。
        private void TestAroundAux(int rowId, int colId)
        {
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            if (bi.CurrentState == BlockInfo.BlockStates.Normal)
            {
                pbBlocks[rowId, colId].Image = Resources.NormalMouseDownImage;
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Questioned)
            {
                pbBlocks[rowId, colId].Image = Resources.QuestionedMouseDownImage;
            }
        }

        private void TestAround(int rowId, int colId)
        {
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            // 上一行
            if (rowId >= 1 && rowId < GameData.RowCount)
            {
                // 上一列
                if (colId >= 1 && colId < GameData.ColCount)
                    TestAroundAux(rowId - 1, colId - 1);
                // 本列
                TestAroundAux(rowId - 1, colId);
                // 下一列
                if (colId >= 0 && colId < GameData.ColCount - 1)
                    TestAroundAux(rowId - 1, colId + 1);
            }

            // 本行
            // 上一列
            if (colId >= 1 && colId < GameData.ColCount)
                TestAroundAux(rowId, colId - 1);
            // 本列
            TestAroundAux(rowId, colId);
            // 下一列
            if (colId >= 0 && colId < GameData.ColCount - 1)
                TestAroundAux(rowId, colId + 1);

            // 下一行
            if (rowId >= 0 && rowId < GameData.RowCount - 1)
            {
                // 上一列
                if (colId >= 1 && colId < GameData.ColCount)
                    TestAroundAux(rowId + 1, colId - 1);
                // 本列
                TestAroundAux(rowId + 1, colId);
                // 下一列
                if (colId >= 0 && colId < GameData.ColCount - 1)
                    TestAroundAux(rowId + 1, colId + 1);
            }
        }

        // RecoverAround的辅助方法，仅当该方块是问号或正常时恢复背景。
        private void RecoverAroundAux(int rowId, int colId, ref int number)
        {
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            if (bi.CurrentState == BlockInfo.BlockStates.Normal)
            {
                pbBlocks[rowId, colId].Image = Resources.NormalImage;
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Questioned)
            {
                pbBlocks[rowId, colId].Image = Resources.QuestionedImage;
            }
            else if (bi.CurrentState == BlockInfo.BlockStates.Flagged)
                number++;
        }

        private void RecoverAround(int rowId, int colId)
        {
            int number = 0;
            BlockInfo bi = MineField.GetCurrentInfo(rowId, colId);
            // 上一行
            if (rowId >= 1 && rowId < GameData.RowCount)
            {
                // 上一列
                if (colId >= 1 && colId < GameData.ColCount)
                    RecoverAroundAux(rowId - 1, colId - 1, ref number);
                // 本列
                RecoverAroundAux(rowId - 1, colId, ref number);
                // 下一列
                if (colId >= 0 && colId < GameData.ColCount - 1)
                    RecoverAroundAux(rowId - 1, colId + 1, ref number);
            }

            // 本行
            // 上一列
            if (colId >= 1 && colId < GameData.ColCount)
                RecoverAroundAux(rowId, colId - 1, ref number);
            // 本列
            RecoverAroundAux(rowId, colId, ref number);
            // 下一列
            if (colId >= 0 && colId < GameData.ColCount - 1)
                RecoverAroundAux(rowId, colId + 1, ref number);

            // 下一行
            if (rowId >= 0 && rowId < GameData.RowCount - 1)
            {
                // 上一列
                if (colId >= 1 && colId < GameData.ColCount)
                    RecoverAroundAux(rowId + 1, colId - 1, ref number);
                // 本列
                RecoverAroundAux(rowId + 1, colId, ref number);
                // 下一列
                if (colId >= 0 && colId < GameData.ColCount - 1)
                    RecoverAroundAux(rowId + 1, colId + 1, ref number);
            }

            // 如果已经被点开，则进一步处理。
            if (bi.CurrentState == BlockInfo.BlockStates.Digged)
            {
                if (number == bi.Number)
                { // 标记的个数与雷数相同，则挖开剩余空格。
                    Dig(rowId - 1, colId - 1);
                    Dig(rowId - 1, colId);
                    Dig(rowId - 1, colId + 1);
                    Dig(rowId + 1, colId - 1);
                    Dig(rowId + 1, colId);
                    Dig(rowId + 1, colId + 1);
                    Dig(rowId, colId - 1);
                    Dig(rowId, colId + 1);
                }
                else
                { // 否则作罢……
                }
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("扫雷 C#版，作者：成立，版本：1.0.0");
        }

        private void mnuNewGame_Click(object sender, EventArgs e)
        {
            GameState.IsGameEnd = true;
            timer1.Enabled = false;
            RestartGame();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuOption_Click(object sender, EventArgs e)
        {
            GameState.IsGameEnd = true;
            Option option = new Option();
            option.ShowDialog();
            RestartGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GameState.IncTime();
            UpdateCaption();
        }

        private void pbCheat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("咳咳……禽兽模式开启，所有雷已秒杀--！谢谢！");
            ShowSuperHints();
        }

        private void ShowSuperHints()
        {
            for (int i = 0; i < GameData.RowCount; i++)
            {
                for (int j = 0; j < GameData.ColCount; j++)
                {
                    BlockInfo bi = MineField.GetCurrentInfo(i, j);
                    if (bi.IsMine)
                    {
                        pbBlocks[i, j].Image = Resources.FlaggedImage;
                        bi.CurrentState = BlockInfo.BlockStates.Flagged;
                    }
                }
            }
            GameState.RemainCount = 0;
        }

        private void UpdateCaption()
        {
            this.Text = "Mine C# 版 时间：" + GameState.ElapsedTime + "秒，剩余雷数：" + GameState.RemainCount;
        }
    }
}
