using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JordanSky.Entity
{
    public class Starting_locations
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Booking_package> _Packages { get; set; }
    }
}
