namespace CWD
{
    partial class fmBrede
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmSewing));
            this.ucBrede1 = new CWD.ucBrede();
            this.SuspendLayout();
            // 
            // ucBrede1
            // 
            this.ucBrede1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBrede1.Location = new System.Drawing.Point(0, 0);
            this.ucBrede1.Name = "ucBrede1";
            this.ucBrede1.Size = new System.Drawing.Size(807, 548);
            this.ucBrede1.TabIndex = 0;
            // 
            // fmBrede
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 548);
            this.Controls.Add(this.ucBrede1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmBrede";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "绣花工序委外收发料";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fmBrede_Load);
            this.Resize += new System.EventHandler(this.fmBrede_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ucBrede ucBrede1;
    }
}