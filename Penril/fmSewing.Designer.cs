namespace CWD
{
    partial class fmSewing
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
            this.ucSewing1 = new CWD.ucSewing();
            this.SuspendLayout();
            // 
            // ucSewing1
            // 
            this.ucSewing1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSewing1.Location = new System.Drawing.Point(0, 0);
            this.ucSewing1.Name = "ucSewing1";
            this.ucSewing1.Size = new System.Drawing.Size(807, 548);
            this.ucSewing1.TabIndex = 0;
            // 
            // fmSewing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 548);
            this.Controls.Add(this.ucSewing1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmSewing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "缝纫委外加工";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fmSewing_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucSewing ucSewing1;
    }
}