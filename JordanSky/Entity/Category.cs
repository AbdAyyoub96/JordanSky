using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JordanSky.Entity
{
   public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Cate_Mazra3a> cate_s { get; set; }
    }
}
