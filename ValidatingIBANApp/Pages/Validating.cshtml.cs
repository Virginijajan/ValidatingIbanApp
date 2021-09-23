using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Validating;

namespace ValidatingIBANApp.Pages
{
    public class ValidatingModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "Upload IBAN file")]      
        public IFormFile Upload { get; set; }
        public string Msg { get; set; }

        public void OnGet()
        {
        }       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Upload.Length> 2097152)
            {
                Msg = "The file is too large";
                return Page();
            }
            if (Directory.Exists("wwwroot\\text"))
                Directory.Delete("wwwroot\\text", true);
            Directory.CreateDirectory("wwwroot\\text");

            var filePath = Path.GetFullPath("wwwroot\\text\\iban.txt");
            var newFilePath = Path.GetFullPath("wwwroot\\text\\iban.out.txt");

            using (var fileStream = new FileStream("iban.txt", FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);                
            }
            
            Msg =ValidatingIbanFile.ValidateFile("iban.txt");
            try
            {
                Directory.Move("iban.out.txt", newFilePath);
                Directory.Move("iban.txt", filePath);
            }
             catch
            {
                Msg = "Error";
            }              
            return Page();                                        
        }       
    }
}
