namespace winform3._5
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chang_location = new System.Windows.Forms.Button();
            this.Block_OfficePlus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chang_location
            // 
            this.chang_location.Cursor = System.Windows.Forms.Cursors.Default;
            this.chang_location.Location = new System.Drawing.Point(158, 83);
            this.chang_location.Name = "chang_location";
            this.chang_location.Size = new System.Drawing.Size(149, 48);
            this.chang_location.TabIndex = 0;
            this.chang_location.TabStop = false;
            this.chang_location.Text = "更改Office安装位置";
            this.chang_location.UseVisualStyleBackColor = true;
            this.chang_location.Click += new System.EventHandler(this.chang_location_Click);
            // 
            // Block_OfficePlus
            // 
            this.Block_OfficePlus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Block_OfficePlus.Location = new System.Drawing.Point(158, 176);
            this.Block_OfficePlus.Name = "Block_OfficePlus";
            this.Block_OfficePlus.Size = new System.Drawing.Size(151, 48);
            this.Block_OfficePlus.TabIndex = 1;
            this.Block_OfficePlus.TabStop = false;
            this.Block_OfficePlus.Text = "阻止OfficePLUS自动安装";
            this.Block_OfficePlus.UseVisualStyleBackColor = true;
            this.Block_OfficePlus.Click += new System.EventHandler(this.Block_OfficePlus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 301);
            this.Controls.Add(this.Block_OfficePlus);
            this.Controls.Add(this.chang_location);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Ofice工具";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button chang_location;
        private System.Windows.Forms.Button Block_OfficePlus;
    }
}

