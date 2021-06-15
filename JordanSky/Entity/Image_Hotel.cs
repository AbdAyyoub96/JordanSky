using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
    public class Image_Hotel
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey("hotel")]
        public int Hotel_id { get; set; }
        public Hotel hotel { get; set; }
    }
}