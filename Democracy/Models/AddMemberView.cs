using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class AddMemberView
    {
        public int GroupId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int UserId { get; set; }
    }
}