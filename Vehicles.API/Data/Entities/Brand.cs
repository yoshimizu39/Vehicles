using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Marca")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe exceder los {1} caracteres")]
        [Required(ErrorMessage = "El ingreso de datos es obligatorio")]
        public string Description { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
