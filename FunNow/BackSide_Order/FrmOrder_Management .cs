using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Order
{
    public partial class Frm : Form
    {
        public Frm()
        {
            InitializeComponent();
        }
        private void queryAll()
        {
            dbFunNow db = new dbFunNow();
            var orders = from order in db.Order
                         join member in db.Member on order.MemberID equals member.MemberID
                         join orderDetail in db.OrderDetails on order.OrderID equals orderDetail.OrderID
                         join room in db.Room on orderDetail.RoomID equals room.RoomID
                         join hotel in db.Hotel on room.HotelID equals hotel.HotelID // 新增連接 Hotel 資料表
                         select new
                         {
                             OrderID = order.OrderID,
                             MemberID = order.MemberID,
                             MemberName = member.Name,
                             HotelName = hotel.HotelName, // 新增飯店名稱欄位
                             RoomID = orderDetail.RoomID,
                             RoomName = room.RoomName,
                             CheckInDate = orderDetail.CheckInDate,
                             CheckOutDate = orderDetail.CheckOutDate,
                             OrderStatusID = order.OrderStatusID,
                             PaymentStatusID = order.PaymentStatusID,
                             TotalPrice = order.TotalPrice,
                             CouponID = order.CouponID,
                             CreatedAt = order.CreatedAt
                         };

            dataGridView1.DataSource = orders.ToList();
            resetGridStyle();
        }
        private void resetGridStyle()
        {
            dataGridView1.Columns[0].HeaderText = "訂單號碼";
            dataGridView1.Columns[1].HeaderText = "會員編號";
            dataGridView1.Columns[2].HeaderText = "會員名稱";
            dataGridView1.Columns[3].HeaderText = "飯店名稱";
            dataGridView1.Columns[4].HeaderText = "房間ID";
            dataGridView1.Columns[5].HeaderText = "房間名稱"; // 新增房間名稱欄位
            dataGridView1.Columns[6].HeaderText = "可入住時間";
            dataGridView1.Columns[7].HeaderText = "可退房時間";
            dataGridView1.Columns[8].HeaderText = "折價券";
            dataGridView1.Columns[9].HeaderText = "付款狀態";
            dataGridView1.Columns[10].HeaderText = "總價";
            dataGridView1.Columns[11].HeaderText = "折價券";
            dataGridView1.Columns[12].HeaderText = "訂單時間";
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width =100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].Width = 80;
            dataGridView1.Columns[9].Width = 80;
            dataGridView1.Columns[10].Width = 80;
            dataGridView1.Columns[11].Width = 80;
            dataGridView1.Columns[12].Width = 200;

            //for (int i = 12; i < dataGridView1.Columns.Count; i++)
            //{
            //    dataGridView1.Columns[i].Visible = false; // 將 column12 以後的列隱藏
            //}


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

        private void Frm_Load(object sender, EventArgs e)
        {
            queryAll();
            Membercomboxquery();

        }
       

        private void Membercomboxquery()
        {
            // 先清空 comboBox 的項目
            MembercomboBox.Items.Clear();

            // 從 dataGridView1 中獲取會員名稱資料行的所有資料
            List<string> memberNames = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string memberName = row.Cells["MemberName"].Value.ToString();
                if (!string.IsNullOrEmpty(memberName) && !memberNames.Contains(memberName))
                {
                    memberNames.Add(memberName);
                }
            }

            // 將會員名稱資料添加到 comboBox 中
            MembercomboBox.Items.AddRange(memberNames.ToArray());
        }

        private void Frm_Activated(object sender, EventArgs e)
        {
            resetGridStyle();

        }
        private void editById(int id)
        {
            dbFunNow db = new dbFunNow();
            Order order = db.Order.FirstOrDefault(x => x.OrderID == id);
            if (order == null)
                return;
            // 查詢訂單詳細資料
            OrderDetails orderDetails = db.OrderDetails.FirstOrDefault(x => x.OrderID == id);

            FrmOrder f = new FrmOrder();
            f.order = order;
            f.orderdetails = orderDetails; // 傳遞訂單詳細資料
            f.ShowDialog();

            if (f.isOk != DialogResult.OK)
                return;
            order.OrderID = f.order.OrderID;
            order.MemberID = f.order.MemberID;
            orderDetails.MemberID = f.order.MemberID;

            order.OrderStatusID = f.order.OrderStatusID;
            order.PaymentStatusID = f.order.PaymentStatusID;
            order.TotalPrice = f.order.TotalPrice;
            order.CouponID = f.order.CouponID;
            order.CouponID = f.order.CouponID;

            // 更新訂單詳細資料

            if (orderDetails != null)
            {
                orderDetails.RoomID = f.orderdetails.RoomID;
                orderDetails.CheckInDate = f.orderdetails.CheckInDate;
                orderDetails.CheckOutDate = f.orderdetails.CheckOutDate;
                orderDetails.isOrdered = f.orderdetails.isOrdered;
            }
            db.SaveChanges();
            queryAll();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            editById(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmOrder f = new FrmOrder();
            // 設定表單的 CreatedAtBox 的預設值為當前時間
            //f.CreatedAtBox.fileValue = dateTimeVariable.ToString(); // 使用 ToString() 方法將 DateTime 轉換為字串



            f.ShowDialog();
            if (f.isOk != DialogResult.OK)
                return;

            // 創建新的 Order 物件，不包含 OrderID 資訊
            Order x = new Order
            {
                MemberID = f.order.MemberID,
                OrderStatusID = f.order.OrderStatusID,
                PaymentStatusID = f.order.PaymentStatusID,
                TotalPrice = f.order.TotalPrice,
                CouponID = f.order.CouponID,
                CreatedAt = f.order.CreatedAt
            };

            dbFunNow db = new dbFunNow();
            db.Order.Add(x);
            db.SaveChanges(); // 先保存訂單以取得訂單ID

            // 新增相關的訂單詳細資料
            // 取得保存後的訂單ID
            // 創建新的 OrderDetails 物件，包含 OrderID 資訊
            OrderDetails y = new OrderDetails
            {
                OrderID = x.OrderID, // 使用剛剛新增的 Order 物件的 OrderID
                MemberID = f.orderdetails.MemberID,
                RoomID = f.orderdetails.RoomID,
                CheckInDate = f.orderdetails.CheckInDate,
                CheckOutDate = f.orderdetails.CheckOutDate,
                isOrdered = f.orderdetails.isOrdered
            };

            // 新增相關的訂單詳細資料
            dbFunNow db2 = new dbFunNow();
            db2.OrderDetails.Add(y);
            db2.SaveChanges();
           
            queryAll();
        }

        private void MembercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            queryAll();
            string selectedMemberName = MembercomboBox.SelectedItem.ToString();

            // 從資料來源中篩選出符合條件的資料
            var filteredOrders = ((IEnumerable<object>)dataGridView1.DataSource)
                                    .Cast<object>()
                                    .Where(row => (string)row.GetType().GetProperty("MemberName").GetValue(row, null) == selectedMemberName)
                                    .ToList();

            // 清除 DataGridView 中的所有資料行
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            // 將符合條件的資料行重新設置為 DataGridView 的資料來源
            dataGridView1.DataSource = filteredOrders;

            // 重新設定反黃行的顏色
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.Yellow; // 將符合條件的行反黃
            }
            resetGridStyle();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmOrder_Analysis analysisForm = new FrmOrder_Analysis();
            analysisForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmOrder f = new FrmOrder();
            f.label2.Visible = true;
            f.OrderIDcomboBox.Visible = true;

            // 先清空 OrderIDcomboBox 控制元件的項目
            f.OrderIDcomboBox.Items.Clear();

            string selectedOrderID = string.Empty; // 定義一個變數來保存所選擇的訂單號碼

            // 從 dataGridView1 中獲取訂單號碼資料行的所有資料
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string orderID = row.Cells["OrderID"].Value.ToString();
                if (!string.IsNullOrEmpty(orderID) && !f.OrderIDcomboBox.Items.Contains(orderID))
                {
                    f.OrderIDcomboBox.Items.Add(orderID);
                    selectedOrderID = orderID; // 將所選擇的訂單號碼保存到變數中
                }
            }

            f.ShowDialog();
            if (f.isOk != DialogResult.OK)
                return;

            // 新增相關的訂單詳細資料
            dbFunNow db2 = new dbFunNow();
            OrderDetails y = new OrderDetails
            {
                OrderID = Convert.ToInt32(selectedOrderID), // 使用所選擇的訂單號碼
                MemberID = f.orderdetails.MemberID,
                RoomID = f.orderdetails.RoomID,
                CheckInDate = f.orderdetails.CheckInDate,
                CheckOutDate = f.orderdetails.CheckOutDate,
                isOrdered = f.orderdetails.isOrdered
            };

            db2.OrderDetails.Add(y);
            db2.SaveChanges();
            queryAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 確保至少選擇了一行
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("請先選擇要刪除的訂單。");
                return;
            }

            // 獲取所選擇的行的訂單號碼
            int selectedOrderID = (int)dataGridView1.SelectedRows[0].Cells["OrderID"].Value;

            // 從資料庫中刪除該訂單的相關訂單詳細資料
            using (dbFunNow db = new dbFunNow())
            {
                OrderDetails orderDetailToDelete = db.OrderDetails.FirstOrDefault(od => od.OrderID == selectedOrderID);
                if (orderDetailToDelete != null)
                {
                    db.OrderDetails.Remove(orderDetailToDelete);
                    db.SaveChanges();
                }
            }

            // 刷新 DataGridView
            queryAll();
        }
    }
    }
