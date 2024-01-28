using E_Phone.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.Core.Entities
{
    public class Order 
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderCondition OrderCondition { get; set; }
        public int OrderCount { get; set; }

        //Navigasyonlar
        public int VersionId { get; set; }
        public Version Version { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
