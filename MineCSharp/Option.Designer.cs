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
            this.mnuCancel = new System.Windows.Forms.Button();
            this.rbBeginning = new System.Windows.Forms.RadioButton();
            this.rbIntermediate = new System.Windows.Forms.RadioButton();
            this.rbAdvanced = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuOk
            // 
            this.mnuOk.Location = new System.Drawing.Point(12, 160);
            this.mnuOk.Name = "mnuOk";
            this.mnuOk.Size = new System.Drawing.Size(93, 50);
            this.mnuOk.TabIndex = 0;
            this.mnuOk.Text = "确定";
            this.mnuOk.UseVisualStyleBackColor = true;
            this.mnuOk.Click += new System.EventHandler(this.mnuOk_Click);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Location = new System.Drawing.Point(163, 160);
            this.mnuCancel.Name = "mnuCancel";
            this.mnuCancel.Size = new System.Drawing.Size(99, 50);
            this.mnuCancel.TabIndex = 1;
            this.mnuCancel.Text = "取消";
            this.mnuCancel.UseVisualStyleBackColor = true;
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // rbBeginning
            // 
            this.rbBeginning.AutoSize = true;
            this.rbBeginning.Location = new System.Drawing.Point(85, 25);
            this.rbBeginning.Name = "rbBeginning";
            this.rbBeginning.Size = new System.Drawing.Size(125, 16);
            this.rbBeginning.TabIndex = 2;
            this.rbBeginning.Text = "初级：9x9(10个雷)";
            this.rbBeginning.UseVisualStyleBackColor = true;
            // 
            // rbIntermediate
            // 
            this.rbIntermediate.AutoSize = true;
            this.rbIntermediate.Checked = true;
            this.rbIntermediate.Location = new System.Drawing.Point(85, 70);
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
            this.rbAdvanced.Location = new System.Drawing.Point(85, 119);
            this.rbAdvanced.Name = "rbAdvanced";
            this.rbAdvanced.Size = new System.Drawing.Size(137, 16);
            this.rbAdvanced.TabIndex = 4;
            this.rbAdvanced.Text = "高级：16x30(99个雷)";
            this.rbAdvanced.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(130, 160);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 11);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(130, 199);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(10, 11);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.DoubleClick += new System.EventHandler(this.pictureBox2_DoubleClick);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 222);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.rbAdvanced);
            this.Controls.Add(this.rbIntermediate);
            this.Controls.Add(this.rbBeginning);
            this.Controls.Add(this.mnuCancel);
            this.Controls.Add(this.mnuOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Option";
            this.Text = "Option";
            this.Load += new System.EventHandler(this.Option_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mnuOk;
        private System.Windows.Forms.Button mnuCancel;
        private System.Windows.Forms.RadioButton rbBeginning;
        private System.Windows.Forms.RadioButton rbIntermediate;
        private System.Windows.Forms.RadioButton rbAdvanced;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}