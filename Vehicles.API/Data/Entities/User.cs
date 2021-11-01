using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vehicles.Common.Enums;

namespace Vehicles.API.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LastName { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Document { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public string ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => string.IsNullOrEmpty(ImageId)
        ? $"https://localhost:44340/images/noimage.png"
        : $"https://localhost:44340/images/users/{ImageId}";

        [Display(Name = "Tipo de Usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName}{LastName}";

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DocumentType DocumentType { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        [Display(Name = "# Vehículos")]
        public int VehiclesCount => Vehicles == null ? 0 : Vehicles.Count;

        //public ICollection<History> Histories { get; set; }
    }
}
