using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Id é um campo requerido.")]
        [RegularExpression("^[(a-zA-Z)]{2}[(0-9)]{7}", ErrorMessage = "O id não está num formato valido.")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Password é um campo requerido.")]
        public string Password { get; set; }
    }
}