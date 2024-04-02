using Fun;
using FunNow.BackSide_POS;
using FunNow.Comment;
using prjFunNowMember.View;
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
    public partial class FrmBooking : Form
    {
        //public FrmBooking(int HotelID)
        //{
        //    selectedHotel.HotelID = HotelID;
        //}


        //痊癒變數+屬性-------------------------------------------------------------------------------
        public Hotel selectedHotel { get; set; }
        public DateTime frmbookingStart { get; set; }
        public DateTime frmbookingEnd { get; set; }
        public FrmBooking()
        {
            InitializeComponent();     
        }
        //Load--------------------------------------------------------------------------------------
        private void FrmBooking_Load(object sender, EventArgs e)
        {
            if (selectedHotel == null)
                return;

            flowLayoutPanel1.Controls.Clear();
            dbFunNow db = new dbFunNow();

            var orders = from o in db.OrderDetails
                         where !(o.CheckInDate >= frmbookingEnd || o.CheckOutDate <= frmbookingStart)
                         select o.RoomID;

            var rooms = from r in db.Room
                        where r.HotelID == selectedHotel.HotelID
                              && !orders.Contains(r.RoomID)
                        select r;

            var comments = (from c in db.CommentRate
                            where c.HotelID == selectedHotel.HotelID
                            orderby c.CreatedAt descending // 按時間遞減排序
                            select c).Take(2); // 取最新的兩筆評論

            int i = 1; // 用於追蹤是 smallCommentBox1 還是 smallCommentBox2
            foreach (var c in comments)
            {   
                if (i == 1)
                {
                    lbSmallComment1.Text = c.Description;
                    i++;
                }
                else if (i == 2)
                {
                    lbSmallComment2.Text = c.Description; // 假設 smallCommentBox2 具有 Text 屬性，用來顯示評論文字

                    break; // 只需顯示最新的兩筆評論
                }
            }

                foreach (var r in rooms)
            {
                roomBox rb = new roomBox();
                rb.room = r;
                rb.Width = flowLayoutPanel1.Width;
                //delegate傳時間: datetimepicker->hotelbox->frmbooking->roombox-------------
                rb.roomboxStart = frmbookingStart;
                rb.roomboxEnd = frmbookingEnd;
                //在roombox註冊顯示房間葉面的事件方法--------------------------------------------
                rb.showRoomEvent += this.showRoomMethod;
                //在roombox註冊加入購物車的事件方法--------------------------------------------
                rb.showAddCart += this.showAddCartMethod;
                flowLayoutPanel1.Controls.Add(rb);
            }

            lblName.Text = selectedHotel.HotelName;
            lblMemo.Text = selectedHotel.HotelDescription;

            //設備--------------------------------------------------------------------------
            var equipments = from eq in db.Hotel_Equipment_Reference
                             where eq.HotelID == selectedHotel.HotelID
                             select eq.HotelEquipment;

            foreach (var equip in equipments)
            {
                CheckBox c = new CheckBox();
                c.Text = equip.HotelEquipmentName.ToString();
                c.Checked = true;
                c.Enabled = false;
                c.Width = flowLayoutPanel2.Width;
                c.Height = 25;
                c.Font = new Font("微軟正黑體", 12, FontStyle.Regular);

                flowLayoutPanel3.Controls.Add(c);
            }
            //圖片--------------------------------------------------------------------------
            var pictures = from p in db.HotelImages
                           where p.HotelID == selectedHotel.HotelID
                           select p.HotelImage;
            foreach (var pic in pictures)
            {
                PictureBox pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Width = 100;
                pb.Height = 50;
                pb.Image = new Bitmap(pic);
                pb.Click += Pb_Click;
                flowLayoutPanel2.Controls.Add(pb);

                if (flowLayoutPanel2.Controls.Count == 1)
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

        //showRoomMethod----------------------------------------------------------------
        private void showRoomMethod(roomBox p)
        {
            FrmRoom f = new FrmRoom();
            f.selectedRoom = p.room;
            f.frmroomStart = p.roomboxStart;
            f.frmroomEnd = p.roomboxEnd;
            f.showAddCart += this.showAddCartMethodf;
            f.ShowDialog();
        }

        //showAddCartMethod(給roombox)----------------------------------------------------------------
        private void showAddCartMethod(roomBox p, DateTime start, DateTime end)
        {
            //存入orderdetails表
            dbFunNow db = new dbFunNow();
            OrderDetails o = new OrderDetails();
            o.RoomID = p.room.RoomID;
            o.MemberID = FrmLogin.auth.MemberID;
            o.CheckInDate = DateTime.Parse(start.ToString("yyyy/MM/dd"));
            o.CheckOutDate = DateTime.Parse(end.ToString("yyyy/MM/dd")); ;
            o.CreatedAt = DateTime.Now;
            o.isOrdered = false;
            db.OrderDetails.Add(o);
            db.SaveChanges();
            MessageBox.Show("已新增至購物車");

            FrmCart f = new FrmCart();
            f.ShowDialog();
        }
        //showAddCartMethodf(給frmroom)----------------------------------------------------------------
        private void showAddCartMethodf(FrmRoom p, DateTime start, DateTime end)
        {
            //存入orderdetails表
            dbFunNow db = new dbFunNow();
            OrderDetails o = new OrderDetails();
            o.RoomID = p.selectedRoom.RoomID;
            o.MemberID = FrmLogin.auth.MemberID;
            o.CheckInDate = DateTime.Parse(start.ToString("yyyy/MM/dd"));
            o.CheckOutDate = DateTime.Parse(end.ToString("yyyy/MM/dd")); ;
            o.CreatedAt = DateTime.Now;
            o.isOrdered = false;
            db.OrderDetails.Add(o);
            db.SaveChanges();
            MessageBox.Show("已新增至購物車");

            FrmCart f = new FrmCart();
            f.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new FrmCart().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {   //bnt showAllComment
            FrmComment frmComment = new FrmComment(selectedHotel);
            frmComment.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            new FrmCart().ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new FrmHotelsLikeFront().Show();
        }
    }
}
