using Fun;
using FunNow.BackSide_POS;
using FunNow.Comment;
using FunNow.FrontSide_Cart;
using prjFunNowMember.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow
{
    public partial class FrmBookingViewOnly : Form
    {
        public FrmBookingViewOnly(int hotelId)
        {
            InitializeComponent();

            dbFunNow db = new dbFunNow();
            this.selectedHotel = db.Hotel.FirstOrDefault(h => h.HotelID == hotelId);

        }


        //痊癒變數+屬性-------------------------------------------------------------------------------
        public Hotel selectedHotel { get; set; }
        
        public FrmBookingViewOnly()
        {
            InitializeComponent();     
        }
        //Load--------------------------------------------------------------------------------------
        private void FrmBookingViewOnly_Load(object sender, EventArgs e)
        {
            if (selectedHotel == null)
                return;

            flowLayoutPanel1.Controls.Clear();
            dbFunNow db = new dbFunNow();
            var rooms = from r in db.Room
                        where r.HotelID == selectedHotel.HotelID
                        select r;

            var comments = (from c in db.CommentRate
                            where c.HotelID == selectedHotel.HotelID
                            orderby c.CreatedAt descending // 按時間遞減排序
                            select c).Take(2); // 取最新的兩筆評論

            int i = 1; // 用於追蹤是 smallCommentBox1 還是 smallCommentBox2
            foreach (var c in comments)
            {
                if (i == 1)
                {
                    lbSmallComment1.Text = c.Description;
                    i++;
                }
                else if (i == 2)
                {
                    lbSmallComment2.Text = c.Description; // 假設 smallCommentBox2 具有 Text 屬性，用來顯示評論文字

                    break; // 只需顯示最新的兩筆評論
                }
            }

            foreach (var r in rooms)
            {
                roomBoxViewOnly rb = new roomBoxViewOnly();
                rb.room = r;
                rb.Width = flowLayoutPanel1.Width;
                flowLayoutPanel1.Controls.Add(rb);
            }

            lblName.Text = selectedHotel.HotelName;
            lblMemo.Text = selectedHotel.HotelDescription;

            //設備--------------------------------------------------------------------------
            var equipments = from eq in db.Hotel_Equipment_Reference
                             where eq.HotelID == selectedHotel.HotelID
                             select eq.HotelEquipment;

            foreach (var equip in equipments)
            {
                CheckBox c = new CheckBox();
                c.Text = equip.HotelEquipmentName.ToString();
                c.Checked = true;
                c.Enabled = false;
                c.Width = flowLayoutPanel2.Width;
                c.Height = 25;
                c.Font = new Font("微軟正黑體", 12, FontStyle.Regular);

                flowLayoutPanel3.Controls.Add(c);
            }
            //圖片--------------------------------------------------------------------------
            var pictures = from p in db.HotelImages
                           where p.HotelID == selectedHotel.HotelID
                           select p.HotelImage;

            foreach (var pic in pictures)
            {
                if (!string.IsNullOrEmpty(pic))//1)先檢查pic是否為空值
                                               //即, HotelImage表的HotelImage欄位是否有存放路徑
                {
                    if (File.Exists(pic))//2)再檢查是否可透過pic存放的路徑, 在電腦的檔案總管找到相對應的檔案
                    {
                        //這個使用 FileStream 的 using 語句，會打開圖片檔案並建立一個 FileStream 實例來讀取圖片的內容。
                        //參數:
                        //pic 是圖片的路徑
                        //FileMode.Open 表示以讀取模式打開檔案
                        //FileAccess.Read 表示訪問檔案的權限
                        using (FileStream fs = new FileStream(pic, FileMode.Open, FileAccess.Read))
                        {
                            //Image img = Image.FromStream(fs);：這一行從 FileStream 中讀取圖片並創建一個 Image 實例。
                            //透過 Image.FromStream 方法，將圖片的內容讀取到 img 變數中。
                            Image img = Image.FromStream(fs);
                            PictureBox pb = new PictureBox();
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Width = 100;
                            pb.Height = 50;
                            pb.Image = img;
                            pb.Click += Pb_Click;
                            flowLayoutPanel2.Controls.Add(pb);

                            if (flowLayoutPanel2.Controls.Count == 1)
                            {
                                pictureBox1.Image = pb.Image;
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                    }
                }
            }
        }
        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                pictureBox1.Image = clickedPictureBox.Image;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {   //bnt showAllComment
            FrmComment frmComment = new FrmComment(selectedHotel);
            frmComment.ShowDialog();
        }
    }
}
