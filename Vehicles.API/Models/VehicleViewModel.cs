﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

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

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Tipo de Vehículo")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccionar un tipo de vehículo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int VehicleTypeId { get; set; }

        [Display(Name = "Tipo de Marca")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccionar una marca")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int BrandId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }

        public ICollection<VehiclePhoto> VehiclePhotos { get; set; }

    }
}
