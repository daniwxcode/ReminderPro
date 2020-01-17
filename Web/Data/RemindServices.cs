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

        public void Save ( Api newApi )
        {
            using (var db = new ReminderContext())
            {
                db.Apis.Update(newApi);
                db.SaveChanges();
            }
        }
        public void Delete ( Api DeletedApi )
        {
            using (var db = new ReminderContext())
            {
                db.Apis.Remove(DeletedApi);
                db.SaveChanges();
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

        public void SaveEngag ( Engagement newEngag )
        {
            using (var db = new ReminderContext())
            {
                db.Engagements.Update(newEngag);
                db.SaveChanges();
            }
        }
        public void DeleteEngag ( Engagement DeletedEngag )
        {
            using (var db = new ReminderContext())
            {
                db.Engagements.Remove(DeletedEngag);
                db.SaveChanges();
            }
        }
        #endregion

        #region Configs

        public Task<List<Configs>> GetConfigsAsync ()
        {
            using (var db = new ReminderContext())
            {
                return Task.FromResult(db.Configs.Include(p => p.Engagement).Include(p => p.Api).ToList());
            }

        }

        public void SaveConfigs ( Configs newConfigs )
        {
            using (var db = new ReminderContext())
            {
                db.Configs.Update(newConfigs);
                db.SaveChanges();
            }
        }
        public void DeleteConfigs ( Configs DeletedConfigs )
        {
            using (var db = new ReminderContext())
            {
                db.Configs.Remove(DeletedConfigs);
                db.SaveChanges();
            }
        }
        #endregion
    }
}

