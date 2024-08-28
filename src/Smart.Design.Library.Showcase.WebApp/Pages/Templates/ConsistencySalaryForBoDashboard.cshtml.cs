using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Design.Library.Showcase.Pages.Templates;

public class ConsistencySalaryForBoDashboardModel : PageModel
{
    [BindProperty]
    public DateTime FromDate { get; set; }

    [BindProperty]
    public DateTime ToDate { get; set; }

    [BindProperty]
    public float MinimumDelta { get; set; }

    public void OnGet()
    {

    }
}

public class ConsistencySalaryModel
{
    public float SamRawSalary { get; set; }

    public float ForHrmRawSalary { get; set; }
}
