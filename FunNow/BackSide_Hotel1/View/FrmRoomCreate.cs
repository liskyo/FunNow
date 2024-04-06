
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

namespace FunNow.BackSide_Hotel.View
{
    public partial class FrmRoomCreate : Form
    {
        private Room _room;
        private DialogResult _isOK;
        private List<string> _ImagePaths = new List<string>();
        public FrmRoomCreate()
        {
            InitializeComponent();
        }

        public Room roomInstance
        {
            get
            {
                int selectedHotelID = (int)comboBox1.SelectedValue;
                int selectedRoomTypeID = (int)comboBox2.SelectedValue;
                
                if (_room == null) { _room = new Room(); }

                _room.RoomName = hotelBox1.fileValue;
                _room.RoomPrice = Convert.ToInt32(hotelBox2.fileValue);
                _room.Description = textBox1.Text;
                _room.HotelID = selectedHotelID;
                _room.RoomTypeID = selectedRoomTypeID;
                _room.RoomStatus = checkBox1.Checked;

                return _room;
            }
        }

        private void FrmRoom_Load(object sender, EventArgs e)
        {
            dbFunNow db = new dbFunNow();
            var hotels = (from h in db.Hotel
                          select new { h.HotelName, h.HotelID }).ToList();

            hotels.Insert(0, new { HotelName = "請選擇房間", HotelID = 0 });
            comboBox1.DisplayMember = "HotelName";
            comboBox1.ValueMember = "HotelID";
            comboBox1.DataSource = hotels;
            comboBox1.SelectedIndex = 0;

            var roomType = (from r in db.RoomType
                          select new { r.RoomTypeName, r.RoomTypeID }).ToList();

            roomType.Insert(0, new { RoomTypeName = "請選擇房間", RoomTypeID = 0 });
            comboBox2.DisplayMember = "RoomTypeName";
            comboBox2.ValueMember = "RoomTypeID";
            comboBox2.DataSource = roomType;
            comboBox2.SelectedIndex = 0;


            using (var db2 = new dbFunNow())
            {           
                var allEquipments = db2.RoomEquipment.ToList();

                // 用這些設施填充 CheckBoxList
                foreach (var equipment in allEquipments)
                {
                    CheckBox checkbox = new CheckBox();
                    checkbox.Text = equipment.RoomEquipmentName;
                    checkbox.Tag = equipment.RoomEquipmentID; // 使用 Tag 屬性來儲存 EquipmentID
                    this.flowLayoutPanel2.Controls.Add(checkbox); 
                }
            }
            
        }

        public DialogResult isOk
        {
            get
            {
                return _isOK;
            }
        }


        //儲存
        private void button1_Click(object sender, EventArgs e)
        {
            int number;
            if (String.IsNullOrWhiteSpace(hotelBox1.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(hotelBox2.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (comboBox1.SelectedIndex == 0) { MessageBox.Show("飯店未選擇"); return; }
            if (comboBox2.SelectedIndex == 0) { MessageBox.Show("房型未選擇"); return; }
            if( !int.TryParse(hotelBox2.fileValue, out number) ) { MessageBox.Show("房價價格必須是數字"); return; }
            if ( flowLayoutPanel1.Controls.OfType<PictureBox>().Count() == 0 ) { MessageBox.Show("請新增圖片。"); return; }

            using (dbFunNow db = new dbFunNow())
            {
                //儲存房間
                var room = roomInstance;
                db.Room.Add(room);
                db.SaveChanges();

                int roomId = room.RoomID;

                //儲存照片
                foreach (string imagePath in _ImagePaths)
                {
                    
                    string filename = Path.GetFileName(imagePath);
                    string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                    string path = Path.Combine(projectRoot, "..\\..\\..\\image\\", filename);

                    RoomImage roomImage = new RoomImage();
                    roomImage.RoomImage1 = path;
                    roomImage.RoomID = roomId;
                    db.RoomImage.Add(roomImage);
                }

                //儲存設備
                foreach (CheckBox checkbox in this.flowLayoutPanel2.Controls)
                {
                    if (checkbox.Checked)
                    {
                        int equipmentId = (int)checkbox.Tag;
                        var roomEquipmentReference = new Room_Equipment_Reference()
                        {
                            RoomID = roomId,
                            RoomEquipmentID = equipmentId
                        };

                        db.Room_Equipment_Reference.Add(roomEquipmentReference);
                    }
                }

                db.SaveChanges();

            }

            MessageBox.Show("新增成功");
            _isOK = DialogResult.OK;
            this.Close();
        }

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            _isOK = DialogResult.Cancel;
            this.Close();
        }

        //加入圖片至畫面
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            string basePath = Application.StartupPath + "..\\..\\..\\image\\"; //取得啟動應用程式的可執行檔路徑，不包括檔名。
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath); //在指定的路徑中建立所有目錄
            }

            flowLayoutPanel1.Controls.Clear();
          
            //int index = 0;
            foreach (string file in openFileDialog1.FileNames)
            {

                string destPath = Path.Combine(basePath, file);
               
                //string uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + index.ToString() + Path.GetExtension(file);
                //string filePath = Path.Combine(basePath, uniqueFileName);
                //File.Copy(file, destPath);  //複製現有的檔案到新的檔案。 不允許覆寫相同名稱的檔案。(string sourceFileName, string destFileName)
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


                var pictureBox = new PictureBox
                {
                    Image = Image.FromFile(destPath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    ImageLocation = destPath
                };

                removeButton.Click += (sender1, e1) =>
                {
                    Button btn = sender1 as Button; 
                    if (btn != null && btn.Tag is string) // 確保 btn 不是 null 並且 Tag 確實是一個 string
                    {
                        string pathToRemove = (string)btn.Tag;
                        RemoveImageAndDescription(pathToRemove);
                    }
                };

                flowLayoutPanel1.Controls.Add(pictureBox);
                flowLayoutPanel1.Controls.Add(removeButton);
                //index++;
            }
        }

        private void RemoveImageAndDescription(string imagePath)
        {
            PictureBox pictureBoxToRemove = flowLayoutPanel1.Controls.OfType<PictureBox>()
     .FirstOrDefault(pb => pb.ImageLocation == imagePath);



            Button removeButtonToRemove = flowLayoutPanel1.Controls
                .OfType<Button>()
                .FirstOrDefault(btn => btn.Tag != null && btn.Tag.ToString() == imagePath);

            if (pictureBoxToRemove != null )
            {

                flowLayoutPanel1.Controls.Remove(pictureBoxToRemove);

                flowLayoutPanel1.Controls.Remove(removeButtonToRemove);

                pictureBoxToRemove.Dispose();
             
                removeButtonToRemove.Dispose();

                flowLayoutPanel1.PerformLayout(); // 強制 flowLayoutPanel1 重新佈局
                flowLayoutPanel1.Invalidate(); // 強制 flowLayoutPanel1 重新繪製
            }

            _ImagePaths.Remove(imagePath);
        }
    }
}
