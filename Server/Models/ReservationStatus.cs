using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class ReservationStatus
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
