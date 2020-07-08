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

        public static async Task<bool> Send(this InfoBipSendSmsService service, Notification notification)
        {
            return await service.SendAsync(notification.Consentement.Tel, notification.Message, AppConfig.Config()["Params:Nom"]);
        }
    }
}