using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace FunNow.BackSide_Order
{
    public partial class FrmOrder_Analysis : Form
    {
        public FrmOrder_Analysis()
        {
            InitializeComponent(); LoadData();
            // 將 AnalysiscomboBox 預設選擇第一個資料
            AnalysiscomboBox.SelectedIndex = 0;
        }

        private void LoadData()
        {
            ShowTotalSalesAnalysis();
        }

        private void ShowTotalSalesAnalysis()
        {
            using (dbFunNow db = new dbFunNow())
            {
                // 查詢每個飯店的總銷售額
                var hotelSales = from order in db.Order
                                 join orderDetail in db.OrderDetails on order.OrderID equals orderDetail.OrderID
                                 join room in db.Room on orderDetail.RoomID equals room.RoomID
                                 join hotel in db.Hotel on room.HotelID equals hotel.HotelID
                                 group order by hotel.HotelName into g
                                 select new
                                 {
                                     飯店名稱 = g.Key,
                                     總營業額 = g.Sum(x => x.TotalPrice)
                                 };

                // 將營業額資料設定為 dataGridView1 的資料來源
                dataGridView1.DataSource = hotelSales.ToList();

                // 設定 DataGridView 的樣式
                dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(66, 204, 156);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.RowHeadersVisible = false;

                // 使用圓餅圖顯示營業額分佈
                chart1.Series.Clear();
                chart1.Series.Add(new Series
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true
                });

                foreach (var sale in hotelSales)
                {
                    chart1.Series[0].Points.AddXY(sale.飯店名稱, sale.總營業額);
                }

                // 設定圓餅圖的樣式
                chart1.BackColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.Transparent;
                chart1.Series[0].Palette = ChartColorPalette.Pastel;
                chart1.Series[0].Font = new Font("微軟正黑體", 10);
                chart1.Legends[0].Font = new Font("微軟正黑體", 10);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根據選擇的索引執行不同的分析
            switch (AnalysiscomboBox.SelectedIndex)
            {
                case 0:
                    ShowTotalSalesAnalysis();
                    break;
                case 1:
                    ShowHotelOrderquantityAnalysis();
                    break;
                case 2:
                    ShowCouponUsageRateAnalysis();
                    break;
                default:
                    // 若索引未匹配到任何分析，則顯示預設分析
                    ShowTotalSalesAnalysis();
                    break;
            }
        }
        private void ShowHotelOrderquantityAnalysis()
        {
            using (dbFunNow db = new dbFunNow())
            {

                // 查詢每個飯店的訂單量
                var hotelquantitySales = from order in db.Order
                                         join orderDetail in db.OrderDetails on order.OrderID equals orderDetail.OrderID
                                         join room in db.Room on orderDetail.RoomID equals room.RoomID
                                         join hotel in db.Hotel on room.HotelID equals hotel.HotelID
                                         group new { order, orderDetail, room, hotel } by new { hotel.HotelID, hotel.HotelName } into g
                                         select new
                                         {
                                             HotelID = g.Key.HotelID,
                                             HotelName = g.Key.HotelName,
                                             TotalSales = g.Select(x => x.order.OrderID).Distinct().Count() // 獲取每個飯店的訂單數量
                                         };

                // 將營業額資料設定為 dataGridView1 的資料來源
                dataGridView1.DataSource = hotelquantitySales.ToList();

                // 設定 dataGridView1 中的列標題為中文
                dataGridView1.Columns["HotelID"].HeaderText = "飯店ID";
                dataGridView1.Columns["HotelName"].HeaderText = "飯店名稱";
                dataGridView1.Columns["TotalSales"].HeaderText = "總訂單量";

                // 設定 DataGridView 的樣式
                dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 12);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
                dataGridView1.DefaultCellStyle.BackColor = Color.White;
                dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(66, 204, 156);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.RowHeadersVisible = false;

                var dailyOrderQuantitiesFromDb = from order in db.Order
                                                 group order by DbFunctions.TruncateTime(order.CreatedAt) into g
                                                 select new
                                                 {
                                                     Date = DbFunctions.TruncateTime(g.Key),
                                                     OrderCount = g.Count()
                                                 };

                // 將結果集載入到內存中
                var dailyOrderQuantities = dailyOrderQuantitiesFromDb.ToList();

                // 使用折線圖顯示每日的訂單量變化趨勢
                chart1.Series.Clear();
                chart1.Series.Add(new Series
                {
                    Name = "總訂單量",
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.DateTime,
                    YValueType = ChartValueType.Int32
                });

                // 添加資料到折線圖中
                foreach (var dailyOrderQuantity in dailyOrderQuantities)
                {
                    chart1.Series["總訂單量"].Points.AddXY(dailyOrderQuantity.Date, dailyOrderQuantity.OrderCount);
                }

                // 設定折線圖的樣式
                chart1.BackColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.Transparent;
                chart1.Series[0].Color = Color.Blue;
                chart1.Series[0].BorderWidth = 2;
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd/yyyy"; // 設定X軸日期格式
                chart1.Series[0].XValueType = ChartValueType.DateTime; // 設定X軸值類型為日期時間
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            }
        }

        private void ShowCouponUsageRateAnalysis()
        {
            using (dbFunNow db = new dbFunNow())
            {
                // 查詢折扣券資料
                var coupons = db.Coupon.ToList();

                // 在 DataGridView 中顯示折扣券資料
                dataGridView1.DataSource = coupons.Select(c => new
                {
                    CouponName = c.CouponName,
                    CouponDescription = c.CouponDescription,
                    Discount = c.Discount
                }).ToList();

                // 查詢已使用的折扣券資料
                var usedCoupons = from order in db.Order
                                  where order.CouponID != null
                                  group order by order.Coupon.CouponName into g
                                  select new
                                  {
                                      CouponName = g.Key,
                                      UsageCount = g.Count()
                                  };

               

                // 使用圓餅圖顯示折扣券使用率
                chart1.Series.Clear();
                chart1.Series.Add(new Series
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true
                });

                // 在圓餅圖中添加使用率資料
                foreach (var coupon in usedCoupons)
                {
                    chart1.Series[0].Points.AddXY($"{coupon.CouponName} (已使用)", coupon.UsageCount);
                }

                // 設定圓餅圖的樣式
                chart1.BackColor = Color.White;
                chart1.ChartAreas[0].BackColor = Color.Transparent;
                chart1.Series[0].Palette = ChartColorPalette.Pastel;
                chart1.Series[0].Font = new Font("微軟正黑體", 10);
                chart1.Legends[0].Font = new Font("微軟正黑體", 10);
            }
        }

       
        
    }
}
