using System;
using System.Collections.Generic;

namespace HotelSite1.Models
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public short Id { get; set; }
        public short Roomnum { get; set; }
        public short Roomtypesid { get; set; }

        public virtual Roomtype Roomtypes { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
