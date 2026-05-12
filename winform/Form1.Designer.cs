namespace Office_location
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
            System.Windows.Forms.Button Block_OfficePlus;
            System.Windows.Forms.Button chang_location;
            Block_OfficePlus = new System.Windows.Forms.Button();
            chang_location = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Block_OfficePlus
            // 
            Block_OfficePlus.Location = new System.Drawing.Point(174, 163);
            Block_OfficePlus.Name = "Block_OfficePlus";
            Block_OfficePlus.Size = new System.Drawing.Size(131, 45);
            Block_OfficePlus.TabIndex = 1;
            Block_OfficePlus.TabStop = false;
            Block_OfficePlus.Text = "阻止OfficePLUS自动安装";
            Block_OfficePlus.UseVisualStyleBackColor = true;
            Block_OfficePlus.Click += new System.EventHandler(this.Block_OfficePlus_Click);
            // 
            // chang_location
            // 
            chang_location.Location = new System.Drawing.Point(174, 87);
            chang_location.Name = "chang_location";
            chang_location.Size = new System.Drawing.Size(131, 45);
            chang_location.TabIndex = 2;
            chang_location.TabStop = false;
            chang_location.Text = "更改Office安装位置";
            chang_location.UseVisualStyleBackColor = true;
            chang_location.Click += new System.EventHandler(this.chang_location_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 301);
            this.Controls.Add(chang_location);
            this.Controls.Add(Block_OfficePlus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Office设置";
            this.ResumeLayout(false);

        }

        #endregion
    }
}