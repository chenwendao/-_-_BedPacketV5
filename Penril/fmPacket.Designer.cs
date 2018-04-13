namespace CWD
{
    partial class fmPacket
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
            this.ucBedPacket1 = new CWD.ucBedPacket();
            this.SuspendLayout();
            // 
            // ucBedPacket1
            // 
            this.ucBedPacket1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBedPacket1.Location = new System.Drawing.Point(0, 0);
            this.ucBedPacket1.Name = "ucBedPacket1";
            this.ucBedPacket1.Size = new System.Drawing.Size(1264, 742);
            this.ucBedPacket1.TabIndex = 0;
            // 
            // fmPacket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1264, 742);
            this.Controls.Add(this.ucBedPacket1);
            this.Name = "fmPacket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "床品包装工序报工录入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fmTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucBedPacket ucBedPacket1;




    }
}