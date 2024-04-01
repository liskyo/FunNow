using Fun;
using FunNow.BackSide_Hotel;
using FunNow.BackSide_POS;
using projFunNow_Ruby;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace FunNow
{
    public partial class FrmLogin : Form
    {
        Image[] imgs;
        bool hidden = false;
        private bool isClosed = true;
        public static Member auth { get; set; }        
        public int count = 0;
        bool _stop = false;
        public FrmLogin()
        {
            InitializeComponent();
            txtPassword.Text = "";
            txtPassword.PasswordChar = '●';
            imgs = new Image[2];
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string path0 = Path.Combine(projectRoot, "..\\..\\..\\image\\", "openEyes.png");
            string path1 = Path.Combine(projectRoot, "..\\..\\..\\image\\", "closeEyes.png");
            imgs[0] = Image.FromFile(path0);
            imgs[1] = Image.FromFile(path1);

            dbFunNow db = new dbFunNow();
            var members = from m in db.Member
                          select m;
            dataGridView1.DataSource = members.ToList();


        }

        static string EncryptBase64(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbFunNow db = new dbFunNow();

            var members = from m in db.Member
                          where m.Email.Equals(txtEmail.Text)  //先找有沒有相符的會員帳號(電子信箱)
                          select m;

            if (members.Any())//如果有相符的會員帳號(電子信箱)
            {
                // 在密碼比對之前，先把使用者輸入的密碼加密               
                string originalPassword = txtPassword.Text;
                string encryPassword = EncryptBase64(originalPassword);

                foreach (var mm in members) //再來比對每一筆資料
                {
                    
                    if (mm.RoleID == 3)
                    {
                        MessageBox.Show("您的密碼已錯誤達3次，\n" + mm.Email + " \n此帳號已被鎖定，\n請聯絡客服解鎖");
                        isClosed = false;
                        Application.Exit();
                    }
                    if (encryPassword.Equals(mm.Password))//如果解密之後的密碼也相符
                    {
                        MessageBox.Show("歡迎" + mm.Name + "登入成功!!!");

                        if (mm.RoleID == 2)
                        {
                            //進入管理員後臺頁面
                            FrmMain fa = new FrmMain();
                            auth = mm;
                        }
                        else
                        {
                            auth = mm;

                        }
                        isClosed = false;
                        Close();
                        return;
                    }

                    else //如果密碼錯誤
                    {
                        count++; //該會員帳號的失敗登入次數 + 1
                        MessageBox.Show("帳號與密碼不相符");
                        MessageBox.Show(mm.Name + "，\n您已登入失敗累計" + count + "次，\n請注意 : 錯誤達3次將鎖定帳號");
                        
                    }
                    if (count >= 3) //如果登入會員失敗次數累計到3次
                    {   
                        MessageBox.Show("您的密碼已錯誤達3次，" + mm.Email + " \n此帳號已被鎖定，\n請聯絡客服解鎖");
                        mm.RoleID = 3; // 將該會員角色設定為被鎖定狀態
                    }
                    
                }
                db.SaveChanges();
            }
            else //如果根本就沒這個帳號
            {
                MessageBox.Show("此帳號不存在");
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
        private void picEyes1_Click_1(object sender, EventArgs e)
        {
            hidddenEyes(picEyes1, txtPassword);
        }


        private void label1_Click(object sender, EventArgs e)
        {
            FrmRegister r = new FrmRegister(); //跳到註冊頁面
            r.Show();
        }

        //===快速按鈕==============================================
        private void button1_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "A@1.com";
            txtPassword.Text = "Aa1111";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "A@2.com";
            txtPassword.Text = "Aa2222";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "A@3.com";
            txtPassword.Text = "Aa3333";
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            dbFunNow db = new dbFunNow();

            var members = from m in db.Member
                          where m.Email.Equals(txtEmail.Text)  //先找有沒有相符的會員帳號(電子信箱)
                          select m;
            bool flag = members.Any();
            if (flag) 
            {
                label3.Text = "驗證通過";
                Correct(label3);
            }
            else
            {
                label3.Text = "查無此電子信箱，請註冊";
                UnCorrect(txtEmail, label3);
            }

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

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isClosed;
        }
    }
}
