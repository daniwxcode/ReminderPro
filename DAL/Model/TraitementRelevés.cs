using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Castle.Components.DictionaryAdapter;

namespace DAL.Model
{
    public class TraitementRelevés
    {
        [System.ComponentModel.DataAnnotations.Key]
        public DateTime SentDate { get; set; }

        [Required]
        public string SentTraitement { get; set; }

        [Required]
        public string Etapes { get; set; }
    }
}