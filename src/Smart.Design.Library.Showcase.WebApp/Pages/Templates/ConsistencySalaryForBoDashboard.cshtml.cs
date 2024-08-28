using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Design.Library.Showcase.Pages.Templates;

public class ConsistencySalaryForBoDashboardModel : PageModel
{
    [BindProperty]
    public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-7);

    [BindProperty]
    public DateTime ToDate { get; set; } = DateTime.Today;

    [BindProperty]
    public float MinimumDelta { get; set; } = 5.0f;

    public void OnGet()
    {

    }
}

public class ConsistencySalaryModel
{
    public float SamRawSalary { get; set; }

    public float ForHrmRawSalary { get; set; }
}
