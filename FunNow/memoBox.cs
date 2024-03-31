using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow
{
    public partial class memoBox : UserControl
    {
        public memoBox()
        {
            InitializeComponent();
        }
        public string mbName
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        public string mbValue
        {
            get
            {
                return label2.Text;
            }
            set
            {
                label2.Text = value;
            }
        }
    }
}
