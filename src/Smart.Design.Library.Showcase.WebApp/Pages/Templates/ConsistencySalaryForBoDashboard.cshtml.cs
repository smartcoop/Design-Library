using System.Globalization;
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

    // mockup of the backend data
    private static readonly List<ConsistencySalaryAnomaly> consistencySalaryAnomalies = new List<ConsistencySalaryAnomaly>();

    public IActionResult OnPostGenerateReport()
    {
        var random = new Random();
        var newAnomalyCount = Convert.ToInt32((ToDate - FromDate).Days * 10);
        for (int i = 1; i <= newAnomalyCount; i++)
        {
            ConsistencySalaryAnomaly anomaly = new ConsistencySalaryAnomaly
            {
                Id = i,

                MemberReference = (i * 549 % 1000).ToString(CultureInfo.InvariantCulture),
                WorkerReference = (i * 487 % 500).ToString(CultureInfo.InvariantCulture),
                ContractReference = (i * 943 % 10000).ToString(CultureInfo.InvariantCulture),

                ContractStartDate = FromDate.AddHours(random.Next(0, (int)(ToDate - FromDate).TotalHours)),
                ContractDurationInDays = (i * 348 % 5) + 1,

                SamRawSalary = i * 5.56f % 100,
                ForHrmRawSalary = i * 6.02f % 100,

                IsActive = Convert.ToBoolean(i * 307 % 10)
            };
            anomaly.RawSalaryDelta = anomaly.ForHrmRawSalary - anomaly.SamRawSalary;

            if (anomaly.RawSalaryDelta > MinimumDelta)
            {
                consistencySalaryAnomalies.Add(anomaly);
            }
        }

        return Page();
    }

    public void OnPostFixIssues(int[] anomalyIds)
    {
        consistencySalaryAnomalies.RemoveAll(anomaly => anomalyIds.Contains(anomaly.Id));
    }

    // handler for the Kendo grid
    public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
    {
        return new JsonResult(consistencySalaryAnomalies.ToDataSourceResult(request));
    }
}

public class ConsistencySalaryAnomaly
{
    public int Id { get; set; }

    public string MemberReference { get; set; } = string.Empty;

    public string WorkerReference { get; set; } = string.Empty;

    public string ContractReference { get; set; } = string.Empty;

    public DateTime ContractStartDate { get; set; }

    public int ContractDurationInDays { get; set; }

    public float SamRawSalary { get; set; }

    public float ForHrmRawSalary { get; set; }

    /// <summary>
    /// ForHrm Raw Salary - Sam Raw Salary
    /// </summary>
    public float RawSalaryDelta { get; set; }

    public bool IsActive { get; set; }
}
