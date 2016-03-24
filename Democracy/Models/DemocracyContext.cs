using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        /// <summary>
        /// Con este metodo desactivamos la eliminacion en cascada de la base de datos
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<State> State { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Voting> Votings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<GroupMember> GroupMembers { get; set; }
    }
}