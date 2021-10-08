using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Display(Name = "Tipos de Vehículo")]
        [MaxLength(50, ErrorMessage = "EL campo {0} solo acepta hasta {1} caracteres")]
        [Required]
        public string Description { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
