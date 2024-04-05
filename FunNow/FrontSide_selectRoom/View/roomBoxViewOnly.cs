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

namespace FunNow
{
    

    public partial class roomBoxViewOnly : UserControl
    {
        
        //痊癒變數+屬性-------------------------------------------------
        private Room _room;
        public Room room
        {
            get { return _room; }
            set
            {
                _room = value;
                lblName.Text = _room.RoomName;
                lblPrice.Text = "$" + _room.RoomPrice.ToString();
            }
        }

        public roomBoxViewOnly()
        {
            InitializeComponent();
            splitContainer3.Panel1Collapsed = true;
        }

        //-------------------------------------------------------------
        private void roomBoxViewOnly_Load(object sender, EventArgs e)
        {
            dbFunNow db = new dbFunNow();
            var pictures = from p in db.RoomImage
                           where p.RoomID == room.RoomID
                           select p.RoomImage1;

            foreach (var pic in pictures)
            {

                if (!string.IsNullOrEmpty(pic))
                {
                    if (File.Exists(pic))
                    {
                        using (FileStream fs = new FileStream(pic, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            PictureBox pb = new PictureBox();
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            pb.Width = 100;
                            pb.Height = 50;
                            pb.Image = img;
                            pb.Click += Pb_Click;
                            flowLayoutPanel1.Controls.Add(pb);

                            if (flowLayoutPanel1.Controls.Count == 1)
                            {
                                pictureBox1.Image = pb.Image;
                            }
                        }
                    }
                }
            }
        }
        private void Pb_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                pictureBox1.Image = clickedPictureBox.Image;
            }
        }
    }
}
