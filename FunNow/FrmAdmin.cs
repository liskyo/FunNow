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
    public partial class FrmAdmin : Form
    {
        public string loginAdmin { get; set; }
        public int loginAdminID { get; set; }
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            FrmLogin f = new FrmLogin();

            this.Text = "歡迎管理員編號為" + loginAdminID + "的" + loginAdmin + "登入";
        }
    }
}
