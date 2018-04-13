using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CWD
{
    public partial class frmBegin : Form
    {
        public string ModelName;
        public frmBegin()
        {
            InitializeComponent();
        }

        private void btnPacket_Click(object sender, EventArgs e)
        {
            Public.ModelName = "CWD.Packet";
            UserAuth user = new UserAuth();
            
            if (user.ShowDialog() != DialogResult.OK)
                return;
            new fmPacket().ShowDialog();
        }

        private void btnBrede_Click(object sender, EventArgs e)
        {
            Public.ModelName = "CWD.Brede";
            UserAuth user = new UserAuth();
           
            if (user.ShowDialog() != DialogResult.OK)
                return;
            fmBrede brede = new fmBrede();
            brede.SRFlag = true;
            brede.ShowDialog();
        }

        private void btnSew_Click(object sender, EventArgs e)
        {
            Public.ModelName = "CWD.Sewing";
            UserAuth user = new UserAuth();
          
            if (user.ShowDialog() != DialogResult.OK)
                return;
            fmSewing sew = new fmSewing();
            sew.SRFlag = true;
            Public.OperID = 17;
            sew.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBredeIn_Click(object sender, EventArgs e)
        {
            Public.ModelName = "CWD.Brede";
            UserAuth user = new UserAuth();

            if (user.ShowDialog() != DialogResult.OK)
                return;
            fmBrede brede = new fmBrede();
            brede.SRFlag = false;
            brede.ShowDialog();
        }

        private void btnSewIn_Click(object sender, EventArgs e)
        {
            Public.ModelName = "CWD.Sewing";
            UserAuth user = new UserAuth();

            if (user.ShowDialog() != DialogResult.OK)
                return;
            fmSewing sew = new fmSewing();
            sew.SRFlag = false;
            sew.ShowDialog();
        }
    }
}