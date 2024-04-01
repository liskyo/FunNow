using FunNow;
using FunNow.Comment.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FunNow.Comment
{
    public partial class FrmWriteComment : Form
    {
        private CComment _comment;
        private readonly dbFunNow db = new dbFunNow();
        private int memberId;
        public Hotel SelectedHotel { get; set; }
        private List<WriteCommentBox> commentBoxes = new List<WriteCommentBox>();

        public FrmWriteComment()
        {   
            InitializeComponent();
            SortRating(); // 加載評分下拉列表
            LoadOrderInfo(memberId); // 加載訂單信息
        }

        private void SortRating()
        {
            List<double> scores = new List<double>();

            // 將平分添加到列表中
            for (double score = 1.0; score <= 5.0; score += 0.1)
            {  scores.Add(score);      }

            // 對評分列表進行降序排序
            scores.Sort((a, b) => b.CompareTo(a));

            // 將排序後的評分添加到 ComboBox 中
            foreach (var score in scores)
            { comboBoxScore.Items.Add(score.ToString("0.0")); }
        }
        private void LoadOrderInfo(int memberId)
        {
            // 查詢並加載訂單信息
            var hotelInfos = QueryOrderInfo(memberId);

            if (hotelInfos != null)
            {
                // 顯示訂單信息
                foreach (var hotelInfo in hotelInfos)
                {
                    var commentBox = new WriteCommentBox();
                    commentBox.DisplayOrderInfo(hotelInfo);
                    flowLayoutPanel1.Controls.Add(commentBox);
                    commentBoxes.Add(commentBox);
                }
            }
        }

        private dynamic QueryOrderInfo(int memberId)
        {
            // 查詢訂單信息
            var query = from c in db.CommentRate
                        join h in db.Hotel on c.HotelID equals h.HotelID
                        join m in db.Member on c.MemberID equals m.MemberID
                        join od in db.OrderDetails on c.MemberID equals od.MemberID
                        join r in db.Room on c.HotelID equals r.HotelID
                        where m.MemberID == memberId
                        select new
                        {
                            HotelName = h.HotelName,
                            RoomType = r.RoomName,
                            CheckInDate = od.CheckInDate,
                            CheckOutDate = od.CheckOutDate
                        };

            return query.FirstOrDefault();
        }

        private void DisplayOrderInfo(dynamic hotelInfo) //dynamic在運行時根據上下文動態解析其類型
        {
            if (hotelInfo != null)
            {
                // 顯示訂單信息到相應lable
                lbHotelName.Text = hotelInfo.HotelName;
                lbRoomName.Text = hotelInfo.RoomType;

                //計算住宿幾晚
                string formattedCID = hotelInfo.CheckInDate.ToString("yyyy/MM/dd");
                string formattedCOD = hotelInfo.CheckOutDate.ToString("yyyy/MM/dd");

                TimeSpan stayDuration = hotelInfo.CheckOutDate - hotelInfo.CheckInDate;
                int nightsStayed = stayDuration.Days;
                lbStayedDays.Text = $"入住{nightsStayed}晚  {formattedCID} - {formattedCOD}";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 確認按鈕點擊事件處理
            DialogResult result = MessageBox.Show("是否送出評論", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveCommentToDatabase();
            }
            else
            {
                MessageBox.Show("已取消送出評論。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveCommentToDatabase()
        {
            // 將評論保存到數據庫
            foreach (var commentBox in commentBoxes)
            {
                // 獲取評論文本
                string commentText = commentBox.Controls.OfType<TextBox>().FirstOrDefault()?.Text;
                commentBox.CommentText = commentText;

                var comment = commentBox.CreateCommentObject();
                if (comment != null)
                {
                    db.CommentRate.Add(comment);
                }
            }

            db.SaveChanges();
            MessageBox.Show("評論已成功送出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmWriteComment_Load(object sender, EventArgs e)
        {
            LoadOrderInfo(memberId);
        }
    }
      
 }



