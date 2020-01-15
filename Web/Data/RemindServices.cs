using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class RemindServices
    {

        #region Api
        public Task<List<Api>> GetApiAsync ()
        {
            using (var db = new ReminderContext())
            {
                return Task.FromResult(db.Apis.ToList());
            }

        }

        public async void Save ( Api newApi )
        {
            using (var db = new ReminderContext())
            {
                db.Apis.Update(newApi);
              await  db.SaveChangesAsync();
            }
        }
        public async  void Delete ( Api DeletedApi )
        {
            using (var db = new ReminderContext())
            {
                db.Apis.Remove(DeletedApi);
                 await  db.SaveChangesAsync();
            }
        }
        #endregion
        #region Engagements
        
        public Task<List<Engagement>> GetEngagAsync ()
        {
            using (var db = new ReminderContext())
            {
                return Task.FromResult(db.Engagements.ToList());
            }

        }

        public async void SaveEngag ( Engagement newEngag )
        {
            using (var db = new ReminderContext())
            {
                db.Engagements.Update(newEngag);
              await  db.SaveChangesAsync();
            }
        }
        public async  void DeleteEngag ( Engagement DeletedEngag )
        {
            using (var db = new ReminderContext())
            {
                db.Engagements.Remove(DeletedEngag);
                 await  db.SaveChangesAsync();
            }
        }
        #endregion
    }
}

