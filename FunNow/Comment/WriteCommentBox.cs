using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.Comment
{
    public partial class WriteCommentBox : UserControl
    {
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Rating { get; set; }
        public string CommentText { get; set; }
        public WriteCommentBox()
        {
            InitializeComponent();
        }
        public void DisplayOrderInfo(dynamic hotelInfo)
        {
            if (hotelInfo != null)
            {
                // 顯示訂單信息到相應控制項
                HotelName = hotelInfo.HotelName;
                RoomType = hotelInfo.RoomType;
                CheckInDate = hotelInfo.CheckInDate;
                CheckOutDate = hotelInfo.CheckOutDate;

                // 更新UI控制項的文本
                lbHotelName.Text = HotelName;
                lbRoomName.Text = RoomType;

                string formattedCID = CheckInDate.ToString("yyyy/MM/dd");
                string formattedCOD = CheckOutDate.ToString("yyyy/MM/dd");

                TimeSpan stayDuration = CheckOutDate - CheckInDate;
                int nightsStayed = stayDuration.Days;
                lbStayedDays.Text = $"入住{nightsStayed}晚  {formattedCID} - {formattedCOD}";
            }
        }

        public CommentRate CreateCommentObject()
        {
            // 創建並設置評論屬性值
            CommentRate comment = new CommentRate();
            comment.Description = CommentText;
            comment.CreatedAt = DateTime.Now;
            comment.Rating = Rating;

            // 確保評分有效
            if (Rating > 0)
            {
                return comment;
            }
            else
            {
                MessageBox.Show("請先選擇評分！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return null;
        }
    }
}
