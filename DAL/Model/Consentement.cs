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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le Matricule")]
        public string Matricule { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le Nom", AllowEmptyStrings = false)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le numero de telephone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Veuillez saisir un numero de telephone valide")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Veuillez saisir un numero de telephone valide")]
        public string Tel { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Veuillez saisir une adresse mail valide")]

        public string Mail { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}