using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Params;

namespace DAL.Model
{
    public class Notification
    {
        public int ID { get; set; }
        public DateTime DateEnreg { get; set; }
        
        [ForeignKey("Api")]
        public int ApiID { get; set; }
        public virtual Api Api { get; set; }

        [ForeignKey("Consentement")]
        public int ConsentementID { get; set; }
        public virtual Consentement Consentement { get; set; }

        public virtual Echeance Echeance{get;set;}
    }
}
