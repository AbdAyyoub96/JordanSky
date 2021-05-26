using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JordanSky.Entity
{
   public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string Msg { get; set; }
        public int Status { get; set; }
    }
}
