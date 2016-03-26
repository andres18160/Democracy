using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class VotingView
    {
        public int VotingId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Voting Description")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede contener mas de {2} caracteres",
         MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Date Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Time Start")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime TimeStart { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Date End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Time End")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime TimeEnd { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Is For All Users?")]
        public bool IsForAllUsers { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Is Enable Blank Vote")]
        public bool IsEnableBlakVote { get; set; }

    }
}