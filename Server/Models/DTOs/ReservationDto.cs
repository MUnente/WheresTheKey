using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class ReservationDto
    {
        [Required]
        public int PlaceId { get; set; }
        [Required]
        [RegularExpression(@"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})", ErrorMessage = "O id não está num formato valido.")]
        public string StartDate { get; set; }
        [Required]
        [RegularExpression(@"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})", ErrorMessage = "O id não está num formato valido.")]
        public string EndDate { get; set; }
    }
}