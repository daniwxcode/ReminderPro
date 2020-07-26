using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using DAL;
using DAL.Model;
using DAL.Params;
using DAL.Services;

using Microsoft.EntityFrameworkCore;

using Services;

namespace App
{
    public static class Service
    {
        public static List<Echeance> Echeances;

        public static List<Echeance> GetEcheances()
        {
            using (var db = new ReminderContext())
            {
                return db.Echeances.FromSqlRaw(@$"exec PROC_ECHEANCE_RELANCE '{DateTime.Today.ToShortDateString()}',
                  '{DateTime.Today.AddDays(AppSettingsParser.Settings.Ecart).ToShortDateString()}',
                  '{AppSettingsParser.Settings.SourceDB}'").ToList();
            }
        }

        public static List<Echeance> GetEcheances(int Ecart)
        {
            using (var db = new ReminderContext())
            {
                return db.Echeances.FromSqlRaw(@$"exec PROC_ECHEANCE_RELANCE '01/01/2019','31/12/2022',
                  '{AppSettingsParser.Settings.SourceDB}'").ToList();
            }
        }

        public static List<Echeance> GetEcheances(DateTime date)
        {
            using (var db = new ReminderContext())
            {
                return db.Echeances.FromSqlRaw(@$"exec PROC_ECHEANCE_RELANCE '{date.ToShortDateString()}',
                  '{date.ToShortDateString()}',
                  '{AppSettingsParser.Settings.SourceDB}'").ToList();
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
            AppSettings appSettings = AppSettingsParser.Settings;
            IDictionary<string, string> map = new Dictionary<string, string>()
            {
                {"_DOSSIER_",@$"{echeance.DossierNumero}"},
                {"_DECHEANCE_",$"{echeance.EchappDate.ToShortDateString()}"},
                {"_MONTANT_",$"{(int)echeance.EchappMontEch}"},
                {"_SIGNATURE_",$"{appSettings.Signature}"}
            };
            var regex = new Regex(String.Join("|", map.Keys));
            return new Notification()
            {
                Canal = Cannal.SMS.ToString(),
                Message = regex.Replace(appSettings.Sms, m => map[m.Value]),
                Consentement = echeance.GetConcentement(),
                Dossier = echeance.DossierNumero,
                Echeance = echeance.EchappNumero,
                DateSend = DateTime.Now
            };
        }

        public static string ToPeriode(this DateTime date) =>
            @$"du {date.AddMonths(-1).AddDays(1).ToShortDateString()} au {date.ToShortDateString()}";

        public static bool Send(this InfoBipSendSmsService service, Notification notification)
        {
            return service.Send(notification.Consentement.Tel, notification.Message, AppSettingsParser.Settings.Sender);
        }
    }
}