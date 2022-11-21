using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class PersonStatus
    {
        public PersonStatus()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; }
    }
}
