using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }
        public string loginMemeber {  get; set; }
        public int loginMemberID { get; set; }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            FrmLogin f = new FrmLogin();
            
            this.Text = "歡迎會員編號為"+loginMemberID +"的"+ loginMemeber + "登入";
        }
    }
}
