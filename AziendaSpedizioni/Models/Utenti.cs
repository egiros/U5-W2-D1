using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AziendaSpedizioni.Models
{
    public class Utenti
    {
        public int IdUser { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage = "lo username e' obbligatorio")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "lo username deve essere compreso tra 3 e 50 caratteri")]
        public string Username { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "la password e' obbligatorio")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "la password deve essere compreso tra 3 e 50 caratteri")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}