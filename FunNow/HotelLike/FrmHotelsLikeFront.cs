using Fun;
using FunNow;
using FunNow.BackSide_Hotel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFunNowMember.View
{
    public partial class FrmHotelsLikeFront : Form
    {

        public FrmHotelsLikeFront()                  // memberID => 目前登入的那個會員
        {
            InitializeComponent();
            //this.MemberID = memberID;                          // 在 FrmHotelsLikeFront表單收藏喜愛飯店的會員ID = 目前登入的那個會員


        }


        private void FrmHotelsLikeFront_Load(object sender, EventArgs e)
        {
            dbFunNow db = new dbFunNow();
            var hotellikes = from hl in db.HotelLikes             // hl 代表db.HotelLikes 資料表的每一列資料  
                             where hl.MemberID == FrmLogin.auth.MemberID && hl.LikeStatus == true  //怎麼抓MemberID  
                             select hl;


            flowLayoutPanel1.Controls.Clear();

            foreach (var hl in hotellikes)                      //根據 this.MemberID  查看這個 this.MemberID 有幾個hotellikes => select hl => 假設 hl有三個 (this.MemberID 按了三個hotellikes) => 跑三次foreach  !!!!!!!!!!!!!
            {
                HotelLikeBox hlb = new HotelLikeBox();         //創建一個新的 HotelLikeBox   

                
                //開始把資料表的資料放進 HotelLikeBox裡面  等號右邊是資料表的每一列每一欄資料 
                // 假设可以直接通过hl对象获取Hotel, City和HotelImages的信息
                hlb.hotelName = hl.Hotel.HotelName;                                // 这里需要确保等號兩邊的属性类型一致
                hlb.cityName = hl.Hotel.City.CityName;                             // 同上
                                                                          //hlb.hotelimage = hl.Hotel.HotelImages.FirstOrDefault().HotelImage;
                var hotelImageEntity = hl.Hotel.HotelImages.FirstOrDefault();
                if (hotelImageEntity != null)
                {
                    hlb.hotelimage = hotelImageEntity.HotelImage;
                }
                else
                {
                    hlb.hotelimage = null;
                }

                hlb.HotelId = hl.Hotel.HotelID;                                    //todo.....

                hlb.GoToHotelRequested += GoToHotelHandler;                                 //HotelLikeBox按下立即查看按鈕 => 跳到相對應的飯店頁面
                hlb.RemoveFromFavoritesRequested += HandleRemoveFromFavorites;
             
                flowLayoutPanel1.Controls.Add(hlb);

                //而等號左邊就是我在hotellikebox定義的屬性  存到等號左邊 等號右邊是來自資料庫資料表中的data
                //hlb.hotelName、cityName、hotelimage => 我定義class HotelLikeBox 的屬性   (就像class Person {姓名、身高、體重 } => 姓名、身高、體重 這些屬性都是我定義的 )

            }
        }

        private void HandleRemoveFromFavorites(int hotelId)  //當 HotelLikeBox中的“移除”按钮被點擊时，會觸發 RemoveFromFavorites 事件，然後調用 HandleRemoveFromFavorites 方法執行相對應的從我的收藏刪除該飯店。
        {
            // 查找与 hotelId 关联的 HotelLikeBox
            HotelLikeBox boxToRemove = null;
            foreach (HotelLikeBox box in flowLayoutPanel1.Controls)
            {
                if (box.HotelId == hotelId)
                {
                    boxToRemove = box;
                    break;
                }
            }

            // 如果找到，从 flowLayoutPanel1 移除
            if (boxToRemove != null)
            {
                flowLayoutPanel1.Controls.Remove(boxToRemove);
                boxToRemove.Dispose();
            }

            using (var db = new dbFunNow())
            {
                var favorite = db.HotelLikes.FirstOrDefault(hl => hl.HotelID == hotelId && hl.MemberID == FrmLogin.auth.MemberID);
                if (favorite != null)
                {
                    //db.HotelLikes.Remove(favorite);
                    //db.SaveChanges();
                    favorite.LikeStatus = false;
                    db.SaveChanges();


                }
            }
        }

        private void GoToHotelHandler(int hotelId)   //當 HotelLikeBox中的“查看住宿”按钮被點擊时，會觸發 GoToHotel事件，然後調用 GoToHotelHandler方法執行相對應的跳轉邏輯。
        {        
           FrmBookingViewOnly f = new FrmBookingViewOnly(hotelId);      
            f.Show();         
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            new FrmCart().Show();
        }
    }
}
