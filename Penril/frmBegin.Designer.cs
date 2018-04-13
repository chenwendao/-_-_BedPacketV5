namespace CWD
{
    partial class frmBegin
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
            this.btnPacket = new System.Windows.Forms.Button();
            this.btnBredeOut = new System.Windows.Forms.Button();
            this.btnSewOut = new System.Windows.Forms.Button();
            this.btnBredeIn = new System.Windows.Forms.Button();
            this.btnSewIn = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPacket
            // 
            this.btnPacket.Font = new System.Drawing.Font("楷体_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPacket.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPacket.Location = new System.Drawing.Point(283, 50);
            this.btnPacket.Name = "btnPacket";
            this.btnPacket.Size = new System.Drawing.Size(80, 55);
            this.btnPacket.TabIndex = 0;
            this.btnPacket.Text = "打包";
            this.btnPacket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPacket.UseVisualStyleBackColor = true;
            this.btnPacket.Click += new System.EventHandler(this.btnPacket_Click);
            // 
            // btnBredeOut
            // 
            this.btnBredeOut.Font = new System.Drawing.Font("楷体_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBredeOut.ForeColor = System.Drawing.Color.Red;
            this.btnBredeOut.Location = new System.Drawing.Point(140, 129);
            this.btnBredeOut.Name = "btnBredeOut";
            this.btnBredeOut.Size = new System.Drawing.Size(124, 55);
            this.btnBredeOut.TabIndex = 0;
            this.btnBredeOut.Text = "绣花发料";
            this.btnBredeOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBredeOut.UseVisualStyleBackColor = true;
            this.btnBredeOut.Click += new System.EventHandler(this.btnBrede_Click);
            // 
            // btnSewOut
            // 
            this.btnSewOut.Font = new System.Drawing.Font("楷体_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSewOut.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSewOut.Location = new System.Drawing.Point(371, 129);
            this.btnSewOut.Name = "btnSewOut";
            this.btnSewOut.Size = new System.Drawing.Size(124, 55);
            this.btnSewOut.TabIndex = 0;
            this.btnSewOut.Text = "缝纫发料";
            this.btnSewOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSewOut.UseVisualStyleBackColor = true;
            this.btnSewOut.Click += new System.EventHandler(this.btnSew_Click);
            // 
            // btnBredeIn
            // 
            this.btnBredeIn.Font = new System.Drawing.Font("楷体_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBredeIn.ForeColor = System.Drawing.Color.Red;
            this.btnBredeIn.Location = new System.Drawing.Point(140, 210);
            this.btnBredeIn.Name = "btnBredeIn";
            this.btnBredeIn.Size = new System.Drawing.Size(124, 55);
            this.btnBredeIn.TabIndex = 0;
            this.btnBredeIn.Text = "绣花收料";
            this.btnBredeIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBredeIn.UseVisualStyleBackColor = true;
            this.btnBredeIn.Click += new System.EventHandler(this.btnBredeIn_Click);
            // 
            // btnSewIn
            // 
            this.btnSewIn.Font = new System.Drawing.Font("楷体_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSewIn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSewIn.Location = new System.Drawing.Point(371, 210);
            this.btnSewIn.Name = "btnSewIn";
            this.btnSewIn.Size = new System.Drawing.Size(124, 55);
            this.btnSewIn.TabIndex = 0;
            this.btnSewIn.Text = "缝纫收料";
            this.btnSewIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSewIn.UseVisualStyleBackColor = true;
            this.btnSewIn.Click += new System.EventHandler(this.btnSewIn_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("楷体_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExit.Location = new System.Drawing.Point(569, 274);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 55);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmBegin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 358);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSewIn);
            this.Controls.Add(this.btnSewOut);
            this.Controls.Add(this.btnBredeIn);
            this.Controls.Add(this.btnBredeOut);
            this.Controls.Add(this.btnPacket);
            this.Name = "frmBegin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPacket;
        private System.Windows.Forms.Button btnBredeOut;
        private System.Windows.Forms.Button btnSewOut;
        private System.Windows.Forms.Button btnBredeIn;
        private System.Windows.Forms.Button btnSewIn;
        private System.Windows.Forms.Button btnExit;
    }
}