using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class PlaceDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}