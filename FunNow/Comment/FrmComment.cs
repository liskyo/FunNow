using FunNow.Comment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FunNow.Comment
{
    public partial class FrmComment : Form
    {
        private CComment _comment;
        private readonly dbFunNow db = new dbFunNow();
        private readonly List<CComment> comments = new List<CComment>();
        private List<CComment> originalComments = new List<CComment>(); // 原始評論列表
        private bool isFilterApplied = false;// 篩選按鈕是否已點擊
        private string currentFilter = string.Empty;
        public Hotel selectedHotel;

        public FrmComment(Hotel hotel)
        {
            InitializeComponent();
            selectedHotel = hotel;
            InitializeForm();
        }

        private void InitializeForm()
        {
            QuerySelectedHotelInfo();
        }

        private void QuerySelectedHotelInfo() // 查詢選定飯店的評論信息並初始化原始評論列表
        {
            if (selectedHotel != null)
            {
                var commentsQuery = from c in db.CommentRate
                                    join h in db.Hotel on c.HotelID equals h.HotelID
                                    join m in db.Member on c.MemberID equals m.MemberID
                                    join od in db.OrderDetails on c.MemberID equals od.MemberID
                                    join r in db.Room on od.RoomID equals r.RoomID
                                    where h.HotelID == selectedHotel.HotelID && r.HotelID == selectedHotel.HotelID

                                    select new CComment
                                    {
                                        MemberName = m.Name,
                                        HotelName = h.HotelName,
                                        RoomType = r.RoomName,
                                        CheckInDate = od.CheckInDate,
                                        Rating = (decimal)c.Rating,
                                        CommentTime = c.CreatedAt,
                                        CommentTxt = c.Description
                                    };

                var commentsData = commentsQuery.ToList();
                dataGridView1.DataSource = commentsData;

                originalComments.AddRange(commentsData.Select(c => new CComment
                {
                    MemberName = c.MemberName,
                    HotelName = c.HotelName,
                    RoomType = c.RoomType,
                    CheckInDate = c.CheckInDate,
                    Rating = c.Rating,
                    CommentTime = c.CommentTime,
                    CommentTxt = c.CommentTxt
                }));

                comments.AddRange(originalComments);
                SendQueryResults();
               
            }
        }

        private void SendQueryResults() // 顯示評論和計算評分
        {
            DisplayComments(SortOption.Newest);
            CalculateRating();
        }

        private void DisplayComments(SortOption sortOption)// 顯示排序後的評論
        {
            var sortedComments = SortComments(comments, sortOption);
            flowLayoutPanel1.Controls.Clear();

            foreach (var comment in sortedComments)
            {
                ShowComments(comment);
            }

            flowLayoutPanel1.Refresh();
        }

        private IEnumerable<CComment> SortComments(List<CComment> comments, SortOption sortOption)
        {   // 根據指定的排序選項對評論進行排序
            switch (sortOption)
            {
                case SortOption.Newest:
                    return comments.OrderByDescending(c => c.CommentTime);

                case SortOption.Oldest:
                    return comments.OrderBy(c => c.CommentTime);

                case SortOption.HighestScore:
                    return comments.OrderByDescending(c => c.Rating);

                case SortOption.LowestScore:
                    return comments.OrderBy(c => c.Rating);

                default:
                    throw new ArgumentException("Invalid sort option");
            }
        }

        private void ShowComments(CComment comment)// 在UI上顯示評論
        {
            CommentBox cb = new CommentBox();
            cb.MemberName = comment.MemberName;
            cb.CommentTime = comment.CommentTime;
            cb.CommentTxt = comment.CommentTxt;
            cb.Rating = comment.Rating;
            cb.CheckInDate = comment.CheckInDate;
            cb.RoomType = comment.RoomType;

            flowLayoutPanel1.Controls.Add(cb);
        }

        private void CalculateRating()  // 計算平均評分+更新UI
        {
            string hotelName = comments.FirstOrDefault()?.HotelName;
            decimal avgRating = comments.Any() ? comments.Average(c => c.Rating) : 0; // Calculate average rating
            var comment = new CComment
            {
                AvgRating = avgRating,
                HotelName = hotelName
            };
            Comment = comment; // 更新 Comment 屬性>更新平均評分
        }

        private void FilterComments(string selectedCategory) // 根據類別篩選評論
        {
            if (string.IsNullOrEmpty(selectedCategory))
            {   
                // 如果選定的類別為空，則恢復原始評論列表
                comments.Clear();
                comments.AddRange(originalComments);
                isFilterApplied = false;// 篩選未點擊
            }
            else //根據選定的類別篩選
            {
                var filteredCommentsQuery = from c in db.CommentRate
                                            join h in db.Hotel on c.HotelID equals h.HotelID
                                            join m in db.Member on c.MemberID equals m.MemberID
                                            join od in db.OrderDetails on c.MemberID equals od.MemberID
                                            join r in db.Room on c.HotelID equals r.HotelID
                                            where c.Description.Contains(selectedCategory)
                                            select new CComment
                                            {
                                                MemberName = m.Name,
                                                HotelName = h.HotelName,
                                                RoomType = r.RoomName,
                                                CheckInDate = od.CheckInDate,
                                                Rating = (decimal)c.Rating,
                                                CommentTime = c.CreatedAt,
                                                CommentTxt = c.Description
                                            };

                comments.Clear();
                comments.AddRange(filteredCommentsQuery);
                isFilterApplied = true; // 篩選已點擊
            }
            DisplayComments(SortOption.Newest);
        }

        private void btnBreakfast_Click(object sender, EventArgs e)
        {
            if (isFilterApplied && currentFilter == "早餐")
            {
                // 如果已點選該篩選，則恢復到原始評論
                FilterComments(string.Empty);
                isFilterApplied = false;
                currentFilter = string.Empty;
            }
            else
            {
                // 如果尚未點選該篩選，則應用該篩選
                FilterComments("早餐");
                isFilterApplied = true;
                currentFilter = "早餐";
            }

        }

        private void bntRoom_Click(object sender, EventArgs e)
        {
            // 檢查是否已點選篩選
            if (isFilterApplied && currentFilter == "房間")
            {
                // 如果已點選該篩選，則恢復到原始評論
                FilterComments(string.Empty);
                isFilterApplied = false;
                currentFilter = string.Empty;
            }
            else
            {
                // 如果尚未點選該篩選，則應用該篩選
                FilterComments("房間");
                isFilterApplied = true;
                currentFilter = "房間";
            }

        }

        private void bntLocation_Click(object sender, EventArgs e)
        {
            // 檢查是否已點選篩選
            if (isFilterApplied && currentFilter == "地點")
            {
                // 如果已點選該篩選，則恢復到原始評論
                FilterComments(string.Empty);
                isFilterApplied = false;
                currentFilter = string.Empty;
            }
            else
            {
                // 如果尚未點選過該篩選，則應用該篩選
                FilterComments("地點");
                isFilterApplied = true;
                currentFilter = "地點";
            }
        }

        private void cbGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortOption selectedSortOption = (SortOption)cbGroupBy.SelectedIndex;
            DisplayComments(selectedSortOption);
        }

        public CComment Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                if (_comment != null)
                {
                    lbGuestComment.Text = _comment.HotelName + "  住客評論";
                    lbAvgRating.Text = "平均評分：" + _comment.AvgRating.ToString("0.0");
                }
            }
        }
    }
}