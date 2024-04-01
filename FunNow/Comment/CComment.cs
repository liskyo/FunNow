using System;
using System.Data.Entity;

namespace FunNow.Comment.Model
{
    public class CComment
    {
        public string MemberName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CommentTime { get; set; }
        public string CommentTxt { get; set; }
        public decimal Rating { get; set; }
        public string RoomType { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal AvgRating { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckOutDate { get; set; }

        // 建立方法，使其可重複使用
        public DateTime? GetCommentTime()
        {
            return CommentTime;
        }

        public DateTime GetDate()
        {
            return CreateAt;
        }
    }

    public enum SortOption
    {
        Newest,
        Oldest,
        HighestScore,
        LowestScore
    }
}