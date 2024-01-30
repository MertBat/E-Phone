using E_Phone.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public int VersionId { get; set; }
        public OrderCondition OrderCondition { get; set; }
        public int OrderCount { get; set; }
    }
}
