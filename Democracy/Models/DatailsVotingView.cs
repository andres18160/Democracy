using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class DetailsVotingView
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
        [Display(Name = "Date Time Start")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeStart { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Date Time End")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeEnd { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Is For All Users?")]
        public bool IsForAllUsers { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Is Enable Blank Vote")]
        public bool IsEnableBlakVote { get; set; }

        [Display(Name = "Quantity Votes")]
        public int QuantityVotes { get; set; }

        [Display(Name = "Quantity Black Votes")]
        public int QuantityBlackVotes { get; set; }

        [Display(Name = "Winner")]
        public int CandidateWinId { get; set; }

        public State State { get; set; }

        public List<VotingGoup> VotingGroups { get; set; }

        public List<Candidate> Candidates { get; set; }

    }
}