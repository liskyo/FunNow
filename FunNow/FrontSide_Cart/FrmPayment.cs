using FunNow.FrontSide.View;
using FunNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunNow.FrontSide_Cart.view;
using FunNow.BackSide_POS;
using FunNow.Comment;
using System.Data.Entity.Core.Metadata.Edm;

namespace Fun
{
    public partial class FrmPayment : Form
    {
        private int _payID;
        private Hotel selectedHotel;
        private int hotelID;
        private string hotelName;
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private string roomType;
        private int roomID;


        // private DateTime CreateAt;
        public void ShowWriteComment()
        {
            FrmWriteComment wc = new FrmWriteComment(hotelID, hotelName, checkInDate, checkOutDate, roomType, roomID);
            wc.SelectedHotel = selectedHotel;
            wc.ShowDialog();
        }
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

        public FrmPayment()
        {
            InitializeComponent();           
        }

        private void FrmPayment_Load(object sender, EventArgs e)
        {
            queryAllPayment();
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

        private void queryAllPayment()
        {
            decimal totalPrice = 0;
            dbFunNow db = new dbFunNow();
            var orderDetails = from o in db.OrderDetails
                               where o.isOrdered == false && o.MemberID == FrmLogin.auth.MemberID
                               select new
                               { o.Room.Hotel.HotelName, o.Room.Hotel.City.CityName, o.Room.RoomType.RoomTypeName, o.Room.RoomPrice, FirstRoomImage = o.Room.RoomImage.Select(ri => ri.RoomImage1).FirstOrDefault(), o.CheckInDate, o.CheckOutDate, o.OrderDetailID, o.Room.RoomName };

            foreach (var od in orderDetails)
            {
                string firstRoomImageString = od.FirstRoomImage != null ? od.FirstRoomImage.ToString() : null;
                payBox c = new payBox();
                c.payHotelName = od.HotelName;
                c.payCityName = od.CityName;
                c.payRoomType = od.RoomTypeName;

                //todo# roomImage
                c.payRoomPicture = firstRoomImageString;
                c.payCheckInDate = od.CheckInDate;
                c.payCheckOutDate = od.CheckOutDate;
                c.orderDetailID = od.OrderDetailID;
                c.payRoomName = od.RoomName;
                int stayDuration = (od.CheckOutDate - od.CheckInDate).Days;
                c.payRoomPrice = od.RoomPrice * stayDuration;
                this.flowLayoutPanel1.Controls.Add(c);


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
            
            if ((int)combo.SelectedValue == 0) { lblPayMethod.Visible = false; label2.Visible = false; lblDiscount.Visible = false; } 
            else
            {
                lblPayMethod.Visible = true; label2.Visible = true; lblDiscount.Visible = true;
           
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
            var orderDetailsToUpdate = from od in db.OrderDetails
                                       where od.MemberID == FrmLogin.auth.MemberID && od.OrderID == null
                                       select od;
            foreach (var orderDetail in orderDetailsToUpdate)
            {
                orderDetail.OrderID = o.OrderID;
                orderDetail.isOrdered = true;
            }
            db.SaveChanges();

            // 獲取所有與當前訂單關聯的訂單明細
            var orderDetails = db.OrderDetails.Where(od => od.OrderID == o.OrderID).OrderBy(od => od.CreatedAt).ToList();

            foreach (var orderDetail in orderDetails)
            {
                // 獲取與訂單相關的酒店資訊
                var hotel = db.Hotel.FirstOrDefault(h => h.HotelID == orderDetail.Room.HotelID);

                // 獲取訂單明細關聯的房型名稱
                var roomTypeName = orderDetail.Room.RoomType.RoomTypeName;

                // 創建新的評論表單,並將相關訂單資訊傳遞給建構函式
                FrmWriteComment wc = new FrmWriteComment(hotel.HotelID, hotel.HotelName, orderDetail.CheckInDate, orderDetail.CheckOutDate, roomTypeName, orderDetail.RoomID);

                wc.ShowDialog();
            }

            this.Close();

        }
    }
}
//根據購物車中商品筆數，顯示相對應的評論表單數
//DateTime targetTime = DateTime.Now.AddSeconds(-30);
//var orderDetails = db.OrderDetails.Where(od => od.MemberID == FrmLogin.auth.MemberID &&
//     od.CreatedAt >= targetTime &&
//     od.CreatedAt <= DateTime.Now).ToList();
//foreach (var orderDetail in orderDetails)
//{

//    // 獲取與訂單相關的酒店資訊
//    var hotel = db.Hotel.FirstOrDefault(h => h.HotelID == orderDetail.Room.HotelID);
//    // 創建新的評論表單，並將相關訂單資訊傳遞給建構函式
//    FrmWriteComment wc = new FrmWriteComment(hotel.HotelID, hotel.HotelName, orderDetail.CheckInDate, orderDetail.CheckOutDate, orderDetail.Room.RoomName);

//    // 顯示評論表單
//    wc.ShowDialog();
//}
// 獲取所有與當前訂單關聯的訂單明細