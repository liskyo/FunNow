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
    public partial class roomBox : UserControl
    {
        private DateTime _start;
        private Room _room;
        public DateTime start
        {
            get { return _start; }
            set
            {
                _start = value;
                lblDate.Text = _start.ToString("yyyy/MM/dd"); 
            }
        }
        public DateTime end { get; set; }
        public Room room
        {
            get { return _room; }
            set
            {
                _room = value;
                lblName.Text = _room.RoomName;
                lblMemo.Text = _room.Description;
                lblPrice.Text = "$" + _room.RoomPrice.ToString();
            }
        }

        public roomBox()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmRoom f = new FrmRoom();
            f.ShowDialog();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            FrmRoom f = new FrmRoom();
            f.ShowDialog();
        }
    }
}
