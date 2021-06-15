using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
    public class Booking_Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        [ForeignKey("hotel")]
        public int Hotel_id { get; set; }
        public Hotel hotel { get; set; }
    }
}