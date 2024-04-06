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
                          join r in db.Role
                          on m.RoleID equals r.RoleID
                          orderby m.MemberID
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
            FillMemberNamesComboBox();    //tabPage2裡面的comboBox => comboBoxInTabPage2

        }
        private void FillMemberNamesComboBox()
        {
            dbFunNow db = new dbFunNow();
            // 獲取所有有收藏紀錄的會員的姓名和電話
            var members = (from m in db.Member
                           join hl in db.HotelLikes on m.MemberID equals hl.MemberID
                           select new { m.Name, m.Phone }).ToList();

            // 檢查是否存在相同的姓名
            var memberNames = members
                .GroupBy(m => m.Name)      //按姓名分组的集合，每个组包含所有该姓名的会员
                .Select(g =>
                {
                    // 如果同名會員超過1個，就在姓名後附加電話
                    if (g.Count() > 1)
                    {
                        return g.Select(m => $"{m.Name} ({m.Phone})").ToList();
                    }
                    else
                    {
                        return g.Select(m => m.Name).ToList();
                    }
                }).SelectMany(m => m).Distinct().ToList(); // 

            memberNames.Insert(0, "會員收藏姓名");          //插入一個選項:會員收藏姓名
            comboBoxInTabPage2.DataSource = memberNames;
            comboBoxInTabPage2.SelectedIndex = 0;         //comboBoxInTabPage2預設值為會員收藏姓名
        }

        private void FrmMemberList_Activated(object sender, EventArgs e)
        {
            resetGridStyle();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)   //會員收藏  //join Tables: Member、HotelLikes、Hotel、City
        {
            tabControl1.SelectedTab = tabPage2;
            ClearDataGridView(dataGridView2);
            querymemberlikesAll();

            //dbFunNow db = new dbFunNow();
            //var memberlikes = from m in db.Member 
            //                  join hl in db.HotelLikes
            //                  on m.MemberID equals hl.MemberID
            //                  join h in db.Hotel
            //                  on hl.HotelID equals h.HotelID
            //                  join c in db.City
            //                  on h.CityID equals c.CityID


            //            select new
            //            {
            //                MemberID = m.MemberID,
            //                Name = m.Name,                          
            //                HotelName = h.HotelName,
            //                CityName = c.CityName,
            //                LikeStatus = hl.LikeStatus,
            //            };
            //dataGridView2.DataSource = memberlikes.ToList();
            //resetmemberlikesGridStyle();
        }
        private void ClearDataGridView(DataGridView dgv)                 //清理 DataGridView
        {
            dgv.DataSource = null;
            dgv.Columns.Clear();
        }
        private void querymemberlikesAll()                                     //從資料庫查詢會員收藏資料並呈現
        {
            dbFunNow db = new dbFunNow();
            var memberlikes = from m in db.Member
                              join hl in db.HotelLikes
                              on m.MemberID equals hl.MemberID
                              join h in db.Hotel
                              on hl.HotelID equals h.HotelID
                              join c in db.City
                              on h.CityID equals c.CityID
                              orderby m.MemberID // 按照MemberID排序

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
            // 检查当前选中的 tabPage
            if (tabControl1.SelectedTab == tabPage1)
            {
                // 原有的对 dataGridView1 的搜索逻辑
                SearchInDataGridView(dataGridView1);
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                // 新增的对 dataGridView2 的搜索逻辑
                SearchInDataGridView(dataGridView2);
            }

            //bool isColorChanged = false;
            //foreach (DataGridViewRow r in dataGridView1.Rows)
            //{
            //    isColorChanged = !isColorChanged;
            //    r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
            //    if (isColorChanged)
            //        r.DefaultCellStyle.BackColor = Color.White;

            //    foreach (DataGridViewCell c in r.Cells)
            //    {
            //        if (c.Value == null)
            //            continue;
            //        if (c.Value.ToString().Contains(txtKeyword.Text))
            //        {
            //            r.DefaultCellStyle.BackColor = Color.LightCyan;      //LightBlue //DeepSkyBlue
            //            break;
            //        }
            //    }
            //}
        }
        private void SearchInDataGridView(DataGridView dgv)              //查詢方法
        {
            bool isColorChanged = false;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                isColorChanged = !isColorChanged;
                row.DefaultCellStyle.BackColor = isColorChanged ? Color.White : Color.FromArgb(229, 229, 229); // isColorChanged :true => Color.White ;isColorChanged:false => Color.FromArgb(229, 229, 229)

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null) continue;

                    // 如果要不区分大小写的搜索，先将两边的字符串转换为同一种大小写形式
                    if (cell.Value.ToString().ToLower().Contains(txtKeyword.Text.ToLower()))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCyan;
                        break; // 找到匹配后即跳出循环
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

        private void comboBoxInTabPage2_SelectedIndexChanged(object sender, EventArgs e)
        {

            // 检查是否选择了“會員收藏姓名”
            if (comboBoxInTabPage2.SelectedItem != null &&
                comboBoxInTabPage2.SelectedItem.ToString() == "會員收藏姓名")   // 如果選單拉到 comboBoxInTabPage2預設值為會員收藏姓名
            {
                QueryAllMemberLikes(); // 查詢並顯示所有會員的收藏數據
            }
            else
            {
                if (comboBoxInTabPage2.SelectedItem == null) return;

                var selectedItem = comboBoxInTabPage2.SelectedItem.ToString();   //選到會員姓名
                string name = selectedItem;
                string phone = null;

                // 检查选项是否包含手机号
                if (selectedItem.Contains("(") && selectedItem.Contains(")"))
                {
                    // 解析姓名和手机号
                    var startIndex = selectedItem.IndexOf("(") + 1;
                    var endIndex = selectedItem.IndexOf(")") - startIndex;
                    name = selectedItem.Substring(0, selectedItem.IndexOf(" ("));
                    phone = selectedItem.Substring(startIndex, endIndex);
                }

                // 根据解析出的姓名和可选的手机号查询收藏
                QueryMemberLikesByNameAndPhone(name, phone);
            }
        }
        private void QueryAllMemberLikes()
        {
            dbFunNow db = new dbFunNow();
            var allMemberLikes = (from m in db.Member
                                  join hl in db.HotelLikes on m.MemberID equals hl.MemberID
                                  join h in db.Hotel on hl.HotelID equals h.HotelID
                                  join c in db.City on h.CityID equals c.CityID
                                  orderby m.MemberID // 按照MemberID排序
                                  select new
                                  {
                                      MemberID = m.MemberID,
                                      Name = m.Name,
                                      HotelName = h.HotelName,
                                      CityName = c.CityName,
                                      LikeStatus = hl.LikeStatus,
                                  }).ToList();

            dataGridView2.DataSource = allMemberLikes;
            resetmemberlikesGridStyle();
        }

        private void QueryMemberLikesByNameAndPhone(string name, string phone)
        {
            dbFunNow db = new dbFunNow();
            var memberlikes = db.Member
                                .Where(m => m.Name == name && (phone == null || m.Phone == phone)) // 如果提供了手机号，进一步筛选
                                .SelectMany(m => m.HotelLikes) // 使用导航属性从Member到HotelLikes
                                .Select(hl => new
                                {
                                    MemberID = hl.Member.MemberID,
                                    Name = hl.Member.Name,
                                    HotelName = hl.Hotel.HotelName,
                                    CityName = hl.Hotel.City.CityName,
                                    LikeStatus = hl.LikeStatus,
                                }).ToList();

            dataGridView2.DataSource = memberlikes;
            resetmemberlikesGridStyle();
        }

    }
}
