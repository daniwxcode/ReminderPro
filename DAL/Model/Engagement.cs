﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model
{
   public  class Engagement
    {
        [Key]
        public int ID { get; set; }
        public string Libelle { get; set; }
        public string Code { get; set; }
      public   ICollection<Configs> Configs {get;set;}

    }
}
