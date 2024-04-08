using FunNow.BackSide_POS;
using FunNow.Comment.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.Comment
{
    public partial class FrmCommentList : Form
    {
      
        private readonly dbFunNow db = new dbFunNow();
        
        public FrmCommentList()
        {
            InitializeComponent();
        }

        private void FrmCommentList_Load(object sender, EventArgs e)
        {
            queryAll();
        }

      
           private void queryAll()
        {

            var comments = from c in db.CommentRate
                           join m in db.Member on c.MemberID equals m.MemberID
                           join h in db.Hotel on c.HotelID equals h.HotelID
                           select new
                           {
                               c.CommentID,
                               h.HotelName,
                               m.Name,
                               c.Rating,
                               c.Description,
                               c.CreatedAt
                           };

            dataGridView1.DataSource = comments.ToList();
            resetGridStyle();
        }

        private void resetGridStyle()
        {
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 250;
            dataGridView1.Columns[5].Width = 250;

            dataGridView1.Columns[0].HeaderText = "留言編號";
            dataGridView1.Columns[1].HeaderText = "飯店名稱";
            dataGridView1.Columns[2].HeaderText = "會員名稱";
            dataGridView1.Columns[3].HeaderText = "評分";
            dataGridView1.Columns[4].HeaderText = "評論內容";
            dataGridView1.Columns[5].HeaderText = "評論時間";

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 11, FontStyle.Bold);


            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.White;
                r.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
            }

        }

        private void FrmCommentList_Activated(object sender, EventArgs e)
        {
            resetGridStyle();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
                return;
            DataGridViewRow r = dataGridView1.SelectedRows[0];
            if (r.Cells.Count <= 0)
                return;
            DataGridViewCell c = r.Cells[0];
            int id = (int)c.Value;

            CommentRate cr = db.CommentRate.FirstOrDefault(x => x.CommentID == id);
            if (cr == null)
                return;
            db.CommentRate.Remove(cr);
            db.SaveChanges();

            queryAll();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            bool isColorChanged = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                isColorChanged = !isColorChanged;
                r.DefaultCellStyle.BackColor = Color.FromArgb(229, 229, 229);
                if (isColorChanged)
                    r.DefaultCellStyle.BackColor = Color.White;

                foreach (DataGridViewCell c in r.Cells)
                {
                    if (c.Value == null)
                        continue;
                    if (c.Value.ToString().Contains(toolStripTextBox1.Text))
                    {
                        r.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    }
                }
            }
        }
    }
}
