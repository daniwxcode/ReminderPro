using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using DAL;
using DAL.Model;
using DAL.Params;

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
                  '{AppConfigs.Source}'").ToList();
            }
        }

        public static List<Echeance> GetEcheances()
        {
            using (var db = new ReminderContext())
            {
                return db.Echeances.FromSqlRaw(@$"exec PROC_ECHEANCE_RELANCE '{DateTime.Today.ToShortDateString()}',
                  '{DateTime.Today.AddDays(AppConfigs.Ecart).ToShortDateString()}',
                  '{AppConfigs.Source}'").ToList();
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
                {"_DATE_",@$"DU {echeance.EchappDate.AddMonths(-1).ToShortDateString()} AU  {echeance.EchappDate.ToShortDateString()}"},
                {"_DECHEANCE_",$"{echeance.EchappDate.ToShortDateString()}"},
                {"_MONTANT_",$"{(int)echeance.EchappMontEch}"},
                {"_SIGNATURE_",$"{AppConfigs.Signature}"},
            };
            var regex = new Regex(String.Join("|", map.Keys));
            return new Notification()
            {
                Canal = Cannal.SMS.ToString(),
                Message = regex.Replace(AppConfigs.Sms, m => map[m.Value]),
                Consentement = echeance.GetConcentement(),
                DateSend = DateTime.Now
            };
        }
    }
}