using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MineCSharp
{
    // IGameView接口表明了对于扫雷游戏的图形绘制，
    // 定时器，信息显示，输赢显示等功能的实现需求，
    // 除此之外，GameView不应提供任何关于游戏的逻辑。
    interface IGameView
    {
        // 设置位于(rowId, colId)处的方块图片。
        void SetBlockImage(int rowId, int colId, Image image);

        // 设置位于(rowId, colId)处的方块可见性。
        void SetBlockVisible(int rowId, int colId, bool visible);

        // 开启定时器
        void TurnOnTimer();

        // 关闭定时器。
        void TurnOffTimer();

        // 测试定时器是否开启。
        bool TestTimer();

        // 更新已用时间显示。
        void UpdateTimeElapsedDisplay(int timeElapsed);

        // 更新剩余雷数显示。
        void UpdateRemainMinesDisplay(int remainMines);

        // 重置用户界面，雷区除外。
        void ResetUserInterfaceExceptMineField();

        // 胜利后的附加处理，比如显示对话框。
        void WinAddProc(int timeElapsed);

        // 失败后的附加处理，比如显示对话框。
        void LoseAddProc(int timeElapsed);
    }
}
