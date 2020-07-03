using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model
{
    public class Consentement
    {
        [Key]
        public int ID { get; set; }

        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}