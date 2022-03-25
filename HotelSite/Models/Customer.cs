using System;
using System.Collections.Generic;

namespace HotelSite.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
        }

        public long Id { get; set; }
        public short Customertypesid { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Streetadress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string? Ice { get; set; }
        public DateTime? Lastupdated { get; set; }

        public virtual Customertype Customertypes { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
