using System;
using System.Collections.Generic;

namespace Server.Models
{
    public partial class PlaceType
    {
        public PlaceType()
        {
            Places = new HashSet<Place>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Place> Places { get; set; }
    }
}
