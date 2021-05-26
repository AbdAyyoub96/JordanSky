using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
   public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        [ForeignKey("mazr3")]
        public int Mazra3a_id { get; set; }
        public Mazr3a mazr3  { get; set; }

    }
}
