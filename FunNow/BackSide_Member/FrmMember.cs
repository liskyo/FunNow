using FunNow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prjFunNowMember
{
    public partial class FrmMember : Form
    {
       
        private DialogResult _isOK;
        
        public DialogResult isOk
        {
            get
            {
                return _isOK;
            }
        }


        private Member _member;                                                   //動態地創建一個Member類型的實例_member
        public Member member                                                     
        {
            get
            {
                if (_member == null)                                            //檢查私有變量_member是否為null。
                {
                    _member = new Member();                                     //如果是，則創建一個新的Member實例賦值給_member。這確保了每次訪問member屬性時，_member都不為null。
                }
                _member.MemberID = Convert.ToInt32(fbId.fieldValue);            //FrmMember表單控件,獲得的字段值賦給_member的相應屬性                
                _member.Name = fbName.fieldValue;
                _member.Email = fbEmail.fieldValue;
                _member.Password = fbPassword.fieldValue;
                _member.Phone = fbPhone.fieldValue;

                _member.Birthday = Convert.ToDateTime(fbBirth.fieldValue);      //DB裡,Birthday的資料型別是date  => 在FrmMember表單是string => 轉換型別 => ToDateTime

                _member.RoleID = Convert.ToInt32(fbRoleId.fieldValue);
                if (comboBox1.SelectedItem != null && _member != null)
                {
                    //取得選擇的 RoleName
                    string selectedRoleName = (comboBox1.SelectedItem as Role).RoleName;       //comboBox1 這個下拉列表中,將這個選中的項目嘗試轉換為Role類型,意味著這個下拉列表被設計為顯示一系列Role對象。這個類包含一個名為RoleName的屬性，用來表示角色的名稱=>  從下拉列表comboBox1中獲取選中的角色名稱（RoleName）。
                    dbFunNow db = new dbFunNow();                                              //創建一個dbFunNow類型的實例db，與數據庫進行操作。 => 尋找對應的 RoleID
                    //在 db.Role 中尋找對應的 RoleID
                    int selectedRoleId = db.Role.FirstOrDefault(r => r.RoleName == selectedRoleName)?.RoleID ?? 0;   //Role集合中查找一個名稱匹配selectedRoleName的角色，並獲取其角色ID。如果找不到匹配的角色，則使用??運算符賦值0給selectedRoleId

                    //將找到的角色ID（selectedRoleId）更新 _member 實例的 RoleID
                    _member.RoleID = selectedRoleId;
                }
                return _member;

            }
            set 
            {
                _member = value;
                fbId.fieldValue = _member.MemberID.ToString();
                fbName.fieldValue = _member.Name;
                fbEmail.fieldValue = _member.Email;
                fbPassword.fieldValue = _member.Password;
                fbPhone.fieldValue = _member.Phone;
                if (_member.Birthday.HasValue)
                {
                    fbBirth.fieldValue = _member.Birthday.Value.ToString("yyyy/MM/dd");      // 格式化日期,否則生日欄位會出現時間
                }
                else
                {
                    // 如果 Birthday 是 null，可以决定如何处理：或者设置为默认值，或者留空
                    fbBirth.fieldValue = ""; // 或者设置为某个默认值，如 "未设置"
                }
                fbRoleId.fieldValue= _member.RoleID.ToString();
            }
        }

        public FrmMember()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isOK = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isOK = DialogResult.Cancel;
            Close();
        }

     
        
        private void FrmMember_Load(object sender, EventArgs e)
        {
           

            dbFunNow db = new dbFunNow();
            var roles = db.Role.ToList();

            comboBox1.DisplayMember = "RoleName";
            comboBox1.ValueMember = "RoleID";
            // comboBox1.DataBindings.Add("......")
            comboBox1.DataSource = roles;

            fbEmail.Enabled = false;
            fbPassword.Enabled = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)           //將判斷寫在member {get} 裡面
        {


        }
    }
}
