﻿using System;  
using System.Drawing;  
using System.Collections;  
using System.ComponentModel;  
using System.Windows.Forms;  
using System.Data;   

namespace WindowsApplication1  
{   
    /// <summary>   
    /// C#打印设置之Form1 的摘要说明。   
    /// </summary>   
    /// 
    public class Form1 : System.Windows.Forms.Form   
    {  
        private System.Drawing.Printing.PrintDocument pd;  
        private PrintPreviewControl ppc;  
        private PrintPreviewDialog ppd;  
        private System.Windows.Forms.PrintDialog printDialog1;  
        private System.Windows.Forms.Button button1;  
        private System.Windows.Forms.Button button2;  
        private System.Windows.Forms.Button button3;  
        private System.Windows.Forms.TextBox textBox1;   
        String text="";  
        
        /// <summary>  
        /// C#打印设置之必需的设计器变量。  
        /// </summary>  
        /// 
        private System.ComponentModel.Container components = null;   
        public Form1()  
        {   
            //  
            // C#打印设置之Windows 窗体设计器支持所必需的   
            //   
            InitializeComponent();    
            //   
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码   
            //  
        }   
        /// <summary>  
        /// C#打印设置之清理所有正在使用的资源。 
        /// /// </summary>  
        /// 
        protected override void Dispose( bool disposing )  
        {   
            if( disposing )   
            {  
                if (components != null)   
                {   
                    components.Dispose();  
                }   
            }  
            base.Dispose( disposing );  
        }  
#region Windows 窗体设计器生成的代码  
        /// <summary>  
        /// C#打印设置之设计器支持所需的方法 - 不要使用代码编辑器修改  
        /// 此方法的内容。  
        /// </summary>  
        /// 
        private void InitializeComponent()  
        {  this.pd = new System.Drawing.Printing.PrintDocument();  
            this.button1 = new System.Windows.Forms.Button();  
            this.button2 = new System.Windows.Forms.Button();  
            this.button3 = new System.Windows.Forms.Button();  
            this.textBox1 = new System.Windows.Forms.TextBox();  
            this.printDialog1 = new System.Windows.Forms.PrintDialog();  
            this.SuspendLayout(); 
            //   
            // button1 
            //   
            this.button1.Location = new System.Drawing.Point(32, 154);  
            this.button1.Name = "button1";  
            this.button1.Size = new System.Drawing.Size(75, 23);  
            this.button1.TabIndex = 1;  
            this.button1.Text = "开始打印";  
            this.button1.Click += new System.EventHandler(this.button1_Click);  
            //   
            // button2 
            //   
            this.button2.Location = new System.Drawing.Point(120, 154);  
            this.button2.Name = "button2";  
            this.button2.Size = new System.Drawing.Size(75, 23);  
            this.button2.TabIndex = 2;  
            this.button2.Text = "打印预览";  
            this.button2.Click += new System.EventHandler(this.button2_Click);  
            
            //   
            // button3  
            //   
            
            this.button3.Location = new System.Drawing.Point(208, 154);  
            this.button3.Name = "button3";  
            this.button3.Size = new System.Drawing.Size(75, 23); 
            this.button3.TabIndex = 3;  
            this.button3.Text = "打印机设置";  
            this.button3.Click += new System.EventHandler(this.button3_Click);  
            
            //   
            // textBox1  
            //   
            this.textBox1.Location = new System.Drawing.Point(16, 16);  
            this.textBox1.Multiline = true;  
            this.textBox1.Name = "textBox1";  
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;  
            this.textBox1.Size = new System.Drawing.Size(270, 116);  
            this.textBox1.TabIndex = 4;  
            //   
            // Form1  
            //   
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);  
            this.ClientSize = new System.Drawing.Size(314, 199);  
            this.Controls.Add(this.textBox1);  
            this.Controls.Add(this.button3);  
            this.Controls.Add(this.button2);  
            this.Controls.Add(this.button1);  
            this.Name = "Form1";  
            this.Text = "打印窗体";  
            this.Load += new System.EventHandler(this.Form1_Load);  
            this.ResumeLayout(false);  
            this.PerformLayout();   
        }  
#endregion   
        /// <summary>  
        /// C#打印设置之应用程序的主入口点。  
        /// </summary>  
        /// 
        [STAThread]  static void Main()   
        {   Application.Run(new Form1());  }   
        private void Form1_Load(object sender, System.EventArgs e)  
        {   
            //C#打印设置之创建实例   
            this.pd=new System.Drawing.Printing.PrintDocument();   
            this.ppc=new PrintPreviewControl();   
            this.ppd=new PrintPreviewDialog();   
            this.printDialog1=new PrintDialog();    
            //C#打印设置之触发事件   
            this.pd.BeginPrint+=new System.Drawing.Printing.PrintEventHandler(pd_BeginPrint);   
            this.pd.PrintPage+=new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage);      
        }   
        private void pd_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)  
        {    
            //C#打印设置之设置横向打印  
            this.pd.DefaultPageSettings.Landscape=true;    
            //C#打印设置之设置彩色打印   
            this.pd.DefaultPageSettings.Color=true;    
            //C#打印设置之设置打印纸张类型和大小   
            this.pd.DefaultPageSettings.PaperSize=  new System.Drawing.Printing.PaperSize("A4",800,1100);    
        }   
        private void pd_PrintPage(object sender,   System.Drawing.Printing.PrintPageEventArgs e)  
        {   
            //C#打印设置之获取文本框的内容绘制图形传到打印机打印   
            text=this.textBox1.Text;   e.Graphics.DrawString(text,   new Font("宋体",30, FontStyle.Regular),   Brushes.Black, 0, 0);     
        }   
        
        private void button1_Click(object sender,   System.EventArgs e)  
        {   
            //C#打印设置之开始打印  
            this.pd.Print();     
        }   
        
        private void button2_Click(object sender,   System.EventArgs e)  
        {   
            //C#打印设置之设置打印预览信息   
            ppc.Document=pd;   
            ppc.Columns=2;  
            ppc.Rows=2;   
            ppc.Zoom=0.5;   
            ppc.StartPage=1;      
            //C#打印设置之显示预览   
            ppd.Document=pd;  
            try {  ppd.ShowDialog();  }  
            catch (Exception excep)  
            {  
                MessageBox.Show(excep.Message,   "打印出错", MessageBoxButtons.OK,   MessageBoxIcon.Error);  
            }      
        }  
        private void button3_Click(object sender,   System.EventArgs e)  
        {   //C#打印设置之打印机设置   
            this.printDialog1.Document=pd;   
            this.printDialog1.AllowSomePages=true;   
            this.printDialog1.PrintToFile=false;   
            //C#打印设置之确定打印机信息后开始打印   
            if(this.printDialog1.ShowDialog()==DialogResult.OK)   
            {  
                try {  this.pd.Print();  }  
                catch (Exception excep)  
                {  
                    MessageBox.Show(excep.Message,   "打印出错", MessageBoxButtons.OK,   MessageBoxIcon.Error);  
                }  
            }  
        }   
    }  
}  