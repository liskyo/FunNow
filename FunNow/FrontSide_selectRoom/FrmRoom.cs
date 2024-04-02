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
    public delegate void D3(FrmRoom p, DateTime start, DateTime end);
    public partial class FrmRoom : Form
    {
        public event D3 showAddCart;

        //痊癒變數+屬性-------------------------------------------------------------
        private DateTime _frmroomStart;
        public DateTime frmroomStart
        {
            get { return _frmroomStart; }
            set
            {
                _frmroomStart = value;
                lblDate.Text = _frmroomStart.ToString("yyyy/MM/dd");
            }
        }
        public DateTime frmroomEnd { get; set; }
        public Room selectedRoom {  get; set; }
        public FrmRoom()
        {
            InitializeComponent();
        }

        //load-----------------------------------------------------------------
        private void FrmRoom_Load(object sender, EventArgs e)
        {
            if (selectedRoom == null) 
                return;
            //設備-------------------------------------------------
            dbFunNow db = new dbFunNow();
            var equipments = from eq in db.Room_Equipment_Reference
                             where eq.RoomID == selectedRoom.RoomID 
                             select eq.RoomEquipment;

            foreach (var equip in equipments)
            {
                CheckBox c = new CheckBox();
                c.Text = equip.RoomEquipmentName.ToString();
                c.Checked = true;
                c.Enabled = false;
                c.Width = flowLayoutPanel2.Width;
                c.Height = 30;
                c.Font = new Font("微軟正黑體", 16, FontStyle.Regular);

                flowLayoutPanel2.Controls.Add(c);
            }

            //圖片-------------------------------------------------
            var pictures = from p in db.RoomImage
                           where p.RoomID == selectedRoom.RoomID
                           select p.RoomImage1;
            foreach (var pic in pictures)
            {
                PictureBox pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Width = 100;
                pb.Height = 50;
                pb.Image = new Bitmap(pic);
                pb.Click += Pb_Click;
                flowLayoutPanel1.Controls.Add(pb);

                if (flowLayoutPanel1.Controls.Count == 1)
                {
                    pictureBox1.Image = pb.Image;
                }
            }

            lblName.Text = selectedRoom.RoomName;
            lblPrice.Text = "$" + selectedRoom.RoomPrice.ToString();
        }
        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                pictureBox1.Image = clickedPictureBox.Image;
            }
        }
        private void btnCart_Click(object sender, EventArgs e)
        {
            showAddCart?.Invoke(this, frmroomStart, frmroomEnd);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            //立即預定?
        }
    }
}
