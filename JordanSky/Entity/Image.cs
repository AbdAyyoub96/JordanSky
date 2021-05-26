using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace JordanSky.Entity
{
   public class Image
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [ForeignKey("mazr")]
        public int Mazr3a_id { get; set; }

        public Mazr3a mazr { get; set; }
    }
}
