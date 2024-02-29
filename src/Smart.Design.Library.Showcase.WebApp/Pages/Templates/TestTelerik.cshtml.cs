using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smart.Design.Library.Showcase.Models;

namespace Smart.Design.Library.Showcase.Pages.Templates;

public class TestTelerikModel : PageModel
{
    
    public FieldsForm? Model { get; set; }

    public void OnGet()
    {
        // Initialisez votre mod�le ici si n�cessaire lors de la requ�te HTTP GET
        Model = new FieldsForm();
        Console.WriteLine("OnGet method called");
    }

    public IActionResult OnPost()
    {
        Console.WriteLine("OnPost method called");
        if (!ModelState.IsValid)
        {
            // Si le mod�le n'est pas valide, retournez la page avec les erreurs
            return Page();
        }

        // Traitement du formulaire ici
        // Par exemple, redirection vers une autre page apr�s soumission r�ussie
        return RedirectToPage("/Templates/Confirmation");
    }
}