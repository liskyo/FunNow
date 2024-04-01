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
        //痊癒變數+屬性-------------------------------------------------------------
        public Room selectedRoom {  get; set; }
        public FrmRoom()
        {
            InitializeComponent();
        }
        //load-----------------------------------------------------------------
        private void FrmRoom_Load(object sender, EventArgs e)
        {
            if (selectedRoom == null) 
                return;
            dbFunNow db = new dbFunNow();
            var equipments = from eq in db.Room_Equipment_Reference
                             where eq.RoomID == selectedRoom.RoomID 
                             select eq.RoomEquipment;

            foreach (var equip in equipments)
            {
                CheckBox c = new CheckBox();
                c.Text = equip.RoomEquipmentName.ToString();
                c.Checked = true;
                c.Enabled = false;
                c.Width = flowLayoutPanel2.Width;
                c.Height = 30;
                c.Font = new Font("微軟正黑體", 16, FontStyle.Regular);

                flowLayoutPanel2.Controls.Add(c);
            }

            lblName.Text = selectedRoom.RoomName;
            lblPrice.Text = "$" + selectedRoom.RoomPrice.ToString();
        }
    }
}
