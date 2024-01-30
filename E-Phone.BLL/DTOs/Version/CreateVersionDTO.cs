using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.DTOs.Version
{
    public class CreateVersionDTO
    {
        public int StorageCapacity { get; set; }
        public decimal price { get; set; }
        public int Stock { get; set; }
    }
}
