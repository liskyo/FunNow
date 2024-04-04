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

namespace FunNow.FrontSide.View
{
    public delegate void D(cartBox c);
    public partial class cartBox : UserControl
    {
        public event D deleteCartBtn;
        public cartBox()
        {
            InitializeComponent();
        }
        public string cartHotelName
        {
            get { return lblHotelName.Text; }
            set { lblHotelName.Text = value; }
        }

        public string cartCityName
        {
            get { return lblCityName.Text; }
            set { lblCityName.Text = value; }
        }

        public string cartRoomType
        {
            get { return RoomType.Text; }
            set { RoomType.Text = value; }
        }

        public decimal cartRoomPrice
        {
            get { return Convert.ToDecimal( lblRoomPrice.Text);}
            set { lblRoomPrice.Text =  value.ToString(); }
        }

        public DateTime cartCheckInDate
        {
            get { return DateTime.Parse(lblcheckInDate.Text); }
            set { lblcheckInDate.Text = value.ToString("yyyy-MM-dd"); }
        }

        public DateTime cartCheckOutDate
        {
            get { return DateTime.Parse(lblcheckoutDate.Text); }
            set { lblcheckoutDate.Text = value.ToString("yyyy-MM-dd"); }
        }

        public string cartRoomPicture
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {


                    value = Path.GetFileName(value);
                    string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                    string path = Path.Combine(projectRoot, "..\\..\\..\\image\\", value);


                    PictureBoxRoom.Image = Image.FromFile(path);
                }
                else
                {
                    PictureBoxRoom.Image = null;
                }
            }
        }

        public int orderDetailID { get; set; }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteCartBtn(this);
        }

        public string roomName { get { return lblRoomName.Text; } set { lblRoomName.Text = value; } }
        public int btn { get { return (int)btnDelete.Tag; } set { btnDelete.Tag = value; } }

        
    }
}
