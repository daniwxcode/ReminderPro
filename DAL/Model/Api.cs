using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Params;

namespace DAL.Model
{
  public  class Api
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string  KeySecret { get; set; }
        public string Token{get;set;}
        public Cannal Cannal { get; set; }
        public ICollection<Configs> Configs{get;set;}
    }
}
