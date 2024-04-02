using FunNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjFunNowMember
{
    public partial class FrmMemberList : Form
    {
        public FrmMemberList()
        {
            InitializeComponent();
        }
        private void queryAll()
        {

            dbFunNow db = new dbFunNow();
            var members = from m in db.Member
                          join h in db.HotelLikes
                          on m.MemberID equals h.MemberID
                          join r in db.Role
                          on m.RoleID equals r.RoleID
                          select new
                          {
                              MemberID = m.MemberID,
                              Name = m.Name,
                              Email = m.Email,
                              Password = m.Password,
                              Phone = m.Phone,
                              Birthday = m.Birthday,
                              RoleName = r.RoleName,
                              //RoleDescription=r.RoleDescription,                             

                          };
            dataGridView1.DataSource = members.ToList();
            resetGridStyle();

        }
        private void resetGridStyle()
        {
            dataGridView1.Columns[0].HeaderText = "會員編號";
            dataGridView1.Columns[1].HeaderText = "會員姓名";
            dataGridView1.Columns[2].HeaderText = "電子郵件";
            dataGridView1.Columns[3].HeaderText = "密碼";
            dataGridView1.Columns[4].HeaderText = "電話";
            dataGridView1.Columns[5].HeaderText = "生日";
            dataGridView1.Columns[6].HeaderText = "角色";
            //dataGridView1.Columns[7].HeaderText = "角色描述";
            

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 100;
            //dataGridView1.Columns[7].Width = 150;
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
       

        private void FrmMemberList_Load(object sender, EventArgs e)
        {
            queryAll();
            
        }

        private void FrmMemberList_Activated(object sender, EventArgs e)
        {
            resetGridStyle();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)   //會員收藏  //join Tables: Member、HotelLikes、Hotel、City
        {
            tabControl1.SelectedTab = tabPage2;
            dbFunNow db = new dbFunNow();
            var memberlikes = from m in db.Member 
                              join hl in db.HotelLikes
                              on m.MemberID equals hl.MemberID
                              join h in db.Hotel
                              on hl.HotelID equals h.HotelID
                              join c in db.City
                              on h.CityID equals c.CityID


                        select new
                        {
                            MemberID = m.MemberID,
                            Name = m.Name,                          
                            HotelName = h.HotelName,
                            CityName = c.CityName,
                            LikeStatus = hl.LikeStatus,
                        };
            dataGridView2.DataSource = memberlikes.ToList();
            resetmemberlikesGridStyle();

        }
        private void resetmemberlikesGridStyle()                              //會員收藏 resetGridStyle
        {
            dataGridView1.Columns[0].HeaderText = "會員編號";
            dataGridView1.Columns[1].HeaderText = "會員姓名";          
            dataGridView1.Columns[2].HeaderText = "飯店名";            
            dataGridView1.Columns[3].HeaderText = "城市";
            dataGridView1.Columns[4].HeaderText = "收藏狀態";           


            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;          
            dataGridView1.Columns[2].Width = 150;         
            dataGridView1.Columns[3].Width = 100;
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

        
       

        private void editById(int id)                 //修改會員資料   
        {
            dbFunNow db = new dbFunNow();
            Member member = db.Member.FirstOrDefault(x => x.MemberID == id);
            if (member == null)
                return;

            FrmMember f = new FrmMember();
            f.member = member;
            f.ShowDialog();

            if (f.isOk != DialogResult.OK)
                return;
            member.MemberID = f.member.MemberID;
            member.Name = f.member.Name;
            member.Email = f.member.Email;
            member.Password = f.member.Password;
            member.Phone = f.member.Phone;
            member.Birthday = f.member.Birthday;
            member.RoleID = f.member.RoleID;
            
            db.SaveChanges();
            queryAll();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)   // 點擊修改
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            editById(id);
        }

        
    
        

        private void toolStripButton3_Click(object sender, EventArgs e)  //查詢
        {
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.White;

                foreach (DataGridViewCell c in r.Cells)
                {
                    if (c.Value == null)
                        continue;
                    if (c.Value.ToString().Contains(txtKeyword.Text))
                    {
                        r.DefaultCellStyle.BackColor = Color.LightCyan;      //LightBlue //DeepSkyBlue
                        break;
                    }
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)   //會員資料
        {
            tabControl1.SelectedTab = tabPage1;
            queryAll();
            resetGridStyle();
        }
    }
}
