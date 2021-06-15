using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace JordanSky.Entity
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Ref_No { get; set; }
        public string Name { get; set; }
        public double Price_Day { get; set; }
        [NotMapped]
        public string defaultImage { get; set; }
        public string Number { get; set; }
        public string Maps { get; set; }
        [ForeignKey("city")]
        public int City_id { get; set; }
        public City city { get; set; }

        [ForeignKey("Hotel_type")]
        public int? Type_Hotel_id { get; set; }
        public Type_Hotel Hotel_type { get; set; }

        [AllowHtml]
        [UIHint("RichEditor")]
        public string Description { get; set; }
        public List<Image_Hotel> images { get; set; }
        public List<Booking_Hotel> bookings { get; set; }
        public List<Cate_Hotel> cate_Hotels { get; set; }
    }
}