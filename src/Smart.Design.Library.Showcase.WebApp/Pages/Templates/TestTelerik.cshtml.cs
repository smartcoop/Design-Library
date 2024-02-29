using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smart.Design.Library.Showcase.Pages.Templates
{
    public class TestTelerikModel : PageModel
    {
        [Required(ErrorMessage = "Le champ Nom est requis.")]
        public string Nom { get; set; }
        public string Genre { get; set; }
        public DateTime DateNaissance { get; set; }

        public void OnGet()
        {
            // Code � ex�cuter lors de la requ�te HTTP GET
        }
        // M�thode pour g�rer la soumission du formulaire (POST)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Si le mod�le n'est pas valide, retourner la page avec les erreurs
                return Page();
            }

            // Traitement du formulaire ici
            // Par exemple, rediriger vers une autre page apr�s soumission r�ussie
            return RedirectToPage("/Templates/Confirmation");
        }
    }
}
