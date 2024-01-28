using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.Core.Entities
{
    public class Version
    {
        public int Id { get; set; }
        public int StorageCapacity { get; set; }
        public decimal price { get; set; }
        public int Stock { get; set; }

        //Navigasyonlar
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public List<Order> Orders { get; set; }

    }
}
