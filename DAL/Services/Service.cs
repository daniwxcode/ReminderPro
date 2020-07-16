using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using DAL;
using DAL.Model;
using DAL.Params;
using DAL.Services;

using Microsoft.EntityFrameworkCore;

namespace App
{
    public static class Service
    {
        public static List<Echeance> GetEcheances(int Ecart)
        {
            using (var db = new ReminderContext())
            {
                return db.Echeances.FromSqlRaw(@$"exec PROC_ECHEANCE_RELANCE '{DateTime.Today.ToShortDateString()}',
                  '{DateTime.Today.AddDays(Ecart).ToShortDateString()}',
                  '{AppConfig.Config()["Params:Source"]}'").ToList();
            }
        }

        public static List<Echeance> GetEcheances()
        {
            using (var db = new ReminderContext())
            {
                return db.Echeances.FromSqlRaw(@$"exec PROC_ECHEANCE_RELANCE '{DateTime.Today.ToShortDateString()}',
                  '{DateTime.Today.AddDays(Convert.ToInt32(AppConfig.Config()["Params:Ecart"])).ToShortDateString()}',
                  '{AppConfig.Config()["Params:Source"]}'").ToList();
            }
        }

        public static Consentement GetConcentement(this Echeance echeance)
        {
            using (var db = new ReminderContext())
            {
                return db.Consentements.FirstOrDefault(p => p.Matricule == echeance.EtcivMatricule);
            }
        }

        public static Notification ToSMS(this Echeance echeance)
        {
            IDictionary<string, string> map = new Dictionary<string, string>()
            {
                {"_DOSSIER_",@$"{echeance.DossierNumero}"},
                {"_DECHEANCE_",$"{echeance.EchappDate.ToShortDateString()}"},
                {"_MONTANT_",$"{(int)echeance.EchappMontEch}"},
                {"_SIGNATURE_",$"{AppConfig.Config()["Params:Signature"]}"},
            };
            var regex = new Regex(String.Join("|", map.Keys));
            return new Notification()
            {
                Canal = Cannal.SMS.ToString(),
                Message = regex.Replace(AppConfig.Config()["Params:Sms"], m => map[m.Value]),
                Consentement = echeance.GetConcentement(),
                DateSend = DateTime.Now
            };
        }

        public static Notification ToMail(this Echeance echeance)
        {
            IDictionary<string, string> map = new Dictionary<string, string>()
            {
                {"_NUMECH_",@$"{echeance.EchappNumero}"},
                {"_NUMDOSSIER_",@$"{echeance.DossierNumero}"},
                {"_NOMCLIENT_",@$"{echeance.EtcivNomreduit}"},
                {"_MATRICULECLIENT_",@$"{echeance.EtcivMatricule}"},
                {"_BPCLIENT_",@$""},
                {"_ADRESSECLIENT_",@$"{echeance.EtcivAdressGeog1}"},
                {"_DFACT_",@$"{echeance.EchappDate.AddDays(-Convert.ToInt32(AppConfig.Config()["Params:Ecart"])).ToShortDateString()}"},
                {"_RIB_",$"{echeance.EtcivNumcptContrib}"},
                {"_ADRESSEBANK_",$"{AppConfig.Config()["Params:Address"]}"},
                {"_VILLEPAYS_",$"{AppConfig.Config()["Params:CityCountry"]}"},
                {"_DFIN_",$"{echeance.EchappDate.ToShortDateString()}"},
                {"_DDEBUT_",$"{echeance.EchappDate.AddMonths(-1).AddDays(1).ToShortDateString()}"},
                {"_ECHEANCEPERIODE_",$" du {echeance.EchappDate.AddMonths(-1).AddDays(1).ToShortDateString()} au {echeance.EchappDate.ToShortDateString()}"},
                {"_MONTANTECHEANCE_",$"{(int)echeance.EchappMontEch}"},
            };
            var regex = new Regex(String.Join("|", map.Keys));
            return new Notification()
            {
                Canal = Cannal.Mail.ToString(),
                Message = regex.Replace(AppConfig.EmailTemplate(), m => map[m.Value]),
                Consentement = echeance.GetConcentement(),
                DateSend = DateTime.Now
            };
        }

        public static async Task<bool> Send(this InfoBipSendSmsService service, Notification notification)
        {
            return await service.SendAsync(notification.Consentement.Tel, notification.Message, AppConfig.Config()["Params:Nom"]);
        }

        public static async Task SendMail(this SendGridEmailSenderService service, Notification notification)
        {
            var emails = notification.Consentement.Mail.Split(';').ToList();
            await service.SendEmailAsync(emails, "FACTURE", notification.Message);
        }
    }
}