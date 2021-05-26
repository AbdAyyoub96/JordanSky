using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JordanSky.Entity
{
    public class Type_Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Mazr3a> mazr3As { get; set; }
    }
}