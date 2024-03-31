using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunNow.BackSide_Hotel.Models
{
    public class Hotel
    {
        private string _hotelName;
        public string hotelName
        {
            get { return _hotelName; }
            set { _hotelName = value; }
        }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string Region { get; set; }
        public string Description { get; set; }


    }

}
