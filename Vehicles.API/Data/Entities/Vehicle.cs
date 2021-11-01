using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Vehicles.API.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public ICollection<VehiclePhoto> VehiclePhotos { get; set; }

        public ICollection<History> Histories { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1900, 3000, ErrorMessage = "Valor de modelo no válido")]
        public int Model { get; set; }

        [Display(Name = "Placa")]
        [RegularExpression(@"[a-zA-Z]{3}[0-9]{2}[a-zA-Z0-9]", ErrorMessage = "Formato de placa incorrecto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener {1} caracteres")]
        public string Plaque { get; set; }

        [Display(Name = "Línea")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string Line { get; set; }

        [Display(Name = "Color")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
        public string Color { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "# Fotos")]
        public int VehiclePhotosCount => VehiclePhotos == null ? 0 : VehiclePhotos.Count;

        [Display(Name = "# Hisorias")]
        public int HistoriesCount => Histories == null ? 0 : Histories.Count;

        [Display(Name = "Foto")]
        public string ImageFullPath => VehiclePhotos == null || VehiclePhotos.Count == 0
            ? $"https://localhost:44340/images/NoImage.png"
            : VehiclePhotos.FirstOrDefault().ImageFullPath;

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public User User { get; set; }

        [Display(Name = "Tipo de Vehículo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public VehicleType VehicleType { get; set; }

        [Display(Name = "Tipo de Vehículo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Brand Brand { get; set; }


    }
}
