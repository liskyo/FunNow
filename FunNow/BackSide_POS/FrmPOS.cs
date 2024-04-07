using Fun;
using FunNow.BackSide_POS.View;
using prjFunNowMember.View;
using System;
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

    public partial class FrmPOS : Form
    {
        public static DateTime checkInDate { get; set; }
        public static DateTime checkOutDate { get; set; }

        private OrderDetails _order;
        public OrderDetails order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderDetails();
                }
                return _order;
            }
        }
        private Hotel _h;
        public Hotel h
        {
            get
            {
                if (_h == null)
                {
                    _h = new Hotel();
                }
                return _h;
            }
        }
        private Room _r;
        private void showHotelMethod(HotelBox p, DateTime start, DateTime end)
        {
            
            FrmBooking f = new FrmBooking();
            f.selectedHotel = p.hotel;
            f.frmbookingStart = start;
            f.frmbookingEnd = end;
            f.ShowDialog();

        }
        public Room r
        {
            get
            {
                if (_r == null)
                {
                    _r = new Room();
                }
                return _r;
            }
        }
        public FrmPOS()
        {
            InitializeComponent();
            this.Text = "FunNow訂房平台，Have Fun!!";
        }
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) //訂房日期鍵
        {
            dateTimePicker1.MinDate = DateTime.Today;//將 dateTimePicker1 的最小日期設定為今天

            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 // List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 所有的hotel
                         where rooms.ToList().Contains(h.HotelID) //r.HotelID = h.HotelID 找出尚有空房的旅館 List<Hotel>
                         select h;                                 


            // 將查詢結果繫結到資料表
            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID)  // List<Hotel>
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;
                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數   
                                      // rb._hotels = hotels2;//顯示全部旅館

                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;



        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) //退房日期鍵
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);//將 dateTimePicker2 的最小日期設定為 dateTimePicker1 的值加 1 天。

            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位，已被訂房的資料
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 //ex. List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 所有的hotel
                         where rooms.ToList().Contains(h.HotelID) //r.HotelID = h.HotelID 找出尚有空房的旅館 List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            // 將查詢結果繫結到資料表  //hotels為IQueryable屬性 是 .NET Framework 中的一個介面，用於表示可查詢的資料集合
            dataGridView1.DataSource = hotels.ToList(); //將hotels IQueryable屬性轉換成陣列
                                                        //提供了一種統一的方式來查詢各種資料源，包括 ADO.NET 資料庫、XML 檔案和 LINQ to Objects 集合。
                                                        //主要優點是它允許您在資料源上執行查詢，而無需將資料載入到記憶體中。這可以顯著提高大型資料集的查詢性能。
                                                        //提供了一系列方法來建立查詢，包括：
                                                        //Where：根據指定條件篩選資料。
                                                        //Select：投影資料，即從資料中選取特定列或值。
                                                        //OrderBy：根據指定條件對資料進行排序。
                                                        //GroupBy：根據指定條件對資料進行分組。
            resetGridStyle();// 重設資料表樣式(包含自訂欄位)

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID)  // List<Hotel>
                          select new { HotelAll =h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                               //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;
                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數   
                             // rb._hotels = hotels2;//顯示全部旅館

                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }      
        private void button1_Click(object sender, EventArgs e)  //KeyWord 查詢 
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位，已被訂房的資料
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 //ex. List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            string keyword = txtKeyword.Text; //關鍵字搜尋

            var hotels = from h in db.Hotel   // 所有的hotel
                         where (rooms.ToList().Contains(h.HotelID)) && h.HotelName.Contains(keyword)
                         || h.HotelAddress.Contains(keyword)
                         || h.HotelPhone.Contains(keyword)
                         || h.City.CityName.Contains(keyword)
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            // 將查詢結果繫結到資料表
            dataGridView1.DataSource = hotels.ToList();
            
            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where (rooms.ToList().Contains(h.HotelID)) && h.HotelName.Contains(keyword)
                          || h.HotelAddress.Contains(keyword)
                          || h.HotelPhone.Contains(keyword)
                          || h.City.CityName.Contains(keyword)
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;

        }
        private void toolStripButton4_Click(object sender, EventArgs e)  //3條件搜尋
        {
            if (toolStripComboBox1.Text=="請選擇人數" || toolStripComboBox2.Text == "請選擇城市")
            {
                MessageBox.Show("請選擇城市及訂房人數");
            }


            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 tOrder 表中所有記錄的 fRoomID 欄位，會關聯r.fId // List<OrderDetails>
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 // List<RoomID>123 234
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) && r.RoomType.Capacity.ToString() == toolStripComboBox1.Text //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 空房的hotel
                         where (rooms.ToList().Contains(h.HotelID)) && h.City.CityName == toolStripComboBox2.Text  // List<Hotel>                                                                  
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var rooms2 = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) && r.RoomType.Capacity.ToString() == toolStripComboBox1.Text //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where (rooms.ToList().Contains(h.HotelID)) && h.City.CityName == toolStripComboBox2.Text  // List<Hotel>
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數
                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }               
        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton7_Click(object sender, EventArgs e) //按旅館均價排列
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 //ex. List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 空房的hotel
                         where rooms.ToList().Contains(h.HotelID)  // List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,
                         };                                  // List<Hotel>

            hotels = hotels.OrderBy(p => p.均價);

            // 將查詢結果繫結到資料表
            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //========================================          

            flowLayoutPanel1.Controls.Clear(); //首先，清除 flowLayoutPanel1 控制項中的所有控制項

            var sorthotelname = hotels.Select(p => p.旅館名稱); //從 sortedhotels 集合中選取所有旅館名稱                                                                     
            List<string> sorthotelnamelist = sorthotelname.ToList();//並轉換為 List<string> 集合 sorthotelname2

            //從 db.Hotel 資料表中選取所有空房的旅館並轉換為 List<Hotel> 集合
            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID)  // List<Hotel> hotels2list
                          select new { HotelAll = h,h.HotelName, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox

            var hotels2list = hotels2.ToList();

            var sorthtels3 = hotels2list   //根據旅館名稱對 hotels2list 集合進行排序
                .OrderBy(h => sorthotelnamelist.IndexOf(h.HotelName)).ToList();

            var hotellike = from hl in db.HotelLikes //從 db.HotelLikes 資料表中選取所有旅館喜好資料。
                            select hl;

            foreach (var h in sorthtels3) //遍歷 sorthtels3 集合中的每個旅館
            {

                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數


                HotelBox hb = new HotelBox();//建立一個 HotelBox 物件，用來顯示旅館資訊。

                //根據會員 ID 和旅館 ID 從 db.HotelLikes 資料表中選取旅館喜好資料
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0) //如果旅館喜好裡有資料，則設定 HotelBox 物件的旅館喜好狀態
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0); //返回 List<HotelLikes> 集合中的第一個元素
                                                                          //將第一個元素轉換為 HotelLikes 型別
                                                                          //rb.hotellike：HotelBox 物件的 HotelLikes 屬性，用於存儲旅館喜好資料。
                                                                          //hls：一個包含旅館喜好資料的 IEnumerable<HotelLikes> 集合。
                                                                          //ToList()：將 IEnumerable< HotelLikes > 集合轉換為 List<HotelLikes> 集合。
                                                                          //ElementAt(0)：返回 List<HotelLikes> 集合中的第一個元素。
                                                                          //(HotelLikes)：將第一個元素轉換為 HotelLikes 型別。
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數   
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;

                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }     
        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //住宿類型查詢
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 tOrder 表中所有記錄的 fRoomID 欄位，會關聯r.fId // List<OrderDetails>
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 // List<RoomID>123 234
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 空房的hotel            
                         where (rooms.ToList().Contains(h.HotelID)) && h.HotelType.HotelTypeName == comboBox1.Text  // List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where (rooms.ToList().Contains(h.HotelID)) && h.HotelType.HotelTypeName == comboBox1.Text  // List<Hotel>
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館

                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }
        private void toolStripButton9_Click(object sender, EventArgs e) //打開購物車
        {
            new FrmCart().Show();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //房間類型查詢
        {
                        dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 tOrder 表中所有記錄的 fRoomID 欄位，會關聯r.fId // List<OrderDetails>
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 // List<RoomID>123 234
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) && r.RoomType.RoomTypeName == comboBox2.Text//k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 空房的hotel            
                         where (rooms.ToList().Contains(h.HotelID)) //&& h.HotelType.HotelTypeName == comboBox1.Text  // List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 



            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where (rooms.ToList().Contains(h.HotelID))   // List<Hotel>
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館

                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)  //旅館設施查詢
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 tOrder 表中所有記錄的 fRoomID 欄位，會關聯r.fId // List<OrderDetails>
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 // List<RoomID>123 234
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotelequipmentreference = from hr in db.Hotel_Equipment_Reference
                                 where hr.HotelEquipment.HotelEquipmentName == comboBox3.Text
                                 select hr.HotelID;

            var hotels = from h in db.Hotel   // 空房的hotel            
                         where rooms.ToList().Contains(h.HotelID) && hotelequipmentreference.ToList().Contains(h.HotelID)    // List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID) && hotelequipmentreference.ToList().Contains(h.HotelID)    // List<Hotel>
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館

                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)  //房間設施查詢
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 tOrder 表中所有記錄的 fRoomID 欄位，會關聯r.fId // List<OrderDetails>
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 // List<RoomID>123 234
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間
            // 

            var roomequipmentreference = from re in db.Room_Equipment_Reference
                                         where re.RoomEquipment.RoomEquipmentName == comboBox4.Text
                                         select re.RoomID;

            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) && roomequipmentreference.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 空房的hotel            
                         where (rooms.ToList().Contains(h.HotelID)) //&& h.HotelType.HotelTypeName == comboBox1.Text  // List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where (rooms.ToList().Contains(h.HotelID))   // List<Hotel>
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }
        private void toolStripButton8_Click(object sender, EventArgs e)  //精選旅館名稱有W的飯店
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位，已被訂房的資料
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 //ex. List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            string keyword = txtKeyword.Text; //關鍵字搜尋

            var hotels = from h in db.Hotel   // 所有的hotel
                         where rooms.ToList().Contains(h.HotelID) && h.HotelName.Contains("w")
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,

                         };  // 將hotels查詢結果繫結到dataGridView1 

            // 將查詢結果繫結到資料表
            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear();

            var hotellike = from hl in db.HotelLikes
                            select hl;

            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID) && h.HotelName.Contains("w")
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox
                                                                                                                                                //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 RoomBox 物件，用來顯示房間資訊。
                                             //rb.start = dateTimePicker1.Value;
                                             //rb.end = dateTimePicker2.Value;
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0)   //愛心顏色才會跟HotelLikes內的LikeStatus同步
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0);
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;
                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;

        }        
        private void toolStripButton6_Click(object sender, EventArgs e)  //按旅館評分排序(進階搜尋原本沒有的欄位可參考)
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位，已被訂房的資料
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 //ex. List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            string keyword = txtKeyword.Text; //關鍵字搜尋


            var hotels = from h in db.Hotel   // 所有的hotel
                         join c in db.CommentRate on h.HotelID equals c.HotelID
                         where rooms.ToList().Contains(h.HotelID) // List<Hotel>
                         select
                         new
                         {                             
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             //均價 = h.Room.Average(p => p.RoomPrice),
                             評分 = h.CommentRate.Average(p => p.Rating)

                         };  // 將hotels查詢結果繫結到dataGridView1 

            hotels = hotels.OrderByDescending(h => h.評分);


            // 將查詢結果繫結到資料表
            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //===================================================  以下為顯示於HotelBox的操作，包含我的最愛功能

            flowLayoutPanel1.Controls.Clear(); //首先，清除 flowLayoutPanel1 控制項中的所有控制項

            var sorthotelname = hotels.Select(p => p.旅館名稱); //從 sortedhotels 集合中選取所有旅館名稱                                                                     
            List<string> sorthotelnamelist = sorthotelname.ToList();//並轉換為 List<string> 集合 sorthotelname2

            //從 db.Hotel 資料表中選取所有空房的旅館並轉換為 List<Hotel> 集合
            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID)  // List<Hotel> hotels2list
                          select new { HotelAll = h,h.HotelName, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox

            var hotels2list = hotels2.ToList();

            var sorthtels3 = hotels2list   //根據旅館名稱對 hotels2list 集合進行排序
                .OrderBy(h => sorthotelnamelist.IndexOf(h.HotelName)).ToList();

            var hotellike = from hl in db.HotelLikes //從 db.HotelLikes 資料表中選取所有旅館喜好資料。
                            select hl;

            foreach (var h in sorthtels3) //遍歷 sorthtels3 集合中的每個旅館
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 HotelBox 物件，用來顯示旅館資訊。

                //根據會員 ID 和旅館 ID 從 db.HotelLikes 資料表中選取旅館喜好資料
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0) //如果旅館喜好裡有資料，則設定 HotelBox 物件的旅館喜好狀態
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0); //返回 List<HotelLikes> 集合中的第一個元素
                                                                          //將第一個元素轉換為 HotelLikes 型別
                                                                          //rb.hotellike：HotelBox 物件的 HotelLikes 屬性，用於存儲旅館喜好資料。
                                                                          //hls：一個包含旅館喜好資料的 IEnumerable<HotelLikes> 集合。
                                                                          //ToList()：將 IEnumerable< HotelLikes > 集合轉換為 List<HotelLikes> 集合。
                                                                          //ElementAt(0)：返回 List<HotelLikes> 集合中的第一個元素。
                                                                          //(HotelLikes)：將第一個元素轉換為 HotelLikes 型別。
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;

                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new FrmHotelsLikeFront().Show();
        } //打開我的最愛
        private void toolStripButton10_Click_1(object sender, EventArgs e) // 按城市名稱搜尋
        {
            dbFunNow db = new dbFunNow();//代表與資料庫的連線

            var orders = from k in db.OrderDetails//使用 LINQ 查詢 OrderDetails 表中所有記錄的 RoomID 欄位
                         where !(k.CheckInDate >= dateTimePicker2.Value.Date || k.CheckOutDate <= dateTimePicker1.Value.Date) // 符合條件List<OrderDetails>
                         //使用 where 子句排除在指定日期範圍內已訂出的房間。 //訂單的開始日期大於或等於 dateTimePicker2 的值
                         select k.RoomID;                           //  或  //訂單的結束日期小於或等於 dateTimePicker1 的值。 //ex. List<RoomID> 123 234(在訂單內)
            //   o 可訂  旅館  房間  
            //   x (1)   3     123 (在訂單內)
            //   x (2)   4     234
            //   o (3)   4     235
            //   o (4)   5     236 
            //   o (4)   5     237

            // 篩選出已在指定日期範圍內訂出的房間                    
            var rooms = from r in db.Room   // 查詢所有房間  List<Room>
                        where !orders.ToList().Contains(r.RoomID) //k.RoomID = r.RoomID => List<Room> 房間 o (3)  4-235    o (4)  5-236   o (4) 5-237 
                        select r.HotelID;                         // List<HotelID>    旅館 o 4 o 5 o 5

            var hotels = from h in db.Hotel   // 空房的hotel
                         where rooms.ToList().Contains(h.HotelID)  // List<Hotel>
                         select
                         new
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice)

                             //旅館評價 = cr.Description,
                         };                                  // List<Hotel>

            // 按 CityID 排列
            hotels = hotels.OrderBy(h => h.城市);

            // 將查詢結果繫結到資料表
            dataGridView1.DataSource = hotels.ToList();

            resetGridStyle();// 重設資料表樣式

            //========================================          

            flowLayoutPanel1.Controls.Clear(); //首先，清除 flowLayoutPanel1 控制項中的所有控制項

            var sorthotelname = hotels.Select(p => p.旅館名稱); //從 sortedhotels 集合中選取所有旅館名稱                                                                     
            List<string> sorthotelnamelist = sorthotelname.ToList();//並轉換為 List<string> 集合 sorthotelname2

            //從 db.Hotel 資料表中選取所有空房的旅館並轉換為 List<Hotel> 集合
            var hotels2 = from h in db.Hotel   // 空房的hotel
                          where rooms.ToList().Contains(h.HotelID)  // List<Hotel> hotels2list
                          select new { HotelAll = h, h.HotelID, FirstRoomImage = h.HotelImages.Select(ri => ri.HotelImage).FirstOrDefault() };  //將hotels2查詢結果繫結到HotelBox

            //var hotels2list = hotels2.ToList();

            //var sorthtels3 = hotels2list   //根據旅館名稱對 hotels2list 集合進行排序
            //    .OrderBy(h => sorthotelnamelist.IndexOf(h.HotelName)).ToList();

            var hotellike = from hl in db.HotelLikes //從 db.HotelLikes 資料表中選取所有旅館喜好資料。
                            select hl;
                                                                                                                                  //設定照片條件
            foreach (var h in hotels2)
            {
                string HotelImageString = h.FirstRoomImage != null ? h.FirstRoomImage.ToString() : null; //設定照片參數

                HotelBox hb = new HotelBox();//建立一個 HotelBox 物件，用來顯示旅館資訊。

                //根據會員 ID 和旅館 ID 從 db.HotelLikes 資料表中選取旅館喜好資料
                var hls = hotellike.Where(p => p.HotelID == h.HotelID && p.MemberID == FrmLogin.auth.MemberID);

                if (hls.ToList().Count != 0) //如果旅館喜好裡有資料，則設定 HotelBox 物件的旅館喜好狀態
                {
                    hb.hotellike = (HotelLikes)hls.ToList().ElementAt(0); //返回 List<HotelLikes> 集合中的第一個元素
                                                                          //將第一個元素轉換為 HotelLikes 型別
                                                                          //rb.hotellike：HotelBox 物件的 HotelLikes 屬性，用於存儲旅館喜好資料。
                                                                          //hls：一個包含旅館喜好資料的 IEnumerable<HotelLikes> 集合。
                                                                          //ToList()：將 IEnumerable< HotelLikes > 集合轉換為 List<HotelLikes> 集合。
                                                                          //ElementAt(0)：返回 List<HotelLikes> 集合中的第一個元素。
                                                                          //(HotelLikes)：將第一個元素轉換為 HotelLikes 型別。
                }

                hb.HotelID = h.HotelID;

                hb.MemberID = FrmLogin.auth.MemberID;

                hb.Width = flowLayoutPanel1.Width;//設定 RoomBox 物件的寬度為 flowLayoutPanel1 的寬度。

                hb.hotelPicture = HotelImageString;
                hb.hotel = h.HotelAll;//設定 RoomBox 物件的房間資料為h。hotel為Hotel的變數
                                      // rb._hotels = hotels2;//顯示全部旅館
                hb.hotelboxStart = dateTimePicker1.Value;
                hb.hotelboxEnd = dateTimePicker2.Value;

                hb.showHotelEvent += this.showHotelMethod;
                flowLayoutPanel1.Controls.Add(hb);
                //將 RoomBox 物件新增到 flowLayoutPanel1 控制項中。
            }
            checkInDate = dateTimePicker1.Value.Date;
            checkOutDate = dateTimePicker2.Value.Date;
        }
        private void FrmPOS_Load(object sender, EventArgs e) //New時自動載入內容
        {
            queryAll(); //SHOW出房間資訊 包含自訂欄位           
            
            FrmLogin f = new FrmLogin();

            f.ShowDialog();

            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1); //設定進首頁時，退房時間的顯示

            toolStripLabel4.Text = "熱烈歡迎會員編號" + FrmLogin.auth.MemberID + "，大名為" + FrmLogin.auth.Name + "登入平台";


            //CITY ComboBox
            dbFunNow db = new dbFunNow();//建立一個新的 dbDemoEntities 物件，代表與資料庫的連線。
            var distinctCity = (from c in db.City
                                select c.CityName).Distinct();//使用 LINQ 查詢 tRoom 表中所有記錄的 fCity 欄位。
                                                              //套用 Distinct() 方法，確保沒有重複的城市名稱。
            toolStripComboBox2.Items.Clear();
            toolStripComboBox2.Items.Add("請選擇城市");//將預設選項 "All City" 新增到 comboBox1 中
            toolStripComboBox2.SelectedIndex = 0;//將起始項目設為 (SelectedIndex = 0)。         
            foreach (var c in distinctCity)// 迴圈遍歷 distinctCity 中的每個城市名稱
            {
                toolStripComboBox2.Items.Add(c);//將每個城市名稱作為項目新增到 comboBox1 中
            }
            //People ComboBox 
            var people = (from rt in db.RoomType
                          select rt.Capacity).Distinct();//使用 LINQ 查詢 tRoom 表中所有記錄的 fCity 欄位。
                                                         //套用 Distinct() 方法，確保沒有重複的城市名稱。
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.Add("請選擇人數");//將預設選項 "All City" 新增到 comboBox1 中
            toolStripComboBox1.SelectedIndex = 0;//將起始項目設為 (SelectedIndex = 0)。         
            foreach (var rt in people)// 迴圈遍歷 distinctCity 中的每個城市名稱
            {
                toolStripComboBox1.Items.Add(rt);//將每個城市名稱作為項目新增到 comboBox1 中
            }
            //HotelType ComboBox
            var hotelType = (from ht in db.HotelType
                             select ht.HotelTypeName).Distinct();//使用 LINQ 查詢 tRoom 表中所有記錄的 fCity 欄位。
                                                                 //套用 Distinct() 方法，確保沒有重複的城市名稱。
            comboBox1.Items.Clear();
            comboBox1.Items.Add("請選擇住宿類型");//將預設選項 "All City" 新增到 comboBox1 中
            comboBox1.SelectedIndex = 0;//將起始項目設為 (SelectedIndex = 0)。         
            foreach (var ht in hotelType)// 迴圈遍歷 distinctCity 中的每個城市名稱
            {
                comboBox1.Items.Add(ht);//將每個城市名稱作為項目新增到 comboBox1 中
            }
            //RoomType ComboBox
            var roomType = (from r in db.RoomType
                            select r.RoomTypeName).Distinct();//使用 LINQ 查詢 tRoom 表中所有記錄的 fCity 欄位。
                                                              //套用 Distinct() 方法，確保沒有重複的城市名稱。
            comboBox2.Items.Clear();
            comboBox2.Items.Add("請選擇房間類型");//將預設選項 "All City" 新增到 comboBox1 中
            comboBox2.SelectedIndex = 0;//將起始項目設為 (SelectedIndex = 0)。         
            foreach (var r in roomType)// 迴圈遍歷 distinctCity 中的每個城市名稱
            {
                comboBox2.Items.Add(r);//將每個城市名稱作為項目新增到 comboBox1 中
            }
            //Hotel_Equipment_Reference ComboBox
            var hotelequipmentname = (from hee in db.HotelEquipment
                                      select hee.HotelEquipmentName).Distinct();//使用 LINQ 查詢 tRoom 表中所有記錄的 fCity 欄位。
                                                                                //套用 Distinct() 方法，確保沒有重複的城市名稱。
            comboBox3.Items.Clear();
            comboBox3.Items.Add("請選擇旅館設施");//將預設選項 "All City" 新增到 comboBox1 中
            comboBox3.SelectedIndex = 0;//將起始項目設為 (SelectedIndex = 0)。         
            foreach (var hee in hotelequipmentname)// 迴圈遍歷 distinctCity 中的每個城市名稱
            {
                comboBox3.Items.Add(hee);//將每個城市名稱作為項目新增到 comboBox1 中
            }
            //Room_Equipment_Reference ComboBox
            var roomequipmentname = (from ree in db.RoomEquipment
                                     select ree.RoomEquipmentName).Distinct();//使用 LINQ 查詢 tRoom 表中所有記錄的 fCity 欄位。
                                                                              //套用 Distinct() 方法，確保沒有重複的城市名稱。
            comboBox4.Items.Clear();
            comboBox4.Items.Add("請選擇房間設施");//將預設選項 "All City" 新增到 comboBox1 中
            comboBox4.SelectedIndex = 0;//將起始項目設為 (SelectedIndex = 0)。         
            foreach (var ree in roomequipmentname)// 迴圈遍歷 distinctCity 中的每個城市名稱
            {
                comboBox4.Items.Add(ree);//將每個城市名稱作為項目新增到 comboBox1 中
            }
        }
        private void FrmPOS_Activated(object sender, EventArgs e)
        {
            //resetGridStyle();
        }
        private void queryAll()  //select Room資料表資料內容
        {
            dbFunNow db = new dbFunNow();
            var hotels = from h in db.Hotel//使用 LINQ 查詢 Hotel 表中所有記錄 自訂新欄位如NEW
                         join c in db.CommentRate on h.HotelID equals c.HotelID
                         select
                         new  //設定要顯示的欄位
                         {
                             城市 = h.City.CityName,
                             旅館名稱 = h.HotelName,
                             旅館地址 = h.HotelAddress,
                             旅館特色 = h.HotelDescription,
                             旅館電話 = h.HotelPhone,
                             旅館種類 = h.HotelType.HotelTypeName,
                             均價 = h.Room.Average(p => p.RoomPrice),
                             //評分 = c.Rating


                             //旅館評價 = cr.Description,
                         };
            dataGridView1.DataSource = hotels.ToList();
            resetGridStyle();
        }
        private void resetGridStyle()//重設資料表樣式
        {
            //dataGridView1.Columns[0].Width = 80;
            //dataGridView1.Columns[1].Width = 120;
            //dataGridView1.Columns[2].Width = 180;
            //dataGridView1.Columns[3].Width = 160;
            //dataGridView1.Columns[4].Width = 120;
            //dataGridView1.Columns[5].Width = 120;
            //dataGridView1.Columns[6].Width = 100;


            bool isColorChanged = false;  //設定行列的顏色
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.White;
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                r.DefaultCellStyle.Font = new Font("微軟正黑體", 14);
            }
        }
        private void editById(int id) //可以修改資料
        {
            //dbFunNowEntities db = new dbFunNowEntities();
            //Room room = db.Room.FirstOrDefault(x => x.RoomID == id);
            //if (room == null)
            //    return;

            //FrmRoom f = new FrmRoom();
            //f.room = room;
            //f.ShowDialog();

            //if (f.isOk != DialogResult.OK)
            //    return;
            ////room.HotelID = f.room.HotelID;
            //room.RoomName = f.room.RoomName;
            //room.RoomPrice = f.room.RoomPrice;
            //room.Description = f.room.Description;
            //room.RoomTypeID = f.room.RoomTypeID;
            //db.SaveChanges();
            //queryAll();
        }
        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

            //dbFunNowEntities db = new dbFunNowEntities();
            //Room room = db.Room.FirstOrDefault(x => x.RoomID == id);
            //if (room == null)
            //    return;

            //// 直接建立新的 tOrder 物件

            //OrderDetails order = new OrderDetails();
            //order.MemberID = order.MemberID;
            //order.RoomID = room.RoomID;
            //order.CheckInDate = dateTimePicker1.Value;
            //order.CheckOutDate = dateTimePicker2.Value;

            //FrmOrderDetails f = new FrmOrderDetails();// 建立 FrmOrder 物件
            //f.order = order;// 將 tOrder 物件傳遞給 FrmOrder 物件
            //f.ShowDialog();// 顯示 FrmOrder 表單

            //if (f.isOk != DialogResult.OK) //不是點選確認，就離開
            //    return;

            //db.OrderDetails.Add(f.order);// 將修改後的 tOrder 物件新增到資料庫
            //db.SaveChanges();// 儲存變更
            //queryAll();// 重新查詢所有資料
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) //設計HotelBox滾輪
        {  //可以用垂直瀏覽資料

            // 設定 FlowLayoutPanel 的 FlowDirection 屬性為 TopDown。
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

            // 設定 FlowLayoutPanel 的 AutoScroll 屬性為 True。
            flowLayoutPanel1.AutoScroll = true;

            // 設定 FlowLayoutPanel 的 WrapContents 屬性為 False。
            flowLayoutPanel1.WrapContents = false;
        }
    }
}
