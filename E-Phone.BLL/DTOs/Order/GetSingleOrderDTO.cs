using E_Phone.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.DTOs.Order
{
    public class GetSingleOrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderCondition OrderCondition { get; set; }
        public int OrderCount { get; set; }
        public int UserId { get; set; }
    }
}
