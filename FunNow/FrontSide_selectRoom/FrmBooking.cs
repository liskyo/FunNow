using Fun;
using FunNow.BackSide_POS;
using FunNow.Comment;
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
        public Hotel selectedHotel { get; set; }
        public DateTime frmbookingStart { get; set; }
        public DateTime frmbookingEnd { get; set; }
        public FrmBooking()
        {
            InitializeComponent();     
        }

        private void FrmBooking_Load(object sender, EventArgs e)
        {
            MessageBox.Show(frmbookingEnd.ToString());
            MessageBox.Show(frmbookingStart.ToString());
            MessageBox.Show(selectedHotel.HotelID.ToString());
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
                rb.showAddCart += this.showCartMethod;
                flowLayoutPanel1.Controls.Add(rb);
            }
        }

        private void showCartMethod(roomBox p, DateTime frmbookingStart, DateTime frmbookingEnd)
        {
            MessageBox.Show(FrmPOS.checkInDate.ToString());
            MessageBox.Show(FrmPOS.checkOutDate.ToString());

            //將資料傳進OrderDetails 資料表
            dbFunNow db = new dbFunNow();
            OrderDetails o = new OrderDetails();
            o.RoomID = p.room.RoomID;
            o.MemberID = FrmLogin.auth.MemberID;
            o.CheckInDate = DateTime.Parse(FrmPOS.checkInDate.ToString("yyyy/MM/dd"));
            o.CheckOutDate = DateTime.Parse(FrmPOS.checkOutDate.ToString("yyyy/MM/dd")); ;
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
    }
}
