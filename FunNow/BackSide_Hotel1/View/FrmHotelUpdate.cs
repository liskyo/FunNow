using CefSharp.DevTools.CSS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Hotel.View
{
    public partial class FrmHotelUpdate : Form
    {
        private Hotel _hotel;
        private DialogResult _isOK;
        private List<string> _ImagePaths = new List<string>();
        public FrmHotelUpdate()
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
                comboBox3.SelectedValue = _hotel.HotelTypeID.ToString();


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



            //國家 城市 HotelType
            dbFunNow db1 = new dbFunNow();
            var hotel = (from h in db1.Hotel
                         where h.HotelID == FrmHotelSystem.Id
                         select new
                         {
                             HotelTypeName = h.HotelType.HotelTypeName,
                             CityName = h.City.CityName,
                             CountryName = h.City.Country.CountryName
                         }).FirstOrDefault(); 

            if (hotel != null)
            {                
                comboBox3.Text = hotel.HotelTypeName;              
                comboBox1.Text = hotel.CountryName;
            }
            comboBox2.SelectedValue = _hotel.CityID;


            //設備
            dbFunNow db2 = new dbFunNow();
            var allEquipments = db2.HotelEquipment.ToList();
            var selectedEquipments = db2.Hotel_Equipment_Reference
                              .Where(h => h.HotelID == FrmHotelSystem.Id)
                              .Select(h => h.HotelEquipmentID)
                              .ToList();
            foreach (var equipment in allEquipments)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Text = equipment.HotelEquipmentName;
                checkbox.Tag = equipment.HotelEquipmentID; // 使用 Tag 屬性來儲存 HotelEquipmentID

                // 如果設備ID在選中設備列表中，將 CheckBox 設為勾選狀態
                if (selectedEquipments.Contains(equipment.HotelEquipmentID))
                {
                    checkbox.Checked = true;
                }

                this.flowLayoutPanel2.Controls.Add(checkbox);
            }

            //圖片
            flowLayoutPanel1.Controls.Clear();

            using (var db = new dbFunNow()) 
            {
                var images = db.HotelImages.Where(h => h.HotelID == FrmHotelSystem.Id).ToList();

                foreach (var img in images)
                {

                    if (!string.IsNullOrEmpty(img.HotelImage))
                    {
                        string filename = Path.GetFileName(img.HotelImage);
                        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                        string path = Path.Combine(projectRoot, "..\\..\\..\\image\\", filename);

                        if (File.Exists(path))
                        {
                            PictureBox pictureBox = new PictureBox
                            {
                                SizeMode = PictureBoxSizeMode.Zoom,
                                BorderStyle = BorderStyle.FixedSingle,
                                ImageLocation = path
                                //Image = Image.FromFile(img.HotelImage),
                                //Width = 300, // 適當的大小
                                //Height = 300  // 適當的大小
                            };
                            flowLayoutPanel1.Controls.Add(pictureBox);
                        }

                    }
                    else
                    {
                        PictureBox pictureBox = new PictureBox
                        {
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderStyle = BorderStyle.FixedSingle,
                            ImageLocation = null
                            //Image = Image.FromFile(img.HotelImage),
                            //Width = 300, // 適當的大小
                            //Height = 300  // 適當的大小
                        };
                        flowLayoutPanel1.Controls.Add(pictureBox);
                    }



                    //if (string.IsNullOrEmpty(img.HotelImage)) break;
                    //if(!File.Exists(img.HotelImage)) break;

     


                    TextBox descriptionBox = new TextBox
                    {
                        Multiline = true,
                        ScrollBars = ScrollBars.Vertical,
                       // Width = 100, // 適當的大小
                        //Height = 40, // 適當的高度
                        Text = img.HImageDescription,
                        Tag = img.HotelImage
                    };

                    Button removeButton = new Button
                    {
                        Text = "刪除",
                        Tag = img.HotelImage
                    };

                    // 刪除按鈕的事件處理
                    removeButton.Click += (sender1, e1) =>
                    {
                        var button = sender1 as Button;
                        string pathToRemove = (string)button.Tag;
                        RemoveImageAndDescription(pathToRemove);
                    };

                    // 將控件添加到 FlowLayoutPanel
                
                    flowLayoutPanel1.Controls.Add(descriptionBox);
                    flowLayoutPanel1.Controls.Add(removeButton);
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

            //設備
            using (var db = new dbFunNow())
            {
                // 首先移除該飯店目前的所有設備關聯
                var existingReferences = db.Hotel_Equipment_Reference
                                           .Where(h => h.HotelID == FrmHotelSystem.Id);
                db.Hotel_Equipment_Reference.RemoveRange(existingReferences);

                // 然後添加新的選擇
                foreach (CheckBox checkbox in flowLayoutPanel2.Controls.OfType<CheckBox>())
                {
                    if (checkbox.Checked)
                    {
                        int equipmentId = (int)checkbox.Tag;
                        var hotelEquipmentReference = new Hotel_Equipment_Reference
                        {
                            HotelID = FrmHotelSystem.Id,
                            HotelEquipmentID = equipmentId
                        };
                        db.Hotel_Equipment_Reference.Add(hotelEquipmentReference);
                    }
                }


               

                db.SaveChanges();
            }


            //圖片
            using (var db = new dbFunNow())
            {
                var existingImages = db.HotelImages.Where(img => img.HotelID == FrmHotelSystem.Id);
                db.HotelImages.RemoveRange(existingImages);

                // 对flowLayoutPanel1中的每个PictureBox和TextBox对进行迭代
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i += 3)
                {
                    var pictureBox = flowLayoutPanel1.Controls[i] as PictureBox;
                    var descriptionBox = flowLayoutPanel1.Controls[i + 1] as TextBox;

                    if (pictureBox != null && descriptionBox != null)
                    {
                        string imagePath = pictureBox.ImageLocation; // 假设您在添加PictureBox时设置了ImageLocation
                        string description = descriptionBox.Text;

                        string filename = Path.GetFileName(imagePath);
                        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                        string path = Path.Combine(projectRoot, "..\\..\\..\\image\\", filename);


                        var hotelImage = new HotelImages
                        {
                            HotelID = FrmHotelSystem.Id,
                            HotelImage = path,
                            HImageDescription = description
                        };

                        db.HotelImages.Add(hotelImage);
                    }
                }

                db.SaveChanges();
            }

            _isOK = DialogResult.OK;
            MessageBox.Show("修改成功");
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

            string basePath = Path.Combine(Application.StartupPath, "..\\..\\..\\image\\"); // 修改 basePath 為新路徑

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath); //在指定的路徑中建立所有目錄
            }

            flowLayoutPanel1.Controls.Clear();
          
         
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
 
    }
}
