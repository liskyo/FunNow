using System;
using System.Windows.Forms;

namespace FunNow.Comment.Model
{
    public partial class CommentBox : UserControl
    {
        private CComment _comment;

        public CommentBox()
        {
            InitializeComponent();
            _comment = new CComment();// 初始化評論物件
        }

        public string MemberName
        {   
            // 使用 null 條件運算子檢查 MemberName 是否為 null
            get => _comment?.MemberName ?? string.Empty;
            set
            {
                _comment.MemberName = value;// 設置MemberName 屬性
                EncryptMemberName(); //加密會員名稱
            }
        }

        public DateTime CheckInDate
        {
            get => _comment.CheckInDate;
            set
            {
                _comment.CheckInDate = value;
                lbCheckInDate.Text = "入住時間：" + _comment.CheckInDate.ToString("yyyy-MM-dd");
            }
        }

        public DateTime? CommentTime
        {
            get => _comment.CommentTime;
            set
            {
                _comment.CommentTime = value;
                if (_comment.CommentTime.HasValue)
                {
                    lbCommentTime.Text = "評論時間：" + _comment.CommentTime.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    lbCommentTime.Text = ""; // 處理 CommentTime 為 null 的情況
                }
            }
        }

        public string CommentTxt
        {
            get => _comment.CommentTxt;
            set
            {
                _comment.CommentTxt = value ?? string.Empty;
                lbCommentTxt.Text = _comment.CommentTxt.ToString();
            }
            // 設置評論物件的 CommentTxt 屬性，如果值為 null，則設置為空字串
        }

        public decimal Rating
        {
            get => _comment.Rating;
            set
            {
                _comment.Rating = value;
                lbRating.Text = _comment.Rating.ToString();
            }
        }

        public string RoomType
        {
            get => _comment.RoomType;
            set
            {
                _comment.RoomType = value ?? string.Empty;
                lbRoomName.Text = _comment.RoomType.ToString();
            }
            // 設置評論物件的 RoomType 屬性，如果值為 null，則設置為空字串
        }

        private void EncryptMemberName()
        {
            if (!string.IsNullOrEmpty(_comment.MemberName) && _comment.MemberName.Length >= 2)
            {
                char[] charArray = _comment.MemberName.ToCharArray();
                charArray[1] = '*'; // 將第二個字元替換為 *
                _comment.MemberName = new string(charArray); // 更新評論物件的 MemberName 屬性
                lbName.Text = _comment.MemberName;// 更新label會員名稱文字
            }
        }
    }
}