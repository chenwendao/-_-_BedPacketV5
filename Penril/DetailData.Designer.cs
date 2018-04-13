namespace CWD
{
    partial class DetailData
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
            this.label6 = new System.Windows.Forms.Label();
            this.lbMo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cwQty = new CWD.cwbTB();
            this.cwMoDID = new CWD.cwbTB();
            this.cwTeam = new CWD.cwbTB();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrnSo = new System.Windows.Forms.Button();
            this.btnSoall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(21, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 21);
            this.label6.TabIndex = 374;
            this.label6.Text = "订单DID";
            // 
            // lbMo
            // 
            this.lbMo.AutoEllipsis = true;
            this.lbMo.AutoSize = true;
            this.lbMo.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMo.ForeColor = System.Drawing.Color.Red;
            this.lbMo.Location = new System.Drawing.Point(31, 64);
            this.lbMo.Name = "lbMo";
            this.lbMo.Size = new System.Drawing.Size(393, 29);
            this.lbMo.TabIndex = 375;
            this.lbMo.Text = "1BE0893-B_1 床单 220*250";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(161, 416);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 21);
            this.label2.TabIndex = 374;
            this.label2.Text = "数量";
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("华文琥珀", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnOK.Location = new System.Drawing.Point(129, 476);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 52);
            this.btnOK.TabIndex = 377;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("华文琥珀", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnCancel.Location = new System.Drawing.Point(630, 476);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 52);
            this.btnCancel.TabIndex = 377;
            this.btnCancel.Text = "返回";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cwQty
            // 
            this.cwQty.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cwQty.Location = new System.Drawing.Point(252, 411);
            this.cwQty.Name = "cwQty";
            this.cwQty.Size = new System.Drawing.Size(100, 31);
            this.cwQty.TabIndex = 1;
            this.cwQty.Value = null;
            this.cwQty.Click += new System.EventHandler(this.cwQty_Click);
            // 
            // cwMoDID
            // 
            this.cwMoDID.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cwMoDID.Location = new System.Drawing.Point(112, 19);
            this.cwMoDID.Name = "cwMoDID";
            this.cwMoDID.Size = new System.Drawing.Size(100, 31);
            this.cwMoDID.TabIndex = 0;
            this.cwMoDID.Value = null;
            this.cwMoDID.Click += new System.EventHandler(this.cwMoDID_Click);
            // 
            // cwTeam
            // 
            this.cwTeam.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cwTeam.Location = new System.Drawing.Point(482, 411);
            this.cwTeam.Name = "cwTeam";
            this.cwTeam.Size = new System.Drawing.Size(100, 31);
            this.cwTeam.TabIndex = 1;
            this.cwTeam.Value = null;
            this.cwTeam.Click += new System.EventHandler(this.cwTeam_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(391, 416);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 374;
            this.label1.Text = "整理组";
            // 
            // btnPrnSo
            // 
            this.btnPrnSo.Enabled = false;
            this.btnPrnSo.Font = new System.Drawing.Font("华文琥珀", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrnSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnPrnSo.Location = new System.Drawing.Point(296, 476);
            this.btnPrnSo.Name = "btnPrnSo";
            this.btnPrnSo.Size = new System.Drawing.Size(100, 52);
            this.btnPrnSo.TabIndex = 378;
            this.btnPrnSo.Text = "预览";
            this.btnPrnSo.UseVisualStyleBackColor = true;
            this.btnPrnSo.Click += new System.EventHandler(this.btnPrnSo_Click);
            // 
            // btnSoall
            // 
            this.btnSoall.Enabled = false;
            this.btnSoall.Font = new System.Drawing.Font("华文琥珀", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSoall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSoall.Location = new System.Drawing.Point(463, 476);
            this.btnSoall.Name = "btnSoall";
            this.btnSoall.Size = new System.Drawing.Size(100, 52);
            this.btnSoall.TabIndex = 378;
            this.btnSoall.Text = "列表";
            this.btnSoall.UseVisualStyleBackColor = true;
            this.btnSoall.Click += new System.EventHandler(this.btnSoall_Click);
            // 
            // DetailData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 540);
            this.Controls.Add(this.btnSoall);
            this.Controls.Add(this.btnPrnSo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbMo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cwTeam);
            this.Controls.Add(this.cwQty);
            this.Controls.Add(this.cwMoDID);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "包装详细数据";
            this.Load += new System.EventHandler(this.DetailData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private cwbTB cwMoDID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbMo;
        private cwbTB cwQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private cwbTB cwTeam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrnSo;
        private System.Windows.Forms.Button btnSoall;
    }
}