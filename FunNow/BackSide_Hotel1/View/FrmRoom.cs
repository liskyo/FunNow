
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
    public partial class FrmRoom : Form
    {
        private Room _room;
        private DialogResult _isOK;
        private List<string> _ImagePaths = new List<string>();
        public FrmRoom()
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
                    RoomImage roomImage = new RoomImage();
                    roomImage.RoomImage1 = imagePath;
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

            string basePath = Application.StartupPath + "\\roomImages"; //取得啟動應用程式的可執行檔路徑，不包括檔名。
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
                File.Copy(file, filePath);

                _ImagePaths.Add(filePath); // 添加到圖片路徑列表


                var pictureBox = new PictureBox
                {
                    Image = Image.FromFile(filePath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };

                flowLayoutPanel1.Controls.Add(pictureBox);
             
            }
        }
    }
}
