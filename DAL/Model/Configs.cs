using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class Configs
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Vueillez choisir l'API")]
        public int ApiID { get; set; }
        [Required(ErrorMessage ="Vueillez choisir Engagement")]
        public int EngagementID { get; set; }
        [ForeignKey("EngagementID")]
        public virtual Engagement Engagement { get; set; }
        [ForeignKey("ApiID")]
        public virtual Api Api { get; set; }
        public bool Actif { get; set; }
    }
}
