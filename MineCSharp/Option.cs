using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineCSharp
{
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
        }

        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuOk_Click(object sender, EventArgs e)
        {
            if (rbBeginning.Checked)
            {
                GameData.RowCount = 9;
                GameData.ColCount = 9;
                GameData.MineCount = 10;
            }
            else if (rbIntermediate.Checked)
            {
                GameData.RowCount = 16;
                GameData.ColCount = 16;
                GameData.MineCount = 40;
            }
            else if (rbAdvanced.Checked)
            {
                GameData.RowCount = 16;
                GameData.ColCount = 30;
                GameData.MineCount = 99;
            }
            GameData.SafeCount = GameData.RowCount * GameData.ColCount - GameData.MineCount;
            this.Close();
        }

        private void Option_Load(object sender, EventArgs e)
        {
            switch (GameData.ColCount)
            {
                case 9:
                    rbBeginning.Checked = true;
                    break;
                case 16:
                    rbIntermediate.Checked = true;
                    break;
                case 30:
                    rbAdvanced.Checked = true;
                    break;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("人品模式开启，有240/480个雷！");
            GameData.RowCount = 16;
            GameData.ColCount = 30;
            GameData.MineCount = 240;
            GameData.SafeCount = GameData.RowCount * GameData.ColCount - GameData.MineCount;
            this.Close();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("极品人品模式开启，有479/480个雷！");
            GameData.RowCount = 16;
            GameData.ColCount = 30;
            GameData.MineCount = 479;
            GameData.SafeCount = 1;
            this.Close();
        }
    }
}
