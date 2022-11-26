using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class PlaceTypeDto
    {
        [Required]
        public string Description { get; set; }
    }
}