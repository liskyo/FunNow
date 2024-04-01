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

namespace Fun
{
    public partial class FrmPayment : Form
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
                               { o.Room.Hotel.HotelName, o.Room.Hotel.City.CityName, o.Room.RoomType.RoomTypeName, o.Room.RoomPrice, FirstRoomImage = o.Room.RoomImage.FirstOrDefault(), o.CheckInDate, o.CheckOutDate, o.OrderDetailID, o.Room.RoomName };

            foreach (var od in orderDetails)
            {
                
                payBox c = new payBox();
                c.payHotelName = od.HotelName;
                c.payCityName = od.CityName;
                c.payRoomType = od.RoomTypeName;
                c.payRoomPrice = od.RoomPrice;
                //todo# roomImage
                //c.payRoomPicture = od.FirstRoomImage.ToString(); 
                c.payCheckInDate = od.CheckInDate;
                c.payCheckOutDate = od.CheckOutDate;
                c.orderDetailID = od.OrderDetailID;
                c.payRoomName = od.RoomName; 
               
                this.flowLayoutPanel1.Controls.Add(c);

               
                totalPrice += od.RoomPrice;
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
            foreach (var orderDetail in orderDetailsToUpdate) {
                orderDetail.OrderID = o.OrderID;
                orderDetail.isOrdered = true;
            }
            db.SaveChanges();
            this.Close();

        }
    }
}
