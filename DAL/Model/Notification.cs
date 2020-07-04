using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using DAL.Params;

namespace DAL.Model
{
    public class Notification
    {
        public int ID { get; set; }
        public DateTime DateSend { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
        public string Canal { get; set; }
        public virtual Consentement Consentement { get; set; }
    }
}