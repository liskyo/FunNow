using Fun;
using FunNow.Comment;
using FunNow.FrontSide.View;
using FunNow.FrontSide_Cart.view;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace FunNow.FrontSide_Cart
{
    public partial class FrmDirectBuy : Form
    {
        private int _payID;
        public string TotalPrice
        {
            get { return lblTotalPrice.Text; }
            set { lblTotalPrice.Text = value; }
        }
        public string Discount
        {
            get { return lblDiscount.Text; }
            set { lblDiscount.Text = value; }
        }

        public string FinalPrice
        {
            get { return lblFinalPrice.Text; }
            set { lblFinalPrice.Text = value; }
        }
        public FrmDirectBuy()
        {
            InitializeComponent();
        }

    

        private void FrmDirectBuy_Load(object sender, EventArgs e)
        {
            queryAllCart();
            displayAllPaymentMethod();
        }

        private void displayAllPaymentMethod()
        {
            dbFunNow db = new dbFunNow();
            var payMehtod = (from p in db.PaymentStatus
                             select new { p.PaymentStatusID, p.PaymentMethod }).ToList();
            payMehtod.Insert(0, new { PaymentStatusID = 0, PaymentMethod = "請選擇付款方式" });
            comboBox1.DisplayMember = "PaymentMethod";
            comboBox1.ValueMember = "PaymentStatusID";

            comboBox1.DataSource = payMehtod;
        }

        private void queryAllCart()
        {
            decimal totalPrice = 0;
            dbFunNow db = new dbFunNow();
            var maxOrderDetailIdQuery = (from o in db.OrderDetails
                                         where o.isOrdered == false && o.MemberID == FrmLogin.auth.MemberID
                                         select o.OrderDetailID).Max();
            var orderDetails = (from o in db.OrderDetails
                                where o.OrderDetailID == maxOrderDetailIdQuery
                                select new
                                { o.Room.Hotel.HotelName, o.Room.Hotel.City.CityName, o.Room.RoomType.RoomTypeName, o.Room.RoomPrice, FirstRoomImage = o.Room.RoomImage.Select(ri => ri.RoomImage1).FirstOrDefault(), o.CheckInDate, o.CheckOutDate, o.OrderDetailID, o.Room.RoomName }).ToList();



            foreach (var od in orderDetails)
            {

                string firstRoomImageString = od.FirstRoomImage != null ? od.FirstRoomImage.ToString() : null;
            
                payBox p = new payBox();
                p.payHotelName = od.HotelName;
                p.payCityName = od.CityName;
                p.payRoomType = od.RoomTypeName;
                p.payRoomName = od.RoomName;
                p.payRoomPicture = firstRoomImageString;
                p.payCheckInDate = od.CheckInDate;
                p.payCheckOutDate = od.CheckOutDate;
                p.orderDetailID = od.OrderDetailID;
               
                int stayDuration = (od.CheckOutDate - od.CheckInDate).Days;
                p.payRoomPrice = od.RoomPrice * stayDuration;
               
                flowLayoutPanel1.Controls.Add(p);
                totalPrice += od.RoomPrice * stayDuration;
            }
            updateTotalPrice(totalPrice);
         
            lblFinalPrice.Text = totalPrice.ToString(); //FinalPrice 未選用付款方式前為Total Price
        }

        private void updateTotalPrice(decimal totalPrice)
        {
            this.TotalPrice = totalPrice.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            _payID = (int)combo.SelectedValue;

            if ((int)combo.SelectedValue == 0) { lblPayMethod.Visible = false; label4.Visible = false; lblDiscount.Visible = false; }
            else
            {
                lblPayMethod.Visible = true; label4.Visible = true; lblDiscount.Visible = true;

                dbFunNow db = new dbFunNow();
                var discount = (from d in db.PaymentStatus
                                where d.PaymentStatusID == (int)combo.SelectedValue
                                select new { d.Coupon.Discount, d.Coupon.CouponName }).FirstOrDefault();

                if (discount != null)
                {
                    this.lblPayMethod.Text = discount.CouponName;
                    this.Discount = discount.Discount.ToString();
                }

                //FinalPrice
                decimal FinalPrice = Convert.ToDecimal(TotalPrice) - Convert.ToDecimal(Discount);
                lblFinalPrice.Text = FinalPrice.ToString();

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (_payID == 0) { MessageBox.Show("尚未選擇付款方式"); return; }

            dbFunNow db = new dbFunNow();
            //CouponID
            PaymentStatus p = db.PaymentStatus.FirstOrDefault(x => x.PaymentStatusID == _payID);

            Order o = new Order();
            o.MemberID = FrmLogin.auth.MemberID;
            o.OrderStatusID = 1; //預設為已訂
            o.PaymentStatusID = _payID;
            o.TotalPrice = Convert.ToDecimal(FinalPrice);
            o.CouponID = p.CouponID;
            o.CreatedAt = DateTime.Now;
            db.Order.Add(o);
            db.SaveChanges();
            MessageBox.Show("訂單新增成功");

            //修改OrderDetails中的OrderID  null => o.OrderID
            var maxOrderDetailIdQuery = (from o1 in db.OrderDetails
                                         where o1.isOrdered == false && o.MemberID == FrmLogin.auth.MemberID
                                         select o1.OrderDetailID).Max();

            var orderDetailsToUpdate = from od in db.OrderDetails
                                       where od.OrderDetailID == maxOrderDetailIdQuery
                                       select od;

            foreach (var orderDetail in orderDetailsToUpdate)
            {
                orderDetail.OrderID = o.OrderID;
                orderDetail.isOrdered = true;
            }
            db.SaveChanges();


            //根據購物車中商品筆數，顯示相對應的評論表單數
            DateTime targetTime = DateTime.Now.AddSeconds(-30);
            var orderDetails = db.OrderDetails.Where(od => od.MemberID == FrmLogin.auth.MemberID &&
                 od.CreatedAt >= targetTime &&
                 od.CreatedAt <= DateTime.Now).ToList();
            foreach (var orderDetail in orderDetails)
            {

                // 獲取與訂單相關的酒店資訊
                var hotel = db.Hotel.FirstOrDefault(h => h.HotelID == orderDetail.Room.HotelID);
                // 創建新的評論表單，並將相關訂單資訊傳遞給建構函式
                FrmWriteComment wc = new FrmWriteComment(hotel.HotelID, hotel.HotelName, orderDetail.CheckInDate, orderDetail.CheckOutDate, orderDetail.Room.RoomName, orderDetail.RoomID);

                // 顯示評論表單
                wc.ShowDialog();
            }





            this.Close();
        }
    }
}
