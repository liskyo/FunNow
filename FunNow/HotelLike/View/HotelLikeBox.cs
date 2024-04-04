using FunNow;
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

namespace prjFunNowMember.View
{
    public delegate void DGoToHotel(int hotelId);
    public delegate void DRemoveFromFavorites(int hotelId);


    public partial class HotelLikeBox : UserControl
    {
        public int HotelId { get; set; }                    //todo...... // 假設添加了這個屬性

        public event DGoToHotel GoToHotel;
        public event DRemoveFromFavorites RemoveFromFavorites;

        private string _hotelImage;                    // HotelLikeBox上的pictureBox1位置放置 HotelImages資料表的 HotelImage欄位   
        public string hotelimage
        {
            //get { return _hotelImage; }
        


            set
            {
                if (!string.IsNullOrEmpty(value) )
                {
                    value = Path.GetFileName(value);
                    string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                    string path = Path.Combine(projectRoot, "..\\..\\..\\image\\", value);


                    pictureBox1.Image = Image.FromFile(path);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }











        }

        private string _hotelName;                         // HotelLikeBox上的lblName位置放 hotel資料表的 hotelName欄位
        public string hotelName
        {
            get { return _hotelName; }
            set
            {
                _hotelName = value;
                lblName.Text = _hotelName;                 //label(改名為lblName) 放 hotelName
            }
        }


        private string _cityName;                           // HotelLikeBox上的 lbCityName 位置放置 City 資料表 的 CityName欄位
        public string cityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                lbCityName.Text = _cityName;                //label(改名為lbCityName) 放 cityName

            }
        }

        



        public HotelLikeBox()
        {
            InitializeComponent();
        }

        //當用戶點擊HotelLikeBox控件中的“查看住宿”按鈕時，就會觸發GoToHotel事件，進而執行GoToHotelHandler方法，根據HotelId跳轉到相應酒店的詳細頁面。

        private void button1_Click(object sender, EventArgs e)       // HotelLikeBox上的"查看住宿"按鈕 => 按下去要跳回 HotelID=? 的飯店頁面  => // todo.....需要哪些資料
        {
                                                                  // 假设 hotelId 存储在 HotelLikeBox 控件中的某个属性或字段
            int hotelId = this.HotelId;                           // 或者其他存储 HotelId 的地方

            // 检查是否有订阅了 GoToHotel 事件的处理方法            
            if (this.GoToHotel != null)
            {
                // 触发事件
                this.GoToHotel(hotelId);
            }
        }

        private void RemoveHotelFromFavorites(object sender, EventArgs e)  // HotelLikeBox上的"刪除"按鈕 => 按下去從"會員的我的收藏" 將該飯店移除 => 刪對飯店
        {
            // 触发事件，传递 HotelId
            RemoveFromFavorites?.Invoke(this.HotelId);
        }
    }
}
