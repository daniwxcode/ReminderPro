using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class AppSettings
    {
        public string DataConnection { get; set; }

        [Required(ErrorMessage = "La source de données doit être renseignée")]
        public string SourceDB { get; set; }

        public int Ecart { get; set; }

        [Required(ErrorMessage = "Le Nom de de la stucture doit être renseigné")]
        public string InstitutionName { get; set; }

        [Required(ErrorMessage = "Le Model SMS doit être fourni")]
        public string Sms { get; set; }

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

        public AppSettings()
        {
        }
    }
}