using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JordanSky.Entity
{
    public class Type_Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Hotel> hotels { get; set; }
    }
}