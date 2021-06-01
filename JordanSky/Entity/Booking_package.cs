using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
    public class Booking_package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int No_Pepole { get; set; }
        public int No_Child { get; set; }
        public double TotalPrice { get; set; }
        [ForeignKey("_Locations")]
        public int Start_Id { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        [ForeignKey("_Package")]
        public int Package_id { get; set; }
        public Internal_Package _Package { get; set; }
        public Starting_locations _Locations { get; set; }
    }
}