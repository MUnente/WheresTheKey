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
        public int? PlaceNumber { get; set; }
        public int PlaceTypeId { get; set; }

        public virtual PlaceType PlaceType { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
