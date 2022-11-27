using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class PlaceDto
    {
        [Required]
        public string Description { get; set; }
    }
}