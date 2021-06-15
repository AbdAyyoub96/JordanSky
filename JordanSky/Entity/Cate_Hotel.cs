using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
    public class Cate_Hotel
    {
        public int Id { get; set; }

        [ForeignKey("hotel")]
        public int Hotel_id { get; set; }

        [ForeignKey("category")]
        public int Category_id { get; set; }

        public Hotel hotel { get; set; }
        public Category category { get; set; }
    }
}