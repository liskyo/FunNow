using FunNow.BackSide_POS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public event D1 showDirectBuy;
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
            using (dbFunNow db = new dbFunNow())
            {
                var orderBooked = db.OrderDetails
                    .Where(o => o.RoomID == this.room.RoomID && o.CheckInDate == FrmPOS.checkInDate && o.CheckOutDate == FrmPOS.checkOutDate);

                if (orderBooked.Any()) { MessageBox.Show("無法重複加入購物車"); return; }

            }
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
                if (!string.IsNullOrEmpty(pic))
                {
                    string path = pic;

                    if (File.Exists(path))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            PictureBox pb = new PictureBox();
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Width = 100;
                            pb.Height = 50;
                            pb.Image = img;
                            pb.Click += Pb_Click;
                            flowLayoutPanel1.Controls.Add(pb);

                            if (flowLayoutPanel1.Controls.Count == 1)
                            {
                                pictureBox1.Image = pb.Image;
                            }
                        }
                    }
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

        private void btnOrder_Click(object sender, EventArgs e)
        {
            using (dbFunNow db = new dbFunNow())
            {
                var orderBooked = db.OrderDetails
                    .Where(o => o.RoomID == this.room.RoomID && o.CheckInDate == FrmPOS.checkInDate && o.CheckOutDate == FrmPOS.checkOutDate);
                if (orderBooked.Any()) { MessageBox.Show("商品已加入至購物車,無法直接下訂"); return; }

            }
            showDirectBuy(this, roomboxStart, roomboxEnd);
        }
    }
}
