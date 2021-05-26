using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JordanSky.Entity
{
   public class Mazr3a
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_owner { get; set; }
        public string Phone_owner { get; set; }
        public string Name_guard { get; set; }
        public string Phone_guard { get; set; }
        public double Price { get; set; }
        public string Number { get; set; }
        [ForeignKey("city")]
        public int City_id { get; set; }
        public string Max_people { get; set; }
        public string No_bedroom { get; set; }
        public string Space { get; set; }
        public string No_bed { get; set; }
        public string internet { get; set; }
        public string pool { get; set; }
        public string Zarb { get; set; }
        public string grill { get; set; }
        public string pool_heating { get; set; }
        public string AC { get; set; }
        public string Children { get; set; }
        public string View { get; set; }
        public string Parking { get; set; }
        public string Playground { get; set; }
        [NotMapped]
        public string defaultImage { get; set; }

        [ForeignKey("product")]
        public int? Type_Product_id { get; set; }
        public string Location { get; set; }
        public string Ref_No { get; set; }
        public string Description { get; set; }
        public City city { get; set; }
        public Type_Product product { get; set; }

        public List<Image> images { get; set; }
        public List<Booking> bookings { get; set; }
        public List<Cate_Mazra3a> cate_s { get; set; }




    }
}
