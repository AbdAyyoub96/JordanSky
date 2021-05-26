using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JordanSky.Entity
{
    public class Type_packge
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Internal_Package> _Packages { get; set; }

    }
}
