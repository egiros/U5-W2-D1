using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AziendaSpedizioni.Models
{
    public class Clienti
    {
        public int IdCliente { get; set; }
        [DisplayName("Nome cliente")]
        [Required(ErrorMessage = "Il Nome è obbligatorio")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Il Nome deve essere compreso tra 3 e 50 caratteri")]
        public string Nominativo { get; set; }
        [DisplayName("Privato o azienda")]
        public bool IsAzienda { get; set; }
        [Remote("CheckCodFiscale", "Home", ErrorMessage = "Inserisci un codice fiscale valido")]
        public string CodiceFiscale { get; set; }
        [Remote("CheckPIVA", "Home", ErrorMessage = "Inserisci una partita IVA valida")]
        public string PartitaIva { get; set; }
    }
}