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

        private void mnuOk_Click(object sender, EventArgs e)
        {
            if (rbBeginner.Checked)
            {
                StaticData.Level = StaticData.Levels.Beginner;
            }
            else if (rbIntermediate.Checked)
            {
                StaticData.Level = StaticData.Levels.Intermediate;
            }
            else if (rbAdvanced.Checked)
            {
                StaticData.Level = StaticData.Levels.Advanced;
            }
            else if (rbRP.Checked)
            {
                StaticData.Level = StaticData.Levels.RP;
            }
            else if (rbSuperRP.Checked)
            {
                StaticData.Level = StaticData.Levels.SuperRP;
            }
            this.Close();
        }

        private void Option_Load(object sender, EventArgs e)
        {
            switch (StaticData.Level)
            {
                case StaticData.Levels.Beginner:
                    rbBeginner.Checked = true;
                    break;
                case StaticData.Levels.Intermediate:
                    rbIntermediate.Checked = true;
                    break;
                case StaticData.Levels.Advanced:
                    rbAdvanced.Checked = true;
                    break;
                case StaticData.Levels.RP:
                    rbRP.Checked = true;
                    break;
                case StaticData.Levels.SuperRP:
                    rbSuperRP.Checked = true;
                    break;
            }
        }
    }
}
