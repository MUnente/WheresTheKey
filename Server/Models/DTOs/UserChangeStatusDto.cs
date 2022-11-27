using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class UserChangeStatusDto
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public int AccountStatusId { get; set; }
    }
}