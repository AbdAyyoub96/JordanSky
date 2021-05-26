using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JordanSky.Entity
{
   public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Mazr3a> mazr3As { get; set; }
        public List<Register> registers { get; set; }
    }
}
