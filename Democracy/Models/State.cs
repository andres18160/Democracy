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

        public string Descripcion { get; set; }
    }
}