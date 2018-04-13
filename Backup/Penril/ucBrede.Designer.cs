namespace CWD
{
    partial class ucBrede
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBrede));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsp1 = new System.Windows.Forms.ToolStrip();
            this.tsbNewsheet = new System.Windows.Forms.ToolStripButton();
            this.tsbAddrow = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPrev = new System.Windows.Forms.ToolStripButton();
            this.tsbNext = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbAudit = new System.Windows.Forms.ToolStripButton();
            this.tsbUnAudit = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.生产订单 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生行号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.存货编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.规格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.颜色 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.计量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoDID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbTitle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpCreate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSheetID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tsbSet = new System.Windows.Forms.ToolStripButton();
            this.cwDep = new CWD.cwbTB();
            this.cwVendor = new CWD.cwbTB();
            this.cwMaker = new CWD.cwbTB();
            this.tsp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // tsp1
            // 
            this.tsp1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.tsp1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewsheet,
            this.tsbAddrow,
            this.tsbSave,
            this.tsbPrint,
            this.tsbPrev,
            this.tsbNext,
            this.tsbDel,
            this.tsbAudit,
            this.tsbUnAudit,
            this.tsbSet,
            this.tsbExit});
            this.tsp1.Location = new System.Drawing.Point(0, 0);
            this.tsp1.Name = "tsp1";
            this.tsp1.Size = new System.Drawing.Size(1028, 71);
            this.tsp1.TabIndex = 362;
            this.tsp1.Text = "toolStrip1";
            // 
            // tsbNewsheet
            // 
            this.tsbNewsheet.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewsheet.Image")));
            this.tsbNewsheet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewsheet.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbNewsheet.Name = "tsbNewsheet";
            this.tsbNewsheet.Size = new System.Drawing.Size(97, 68);
            this.tsbNewsheet.Text = "新单";
            this.tsbNewsheet.Click += new System.EventHandler(this.tsbNewsheet_Click);
            // 
            // tsbAddrow
            // 
            this.tsbAddrow.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddrow.Image")));
            this.tsbAddrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddrow.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbAddrow.Name = "tsbAddrow";
            this.tsbAddrow.Size = new System.Drawing.Size(97, 68);
            this.tsbAddrow.Text = "增行";
            this.tsbAddrow.Click += new System.EventHandler(this.tsbAddrow_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(97, 68);
            this.tsbSave.Text = "保存";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(97, 68);
            this.tsbPrint.Text = "打印";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // tsbPrev
            // 
            this.tsbPrev.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrev.Image")));
            this.tsbPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrev.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbPrev.Name = "tsbPrev";
            this.tsbPrev.Size = new System.Drawing.Size(97, 68);
            this.tsbPrev.Text = "前单";
            this.tsbPrev.Click += new System.EventHandler(this.tsbPrev_Click);
            // 
            // tsbNext
            // 
            this.tsbNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbNext.Image")));
            this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNext.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new System.Drawing.Size(97, 68);
            this.tsbNext.Text = "后单";
            this.tsbNext.Click += new System.EventHandler(this.tsbNext_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.Image = ((System.Drawing.Image)(resources.GetObject("tsbDel.Image")));
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(97, 68);
            this.tsbDel.Text = "删除";
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
            // 
            // tsbAudit
            // 
            this.tsbAudit.Image = ((System.Drawing.Image)(resources.GetObject("tsbAudit.Image")));
            this.tsbAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAudit.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbAudit.Name = "tsbAudit";
            this.tsbAudit.Size = new System.Drawing.Size(97, 68);
            this.tsbAudit.Text = "审核";
            this.tsbAudit.Click += new System.EventHandler(this.tsbAudit_Click);
            // 
            // tsbUnAudit
            // 
            this.tsbUnAudit.Image = ((System.Drawing.Image)(resources.GetObject("tsbUnAudit.Image")));
            this.tsbUnAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUnAudit.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbUnAudit.Name = "tsbUnAudit";
            this.tsbUnAudit.Size = new System.Drawing.Size(97, 68);
            this.tsbUnAudit.Text = "弃审";
            this.tsbUnAudit.Click += new System.EventHandler(this.tsbUnAudit_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(97, 68);
            this.tsbExit.Text = "退出";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeight = 40;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.生产订单,
            this.生行号,
            this.存货编码,
            this.名称,
            this.规格,
            this.颜色,
            this.数量,
            this.计量,
            this.MoDID});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv.Location = new System.Drawing.Point(3, 130);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 28;
            this.dgv.Size = new System.Drawing.Size(1274, 471);
            this.dgv.TabIndex = 363;
            this.dgv.Resize += new System.EventHandler(this.dgv_Resize);
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // 生产订单
            // 
            this.生产订单.DataPropertyName = "MoCode";
            this.生产订单.HeaderText = "生产订单";
            this.生产订单.Name = "生产订单";
            this.生产订单.ReadOnly = true;
            this.生产订单.Width = 120;
            // 
            // 生行号
            // 
            this.生行号.DataPropertyName = "Moseq";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.生行号.DefaultCellStyle = dataGridViewCellStyle3;
            this.生行号.HeaderText = "生行号";
            this.生行号.Name = "生行号";
            this.生行号.ReadOnly = true;
            this.生行号.Width = 60;
            // 
            // 存货编码
            // 
            this.存货编码.DataPropertyName = "InvCode";
            this.存货编码.HeaderText = "存货编码";
            this.存货编码.Name = "存货编码";
            this.存货编码.ReadOnly = true;
            this.存货编码.Width = 120;
            // 
            // 名称
            // 
            this.名称.DataPropertyName = "InvName";
            this.名称.HeaderText = "名称";
            this.名称.Name = "名称";
            this.名称.ReadOnly = true;
            this.名称.Width = 300;
            // 
            // 规格
            // 
            this.规格.DataPropertyName = "InvStd";
            this.规格.HeaderText = "规格";
            this.规格.Name = "规格";
            this.规格.ReadOnly = true;
            this.规格.Width = 200;
            // 
            // 颜色
            // 
            this.颜色.DataPropertyName = "Color";
            this.颜色.HeaderText = "颜色";
            this.颜色.Name = "颜色";
            this.颜色.ReadOnly = true;
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "iQuantity";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.数量.DefaultCellStyle = dataGridViewCellStyle4;
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            this.数量.ReadOnly = true;
            // 
            // 计量
            // 
            this.计量.DataPropertyName = "UnitName";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.计量.DefaultCellStyle = dataGridViewCellStyle5;
            this.计量.HeaderText = "计量";
            this.计量.Name = "计量";
            this.计量.ReadOnly = true;
            this.计量.Width = 80;
            // 
            // MoDID
            // 
            this.MoDID.DataPropertyName = "AutoID";
            this.MoDID.HeaderText = "AutoID";
            this.MoDID.Name = "MoDID";
            this.MoDID.ReadOnly = true;
            this.MoDID.Visible = false;
            this.MoDID.Width = 50;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbTitle.ImageKey = "(无)";
            this.lbTitle.Location = new System.Drawing.Point(362, 65);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(195, 33);
            this.lbTitle.TabIndex = 386;
            this.lbTitle.Text = "绣花委外发料";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(183, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 21);
            this.label6.TabIndex = 385;
            this.label6.Text = "部门";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(550, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 21);
            this.label7.TabIndex = 383;
            this.label7.Text = "供应商";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(384, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 384;
            this.label5.Text = "操作员";
            // 
            // dtpCreate
            // 
            this.dtpCreate.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpCreate.Location = new System.Drawing.Point(885, 95);
            this.dtpCreate.Name = "dtpCreate";
            this.dtpCreate.Size = new System.Drawing.Size(190, 31);
            this.dtpCreate.TabIndex = 382;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(830, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 21);
            this.label2.TabIndex = 379;
            this.label2.Text = "日期";
            // 
            // lbSheetID
            // 
            this.lbSheetID.AutoSize = true;
            this.lbSheetID.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSheetID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbSheetID.Location = new System.Drawing.Point(58, 99);
            this.lbSheetID.Name = "lbSheetID";
            this.lbSheetID.Size = new System.Drawing.Size(120, 21);
            this.lbSheetID.TabIndex = 380;
            this.lbSheetID.Text = "2011120203";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 381;
            this.label1.Text = "单号";
            // 
            // tsbSet
            // 
            this.tsbSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbSet.Image")));
            this.tsbSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSet.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbSet.Name = "tsbSet";
            this.tsbSet.Size = new System.Drawing.Size(97, 68);
            this.tsbSet.Text = "设置";
            this.tsbSet.Click += new System.EventHandler(this.tsbSet_Click);
            // 
            // cwDep
            // 
            this.cwDep.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cwDep.Location = new System.Drawing.Point(236, 96);
            this.cwDep.Name = "cwDep";
            this.cwDep.Size = new System.Drawing.Size(149, 31);
            this.cwDep.TabIndex = 377;
            this.cwDep.Text = "床品公司";
            this.cwDep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cwDep.Value = null;
            this.cwDep.Click += new System.EventHandler(this.cwDep_Click);
            // 
            // cwVendor
            // 
            this.cwVendor.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cwVendor.Location = new System.Drawing.Point(629, 96);
            this.cwVendor.Name = "cwVendor";
            this.cwVendor.Size = new System.Drawing.Size(201, 31);
            this.cwVendor.TabIndex = 376;
            this.cwVendor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cwVendor.Value = null;
            this.cwVendor.Click += new System.EventHandler(this.cwVendor_Click);
            // 
            // cwMaker
            // 
            this.cwMaker.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cwMaker.Location = new System.Drawing.Point(463, 96);
            this.cwMaker.Name = "cwMaker";
            this.cwMaker.Size = new System.Drawing.Size(79, 31);
            this.cwMaker.TabIndex = 378;
            this.cwMaker.Text = "张玉兰";
            this.cwMaker.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cwMaker.Value = null;
            // 
            // ucBrede
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.cwDep);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpCreate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbSheetID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cwVendor);
            this.Controls.Add(this.cwMaker);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.tsp1);
            this.Name = "ucBrede";
            this.Size = new System.Drawing.Size(1028, 504);
            this.Load += new System.EventHandler(this.ucBrede_Load);
            this.tsp1.ResumeLayout(false);
            this.tsp1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsp1;
        private System.Windows.Forms.ToolStripButton tsbNewsheet;
        private System.Windows.Forms.ToolStripButton tsbAddrow;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPrev;
        private System.Windows.Forms.ToolStripButton tsbNext;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripButton tsbAudit;
        private System.Windows.Forms.ToolStripButton tsbUnAudit;
        private System.Windows.Forms.ToolStripButton tsbExit;
        public System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lbTitle;
        private cwbTB cwDep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpCreate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbSheetID;
        private System.Windows.Forms.Label label1;
        private cwbTB cwVendor;
        private cwbTB cwMaker;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生行号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 存货编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 规格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 颜色;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计量;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoDID;
        private System.Windows.Forms.ToolStripButton tsbSet;
    }
}
