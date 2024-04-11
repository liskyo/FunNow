using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow.BackSide_Order.View
{
    public partial class MAP : Form
    {
        public MAP()
        {
            InitializeComponent();
            LoadHtmlIntoWebBrowser();
        }
        private void LoadHtmlIntoWebBrowser()
        {
            chromiumWebBrowser1.Visible = true;
            string powerBiMapEmbedLink = "https://app.powerbi.com/view?r=eyJrIjoiOWRkNzY5Y2QtODAxYS00MDE5LTkxN2UtYzE5Y2U0ZWU2YjBmIiwidCI6IjcwODk3ZDZmLTBhNDgtNDlkZS04ODBmLTI3ZDhhZDQ1ZDc2ZSIsImMiOjEwfQ%3D%3D";
            chromiumWebBrowser1.Load(powerBiMapEmbedLink);
        }
    }
}
