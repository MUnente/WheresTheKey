using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Reservation
    {
        [Required(ErrorMessage = "Id é um campo requerido.")]
        public int placeId { get; set; }
        [Required(ErrorMessage = "StartDate é um campo requerido.")]
        [RegularExpression(@"[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]", ErrorMessage = "StartDate não está num formato valido.")]
        public string startDate { get; set; }
        [Required(ErrorMessage = "EndDate é um campo requerido.")]
        [RegularExpression(@"[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]", ErrorMessage = "EndDate não está num formato valido.")]
        public string endDate { get; set; }
        public int reservationStatus { get; set; }

    }
}