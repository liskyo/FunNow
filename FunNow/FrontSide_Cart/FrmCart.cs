using FunNow;
using FunNow.BackSide_POS;
using FunNow.FrontSide.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fun
{
    public partial class FrmCart : Form
    {
        public FrmCart()
        {
            InitializeComponent();
        }

        public int Count { get; set; }
        private void FrmCart_Load(object sender, EventArgs e)
        {
            queryAllCart();
         
        }

        private void updateCount(int ctn)
        {
            this.lblCount.Text = "(" + ctn.ToString() + ")";
        }

        private void queryAllCart()
        {
            decimal totalPrice = 0;
            dbFunNow db = new dbFunNow();
            var orderDetails = (from o in db.OrderDetails
                               where o.isOrdered == false && o.MemberID == FrmLogin.auth.MemberID
                               select new
                               { o.Room.Hotel.HotelName, o.Room.Hotel.City.CityName, o.Room.RoomType.RoomTypeName, o.Room.RoomPrice, FirstRoomImage = o.Room.RoomImage.FirstOrDefault(), o.CheckInDate, o.CheckOutDate, o.OrderDetailID }).ToList();
            Count = orderDetails.Count; // count幾筆資料
            updateCount(Count);

            foreach (var od in orderDetails)
            {
                cartBox c = new cartBox();
                c.cartHotelName = od.HotelName;
                c.cartCityName = od.CityName;
                c.cartRoomType = od.RoomTypeName;
                c.cartRoomPrice = od.RoomPrice;
                //todo# roomImage
                //c.cartRoomPicture = od.FirstRoomImage.ToString(); 
                c.cartCheckInDate = od.CheckInDate;
                c.cartCheckOutDate = od.CheckOutDate;
                c.orderDetailID = od.OrderDetailID;
                c.deleteCartBtn += this.deleteMethod;
                flowLayoutPanel1.Controls.Add(c);

                c.btn = od.OrderDetailID;
                totalPrice += od.RoomPrice;
            }
            updateTotalPrice(totalPrice);
            
        }

        private void updateTotalPrice(decimal totalPrice)
        {         
            lblTotalPrice.Text = totalPrice.ToString();             
        }

        private void deleteMethod(cartBox c)
        {
            DialogResult confirmResult = MessageBox.Show("您確定要刪除這個項目嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                dbFunNow db = new dbFunNow();
                OrderDetails od = db.OrderDetails.FirstOrDefault(x => x.OrderDetailID == c.orderDetailID);
                if (od == null) return;
                db.OrderDetails.Remove(od);
                db.SaveChanges();
                MessageBox.Show("成功刪除");
                flowLayoutPanel1.Controls.Clear();
                queryAllCart();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Count <= 0) { MessageBox.Show("購物車沒有商品喔!"); return; }
            
            new FrmPayment().Show();
            //this.Close();
        }
    }
}
