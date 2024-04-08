using FunNow.BackSide_Hotel.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel1.View
{
    public partial class FrmHotelCreate : Form
    {
        int selectedCountryID;
        private Hotel _hotel;
        private DialogResult _isOK;
        private List<string> _ImagePaths = new List<string>();
        public FrmHotelCreate()
        {
            InitializeComponent();
        }
        public DialogResult isOk { get { return _isOK; } }

        public Hotel hotelInstance
        {
            get
            {
                if (_hotel == null) { _hotel = new Hotel(); }

                _hotel.HotelName = hotelBox1.fileValue;
                _hotel.HotelPhone = hotelBox3.fileValue;
                _hotel.HotelAddress = hotelBox2.fileValue;
                _hotel.HotelDescription = textBox1.Text;
                _hotel.CityID = (int)comboBox2.SelectedValue;
                _hotel.HotelTypeID = (int)comboBox3.SelectedValue;

                return _hotel;
            }
            set
            {
                _hotel = value;

                hotelBox1.fileValue = _hotel.HotelName;
                hotelBox2.fileValue = _hotel.HotelAddress.ToString();
                hotelBox3.fileValue = _hotel.HotelPhone.ToString();
                textBox1.Text = _hotel.HotelDescription.ToString();
                comboBox3.SelectedValue = _hotel.HotelTypeID.ToString();
            }
        }

        private void FrmHotelCreate_Load(object sender, EventArgs e)
        {
            //新增國家至ComboBox
            using (var db = new dbFunNow())
            {

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
                var allEquipments2 = db.HotelEquipment.ToList();

                foreach (var equipment in allEquipments2)
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

        //加入圖片至FlowLayoutPanel
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            string basePath = Application.StartupPath + "\\image"; //取得啟動應用程式的可執行檔路徑，不包括檔名。
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath); //在指定的路徑中建立所有目錄
            }

            flowLayoutPanel1.Controls.Clear();

            //int index = 0;
            foreach (string file in openFileDialog1.FileNames)
            {
                string destPath = Path.Combine(basePath, file);

                try
                {
                    File.Copy(file, destPath);
                    Console.WriteLine("File copied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }


                _ImagePaths.Add(destPath); // 添加到圖片路徑列表

                //Delete ICON
                Button removeButton = new Button();
                removeButton.Text = "刪除";
                removeButton.Tag = destPath;

                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(destPath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    ImageLocation = destPath
                };


                int numberOfLines = 2;
                TextBox descriptionBox = new TextBox();
                descriptionBox.Width = pictureBox.Width;
                descriptionBox.Multiline = true;
                descriptionBox.ScrollBars = ScrollBars.Vertical;
                descriptionBox.Height = descriptionBox.Font.Height * numberOfLines;
                descriptionBox.Tag = destPath;  //Tag 屬性

                removeButton.Click += (sender1, e1) =>
                {
                    Button btn = sender1 as Button; // 使用 as 關鍵字嘗試轉換 sender 為 Button 類型
                    if (btn != null && btn.Tag is string) // 確保 btn 不是 null 並且 Tag 確實是一個 string
                    {
                        string pathToRemove = (string)btn.Tag;
                        RemoveImageAndDescription(pathToRemove);
                    }
                };

                flowLayoutPanel1.Controls.Add(pictureBox);
                flowLayoutPanel1.Controls.Add(descriptionBox);
                flowLayoutPanel1.Controls.Add(removeButton);
                //index++;
            }

        }

        private void RemoveImageAndDescription(string imagePath)
        {
            PictureBox pictureBoxToRemove = flowLayoutPanel1.Controls.OfType<PictureBox>()
          .FirstOrDefault(pb => pb.ImageLocation == imagePath);


            TextBox descriptionBoxToRemove = flowLayoutPanel1.Controls.OfType<TextBox>()
                .FirstOrDefault(tb => (string)tb.Tag == imagePath);

            Button removeButtonToRemove = flowLayoutPanel1.Controls
                .OfType<Button>()
                .FirstOrDefault(btn => btn.Tag != null && btn.Tag.ToString() == imagePath);

            if (pictureBoxToRemove != null && descriptionBoxToRemove != null)
            {

                flowLayoutPanel1.Controls.Remove(pictureBoxToRemove);
                flowLayoutPanel1.Controls.Remove(descriptionBoxToRemove);
                flowLayoutPanel1.Controls.Remove(removeButtonToRemove);

                pictureBoxToRemove.Dispose();
                descriptionBoxToRemove.Dispose();
                removeButtonToRemove.Dispose();

                flowLayoutPanel1.PerformLayout(); // 強制 flowLayoutPanel1 重新佈局
                flowLayoutPanel1.Invalidate(); // 強制 flowLayoutPanel1 重新繪製
            }

            _ImagePaths.Remove(imagePath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isOK = DialogResult.Cancel;
            this.Close();
        }

        //儲存
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(hotelBox1.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(hotelBox2.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(hotelBox3.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(textBox1.Text)) { MessageBox.Show("有欄位未填寫"); return; }
            if (flowLayoutPanel1.Controls.OfType<PictureBox>().Count() == 0) { MessageBox.Show("請新增圖片。"); return; }

            dbFunNow db = new dbFunNow();

            db.Hotel.Add(hotelInstance);
            db.SaveChanges();

            int hotelId = hotelInstance.HotelID;

            //儲存照片
            foreach (Control control in this.flowLayoutPanel1.Controls)
            {

                if (control is TextBox)
                {
                    TextBox descriptionbox = (TextBox)control;
                    string imagepath = (string)descriptionbox.Tag;
                    string description = descriptionbox.Text;


                    string filename = Path.GetFileName(imagepath);
                    string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                    string path = Path.Combine(projectRoot, "..\\..\\..\\image\\", filename);


                    HotelImages hotelimage = new HotelImages
                    {
                        HotelID = hotelId,
                        HotelImage = path, // 這裡存儲圖片的路徑
                        HImageDescription = description
                    };

                    db.HotelImages.Add(hotelimage);
                }
            }

            //新增設備
            foreach (CheckBox checkbox in this.flowLayoutPanel2.Controls)
            {
                if (checkbox.Checked)
                {
                    int equipmentid = (int)checkbox.Tag;
                    var hotelequipmentreference = new Hotel_Equipment_Reference
                    {
                        HotelID = hotelId,
                        HotelEquipmentID = equipmentid
                    };

                    db.Hotel_Equipment_Reference.Add(hotelequipmentreference);
                }
            }

            db.SaveChanges();

            MessageBox.Show("新增成功");


            _isOK = DialogResult.OK;
            this.Close();
        }
    }
}
