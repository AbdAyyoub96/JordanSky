using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
    public class Image_Package
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey("_Package")]
        public int Package_id { get; set; }

        public Internal_Package _Package { get; set; }
    }
}