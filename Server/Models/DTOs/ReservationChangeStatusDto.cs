using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class ReservationChangeStatusDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ReservationStatusId { get; set; }
    }
}