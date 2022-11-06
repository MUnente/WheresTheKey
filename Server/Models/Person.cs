using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class Person
    {
        public Person()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RolePerson { get; set; }
        public int AccountStatus { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
