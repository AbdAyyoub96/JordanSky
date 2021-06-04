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
        [Required(ErrorMessage = "الرجاء ادخال الإسم")]
        public string Name { get; set; }
        [Required(ErrorMessage = "الرجاء ادخال رقم الهاتف")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "اختر المحافظة")]
        [ForeignKey("city")]
        public int City_id { get; set; }
        [Required(ErrorMessage = "ادخل الموقع")]
        public string Location { get; set; }
        [Required(ErrorMessage = "ارفق رخصة المنشأه")]
        public string license { get; set; }
        [Required(ErrorMessage = "ارفق صورة المنشأه")]
        public string Picture { get; set; }
        public int Status { get; set; }
        public City city { get; set; }
    }
}
