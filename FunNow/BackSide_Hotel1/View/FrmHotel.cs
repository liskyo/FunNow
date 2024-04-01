using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel.View
{
    public partial class FrmHotel : Form
    {
        //private  dbFunNow _db;
        internal dbFunNow db = new dbFunNow();
        //public dbFunNow db { get { return _db; } set { _db = value; } }

        private Hotel _hotel;
        private DialogResult _isOK;
        private List<string> _ImagePaths = new List<string>();
        public FrmHotel()
        {
            InitializeComponent();
           
        }

        public DialogResult isOk
        {
            get
            {
                return _isOK;
            }
        }

        public Hotel hotelInstance
        {
            get
            {             
                if (_hotel == null) { _hotel = new Hotel(); }

                _hotel.HotelName = hotelBox1.fileValue;
                _hotel.HotelPhone = hotelBox2.fileValue;
                _hotel.HotelAddress = hotelBox3.fileValue;
                _hotel.HotelDescription = textBox1.Text;
                _hotel.CityID = (int)comboBox2.SelectedValue;
                _hotel.HotelTypeID = (int)comboBox3.SelectedValue;

                return _hotel;
            }

            set
            {
                _hotel = value;
               
                hotelBox1.fileValue = _hotel.HotelName;
                hotelBox3.fileValue = _hotel.HotelAddress.ToString();
                hotelBox2.fileValue = _hotel.HotelPhone.ToString();
                textBox1.Text = _hotel.HotelDescription.ToString();


            }
        }

      
        private void FrmHotel_Load(object sender, EventArgs e)
        {
            //新增國家至ComboBox
            using (var db = new dbFunNow()) {
       
                var countries = from c in db.Country
                              select new { c.CountryName, c.CountryID };
                                
                comboBox1.DisplayMember = "CountryName";
                comboBox1.ValueMember = "CountryID";
                comboBox1.Items.Clear();
                comboBox1.DataSource = countries.ToList();

            }

            //新增飯店房型至ComboBox
            using (var db = new dbFunNow())
            {
                var hotelType = from h in db.HotelType
                                select new { h.HotelTypeName, h.HotelTypeID };

                comboBox3.DisplayMember = "HotelTypeName";
                comboBox3.ValueMember = "HotelTypeID";
                comboBox3.Items.Clear();
                comboBox3.DataSource = hotelType.ToList();
            }

            //新增設備至flowLayoutPanel
            using (var db = new dbFunNow())
            {            
                var allEquipments = db.HotelEquipment.ToList();
          

                // 用這些設施填充 CheckBoxList
                foreach (var equipment in allEquipments)
                {
                    CheckBox checkbox = new CheckBox();
                    checkbox.Text = equipment.HotelEquipmentName;
                    checkbox.Tag = equipment.HotelEquipmentID; // 使用 Tag 屬性來儲存 HotelEquipmentID
                    this.flowLayoutPanel2.Controls.Add(checkbox); 
                }
            }


        }

        //選擇城市
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                int selectedCountryID = (int)comboBox1.SelectedValue;
                using (var db = new dbFunNow())
                {
                    var cities = from c in db.City
                                 where c.CountryID == selectedCountryID
                                 select new { c.CityName, c.CityID };

                    comboBox2.DisplayMember = "CityName";
                    comboBox2.ValueMember = "CityID";
                    comboBox2.DataSource = cities.ToList();
                }
            }
        }


        //儲存
        private void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(hotelBox1.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(hotelBox2.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(hotelBox3.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(textBox1.Text)) { MessageBox.Show("有欄位未填寫"); return; }
            if (flowLayoutPanel1.Controls.OfType<PictureBox>().Count() == 0) { MessageBox.Show("請新增圖片。"); return; }


            var hotel = hotelInstance;
            int hotelId = hotel.HotelID;
            if (hotelId > 0) // 假設 HotelID 大於 0 表示已存在
            {
                // 更新現有酒店資訊
                db.Entry(hotel).State = EntityState.Modified;
            }
            else
            {
   
                db.Hotel.Add(hotel);
               

            }


            //新增圖片
            foreach (Control control in this.flowLayoutPanel1.Controls)
            {
                if (control is TextBox)
                {
                    TextBox descriptionBox = (TextBox)control;
                    string imagePath = (string)descriptionBox.Tag;
                    string description = descriptionBox.Text;

                    HotelImages hotelImage = new HotelImages
                    {
                        HotelID = hotelId,
                        HotelImage = imagePath, // 這裡存儲圖片的路徑
                        HImageDescription = description
                    };

                    db.HotelImages.Add(hotelImage);
                }
            }

            //新增設備
            foreach (CheckBox checkbox in this.flowLayoutPanel2.Controls)
            {
                if (checkbox.Checked)
                {
                    int equipmentId = (int)checkbox.Tag;
                    var hotelEquipmentReference = new Hotel_Equipment_Reference
                    {
                        HotelID = hotelId,
                        HotelEquipmentID = equipmentId
                    };

                    db.Hotel_Equipment_Reference.Add(hotelEquipmentReference);
                }
            }

            db.SaveChanges();



            MessageBox.Show("新增成功");
            _isOK = DialogResult.OK;
            this.Close();
        }

        //取消button
        private void button2_Click(object sender, EventArgs e)
        {
            _isOK = DialogResult.Cancel;
            this.Close();
        }

        //加入圖片至FlowLayoutPanel
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            string basePath = Application.StartupPath + "\\hotelImages"; //取得啟動應用程式的可執行檔路徑，不包括檔名。
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath); //在指定的路徑中建立所有目錄
            }

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            foreach (string file in openFileDialog1.FileNames)
            {
                string uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file);
                string filePath = Path.Combine(basePath, uniqueFileName);
                File.Copy(file, filePath);  //複製現有的檔案到新的檔案。 不允許覆寫相同名稱的檔案。(string sourceFileName, string destFileName)

                _ImagePaths.Add(filePath); // 添加到圖片路徑列表
          
                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(filePath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };

                
                int numberOfLines = 2;
                TextBox descriptionBox = new TextBox();
                descriptionBox.Width = pictureBox.Width; 
                descriptionBox.Multiline = true;
                descriptionBox.ScrollBars = ScrollBars.Vertical;
                descriptionBox.Height = descriptionBox.Font.Height * numberOfLines;

                descriptionBox.Tag = file;  //Tag 屬性

                flowLayoutPanel1.Controls.Add(pictureBox); 
                flowLayoutPanel1.Controls.Add(descriptionBox);
            }
        }
   
    }
}
