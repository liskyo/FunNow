using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow
{
    public partial class FrmBooking : Form
    {
        private Hotel _selectedHotel;
        public Hotel selectedHotel{ get; set; }

        private Hotel _testHotel;
        public FrmBooking(Hotel p)
        {
            InitializeComponent();
            this.selectedHotel = p;
        }

        public FrmBooking()
        {
            InitializeComponent();
        }

        private void FrmBooking_Load(object sender, EventArgs e)
        {

            if (selectedHotel == null)
                return;

            flowLayoutPanel1.Controls.Clear();
            dbFunNow db = new dbFunNow();
            var rooms = from r in db.Room
                        where r.HotelID == selectedHotel.HotelID // todo# 而且...有空房
                        select r;

            //todo# lblName.Text = _selectedHotel.HotelName; => null問題

            foreach (var r in rooms)
            {
                roomBox rb = new roomBox();
                rb.start = dateTimePicker1.Value;
                rb.end = dateTimePicker2.Value;
                rb.room = r;
                rb.Width = flowLayoutPanel1.Width;
                flowLayoutPanel1.Controls.Add(rb);
            }
        }
    }
}
