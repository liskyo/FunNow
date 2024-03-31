using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_OrderDetails
{
    public partial class FrmOrderDetails : Form
    {
        private DialogResult _isOk;
        private OrderDetails _order;
        public OrderDetails order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderDetails();
                }
                _order.OrderDetailID = Convert.ToInt32(fbOrderDetailID.fieldValue);
                _order.MemberID = Convert.ToInt32(fbMemberID.fieldValue);
                _order.RoomID = Convert.ToInt32(fbRoomID.fieldValue);
                _order.CheckInDate = Convert.ToDateTime(fbCheckInDate.Value);
                _order.CheckOutDate = Convert.ToDateTime(fbCheckOutDate.Value);
                return _order;
            }
            set
            {
                _order = value;
                fbOrderDetailID.fieldValue = _order.OrderDetailID.ToString();
                fbMemberID.fieldValue = _order.MemberID.ToString();
                fbRoomID.fieldValue = _order.RoomID.ToString();
                fbCheckInDate.Value = (DateTime)_order.CheckInDate;
                fbCheckOutDate.Value = (DateTime)_order.CheckOutDate;
            }
        }
        public DialogResult isOk
        {
            get
            {
                return _isOk;
            }
        }

        public FrmOrderDetails()
        {
            InitializeComponent();
        }






        private void FrmOrderDetails_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isOk = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isOk = DialogResult.OK;
            Close();
        }

        private void fbCheckInDate_ValueChanged(object sender, EventArgs e)
        {
            fbCheckInDate.MinDate = DateTime.Today;
        }

        private void fbCheckOutDate_ValueChanged(object sender, EventArgs e)
        {
            fbCheckOutDate.MinDate = fbCheckInDate.Value.AddDays(1);

        }
    }
}
