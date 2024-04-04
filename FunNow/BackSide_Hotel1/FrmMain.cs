using FunNow.BackSide_Hotel.View;
using FunNow.BackSide_Order;
using FunNow.Comment;
using prjFunNowMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            showHotelInfo();
        }

        private void showHotelInfo()
        {
          FrmHotelSystem f = new FrmHotelSystem();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Frm f = new Frm();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmMemberList f = new FrmMemberList();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FrmCommentList f = new FrmCommentList();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }
    }
}
