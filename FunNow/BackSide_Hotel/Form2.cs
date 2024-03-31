using FunNow.BackSide_Hotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel
{
    public partial class Form2 : Form
    {
        private RoomType _roomType;
       
        public Form2()
        {
            InitializeComponent();
        }

        //public RoomType roomType
        //{
        //    //get
        //    //{
        //    //    int selectedHotelId = (int)comboBoxHotel.SelectedValue;

        //    //    if (_roomType == null) { _roomType = new RoomType(); }
               
        //    //    _roomType.HotelID = Convert.ToInt32(selectedHotelId);
        //    //    _roomType.TypeName = RoomType.fileValue;
        //    //    _roomType.Capacity = Convert.ToInt32( RoomCapacity.fileValue);
        //    //    _roomType.RoomPrice = Convert.ToInt32(Price.fileValue);
        //    //    _roomType.Description = Desc.fileValue;
               
        //    //    return _roomType;
        //    //}

        //    //set 給你寫

        //}

        private void button1_Click(object sender, EventArgs e)
        {
          
            //dbFunNowEntities db = new dbFunNowEntities();
            //db.RoomType.Add(roomType);
            //db.SaveChanges();
            //MessageBox.Show("新增成功");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dbFunNowEntities db = new dbFunNowEntities();
            var hotels = from h in db.Hotel
                          select new { h.HotelID, h.HotelName };
            comboBoxHotel.DataSource = hotels.ToList();
            comboBoxHotel.DisplayMember = "HotelName";
            comboBoxHotel.ValueMember = "HotelID";
        }


    }
}
