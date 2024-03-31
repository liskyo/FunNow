using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel
{
    public partial class Form1 : Form
    {
        private Hotel _hotel;
        public Form1()
        {
            InitializeComponent();
        }
        //public Hotel hotel
        //{
        //    //get
        //    //{
        //    //    //if (_hotel == null) { _hotel = new Hotel(); }

        //    //    //_hotel.HotelName = hotelBoxName.fileValue;
        //    //    //_hotel.HotelPhone = hotelBoxPhone.fileValue;
        //    //    //_hotel.HotelAddress = hotelBoxAddress.fileValue;
        //    //    //_hotel.HotelDescription = hotelBoxDesc.fileValue;
        //    //    //_hotel.HotelRegion = ComboxHotelRegion.SelectedItem.ToString();
        //    //    //return _hotel;
        //    //}

        //    //set 給你寫
      
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            //FunNowEntities db = new FunNow5Entities();
            //db.Hotel.Add(hotel);
            //db.SaveChanges();
            //MessageBox.Show("新增成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }
    }
}
