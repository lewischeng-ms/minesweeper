namespace MineCSharp
{
    partial class Option
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuOk = new System.Windows.Forms.Button();
            this.rbBeginner = new System.Windows.Forms.RadioButton();
            this.rbIntermediate = new System.Windows.Forms.RadioButton();
            this.rbAdvanced = new System.Windows.Forms.RadioButton();
            this.rbRP = new System.Windows.Forms.RadioButton();
            this.rbSuperRP = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mnuOk
            // 
            this.mnuOk.Location = new System.Drawing.Point(91, 234);
            this.mnuOk.Name = "mnuOk";
            this.mnuOk.Size = new System.Drawing.Size(93, 50);
            this.mnuOk.TabIndex = 0;
            this.mnuOk.Text = "确定";
            this.mnuOk.UseVisualStyleBackColor = true;
            this.mnuOk.Click += new System.EventHandler(this.mnuOk_Click);
            // 
            // rbBeginner
            // 
            this.rbBeginner.AutoSize = true;
            this.rbBeginner.Location = new System.Drawing.Point(59, 25);
            this.rbBeginner.Name = "rbBeginner";
            this.rbBeginner.Size = new System.Drawing.Size(125, 16);
            this.rbBeginner.TabIndex = 2;
            this.rbBeginner.Text = "初级：9x9(10个雷)";
            this.rbBeginner.UseVisualStyleBackColor = true;
            // 
            // rbIntermediate
            // 
            this.rbIntermediate.AutoSize = true;
            this.rbIntermediate.Checked = true;
            this.rbIntermediate.Location = new System.Drawing.Point(59, 70);
            this.rbIntermediate.Name = "rbIntermediate";
            this.rbIntermediate.Size = new System.Drawing.Size(137, 16);
            this.rbIntermediate.TabIndex = 3;
            this.rbIntermediate.TabStop = true;
            this.rbIntermediate.Text = "中级：16x16(40个雷)";
            this.rbIntermediate.UseVisualStyleBackColor = true;
            // 
            // rbAdvanced
            // 
            this.rbAdvanced.AutoSize = true;
            this.rbAdvanced.Location = new System.Drawing.Point(59, 119);
            this.rbAdvanced.Name = "rbAdvanced";
            this.rbAdvanced.Size = new System.Drawing.Size(137, 16);
            this.rbAdvanced.TabIndex = 4;
            this.rbAdvanced.Text = "高级：16x30(99个雷)";
            this.rbAdvanced.UseVisualStyleBackColor = true;
            // 
            // rbRP
            // 
            this.rbRP.AutoSize = true;
            this.rbRP.Location = new System.Drawing.Point(59, 165);
            this.rbRP.Name = "rbRP";
            this.rbRP.Size = new System.Drawing.Size(167, 16);
            this.rbRP.TabIndex = 5;
            this.rbRP.Text = "人品模式：16x30(240个雷)";
            this.rbRP.UseVisualStyleBackColor = true;
            // 
            // rbSuperRP
            // 
            this.rbSuperRP.AutoSize = true;
            this.rbSuperRP.Location = new System.Drawing.Point(59, 207);
            this.rbSuperRP.Name = "rbSuperRP";
            this.rbSuperRP.Size = new System.Drawing.Size(191, 16);
            this.rbSuperRP.TabIndex = 6;
            this.rbSuperRP.Text = "超级人品模式：16x30(476个雷)";
            this.rbSuperRP.UseVisualStyleBackColor = true;
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 296);
            this.Controls.Add(this.rbSuperRP);
            this.Controls.Add(this.rbRP);
            this.Controls.Add(this.rbAdvanced);
            this.Controls.Add(this.rbIntermediate);
            this.Controls.Add(this.rbBeginner);
            this.Controls.Add(this.mnuOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Option";
            this.Text = "选项";
            this.Load += new System.EventHandler(this.Option_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mnuOk;
        private System.Windows.Forms.RadioButton rbBeginner;
        private System.Windows.Forms.RadioButton rbIntermediate;
        private System.Windows.Forms.RadioButton rbAdvanced;
        private System.Windows.Forms.RadioButton rbRP;
        private System.Windows.Forms.RadioButton rbSuperRP;
    }
}