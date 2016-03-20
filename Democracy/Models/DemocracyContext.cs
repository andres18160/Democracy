using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    /// <summary>
    /// Clase de conecion a la Db
    /// </summary>
    public class DemocracyContext : DbContext //la ponemos a heredar de la clase DbContext 
    {
        public DemocracyContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<State> State { get; set; }
    }
}