using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FunNow.BackSide_Room
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
                _room.RoomID = Convert.ToInt32(fbRoomID.fieldValue);
                _room.HotelID = Convert.ToInt32(fbHotelID.fieldValue);
                _room.RoomName = fbRoomName.fieldValue;
                _room.RoomPrice = Convert.ToDecimal(fbRoomPrice.fieldValue);
                _room.RoomTypeID= Convert.ToInt32(fbRoomTypeID.fieldValue);
                _room.Description = fbDescription.fieldValue;
                return _room;
            }
            set
            {
                _room = value;
                fbRoomID.fieldValue = _room.RoomID.ToString();
                fbHotelID.fieldValue = _room.HotelID.ToString();
                fbRoomName.fieldValue = _room.RoomName;
                fbRoomPrice.fieldValue = _room.RoomPrice.ToString();
                fbRoomTypeID.fieldValue = _room.RoomTypeID.ToString();
                fbDescription.fieldValue = _room.Description;
  
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
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isOk = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isOk = DialogResult.OK;
            Close();
        }



    }
}
