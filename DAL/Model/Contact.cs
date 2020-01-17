using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DAL.Params;

namespace DAL.Model
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public Cannal Canal { get; set; }
        [Required]
        public string Addresse { get; set; }
        [ForeignKey("Consentement")]
        public int ConsentementID { get; set; }
        public virtual Consentement Consentement { get; set; }
    }
}
