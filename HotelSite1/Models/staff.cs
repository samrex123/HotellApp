using System;
using System.Collections.Generic;

namespace HotelSite1.Models
{
    public partial class staff
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
