using FunNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace projFunNow_Ruby
{
    public partial class FrmRegister : Form
    {
        Image[] imgs;
        bool hidden = false;
        bool _stop = false;
        private DialogResult _isOK; //宣告全域變數才拿得到
        public DialogResult isOK//要在Room裡面創造屬性
        {
            get { return _isOK; }
        }

        private Member _member;
        public Member member //創造屬性
        {
            get
            {
                if (_member == null)
                {
                    _member = new Member();
                }
                _member.Email = txtEmail.Text;
                _member.Password = txtPassword.Text;
                _member.Name = txtName.Text;
                _member.Phone = txtPhone.Text;
                _member.Birthday = Convert.ToDateTime(dateTimePicker1.Text);
                _member.RoleID = 1;

                return _member;
            }

            set //為了能傳回顯示資料
            {
                _member = value;
                txtEmail.Text = _member.Email;
                txtPassword.Text = _member.Password;
                txtName.Text = _member.Name;
                txtPhone.Text = _member.Phone;
                dateTimePicker1.Text = _member.Birthday.ToString();
            }
        }

        public FrmRegister()
        {
            InitializeComponent();
            txtPassword.Text = "";
            txtPassword.PasswordChar = '●';
            txtRecheck.Text = "";
            txtRecheck.PasswordChar = '●';
            imgs = new Image[2];
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string path0 = Path.Combine(projectRoot, "..\\..\\..\\image\\", "openEyes.png");
            string path1 = Path.Combine(projectRoot, "..\\..\\..\\image\\", "closeEyes.png");
            imgs[0] = Image.FromFile(path0);
            imgs[1] = Image.FromFile(path1);

            //imgs[0] = Image.FromFile("c:\\openEyes.png");
            //imgs[1] = Image.FromFile("c:\\closeEyes.png");
        }


        private void label1_Click(object sender, EventArgs e) //顯示登入頁面
        {
            FrmLogin f = new FrmLogin();
            f.Show();
        }

        private void notNull(System.Windows.Forms.TextBox t, System.Windows.Forms.Label l)
        {
            if (string.IsNullOrWhiteSpace(t.Text))
            {
                MessageBox.Show(l.Text +"欄位為必填");
                _stop = true;
            }
            
        }

        private void button2_Click(object sender, EventArgs e) //註冊按鈕，要將會員資料輸入資料庫
        {
            notNull(txtEmail, label3);
            notNull(txtPassword, label4);
            notNull(txtRecheck, label5);
        
            if (!_stop)
            {
                _isOK = DialogResult.OK;
                Member x = this.member; //把畫面拿到的欄位回傳到x裡面

                dbFunNow db = new dbFunNow(); //x要放入db中 

                // 加密使用者輸入的密碼
                string originalPassword = txtPassword.Text;
                string encryPassword = EncryptBase64(originalPassword); // 用自訂的密碼函數把密碼加密

                x.Password = encryPassword; // 存儲加密後的密碼

                db.Member.Add(x); //把x資料新增至db中，用add關鍵字
                db.SaveChanges(); // 加入之後，要存到db中，用SaveChanges關鍵字

                var members = from r in db.Member //顯示加入資料後的資料庫，查詢資料
                              select r;
                dataGridView1.DataSource = members.ToList();
                MessageBox.Show("你已成功註冊");
                //Close(); //註冊完之後要關閉註冊頁面，並跳出登入頁面
                FrmLogin f = new FrmLogin();
                f.Show();
            }
            else 
            { 
                MessageBox.Show("請確認必填處與紅字處是否皆已完成修正");   
            }
        }

        static string EncryptBase64(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes);
        }

        private void FrmRegister_Load(object sender, EventArgs e) 
        {
            dbFunNow db = new dbFunNow();
            var members = from r in db.Member //顯示加入資料後的資料庫，查詢資料
                          select r;
            dataGridView1.DataSource = members.ToList();

            if (this.isOK != DialogResult.OK) //如果拿到的結果是取消
            {
                return; //就返回結束 
            }    
        }

        private void UnCorrect(System.Windows.Forms.TextBox t ,System.Windows.Forms.Label l)
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

        private void txtRecheck_TextChanged(object sender, EventArgs e)
        {
            string p1 = txtPassword.Text;
            string p2 = txtRecheck.Text;
            if (String.Equals(p1, p2))
            {
                lblRecheck.Text = "驗證通過";
                Correct(lblRecheck);
            }
            else
            {
                lblRecheck.Text = "噢噢...兩次密碼輸入不同，請再次確認";
                UnCorrect(txtRecheck,lblRecheck);
            }
        }

       
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string typein = txtPassword.Text;
            bool flag = (Regex.IsMatch(typein, @"^(?=.*[a-z])(?=.*[A-Z]).{6,100}$"));
            //必须包含大小写字母和数字的组合，長度在6以上
            if (flag)
            {
                lblPassword.Text = "很棒~~這是高強度密碼唷";
                Correct(lblPassword);

            }
            else if (txtPassword.Text.Length<6)
            {
                lblPassword.Text = "您輸入的密碼長度應大於6碼";
                UnCorrect(txtPassword,lblPassword);
            }
            else if (!flag)
            {
                lblPassword.Text = "您輸入的密碼應包含英文大小寫字母及數字";
                UnCorrect(txtPassword, lblPassword);
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
            hidddenEyes(picEyes1, txtPassword);
        }


        private void picEyes2_Click(object sender, EventArgs e)
        {   
            hidddenEyes(picEyes2,txtRecheck);
        }
        

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            string typePhone = txtPhone.Text;
            bool flag = (Regex.IsMatch(typePhone, @"^09[0-9]{8}$"));
            if (flag) 
            { 
                lblPhone.Text = "驗證通過";
                Correct(lblPhone);
            }
            else 
            { 
                lblPhone.Text = "請輸入正確格式之手機號碼";
                UnCorrect(txtPhone,lblPhone);
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string typeMail = txtEmail.Text;
            bool flag = (Regex.IsMatch(typeMail, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"));
            if (flag) 
            { 
                lblEmail.Text = "驗證通過";
                Correct(lblEmail);
            }
            else 
            { 
                lblEmail.Text = "請輸入正確格式之電子郵件"; 
                UnCorrect(txtEmail,lblEmail);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        // 自訂的密碼加密函數
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
