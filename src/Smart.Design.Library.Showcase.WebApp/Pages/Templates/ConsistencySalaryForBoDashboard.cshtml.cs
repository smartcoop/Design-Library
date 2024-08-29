using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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

    private static readonly List<ConsistencySalaryAnomaly> consistencySalaryAnomalies = new List<ConsistencySalaryAnomaly>();

    public void OnGet()
    {
    }

    public void OnPostGenerateReport()
    {
        for (int i = 0; i < 42; i++)
        {
            consistencySalaryAnomalies.Add(
                new ConsistencySalaryAnomaly
                {
                    SamRawSalary = (i * 5.56f) % 10,
                    ForHrmRawSalary = (i * 5.59f) % 10
                }
            );
        }
    }

    public void OnPostFixIssues()
    {
        consistencySalaryAnomalies.Clear();
    }

    // handlers for the Kendo grid
    public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
    {
        return new JsonResult(consistencySalaryAnomalies.ToDataSourceResult(request));
    }
}

public class ConsistencySalaryAnomaly
{
    public float SamRawSalary { get; set; }

    public float ForHrmRawSalary { get; set; }
}
