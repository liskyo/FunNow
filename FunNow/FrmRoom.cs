using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow
{
    public partial class FrmRoom : Form

    {
        private DialogResult _isOk;
        private Room _room;
        public Room room
        {
            get
            {
                if (_room == null)
                {
                    _room = new Room();
                }
                //todo# 教官寫的
                //_room.RoomID = Convert.ToInt32(fbRoomID.fieldValue);
                //_room.HotelID = Convert.ToInt32(fbHotelID.fieldValue);
                //_room.RoomName = fbRoomName.fieldValue;
                //_room.RoomPrice = Convert.ToDecimal(fbRoomPrice.fieldValue);
                //_room.RoomTypeID = Convert.ToInt32(fbRoomTypeID.fieldValue);
                //_room.Description = fbDescription.fieldValue;
                return _room;
            }
            set
            {
                _room = value;
                //fbRoomID.fieldValue = _room.RoomID.ToString();
                //fbHotelID.fieldValue = _room.HotelID.ToString();
                //fbRoomName.fieldValue = _room.RoomName;
                //fbRoomPrice.fieldValue = _room.RoomPrice.ToString();
                //fbRoomTypeID.fieldValue = _room.RoomTypeID.ToString();
                //fbDescription.fieldValue = _room.Description;

            }
        }
        public DialogResult isOk
        {
            get
            {
                return _isOk;
            }
        }


        public FrmRoom()
        {
            InitializeComponent();
        }

        private void FrmRoom_Load(object sender, EventArgs e)
            //寫死示意而已
        {
            string s1 = "✔1張加寬特大床\r\n✔客房面積：20平方公尺/215平方英尺\r\n✔景觀： 市景\r\n✔禁菸房\r\n✔淋浴設備\r\n✔免費Wi-Fi";
            memoBox1.mbValue = s1;

            string s2 = "✔吹風機\r\n✔浴巾\r\n✔盥洗用品";
            memoBox2.mbValue = s2;

            string s3 = "✔房內免費Wi-Fi\r\n✔電話\r\n✔衛星頻道\r\n✔有線電視";
            memoBox3.mbValue = s3;

            string s4 = "✔舒眠用品\r\n✔寢具用品\r\n✔遮光窗簾\r\n✔轉接器";
            memoBox4.mbValue = s4;

            string s5 = "✔冰箱\r\n✔免費即溶咖啡\r\n✔免費茶包\r\n✔免費瓶裝水";
            memoBox5.mbValue = s5;
        }
    }
}
