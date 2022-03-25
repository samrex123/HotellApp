using System;
using System.Collections.Generic;

namespace HotelSite.Models
{
    public partial class Roomtype
    {
        public Roomtype()
        {
            Rooms = new HashSet<Room>();
        }

        public short Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public short Qtybeds { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
