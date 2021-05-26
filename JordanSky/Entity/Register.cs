using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
    public class Register
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [ForeignKey("city")]
        public int City_id { get; set; }
        public string Location { get; set; }
        public string license { get; set; }
        public string Picture { get; set; }
        public int Status { get; set; }
        public City city { get; set; }
    }
}
