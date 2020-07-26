using System;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace DAL.Model
{
    public partial class Echeance
    {
        [Key]
        [JsonProperty("DOSSIER_NUMERO")]
        public string DossierNumero { get; set; }

        [JsonProperty("ECHAPP_NUMERO")]
        public string EchappNumero { get; set; }

        [JsonProperty("LIBELLE_ENGAGEMENT")]
        public string LibelleEngagement { get; set; }

        [JsonProperty("ETCIV_MATRICULE")]
        public string EtcivMatricule { get; set; }

        [JsonProperty("ETCIV_NOMREDUIT")]
        public string EtcivNomreduit { get; set; }

        [JsonProperty("ETCIV_TELEPHONE")]
        public string EtcivTelephone { get; set; }

        [JsonProperty("ETCIV_ADRESS_GEOG1")]
        public string EtcivAdressGeog1 { get; set; }

        [JsonProperty("ETCIV_NUMCPT_CONTRIB")]
        public string EtcivNumcptContrib { get; set; }

        [JsonProperty("ECHAPP_DATE")]
        public DateTime EchappDate { get; set; }

        [JsonProperty("ECHAPP_MONT_CAPITAL")]
        public decimal EchappMontCapital { get; set; }

        [JsonProperty("ECHAPP_MONT_TAXE")]
        public decimal EchappMontTaxe { get; set; }

        [JsonProperty("ECHAPP_MONT_ECH")]
        public decimal EchappMontEch { get; set; }

        [JsonProperty("ECHAPP_DATE_TOMB_ECHE")]
        public DateTime EchappDateTombEche { get; set; }
    }
}