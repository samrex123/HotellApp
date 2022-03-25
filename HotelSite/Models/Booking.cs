using System;
using System.Collections.Generic;

namespace HotelSite.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Rooms = new HashSet<Room>();
        }

        public long Id { get; set; }
        public long Customersid { get; set; }
        public short Qtypersons { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public DateTime? Eta { get; set; }
        public DateTime? Timearrival { get; set; }
        public DateTime? Timedeparture { get; set; }
        public string? Specialneeds { get; set; }
        public bool Extrabed { get; set; }

        public virtual Customer Customers { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
