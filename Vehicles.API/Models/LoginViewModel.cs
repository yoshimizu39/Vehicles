using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Introducir un email válido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener un longitud mínima de {1} caracteres")]
        [Required(ErrorMessage = "El camp {0} es requerido")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool Rememberme { get; set; }
    }
}
