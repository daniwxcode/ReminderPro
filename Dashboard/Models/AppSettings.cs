using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class AppSettings
    {
        [Required(ErrorMessage = "La source de données doit être renseignée")]
        public string SourceDB { get; set; }

        public int DaysEcart { get; set; }

        [Required(ErrorMessage = "Le Nom de de la stucture doit être renseigné")]
        public string InstitutionName { get; set; }

        public string Signature { get; set; }

        [Required(ErrorMessage = "L'URL de l'API doit être renseignée")]
        public string ApiUrl { get; set; }

        [Required(ErrorMessage = "Le Nom de l'utilisateur doit être renseigné")]
        public string User { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        [Required(ErrorMessage = "Votre Nom d'expéditeur doit être renseigné")]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Le code Pays doit être fourni")]
        public string CountryCode { get; set; }
    }
}