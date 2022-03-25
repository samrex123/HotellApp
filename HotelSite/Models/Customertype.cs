using System;
using System.Collections.Generic;

namespace HotelSite.Models
{
    public partial class Customertype
    {
        public Customertype()
        {
            Customers = new HashSet<Customer>();
        }

        public short Id { get; set; }
        public string? Typename { get; set; }
        public decimal? Discount { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
