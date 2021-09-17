using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class DocumentType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Documento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe exceder de {1} caracteres")]
        [Required]
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
