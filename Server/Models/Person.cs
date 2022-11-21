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
        public byte[] Password { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public int RolePersonId { get; set; }
        public int AccountStatusId { get; set; }

        public virtual PersonStatus AccountStatus { get; set; } = null!;
        public virtual Role RolePerson { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
