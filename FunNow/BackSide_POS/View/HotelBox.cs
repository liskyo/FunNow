using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_POS.View
{
   
    //DELEGATE
    public delegate void D(HotelBox p, DateTime start, DateTime end);

    public partial class HotelBox : UserControl
    {
        //EVENT
        public event D showHotelEvent;
        public DateTime hotelboxStart { get; set; }
        public DateTime hotelboxEnd { get; set; }



        Image[] imags;
        //bool hidden = false;

        private HotelLikes _hotellike;
        public HotelLikes hotellike
        {
            get
            {
                if (_hotellike == null)
                {
                    _hotellike = new HotelLikes();
                }
                return _hotellike;
            }
            set
            {
                _hotellike = value;
                if (_hotellike.LikeStatus == true)
                {
                    pictureBox2.Image = imags[1];

                }
                else pictureBox2.Image = imags[0];
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
  
            showHotelEvent?.Invoke(this, hotelboxStart, hotelboxEnd);
        }

   

        private Hotel _hotel;
        public IQueryable<Hotel> _hotels;


        public Hotel hotel//用來存取一個 tRoom 類型的物件 (代表房間資訊)。
        {
            get { return _hotel; }//回傳私有變數 _room 的值
            set
            {
                _hotel = value;//更新 _room 的值，並根據新值更新其他 UI 元素
                lblHotelName.Text = _hotel.HotelName;//lblName顯示訂單房名
                lblHotelAddress.Text = "地址:"+ _hotel.HotelAddress;
                lblHotelPhone.Text = "電話:" + _hotel.HotelPhone;
                lblAvgPrice.Text = "" + _hotel.Room.Average(p => p.RoomPrice);


                //if (!string.IsNullOrEmpty(_room.fImagepath))//不是空字串則載入圖片
                //{
                //    string path = Application.StartupPath + "\\roomImages";
                //    pictureBox1.Image = new Bitmap(path + "\\" + _room.fImagepath);
                //}
            }
        }


        public int HotelID { get; internal set; }
        public int MemberID { get; internal set; }



        public HotelBox()  //為結構子 New時會產生
        {
            InitializeComponent();
            imags = new Image[2];
            imags[0] = Image.FromFile("c:\\FunNowTopics\\FunNow\\image\\C_heart.png");
            imags[1] = Image.FromFile("c:\\FunNowTopics\\FunNow\\image\\O_heart.png");
            pictureBox2.Image = imags[0];

        }
   

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            if(pictureBox2.Image == imags[0])  //狀態為灰色
            {
                dbFunNow db = new dbFunNow();//代表與資料庫的連線

                var hotellikedata = from hl in db.HotelLikes
                                select hl;
                var hls = hotellikedata.Where(p => p.HotelID == HotelID && p.MemberID == MemberID);

                if (hls.ToList().Count == 1)
                {
                        pictureBox2.Image = imags[1];
                        hls.ToList().ElementAt(0).LikeStatus = true;
                }
                else 
                {
                    pictureBox2.Image = imags[1];
                    hotellike.HotelID = HotelID;
                    hotellike.MemberID = MemberID;
                    hotellike.LikeStatus = true;
                    db.HotelLikes.Add(hotellike);
                }
                db.SaveChanges();// 儲存變更
            }
            else  //狀態為紅色
            {
                dbFunNow db = new dbFunNow();//代表與資料庫的連線
                var hotellikedata = from hl in db.HotelLikes
                                    select hl;
                var hls = hotellikedata.Where(p => p.HotelID == HotelID && p.MemberID == MemberID);
                if (hls.ToList().Count != 0)
                {
                    pictureBox2.Image = imags[0];
                    hls.ToList().ElementAt(0).LikeStatus = false;
                }

                db.SaveChanges();// 儲存變更
            }


        }
    }
}
