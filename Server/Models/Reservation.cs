using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string PersonId { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ReservationStatus { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual Place Place { get; set; } = null!;
    }
}
