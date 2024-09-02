using System.Globalization;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Design.Library.Showcase.Pages.Templates;

public class ConsistencyDayForBoDashboardModel : PageModel
{
    private static readonly List<ConsistencyDayAnomaly> consistencyDayAnomalies = new List<ConsistencyDayAnomaly>();

    public IActionResult OnPostGenerateReport()
    {   
        var newAnomalyCount = 10;
        for (int i = 1; i <= newAnomalyCount; i++)
        {
            ConsistencyDayAnomaly anomaly = new ConsistencyDayAnomaly
            {
                Id = i,

                ProviderReference = (i * 549 % 1000).ToString(CultureInfo.InvariantCulture),
                Date = DateTime.Now,
                SmartWorkingHours = 7.6f,
                ForHrmId = i * 943 % 10000,
                ForHrmWorkingHours = 7.6f,
                AnomalyType = ConsistencyDayAnomalyType.None
            };
            
            consistencyDayAnomalies.Add(anomaly);
        }

        return Page();
    }

    // handlers for the Kendo grid
    public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
    {
        return new JsonResult(consistencyDayAnomalies.ToDataSourceResult(request));
    }
}

public class ConsistencyDayAnomaly
{
    public int Id { get; set; }

    public string ProviderReference { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public float SmartWorkingHours { get; set; }

    public int ForHrmId { get; set; }

    public float ForHrmWorkingHours { get; set; }

    public ConsistencyDayAnomalyType AnomalyType { get; set; }
}

public enum ConsistencyDayAnomalyType
{
    None,
    DayOnlyExistsInSmart,
    DayOnlyExistsInForHrm,
    DayHasDifferentHours,
    MultipleDaysInForHrm
}
