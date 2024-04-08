using FunNow.BackSide_POS;
using prjFunNowMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace FunNow
{
    public partial class FrmFrontMember : Form
    {
        Image[] imgs;
        bool _stop = false;
        public int count = 0;
        bool hidden = false;
        public FrmFrontMember()
        {
            InitializeComponent();
            textBox1.Text = "";
            textBox1.PasswordChar = '●';
            textBox2.Text = "";
            textBox2.PasswordChar = '●';
            textBox3.Text = "";
            textBox3.PasswordChar = '●';

            imgs = new Image[2];
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string path0 = Path.Combine(projectRoot, "..\\..\\..\\image\\", "openEyes.png");
            string path1 = Path.Combine(projectRoot, "..\\..\\..\\image\\", "closeEyes.png");
            imgs[0] = Image.FromFile(path0);
            imgs[1] = Image.FromFile(path1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private DialogResult _isOK;

        public DialogResult isOk
        {
            get
            {
                return _isOK;
            }
        }


        private Member _member;//動態地創建一個Member類型的實例_member
        public Member member
        {
            get
            {
                if (_member == null) //檢查私有變量_member是否為null。
                {
                    _member = new Member(); //如果是，則創建一個新的Member實例賦值給_member。這確保了每次訪問member屬性時，_member都不為null。
                }
                                            
                _member.Name = lblName.Text;
                
                //_member.Password = fbPassword.fieldValue;
                _member.Phone = lblPhone.Text;

                _member.Birthday = Convert.ToDateTime(lblBirthday.Text);     
                return _member;
            }
            set
            {
                _member = value;
               
                lblName.Text = _member.Name;
               
                //fbPassword.fieldValue = _member.Password;
                lblPhone.Text = _member.Phone;
                lblBirthday.Text = _member.Birthday.Value.ToString();
            }
        }
        
        private void queryAll()
        {
            dbFunNow db = new dbFunNow();
            var members = from m in db.Member
                          where m.MemberID.Equals(FrmLogin.auth.MemberID)  //先根據登入進來的會員顯示資料
                          select new
                          {
                              MemberID = m.MemberID,
                              Name = m.Name,
                              Email = m.Email,
                              Password = m.Password,
                              Phone = m.Phone,
                              Birthday = m.Birthday,

                          };
            dataGridView1.DataSource = members.ToList();

            foreach (var mm in members) //再來比對每一筆資料
            {
                lblEmail.Text = mm.Email;
                lblBirthday.Text = mm.Birthday.Value.ToString("yyyy年MM月dd日");
                dateTimePicker1.Value = mm.Birthday.Value;
                lblName.Text = mm.Name;
                txtName.Text = mm.Name;
                lblPhone.Text = mm.Phone;
                txtName.Text = mm.Name;
            }
        }

        private void FrmFrontMember_Load(object sender, EventArgs e)
        {

            queryAll();

        }

        

        private void button2_Click(object sender, EventArgs e) //關閉頁面並自動重新啟動程式
        {

           
            Application.Exit();
            // 取得程式的路徑
            string appPath = Application.ExecutablePath;

            // 使用 Process.Start 重新啟動程式
            Process.Start(appPath);

            // 退出當前應用程序進程
            Environment.Exit(0);



        }

        

        private void btnChange_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

       

        private void button5_Click(object sender, EventArgs e) //姓名欄的點我修改
        {
            gbName.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //姓名欄的保存修改
        {
            dbFunNow db = new dbFunNow();

            string newName = txtName.Text;
            var members = from m in db.Member
                          where m.MemberID.Equals(FrmLogin.auth.MemberID)  //先找有沒有相符的會員帳號(電子信箱)
                          select m;

            if (members != null)
            {
                foreach (var mm in members)
                {
                    mm.Name = newName;
                }
            }

            db.SaveChanges();
            gbName.Visible = false;

            queryAll();

        }

        private void button4_Click(object sender, EventArgs e) //姓名欄的取消
        {
            gbName.Visible = false;
        }

        private void button5_Click_1(object sender, EventArgs e) //手機欄的點我修改
        {
            gbPhone.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e) //手機欄的取消
        {
            gbPhone.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e) //手機欄的確認修改
        {
            if (! _stop)
            {
                dbFunNow db = new dbFunNow();

                string newPhone = txtPhone.Text;
                var members = from m in db.Member
                              where m.MemberID.Equals(FrmLogin.auth.MemberID)  //先找有沒有相符的會員帳號(電子信箱)
                              select m;

                if (members != null)
                {
                    foreach (var mm in members)
                    {
                        mm.Phone = newPhone;
                    }
                }

                db.SaveChanges();
                gbPhone.Visible = false;

                queryAll();
            }
            else
            {
                label5.Text = "請輸入正確格式之手機號碼";
            }
        }

        private void button7_Click(object sender, EventArgs e) //生日欄的點我修改
        {
            gbBirth.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e) //生日欄的取消
        {
            gbBirth.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e) //生日欄的保存修改
        {
            dbFunNow db = new dbFunNow();

            DateTime newBirth = dateTimePicker1.Value;
            var members = from m in db.Member
                          where m.MemberID.Equals(FrmLogin.auth.MemberID)  //先找有沒有相符的會員帳號(電子信箱)
                          select m;

            if (members != null)
            {
                foreach (var mm in members)
                {
                    mm.Birthday = newBirth;
                }
            }

            db.SaveChanges();
            gbBirth.Visible = false;

            queryAll();
        }

        private void UnCorrect(System.Windows.Forms.TextBox t, System.Windows.Forms.Label l)
        {
            l.ForeColor = Color.Red;
            _stop = true;
            t.Focus();
            t.Select(t.TextLength, 1);
        }

        private void Correct(System.Windows.Forms.Label l)
        {
            l.ForeColor = Color.Green;
            _stop = false;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            string typePhone = txtPhone.Text;
            bool flag = (Regex.IsMatch(typePhone, @"^09[0-9]{8}$"));
            if (flag)
            {
                label5.Text = "驗證通過";
                Correct(label5);
            }
            else
            {
                label5.Text = "請輸入正確格式之手機號碼";
                UnCorrect(txtPhone, label5);
            }
        }

        private void hidddenEyes(PictureBox p, System.Windows.Forms.TextBox t)
        {
            if (hidden)
            {
                p.Image = imgs[1];
                t.PasswordChar = '●';
            }
            else
            {
                p.Image = imgs[0];
                t.PasswordChar = '\0';
            }
            hidden = !hidden;
        }

        private void picEyes1_Click(object sender, EventArgs e)
        {

            hidddenEyes(picEyes1, textBox1);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            hidddenEyes(pictureBox1, textBox2);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            hidddenEyes(pictureBox2, textBox3);
        }


        static string EncryptBase64(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes);
        }

        private void btnConfirm_Click(object sender, EventArgs e) //確認原密碼是否正確
        {
            dbFunNow db = new dbFunNow();

            var members = from m in db.Member
                          where m.Email.Equals(lblEmail.Text)  //先找有沒有相符的會員帳號(電子信箱)
                          select m;
            if (members.Any())//如果有相符的會員帳號(電子信箱)
            {
                // 在密碼比對之前，先把使用者輸入的密碼加密               
                string originalPassword = textBox1.Text;
                string encryPassword = EncryptBase64(originalPassword);

                foreach (var mm in members) //再來比對每一筆資料
                {
                    if (encryPassword.Equals(mm.Password))
                    {
                        groupBox2.Visible = true;
                    }
                    else
                    {
                        count++; //該會員帳號的失敗登入次數 + 1
                        MessageBox.Show("密碼輸入錯誤");
                        MessageBox.Show(mm.Name + "，\n您已密碼輸入錯誤累計" + count + "次，\n請注意 : 錯誤達3次將鎖定帳號\n並強制登出");

                    }
                    if (count >= 3) //如果登入會員失敗次數累計到3次
                    {
                        MessageBox.Show("您的密碼已輸入錯誤達3次，" + mm.Email + " \n此帳號已被鎖定，\n請聯絡客服解鎖");
                        mm.RoleID = 3; // 將該會員角色設定為被鎖定狀態
                        Application.Exit();
                    }
                }
                }

            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string typein = textBox2.Text;
            bool flag = (Regex.IsMatch(typein, @"^(?=.*[a-z])(?=.*[A-Z]).{6,100}$"));
            //必须包含大小写字母和数字的组合，長度在6以上
            if (flag)
            {
                label6.Text = "很棒~~這是高強度密碼唷";
                Correct(label6);

            }
            else if (textBox2.Text.Length < 6)
            {
                label6.Text = "您輸入的密碼長度應大於6碼";
                UnCorrect(textBox2, label6);
            }
            else if (!flag)
            {
                label6.Text = "您輸入的密碼應包含英文大小寫字母及數字";
                UnCorrect(textBox2, label6);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string p1 = textBox2.Text;
            string p2 = textBox3.Text;
            if (String.Equals(p1, p2))
            {
                label9.Text = "驗證通過";
                Correct(label9);
            }
            else
            {
                label9.Text = "噢噢...兩次密碼輸入不同，請再次確認";
                UnCorrect(textBox3, label9);
            }
        }

        private void button10_Click(object sender, EventArgs e) //修改密碼的保存修改
        {
            dbFunNow db = new dbFunNow();

            

            // 加密使用者輸入的密碼
            string originalPassword = textBox3.Text;
            string encryPassword = EncryptBase64(originalPassword); // 用自訂的密碼函數把密碼加密

           

            var members = from m in db.Member
                          where m.MemberID.Equals(FrmLogin.auth.MemberID)  //先找有沒有相符的會員帳號(電子信箱)
                          select m;

            if (members != null)
            {
                foreach (var mm in members)
                {
                    mm.Password = encryPassword; // 存儲加密後的密碼
                }
            }

            db.SaveChanges();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            MessageBox.Show("密碼修改成功! \n下次登入請使用新密碼");

            queryAll();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }
    }
    }

