using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Validating;

namespace ValidatingIBANApp.Pages
{
    public class IndexModel : PageModel
    {
        public string IbanIsValid { get; set; } = "";
        [BindProperty]
        [Display(Name = "IBAN")]
        [Required]
        [MaxLength(34, ErrorMessage = "Max length is 20 characters")]
        [MinLength(5, ErrorMessage ="Min length is 5 characters")]
        public string IBAN { get; set; }
        public void OnGet()
        {         
        }
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                if (IbanValidating.IsValid(IBAN))
                    IbanIsValid = $"IBAN {IBAN} is valid";
                else
                    IbanIsValid = $"IBAN {IBAN} is not valid";
            }
            Page();
        }
    }
}
