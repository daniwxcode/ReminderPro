using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Model
{
    public class Configs
    {
        [Key]
        public int ID { get; set; }
        public int ApiID{get;set;}
        public int EngagementID{get;set;}
        [ForeignKey("EngagementID")]
        public virtual Engagement Engagement{get;set;}
       [ForeignKey("ApiID")]
        public virtual Api Api{get;set;}
        public bool Actif {get;set;}
    }
}
