using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.FrontSide_Cart.view
{
    public partial class payBox : UserControl
    {
        public payBox()
        {
            InitializeComponent();
        }

        public string payHotelName
        {
            get { return lblHotelName.Text; }
            set { lblHotelName.Text = value; }
        }

        public string payCityName
        {
            get { return lblCityName.Text; }
            set { lblCityName.Text = value; }
        }

        public string payRoomName
        {
            get { return lblRoomName.Text; }
            set { lblRoomName.Text = value; }
        }

        public string payRoomType
        {
            get { return RoomType.Text; }
            set { RoomType.Text = value; }
        }

        public decimal payRoomPrice
        {
            get { return Convert.ToDecimal(lblRoomPrice.Text); }
            set { lblRoomPrice.Text = value.ToString(); }
        }

        public DateTime payCheckInDate
        {
            get { return DateTime.Parse(lblcheckInDate.Text); }
            set { lblcheckInDate.Text = value.ToString("yyyy-MM-dd"); }
        }

        public DateTime payCheckOutDate
        {
            get { return DateTime.Parse(lblcheckoutDate.Text); }
            set { lblcheckoutDate.Text = value.ToString("yyyy-MM-dd"); }
        }

        public string payRoomPicture
        {
            //get { return; }
            set { PictureBoxRoom.Image = Image.FromFile(value); }
        }

        public int orderDetailID { get; set; }


    }
}
