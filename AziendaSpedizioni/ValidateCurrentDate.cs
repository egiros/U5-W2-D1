using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AziendaSpedizioni
{

    public class ValidateCurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputDate = value as DateTime? ?? new DateTime();


            if (inputDate.Date >= DateTime.Today)
            {
                return true;
            }
            else
            {
                ErrorMessage = "La data deve essere maggiore della data odierna.";
                return false;
            }
        }

    }
}