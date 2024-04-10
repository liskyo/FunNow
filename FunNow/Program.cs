using Fun;
using FunNow.BackSide_Hotel;
using FunNow.BackSide_Order;
using FunNow.BackSide_POS;
using FunNow.Comment;
using prjFunNowMember;
using prjFunNowMember.View;

//using FunNow.BackSide_Hotel;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunNow
{
    internal static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Environment.OSVersion.Version.Major >= 6) { SetProcessDPIAware(); }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            Application.Run(new FrmLogin());
            //Application.Run(new FrmPOS());
            //Application.Run(new FrmMain());
            //Application.Run(new Frm());
            //Application.Run(new FrmMemberList());

            //Application.Run(new FrmCommentList());

            //Application.Run(new FrmHotelLikeBack());
           






        }
    }
}
