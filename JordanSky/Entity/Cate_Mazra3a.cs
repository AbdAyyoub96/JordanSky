using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
   public class Cate_Mazra3a
    {
        public int Id { get; set; }

        [ForeignKey("mazr3")]
        public int Mazra3a_id { get; set; }

        [ForeignKey("category")]
        public int Category_id { get; set; }

        public Mazr3a mazr3 { get; set; }
        public Category category { get; set; }
    }
}
