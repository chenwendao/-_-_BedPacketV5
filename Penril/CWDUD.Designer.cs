namespace CWD
{
    partial class CWDUD
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CWDUD));
            this.tbMain = new System.Windows.Forms.TextBox();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbMain
            // 
            this.tbMain.BackColor = System.Drawing.SystemColors.Menu;
            this.tbMain.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMain.ForeColor = System.Drawing.Color.Red;
            this.tbMain.Location = new System.Drawing.Point(0, 4);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(59, 44);
            this.tbMain.TabIndex = 0;
            this.tbMain.Text = "0";
            this.tbMain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMain.Click += new System.EventHandler(this.tbMain_Click);
            // 
            // btDown
            // 
            this.btDown.Image = ((System.Drawing.Image)(resources.GetObject("btDown.Image")));
            this.btDown.Location = new System.Drawing.Point(122, 1);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(53, 52);
            this.btDown.TabIndex = 1;
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            this.btUp.Image = ((System.Drawing.Image)(resources.GetObject("btUp.Image")));
            this.btUp.Location = new System.Drawing.Point(64, 0);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(52, 53);
            this.btUp.TabIndex = 1;
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // CWDUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.tbMain);
            this.Name = "CWDUD";
            this.Size = new System.Drawing.Size(180, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMain;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btDown;
    }
}
