using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_POS
{
    public partial class FrmPOS2 : Form
    {
        //private Room room = null;
        //private IQueryable<Room> rooms = null;
        private Hotel hotel = null;
        private IQueryable<Hotel> hotels = null;


        public FrmPOS2()
        {
            InitializeComponent();
           // this.room = room;

        }
        //public FrmPOS2(Room room)
        //{
        //    InitializeComponent();
        //    this.room = room;
        //    this.rooms = null;


        //}
        //public FrmPOS2(IQueryable<Room> rooms)
        //{
        //    InitializeComponent();
        //    this.rooms = rooms;
        //    this.room = null;


        //}

        public FrmPOS2(Hotel hotel)
        {

            InitializeComponent();
            this.hotel = hotel;
            this.hotels = null;

        }

        public FrmPOS2(IQueryable<Hotel> hotels)
        {
            InitializeComponent();
            this.hotels = hotels;
            this.hotel = null;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmPOS2_Load(object sender, EventArgs e)
        {

            //if (this.room != null )
            //{
            //    List<Room> list = new List<Room>();
            //    list.Add(this.room);
            //    dataGridView1.DataSource = list; //顯示點擊單筆
            //}
            //else
            //{ 
            //    dataGridView1.DataSource = rooms.ToList(); //顯示全部
            //}

            if (this.hotel != null)
            {
                List<Hotel> list = new List<Hotel>();
                list.Add(this.hotel);
                dataGridView1.DataSource = list; //顯示點擊單筆
            }
            else
            {
                dataGridView1.DataSource = hotels.ToList(); //顯示全部
            }

        }





    }
}
