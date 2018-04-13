using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CWD
{
    
    public partial class cwbTB : System.Windows.Forms.TextBox
    {
        //[]
        private string _value;
        public cwbTB()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }

        [NotifyParentProperty(true), Description("内部值"), Category("编辑器"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Value
        {
            get
            { return _value;}
            set
            { _value=value;}
        }
    }
}
