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
    public partial class FrmRoomUpdate : Form
    {
        
        private Room _room;
    
        private DialogResult _isOK;
        private List<string> _ImagePaths = new List<string>();
        public FrmRoomUpdate()
        {
            InitializeComponent();
            
        }

        public DialogResult isOk { get { return _isOK; } }

        public Room RoomInstance
        {
            get
            {
                if (_room == null) { _room = new Room(); }

                _room.RoomName = hotelBox1.fileValue;
                _room.RoomPrice = Convert.ToDecimal(hotelBox2.fileValue);
                _room.Description = textBox1.Text;
                _room.HotelID = (int)comboBox1.SelectedValue; ;
                _room.RoomTypeID = (int)comboBox2.SelectedValue;
                _room.RoomStatus = checkBox1.Checked;

                return _room;
            }
            set 
            {
             
                _room = value;
                checkBox1.Checked = (bool)_room.RoomStatus;
                comboBox1.SelectedValue = _room.HotelID.ToString();
                comboBox2.SelectedValue = _room.RoomTypeID.ToString();
                hotelBox1.fileValue = _room.RoomName.ToString();
                hotelBox2.fileValue = _room.RoomPrice.ToString();
                textBox1.Text = _room.Description;


            }
        }

        private void FrmRoomUpdate_Load(object sender, EventArgs e)
        {
            //飯店
            dbFunNow db = new dbFunNow();

            var hotels = (from r in db.Hotel
                         select new { r.HotelName, r.HotelID }).ToList();

            comboBox1.DisplayMember = "HotelName";
            comboBox1.ValueMember = "HotelID";
            comboBox1.Items.Clear();
            comboBox1.DataSource = hotels.ToList();


            //房型
            var roomType = (from r in db.RoomType
                            select new { r.RoomTypeName, r.RoomTypeID }).ToList();

            comboBox2.DisplayMember = "RoomTypeName";
            comboBox2.ValueMember = "RoomTypeID";
            comboBox2.Items.Clear();
            comboBox2.DataSource = roomType.ToList();

            //房型
            var roomBase = (from r in db.Room
                       where r.RoomID == FrmHotelSystem._roomId
                       select new { r.RoomName, r.RoomPrice, r.Description }).ToList();

           foreach(var r  in roomBase)
            {
                hotelBox1.fileValue =  r.RoomName.ToString();
                hotelBox2.fileValue = r.RoomPrice.ToString();
                textBox1.Text = r.Description.ToString();
             }


            //圖片
            flowLayoutPanel1.Controls.Clear();
            using (var db3 = new dbFunNow()) // 
            {
                var images = db3.RoomImage.Where(r => r.RoomID == FrmHotelSystem._roomId).ToList();

                foreach (var img in images)
                {
                    if (string.IsNullOrEmpty(img.RoomImage1) || !File.Exists(img.RoomImage1)) break;
          
                    PictureBox pictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                        ImageLocation = img.RoomImage1,

                        //Width = 100, // 適當的大小
                        // Height = 100  // 適當的大小
                    };

         

                    Button removeButton = new Button
                    {
                        Text = "刪除",
                        Tag = img.RoomImage1
                    };

                    // 刪除按鈕的事件處理
                    removeButton.Click += (sender1, e1) =>
                    {
                        var button = sender1 as Button;
                        string pathToRemove = (string)button.Tag;
                        RemoveImageAndDescription(pathToRemove);
                    };

                    // 將控件添加到 FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(pictureBox);           
                    flowLayoutPanel1.Controls.Add(removeButton);
                }
            }


            //Hotel RoomType 顯示原本的
            dbFunNow db1 = new dbFunNow();
            var room = (from r in db1.Room
                         where r.RoomID == FrmHotelSystem._roomId
                         select new
                         {
                             HotelName = r.Hotel.HotelName,
                             RoomType = r.RoomType.RoomTypeName,                         
                         }).FirstOrDefault();

            if (room != null)
            {
                comboBox1.Text = room.HotelName;
                comboBox2.Text = room.RoomType;
            }

            //設備顯示原本的
            dbFunNow db4 = new dbFunNow();
            var allEquipments = db4.RoomEquipment.ToList();
            var selectedEquipments = db4.Room_Equipment_Reference
                              .Where(r => r.RoomID == FrmHotelSystem._roomId)
                              .Select(r => r.RoomEquipmentID)
                              .ToList();
            foreach (var equipment in allEquipments)
            {
                CheckBox checkbox = new CheckBox();
                checkbox.Text = equipment.RoomEquipmentName;
                checkbox.Tag = equipment.RoomEquipmentID; // 使用 Tag 屬性來儲存 HotelEquipmentID

                // 如果設備ID在選中設備列表中，將 CheckBox 設為勾選狀態
                if (selectedEquipments.Contains(equipment.RoomEquipmentID))
                {
                    checkbox.Checked = true;
                }

                this.flowLayoutPanel2.Controls.Add(checkbox);
            }


            dbFunNow db6 = new dbFunNow();
            var room6 = (from r in db6.Room
                        where r.RoomID == FrmHotelSystem._roomId
                        select new
                        {
                           r.RoomStatus
                        }).FirstOrDefault();

            checkBox1.Checked = (bool)room6.RoomStatus;



        }

        private void RemoveImageAndDescription(string imagePath)
        {
            PictureBox pictureBoxToRemove = flowLayoutPanel1.Controls.OfType<PictureBox>()
          .FirstOrDefault(pb => pb.ImageLocation == imagePath);

            Button removeButtonToRemove = flowLayoutPanel1.Controls
                .OfType<Button>()
                .FirstOrDefault(btn => btn.Tag != null && btn.Tag.ToString() == imagePath);

            if (pictureBoxToRemove != null)
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

            string basePath = Application.StartupPath + "\\image"; //取得啟動應用程式的可執行檔路徑，不包括檔名。
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath); //在指定的路徑中建立所有目錄
            }

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            int index = 0;
            foreach (string file in openFileDialog1.FileNames)
            {
                string uniqueFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + index.ToString() + Path.GetExtension(file);
                string filePath = Path.Combine(basePath, uniqueFileName);
                File.Copy(file, filePath);  //複製現有的檔案到新的檔案。 不允許覆寫相同名稱的檔案。(string sourceFileName, string destFileName)
                _ImagePaths.Add(filePath); // 添加到圖片路徑列表

                //Delete ICON
                Button removeButton = new Button();
                removeButton.Text = "刪除";
                removeButton.Tag = filePath;

                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(filePath),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    ImageLocation = filePath
                };

  

                removeButton.Click += (sender1, e1) =>
                {
                    Button btn = sender1 as Button; 
                    if (btn != null && btn.Tag is string) 
                    {
                        string pathToRemove = (string)btn.Tag;
                        RemoveImageAndDescription(pathToRemove);
                    }
                };

                flowLayoutPanel1.Controls.Add(pictureBox);              
                flowLayoutPanel1.Controls.Add(removeButton);
                index++;
            }
        }


        //Save button
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(hotelBox1.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(hotelBox2.fileValue)) { MessageBox.Show("有欄位未填寫"); return; }
            if (String.IsNullOrWhiteSpace(textBox1.Text)) { MessageBox.Show("有欄位未填寫"); return; }
            if (flowLayoutPanel1.Controls.OfType<PictureBox>().Count() == 0) { MessageBox.Show("請新增圖片。"); return; }
            if (!decimal.TryParse(hotelBox2.fileValue, out _)) { MessageBox.Show("房價請填數字"); return; }

            //checkBox

            using (var db = new dbFunNow())
            {
                var roomToUpdate = (from room in db.Room
                                    where room.RoomID == FrmHotelSystem._roomId
                                    select room).FirstOrDefault();
                roomToUpdate.HotelID = (int)comboBox1.SelectedValue;
                roomToUpdate.RoomTypeID = (int)comboBox2.SelectedValue;
                roomToUpdate.RoomStatus = checkBox1.Checked;

                

                db.SaveChanges();
            }

            //comboBox 
           
            


            //設備
            using (var db = new dbFunNow())
            {
                // 首先移除該房間目前的所有設備關聯
                var existingReferences = db.Room_Equipment_Reference
                                           .Where(r => r.RoomID == FrmHotelSystem._roomId);
                db.Room_Equipment_Reference.RemoveRange(existingReferences);

                // 然後添加新的選擇
                foreach (CheckBox checkbox in flowLayoutPanel2.Controls.OfType<CheckBox>())
                {
                    if (checkbox.Checked)
                    {
                        int equipmentId = (int)checkbox.Tag;
                        var roomEquipmentReference = new Room_Equipment_Reference
                        {
                            RoomID = FrmHotelSystem._roomId,
                            RoomEquipmentID = equipmentId
                        };
                        db.Room_Equipment_Reference.Add(roomEquipmentReference);
                    }
                }

                db.SaveChanges();
            }

            //圖片
            using (var db = new dbFunNow())
            {
            
                var existingImages = db.RoomImage.Where(img => img.RoomID == FrmHotelSystem._roomId);
                db.RoomImage.RemoveRange(existingImages);

                // 对flowLayoutPanel1中的每个PictureBox和TextBox对进行迭代
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i += 2)
                {
                    var pictureBox = flowLayoutPanel1.Controls[i] as PictureBox;

                    if (pictureBox != null)
                    {
                       
                        string imagePath = pictureBox.ImageLocation; // 假设您在添加PictureBox时设置了ImageLocation
                    

                        // 创建新的HotelImage实体对象
                        var roomImage = new RoomImage
                        {
                            RoomID = FrmHotelSystem._roomId,
                            RoomImage1 = imagePath                          
                        };

                        db.RoomImage.Add(roomImage);
                    }
                }


                db.SaveChanges();
            }

            //textbox
            using (dbFunNow db = new dbFunNow())
            {
                Room room = db.Room.FirstOrDefault(x => x.RoomID == FrmHotelSystem._roomId);
                if (room == null) return;

                //更新 Room 物件

                room.RoomName = hotelBox1.fileValue;
                room.RoomPrice = Convert.ToDecimal(hotelBox2.fileValue);
                room.Description = textBox1.Text;

                //保存更改
                db.SaveChanges();

            }


            MessageBox.Show("已修改資料");
            this.Close();


        }

    }
}
