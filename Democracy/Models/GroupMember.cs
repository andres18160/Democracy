using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class GroupMember
    {
        [Key]
        public int GroupMemberId { get; set; }

        public int GroupId { get; set; }

        public int UserId { get; set; }


        //se crea la ralacion con las demas tablas    
        //Cada GroupMember tiene un grupo
        public virtual Group Group { get; set; }
        //Cada GroupMember tiene un User
        public virtual User User { get; set; }
    }
}