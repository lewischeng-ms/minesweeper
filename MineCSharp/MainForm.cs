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
    public partial class MainForm : Form, IGameView
    {
        #region 数据成员

        // 图形数据。
        public const int BlockWidth = 30;
        public const int BlockHeight = 30;
        public const int FieldLeft = 20;
        public const int FieldTop = 50;
        public const int FieldRight = 30;
        public const int FieldBottom = 45;

        // 判断同时点击
        private bool isLeftDown;
        private bool isRightDown;

        private IGameController controller;

        // 雷区的图形前端。
        private PictureBox[,] pbBlocks;

        #endregion

        #region 初始化部分

        public MainForm()
        {
            InitializeComponent();
            // 创建所有方块。
            CreateBlocks();
            // 创建游戏控制器。
            controller = new GameController(this);
        }

        // 创建所有方块。
        private void CreateBlocks()
        {
            pbBlocks = new PictureBox[StaticData.MaxRowCount, StaticData.MaxColCount];
            // 循环初始化每个图片框。
            for (int i = 0; i < StaticData.MaxRowCount; i++)
            {
                for (int j = 0; j < StaticData.MaxColCount; j++)
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
                    pbBlocks[i, j].Width = BlockWidth;
                    pbBlocks[i, j].Height = BlockHeight;
                    pbBlocks[i, j].BorderStyle = BorderStyle.FixedSingle;
                    pbBlocks[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbBlocks[i, j].Image = Resources.NormalImage;
                    // 设置位置。
                    pbBlocks[i, j].Left = FieldLeft + j * BlockWidth;
                    pbBlocks[i, j].Top = FieldTop + i * BlockHeight;
                    // 添加控件。
                    Controls.Add(pbBlocks[i, j]);
                }
            }
        }

        #endregion

        #region IGameView接口成员

        public void SetBlockImage(int rowId, int colId, Image image)
        {
            pbBlocks[rowId, colId].Image = image;
        }

        public void SetBlockVisible(int rowId, int colId, bool visible)
        {
            pbBlocks[rowId, colId].Visible = visible;
        }

        public void TurnOnTimer()
        {
            gameTimer.Enabled = true;
        }

        public void TurnOffTimer()
        {
            gameTimer.Enabled = false;
        }

        public bool TestTimer()
        {
            return gameTimer.Enabled;
        }

        public void ResetUserInterfaceExceptMineField()
        {
            // 清空左右按键记录。
            isLeftDown = false;
            isRightDown = false;

            // 调整窗体大小。
            this.Width = FieldLeft + FieldRight + StaticData.ColCount * BlockWidth;
            this.Height = FieldTop + FieldBottom + StaticData.RowCount * BlockHeight;

            // 设置窗口标题。
            switch (StaticData.Level)
            {
                case StaticData.Levels.Beginner:
                    this.Text = "扫雷 v2.1【初级】";
                    break;
                case StaticData.Levels.Intermediate:
                    this.Text = "扫雷 v2.1【中级】";
                    break;
                case StaticData.Levels.Advanced:
                    this.Text = "扫雷 v2.1【高级】";
                    break;
                case StaticData.Levels.RP:
                    this.Text = "扫雷 v2.1【人品模式】……能点开5个说明你人品不错！";
                    break;
                case StaticData.Levels.SuperRP:
                    this.Text = "扫雷 v2.1【超级人品模式】……能点开两个以上你可以去买彩票了！";
                    break;
            }
            // 设置标签。
            UpdateTimeElapsedDisplay(0);
            UpdateRemainMinesDisplay(StaticData.MineCount);
        }

        public void UpdateTimeElapsedDisplay(int timeElapsed)
        {
            lblTimeElapsed.Text = "已用时间：" + timeElapsed + " 秒";
        }

        public void UpdateRemainMinesDisplay(int remainMines)
        {
            lblRemainCount.Text = "剩余雷数：" + remainMines + " 个";
        }

        public void WinAddProc(int timeElapsed)
        {
            MessageBox.Show("你好强力！花费时间：" + timeElapsed + "秒");
        }

        public void LoseAddProc(int timeElapsed)
        {
            MessageBox.Show("悲剧了吧！花费时间：" + timeElapsed + "秒");
        }

        #endregion

        #region 窗口事件处理部分

        // 返回是否鼠标左右键同时按下。
        private bool AreTwoButtonsDown()
        {
            return isLeftDown && isRightDown;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 开始新游戏。
            controller.NewGame();
        }

        private void Blocks_MouseMove(object sender, MouseEventArgs e)
        {
            // 获得图片框对象。
            PictureBox block = sender as PictureBox;
            // 获取方块在雷区中的位置。
            Position position = block.Tag as Position;
            // 调用游戏控制中的事件处理程序。
            controller.ProcessMouseMove(position.RowId, position.ColId);
        }

        private void Blocks_MouseLeave(object sender, EventArgs e)
        {
            // 获得图片框对象。
            PictureBox block = sender as PictureBox;
            // 获取方块在雷区中的位置。
            Position position = block.Tag as Position;
            // 调用游戏控制中的事件处理程序。
            controller.ProcessMouseLeave(position.RowId, position.ColId);
        }

        private void Blocks_MouseDown(object sender, MouseEventArgs e)
        {
            // 获得图片框对象。
            PictureBox block = sender as PictureBox;
            // 获取方块在雷区中的位置。
            Position position = block.Tag as Position;
            // 检测按键。
            StaticData.MyMouseButtons buttons = StaticData.MyMouseButtons.None;
            if (e.Button == MouseButtons.Left)
            { // 按下的左键。
                isLeftDown = true;
                buttons = StaticData.MyMouseButtons.Left;
            }
            else if (e.Button == MouseButtons.Right)
            { // 按下的右键。
                isRightDown = true;
                buttons = StaticData.MyMouseButtons.Right;
            }
            // 检测是否同时按下。
            if (isLeftDown && isRightDown)
                buttons = StaticData.MyMouseButtons.Both;
            // 调用游戏控制中的事件处理程序。
            controller.ProcessMouseDown(position.RowId, position.ColId, buttons);
        }

        private void Blocks_MouseUp(object sender, MouseEventArgs e)
        {
            // 获得图片框对象。
            PictureBox block = sender as PictureBox;
            // 获取方块在雷区中的位置。
            Position position = block.Tag as Position;
            // 检测按键释放。
            StaticData.MyMouseButtons buttons = StaticData.MyMouseButtons.None;
            if (isLeftDown && isRightDown)
            { // 刚刚的状态是双键同时按下。
                buttons = StaticData.MyMouseButtons.Both;
                // 必须释放相应键。
                if (e.Button == MouseButtons.Left)
                    isLeftDown = false;
                else if (e.Button == MouseButtons.Right)
                    isRightDown = false;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                { // 释放的是左键。
                    isLeftDown = false;
                    buttons = StaticData.MyMouseButtons.Left;
                }
                else if (e.Button == MouseButtons.Right)
                { // 释放的是右键。
                    isRightDown = false;
                    buttons = StaticData.MyMouseButtons.Right;
                }
            }
            // 调用游戏控制中的事件处理程序。
            controller.ProcessMouseUp(position.RowId, position.ColId, buttons);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("扫雷 V2.1重构版 By 成立 @2010/3/4");
        }

        private void mnuNewGame_Click(object sender, EventArgs e)
        {
            controller.NewGame();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuOption_Click(object sender, EventArgs e)
        {
            // 终止当前游戏。
            controller.TerminateGame();
            Option option = new Option();
            // 显示选项对话框。
            option.ShowDialog();
            // 开始新的游戏。
            controller.NewGame();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // 调用时间处理程序。
            controller.TimeTick();
        }

        private void pbCheat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("咳咳……禽兽模式开启，所有雷已秒杀--！谢谢！");
            controller.Cheat();
        }

        #endregion
    }
}
