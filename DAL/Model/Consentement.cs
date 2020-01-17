using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Consentement
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Matricule { get; set; }
        [Required]
        public string Identite { get; set; }
        public DateTime DateEnregistre { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Notification> Notifications{get;set;}

    }
}
