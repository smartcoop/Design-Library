using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Design.Library.Showcase.Pages.Templates;

public class ConsistencyDayForBoDashboardModel : PageModel
{
    [BindProperty]
    public DateTime MonthAndYear { get; set; } = DateTime.Today;

    [BindProperty]
    public int EntityId { get; set; }

    // structure to speed up the display of a basic summary banner
    public Dictionary<ConsistencyDayAnomalyType, int> AnomalyCountsByTypes { get; set; } = new Dictionary<ConsistencyDayAnomalyType, int>();

    // mockup of the backend data
    private static readonly List<ConsistencyDayAnomaly> consistencyDayAnomalies = new List<ConsistencyDayAnomaly>();
    private static readonly List<SmartEntity> smartEntities = new List<SmartEntity>();

    public void OnGet()
    {
        if (smartEntities.Count == 0)
        {
            smartEntities.Add(new SmartEntity() { Id = 3, Name = "APMC" });
            smartEntities.Add(new SmartEntity() { Id = 8, Name = "UBIK" });
            smartEntities.Add(new SmartEntity() { Id = 16, Name = "Productions Associées" });
            smartEntities.Add(new SmartEntity() { Id = 17, Name = "Secrétariat Pour Intermittents" });
            smartEntities.Add(new SmartEntity() { Id = 18, Name = "Fondation SMartBE" });
            smartEntities.Add(new SmartEntity() { Id = 19, Name = "MatLease" });
            smartEntities.Add(new SmartEntity() { Id = 20, Name = "SMartImmo" });
            smartEntities.Add(new SmartEntity() { Id = 21, Name = "Interim Paleis" });
            smartEntities.Add(new SmartEntity() { Id = 22, Name = "Formateurs Associés" });
            smartEntities.Add(new SmartEntity() { Id = 23, Name = "SmartCoop" });
            smartEntities.Add(new SmartEntity() { Id = 25, Name = "Smart Productions" });
            smartEntities.Add(new SmartEntity() { Id = 30, Name = "SMartSol" });
            smartEntities.Add(new SmartEntity() { Id = 31, Name = "Tax Shelter Ethique" });
            smartEntities.Add(new SmartEntity() { Id = 901, Name = "SCRL Cinéastes Associés" });
        }

        AnomalyCountsByTypes.Clear();
        foreach (ConsistencyDayAnomalyType anomlyType in Enum.GetValues(typeof(ConsistencyDayAnomalyType)).Cast<ConsistencyDayAnomalyType>())
        {
            AnomalyCountsByTypes[anomlyType] = consistencyDayAnomalies.Count(a => a.AnomalyType == anomlyType);
        }
    }

    public IActionResult OnPostGenerateReport()
    {
        List<ConsistencyDayAnomalyType> anomalyTypes = Enum
            .GetValues(typeof(ConsistencyDayAnomalyType))
            .Cast<ConsistencyDayAnomalyType>()
            .ToList();

        // Let's generate some random stuff.
        var newAnomalyCount = 100;
        for (int i = 1; i <= newAnomalyCount; i++)
        {
            ConsistencyDayAnomaly anomaly = new ConsistencyDayAnomaly
            {
                Id = consistencyDayAnomalies.Count + i,

                ProviderReference = (i * 549 % 1000).ToString(CultureInfo.InvariantCulture),
                Date = MonthAndYear,
                SmartWorkingHours = 7.6f,
                ForHrmId = i * 943 % 10000,
                ForHrmWorkingHours = 7.6f,
                AnomalyType = anomalyTypes[i % anomalyTypes.Count]
            };
            
            consistencyDayAnomalies.Add(anomaly);
        }

        // We reload the page. An easy solution but probably a bit rough...
        return Redirect("/Templates/ConsistencyDayForBoDashboard");
    }

    // handler for the Kendo dropdownlist
    public JsonResult OnGetReadEntities()
    {
        return new JsonResult(smartEntities);
    }

    // handler for the Kendo grid
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

public class SmartEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public enum ConsistencyDayAnomalyType
{
    [Display(Name = "None")]
    None,
    [Display(Name = "Day only exists in Smart")]
    DayOnlyExistsInSmart,
    [Display(Name = "Day only exists in ForHrm")]
    DayOnlyExistsInForHrm,
    [Display(Name = "Day has different hours")]
    DayHasDifferentHours,
    [Display(Name = "Multiple days in ForHrm")]
    MultipleDaysInForHrm
}
