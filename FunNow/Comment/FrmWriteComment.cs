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
        public Hotel SelectedHotel { get; set; }
        //private List<WriteCommentBox> commentBoxes = new List<WriteCommentBox>();
        private int hotelID;
        private string hotelName;
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private string roomType;
        public FrmWriteComment(int hotelID, string hotelName, DateTime checkInDate, DateTime checkOutDate, string roomType)
        {
            this.hotelID = hotelID;
            this.hotelName = hotelName;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.roomType = roomType;

            InitializeComponent();
            SortRating(); // 加載評分下拉列表
            // 加載訂單資訊


            // 在初始化時使用訂單相關資訊來顯示訂單內容
           
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
       
        private IEnumerable<dynamic> QueryOrderInfo()
        {
            // 查詢訂單資訊
            var query = from od in db.OrderDetails
                        join m in db.Member on od.MemberID equals m.MemberID
                        join o in db.Order on od.OrderID equals o.OrderID
                        join r in db.Room on od.RoomID equals r.RoomID
                        join h in db.Hotel on r.HotelID equals h.HotelID
                        where m.MemberID == FrmLogin.auth.MemberID 
                        orderby od.CreatedAt 
                        select new
                        {
                            HotelID = h.HotelID,
                            HotelName = h.HotelName,
                            CheckInDate = od.CheckInDate,
                            CheckOutDate = od.CheckOutDate,
                            RoomType = r.RoomName
                        };

            foreach (var hotelInfo in query)
            {
                if (hotelInfo.HotelName == this.hotelName && hotelInfo.CheckInDate == this.checkInDate) // 找到要評論的HOTEL及訂房時間
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

                    SelectedHotel = new Hotel { HotelID = hotelInfo.HotelID, HotelName = hotelInfo.HotelName };
                    break;
                }
            }
           
            return query.ToList();

        }

        

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 確認按鈕點擊事件處理
            DialogResult result = MessageBox.Show("是否送出評論", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveCommentToDatabase();
                this.Close();
            }
            else
            {
                MessageBox.Show("已取消送出評論。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void SaveCommentToDatabase()
        {
            //var query = from od in db.OrderDetails
            //             join m in db.Member on od.MemberID equals m.MemberID
            //             join o in db.Order on od.OrderID equals o.OrderID 
            //             join r in db.Room on od.RoomID equals r.RoomID
            //             join h in db.Hotel on r.HotelID equals h.HotelID
            //             where m.MemberID == FrmLogin.auth.MemberID // 將 loggedInMemberID 替換為登入會員的 ID
            //             orderby od.CreatedAt descending
            //             select new
            //             {
            //                 HotelID = h.HotelID,
            //                 HotelName = h.HotelName,
            //                 CheckInDate = od.CheckInDate,
            //                 CheckOutDate = od.CheckOutDate,
            //                 RoomType = r.RoomName
            //             };

            //dataGridView1.DataSource = query.ToList();
            //var result = query.FirstOrDefault();

            //if (result != null)
            //{
                // 获取酒店ID
              //  int hotelId = this.hotelID;

                // 使用获取到的酒店ID进行其他操作
            //}
            
            int memberId = FrmLogin.auth.MemberID;
            string commentText = tbInput.Text;
            double rating = Convert.ToDouble(comboBoxScore.SelectedItem);

            // 創建並設置評論屬性值
            CommentRate comment = new CommentRate
            {
                Description = commentText,
                CreatedAt = DateTime.Now,
                Rating = (int)rating,
                MemberID = FrmLogin.auth.MemberID,
                HotelID = this.hotelID
            };

            // 確保評分有效
            if (comment.Rating >= 0)
            {
                db.CommentRate.Add(comment);
                db.SaveChanges();
                MessageBox.Show("評論已成功送出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 清除評論文字框和評分
                tbInput.Clear();
                comboBoxScore.Items.Clear();
            }
            else
            {
                MessageBox.Show("請先選擇評分！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //// 將評論保存到數據庫
            //foreach (var commentBox in commentBoxes)
            //{
            //    // 獲取評論文本
            //    string commentText = commentBox.Controls.OfType<TextBox>().FirstOrDefault()?.Text;
            //    commentBox.CommentText = commentText;

            //    // 獲取 hotelId
            //    //int hotelId = 

            //    var comment = commentBox.CreateCommentObject(hotelId);
            //    if (comment != null)
            //    {
            //        db.CommentRate.Add(comment);
            //    }
            //}

            //db.SaveChanges();
            //MessageBox.Show("評論已成功送出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmWriteComment_Load(object sender, EventArgs e)
        {
            QueryOrderInfo();
        }
    }
      
 }



