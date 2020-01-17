using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class Echeance
    {
        [Key, Column(Order = 0)]
        public string DossierNumero{ get; set; }
        [Key, Column(Order = 1)]
        public string Matricule { get; set; }
        [Key, Column(Order = 2)]
        public string EchappNumero { get; set; }
        [Required]
        public DateTime EchappDATE { get; set; }
        [Required]
        public string EchappMontCapital { get; set; }
        [Required]
        public string EchappMontInterets { get; set; }
        [Required]
        public string EchappMontTaxe { get; set; }
        [Required]
        public string EchappMontEch { get; set; }
        [Required]
        public DateTime EchappDateTombEche { get; set; }
        [Required]
        public string TypeEcheance { get; set; }
        
        
       
    }
}
