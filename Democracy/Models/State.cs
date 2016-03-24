using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class State
    {
        [Key]//se necesita la anotacion y quiere decir que es la clave primaria
        public int StateId { get; set; }

        [Required(ErrorMessage="El campo {0} es requerido")]
        [Display(Name = "State Description")]
        [StringLength(50,ErrorMessage="El campo {0} no puede contener mas de {2} caracteres",
            MinimumLength=3)]
        public string Descripcion { get; set; }

        public virtual ICollection<Voting> Votings { get; set; }
    }
}