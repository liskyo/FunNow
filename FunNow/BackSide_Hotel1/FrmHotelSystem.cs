using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel.View
{
    public partial class FrmHotelSystem : Form
    {
        public FrmHotelSystem()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          
                FrmHotel f = new FrmHotel();
                f.ShowDialog();
                if (f.isOk != DialogResult.OK) return;
            
      ;
                    
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmRoom r = new FrmRoom();
            r.ShowDialog();
            if (r.isOk != DialogResult.OK) return;

        }

        private void FrmHotelSystem_Load(object sender, EventArgs e)
        {
            HotelList();
        }

        private void HotelList()
        {
            dbFunNow db = new dbFunNow();
            var hotels =
            from h in db.Hotel
            join c in db.City on h.CityID equals c.CityID
            join ht in db.HotelType on h.HotelTypeID equals ht.HotelTypeID
            select new
            {

                Hotel1 = h.HotelID,
                HotelName = h.HotelName,
                HotelAddress = h.HotelAddress,
                HotelPhone = h.HotelPhone,
                HotelDescription = h.HotelDescription,
                CityName = c.CityName,
                HotelTypeName = ht.HotelTypeName
            };
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.White;
            }

            dataGridView1.DataSource = hotels.ToList();
        }

   

        private void editByIdhotel(int id)
        {
            FrmHotel f = new FrmHotel();
            Hotel hotel = f.db.Hotel.FirstOrDefault(x => x.HotelID == id);
            if (hotel == null) return;
            f.hotelInstance = hotel; // 傳回我看到的

            f.ShowDialog();
                 
            MessageBox.Show("123");
             
            HotelList();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            editByIdhotel(id);
        }
    }
}
