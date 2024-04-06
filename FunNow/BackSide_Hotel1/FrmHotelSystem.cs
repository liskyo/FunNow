using FunNow.BackSide_Hotel1.View;
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
        public static int Id;
        public static int _roomId;
        public FrmHotelSystem()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            FrmHotelCreate f = new FrmHotelCreate();       
            f.ShowDialog();
                     
            HotelList();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            //FrmRoom r = new FrmRoom();
            FrmRoomCreate r = new FrmRoomCreate();
            r.ShowDialog();
            if (r.isOk != DialogResult.OK) return;
            RoomList();

        }

        private void FrmHotelSystem_Load(object sender, EventArgs e)
        {
            HotelList();
            RoomList();
        }

        private void RoomList()
        {
            dbFunNow db = new dbFunNow();
            var rooms =
            from r in db.Room
            join h in db.Hotel on r.HotelID equals h.HotelID
            join rt in db.RoomType on r.RoomTypeID equals rt.RoomTypeID
            select new
            {
                RoomID = r.RoomID,
                HotelName = h.HotelName,
                RoomTypeName = rt.RoomTypeName,
                RoomName = r.RoomName,
                RoomPrice = r.RoomPrice,
                RoomDescriptione = r.Description,
                RoomStatus = r.RoomStatus,

            };
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.White;
            }
            dataGridView2.DataSource = rooms.ToList();
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
            dbFunNow db = new dbFunNow();
            Hotel hotel = db.Hotel.FirstOrDefault(x => x.HotelID == id);
            if (hotel == null) return;


            //畫面顯示原本資料
            // FrmHotel f = new FrmHotel();
            FrmHotelUpdate f = new FrmHotelUpdate();
            f.hotelInstance = hotel;            

            f.ShowDialog();
            if (f.isOk != DialogResult.OK) return;
            
        
            //更新 DB hotel 物件       
            hotel.HotelName = f.hotelInstance.HotelName;
            hotel.HotelAddress = f.hotelInstance.HotelAddress;
            hotel.HotelPhone = f.hotelInstance.HotelPhone;
            hotel.HotelDescription = f.hotelInstance.HotelDescription;
            hotel.HotelTypeID = f.hotelInstance.HotelTypeID;
           
            //保存更改
            db.SaveChanges();
            HotelList();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             Id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            editByIdhotel(Id);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _roomId = (int)dataGridView2.Rows[e.RowIndex].Cells[0].Value;

            FrmRoomUpdate f = new FrmRoomUpdate();
            f.ShowDialog();

            RoomList();
        }

    }
}
