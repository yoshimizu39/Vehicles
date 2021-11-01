using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [Display(Name = "Foto")]
        public string ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => string.IsNullOrEmpty(ImageId)
        ? $"https://localhost:44340/images/noimage.png"
        : $"https://localhost:44340/images/vehiclephotos/{ImageId}";

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Vehicle Vehicle { get; set; }

    }
}
