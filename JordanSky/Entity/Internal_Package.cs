using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace JordanSky.Entity
{
    public class Internal_Package
    {
        public int Id { get; set; }//done
        public string Name_Pakage { get; set; }//done
        [ForeignKey("_Package")]
        public int Type_id { get; set; }
        public string Phone { get; set; }//done
        public string Ref_No { get; set; }//done
        public double Price { get; set; } //done
        public string Picture { get; set; }//done
        public double Child_Price { get; set; }//done
        public string Note_price {get;set; }//done
        public int Transportation { get; set; }//done
        public int overnight_Stay { get; set; } //done
        public string overnight_Stay_name { get; set; } //done
        public int Food { get; set; } //done
        public int name_restaurant { get; set; } //done
        public string No_Day { get; set; } //done
        public string StartDate { get; set; } //done
        public string EndDate { get; set; } //done
        public string Description { get; set; } //done
        public int Status { get; set; }//done
        public List<Image_Package> images { get; set; }//done
        public List<Booking_package> bookings { get; set; }//done
        public Type_packge _Package { get; set; }//done



    }
}