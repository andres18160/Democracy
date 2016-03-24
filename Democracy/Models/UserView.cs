using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class UserView
    {
        [Key]
        public int UserId { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",
         MinimumLength = 7)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "E-Mail")]
        public string UserName { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",
         MinimumLength = 2)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres", MinimumLength = 2)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",
         MinimumLength = 7)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres", MinimumLength = 10)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Address { get; set; }

        public string Grade { get; set; }

        public string Group { get; set; }

        public HttpPostedFileBase Photo { get; set; } //propiedad que me permite capturar la foto pero tambien sirve para capturar cualquier archivo
    }
}