using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        //Navigasyonlar
        public List<Model> Models { get; set; }
    }
}
