using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.Core.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public string ModelName { get; set; }

        //Navigasyonlar
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<Version> Versions { get; set; }
    }
}
