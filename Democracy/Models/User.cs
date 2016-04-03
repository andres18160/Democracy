using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Democracy.Models
{

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Index("UserNameIndex", IsUnique = true)]
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

        [StringLength(50, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",MinimumLength = 2)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

          [Display(Name = "User")]
        public string FullName { get {return string.Format("{0} {1}",this.FirstName,this.LastName);}}

        [StringLength(20, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",
         MinimumLength = 7)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",MinimumLength = 10)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Address { get; set; }

        public string Grade { get; set; }

        public string Group { get; set; }

        [StringLength(200, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres", MinimumLength = 5)]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }


        //un usuario puede pertenecer a muchos GropMembers
        public virtual ICollection<GroupMember> GroupMember { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}