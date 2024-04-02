﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FunNow.BackSide_Order
{
    public partial class FrmOrder : Form

    {
       // private string _ImagePath = "";
        private DialogResult _isOk;
        private Order _order;
        private OrderDetails _orderDetails;

        public OrderDetails orderdetails 
        {
            get
            {
                if (_orderDetails == null)
                {
                    _orderDetails = new OrderDetails();
                }
                _orderDetails.MemberID = Convert.ToInt32(MemberIDBox.fileValue);
                _orderDetails.RoomID = Convert.ToInt32(RoomIDBox.fileValue);
                _orderDetails.CheckInDate = Convert.ToDateTime(CheckInDateBox.fileValue);
                _orderDetails.CheckOutDate = Convert.ToDateTime(CheckOutDateBox.fileValue);
                _orderDetails.CreatedAt = Convert.ToDateTime(CreatedAtBox.fileValue);
                _orderDetails.isOrdered = Convert.ToBoolean(isOrderedBox.fileValue);
                //_orderDetails.OrderID = Convert.ToInt32(OrderIDBox.fileValue);


                return _orderDetails;
            }
            set 
            {
                _orderDetails = value;
                MemberIDBox.fileValue = _orderDetails.MemberID.ToString();
                RoomIDBox.fileValue =_orderDetails.RoomID.ToString();
                CheckInDateBox.fileValue = _orderDetails.CheckInDate.ToString();
                CheckOutDateBox.fileValue = _orderDetails.CheckOutDate.ToString();
                CreatedAtBox.fileValue = _orderDetails.CreatedAt.ToString();
                isOrderedBox.fileValue =_orderDetails.isOrdered.ToString();
               // OrderIDBox.fileValue = _orderDetails.OrderID.ToString();

            }
        }

        public Order order
        {
            get
            {
                if (_order == null)
                {
                    _order = new Order();
                }
                //_order.OrderID = Convert.ToInt32(OrderIDBox.fileValue);
                _order.MemberID = Convert.ToInt32(MemberIDBox.fileValue);
                _order.OrderStatusID = Convert.ToInt32(OrderStatusIDBox.fileValue);
                _order.PaymentStatusID = Convert.ToInt32(PaymentStatusIDBox.fileValue);
                _order.TotalPrice = Convert.ToDecimal(TotalPriceBox.fileValue);
                _order.CouponID = Convert.ToInt32(CouponIDBox.fileValue);
                _order.CreatedAt = Convert.ToDateTime(CreatedAtBox.fileValue);
                

                return _order;
            }
            set
            {
                _order = value;
                //OrderIDBox.fileValue = _order.OrderID.ToString();
                MemberIDBox.fileValue = _order.MemberID.ToString();
                OrderStatusIDBox.fileValue = _order.OrderStatusID.ToString();
                PaymentStatusIDBox.fileValue = _order.PaymentStatusID.ToString();
                TotalPriceBox.fileValue = _order.TotalPrice.ToString();
                CouponIDBox.fileValue = _order.CouponID.ToString();
                CreatedAtBox.fileValue = _order.CreatedAt.ToString();

            }
        }
        public DialogResult isOk
        {
            get
            {
                return _isOk;
            }
        }
        public FrmOrder()
        {
            InitializeComponent();

            //CustomComboBoxControl1 = new CustomComboBoxControl(); // 初始化 CustomComboBoxControl1 控制元件
            //this.Controls.Add(CustomComboBoxControl1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isOk = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isOk = DialogResult.Cancel;
            Close();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CreatedAtBox.fileValue))

                CreatedAtBox.fileValue = DateTime.Now.ToString(); // 使用 ToString() 方法將 DateTime 轉換為字串並賦值給 CreatedAtBox 的 Text 屬性

        }
    }
}