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
    //delegate---------------------------------------------------------
    public delegate void D1(roomBox p, DateTime start, DateTime end);
    public delegate void D2(roomBox p);

    public partial class roomBox : UserControl
    {
        //event--------------------------------------------------------
        public event D1 showAddCart;
        public event D2 showRoomEvent;
        //痊癒變數+屬性-------------------------------------------------
        private DateTime _roomboxStart;
        public DateTime roomboxStart
        {
            get { return _roomboxStart; }
            set
            {
                _roomboxStart = value;
                lblDate.Text = _roomboxStart.ToString("yyyy/MM/dd");
            }
        }

        public DateTime roomboxEnd { get; set; }
        
        private Room _room;
        public Room room
        {
            get { return _room; }
            set
            {
                _room = value;
                lblName.Text = _room.RoomName;
                lblPrice.Text = "$" + _room.RoomPrice.ToString();
            }
        }
        public roomBox()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------------
        private void lblName_Click(object sender, EventArgs e)
        {
            showRoomEvent?.Invoke(this);
        }

        private void btnCart_Click_1(object sender, EventArgs e)
        {
            showAddCart(this, roomboxStart, roomboxEnd);
        }

        private void roomBox_Load(object sender, EventArgs e)
        {
            dbFunNow db = new dbFunNow();
            var pictures = from p in db.RoomImage
                           where p.RoomID == room.RoomID
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
        }
        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                pictureBox1.Image = clickedPictureBox.Image;
            }
        }
    }
}
