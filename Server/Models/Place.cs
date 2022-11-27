using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Place
    {
        public Place()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
