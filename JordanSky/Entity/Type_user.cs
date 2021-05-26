using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JordanSky.Entity
{
   public class Type_user
    {
        public int id { get; set; }
        public string Type { get; set; }


        public List<User> users { get; set; }


    }
}
