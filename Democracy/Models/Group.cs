using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class Group
    {
        [Key]//se necesita la anotacion y quiere decir que es la clave primaria
        public int GroupId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",
            MinimumLength = 3)]
        public string Descripcion { get; set; }

        //Decimos que la tabla Group puede tener muchos group members
        public virtual ICollection<GroupMember> GroupMember  { get; set; }

        public virtual ICollection<VotingGoup> VotingGroups { get; set; }

    }
}