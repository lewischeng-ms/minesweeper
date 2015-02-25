namespace MineCSharp
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.pbCheat = new System.Windows.Forms.PictureBox();
            this.lblTimeElapsed = new System.Windows.Forms.Label();
            this.lblRemainCount = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheat)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGame,
            this.mnuHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(575, 25);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "Main Menu";
            // 
            // mnuGame
            // 
            this.mnuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewGame,
            this.mnuSeparator1,
            this.mnuStat,
            this.mnuOption,
            this.mnuSeparator2,
            this.mnuExit});
            this.mnuGame.Name = "mnuGame";
            this.mnuGame.Size = new System.Drawing.Size(61, 21);
            this.mnuGame.Text = "游戏(&G)";
            // 
            // mnuNewGame
            // 
            this.mnuNewGame.Name = "mnuNewGame";
            this.mnuNewGame.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuNewGame.Size = new System.Drawing.Size(160, 22);
            this.mnuNewGame.Text = "新游戏(&N)";
            this.mnuNewGame.Click += new System.EventHandler(this.mnuNewGame_Click);
            // 
            // mnuSeparator1
            // 
            this.mnuSeparator1.Name = "mnuSeparator1";
            this.mnuSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuStat
            // 
            this.mnuStat.Enabled = false;
            this.mnuStat.Name = "mnuStat";
            this.mnuStat.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuStat.Size = new System.Drawing.Size(160, 22);
            this.mnuStat.Text = "统计信息(&S)";
            // 
            // mnuOption
            // 
            this.mnuOption.Name = "mnuOption";
            this.mnuOption.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuOption.Size = new System.Drawing.Size(160, 22);
            this.mnuOption.Text = "选项(&O)";
            this.mnuOption.Click += new System.EventHandler(this.mnuOption_Click);
            // 
            // mnuSeparator2
            // 
            this.mnuSeparator2.Name = "mnuSeparator2";
            this.mnuSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(160, 22);
            this.mnuExit.Text = "退出(&X)";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewHelp,
            this.mnuSeparator3,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(61, 21);
            this.mnuHelp.Text = "帮助(&H)";
            // 
            // mnuViewHelp
            // 
            this.mnuViewHelp.Enabled = false;
            this.mnuViewHelp.Name = "mnuViewHelp";
            this.mnuViewHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuViewHelp.Size = new System.Drawing.Size(185, 22);
            this.mnuViewHelp.Text = "查看帮助(&V)";
            // 
            // mnuSeparator3
            // 
            this.mnuSeparator3.Name = "mnuSeparator3";
            this.mnuSeparator3.Size = new System.Drawing.Size(182, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(185, 22);
            this.mnuAbout.Text = "关于 Mine C#版 (&A)";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // pbCheat
            // 
            this.pbCheat.Location = new System.Drawing.Point(0, 28);
            this.pbCheat.Name = "pbCheat";
            this.pbCheat.Size = new System.Drawing.Size(15, 14);
            this.pbCheat.TabIndex = 1;
            this.pbCheat.TabStop = false;
            this.pbCheat.DoubleClick += new System.EventHandler(this.pbCheat_Click);
            // 
            // lblTimeElapsed
            // 
            this.lblTimeElapsed.AutoSize = true;
            this.lblTimeElapsed.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTimeElapsed.ForeColor = System.Drawing.Color.White;
            this.lblTimeElapsed.Location = new System.Drawing.Point(12, 30);
            this.lblTimeElapsed.Name = "lblTimeElapsed";
            this.lblTimeElapsed.Size = new System.Drawing.Size(113, 14);
            this.lblTimeElapsed.TabIndex = 2;
            this.lblTimeElapsed.Text = "已用时间：0 秒";
            // 
            // lblRemainCount
            // 
            this.lblRemainCount.AutoSize = true;
            this.lblRemainCount.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemainCount.ForeColor = System.Drawing.Color.White;
            this.lblRemainCount.Location = new System.Drawing.Point(167, 30);
            this.lblRemainCount.Name = "lblRemainCount";
            this.lblRemainCount.Size = new System.Drawing.Size(121, 14);
            this.lblRemainCount.TabIndex = 3;
            this.lblRemainCount.Text = "剩余雷数：40 个";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(575, 429);
            this.Controls.Add(this.lblRemainCount);
            this.Controls.Add(this.lblTimeElapsed);
            this.Controls.Add(this.pbCheat);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "扫雷【中级】";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuGame;
        private System.Windows.Forms.ToolStripMenuItem mnuNewGame;
        private System.Windows.Forms.ToolStripSeparator mnuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuStat;
        private System.Windows.Forms.ToolStripMenuItem mnuOption;
        private System.Windows.Forms.ToolStripSeparator mnuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuViewHelp;
        private System.Windows.Forms.ToolStripSeparator mnuSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.PictureBox pbCheat;
        private System.Windows.Forms.Label lblTimeElapsed;
        private System.Windows.Forms.Label lblRemainCount;
    }
}

