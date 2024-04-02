using FunNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjFunNowMember.View
{
    public partial class FrmHotelLikeBack : Form
    {
        public FrmHotelLikeBack()
        {
            InitializeComponent();
        }

        private void queryAll()
        {                        
            dbFunNow db = new dbFunNow();                        //join tables: Member HotelLike Hotel city
            var hotellikes = from m in db.Member
                          join hl in db.HotelLikes 
                          on m.MemberID equals hl.MemberID
                          join h in db.Hotel 
                          on hl.HotelID equals h.HotelID
                          join c in db.City 
                          on h.CityID equals c.CityID


                          select new
                          {
                              MemberID=m.MemberID,
                              Name = m.Name,
                              HotelName = h.HotelName,
                              CityName = c.CityName,
                              LikeStatus = hl.LikeStatus,


                          };
            dataGridView1.DataSource = hotellikes.ToList();
            resetGridStyle();

        }

        private void resetGridStyle()
        {
            dataGridView1.Columns[0].HeaderText = "會員編號";
            dataGridView1.Columns[1].HeaderText = "會員姓名";
            dataGridView1.Columns[2].HeaderText = "飯店名";
            dataGridView1.Columns[3].HeaderText = "城市";
            dataGridView1.Columns[4].HeaderText = "收藏狀態";
       


            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 100;
 
         
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.White;
                r.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
            //queryAll();
            //resetGridStyle();
        }

        private void FrmHotelLikeBack_Load(object sender, EventArgs e)
        {
            queryAll();
            resetGridStyle();
        }
    }
}
