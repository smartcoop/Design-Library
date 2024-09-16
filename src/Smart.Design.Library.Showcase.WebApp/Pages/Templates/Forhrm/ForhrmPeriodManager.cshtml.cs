using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Design.Library.Showcase.Pages.Templates.Forhrm;

public class ForhrmPeriodModel
{
    public ForhrmPeriodModel(int year, int month, bool blocked, int nbBlocked)
    {
        Year = year;
        Month = month;
        Blocked = blocked;
        NbBlocked = nbBlocked;
    }

    public ForhrmPeriodModel() { } //json serialization

    public int Year { get; set; }
    public int Month { get; set; }
    public bool Blocked { get; set; }
    public int NbBlocked { get; set; }
}

public class ForhrmPeriodManagerModel : PageModel
{
    private static List<ForhrmPeriodModel> _forhrmPeriods { get; } = new();

    static ForhrmPeriodManagerModel()
    {
        InitFakeData();
    }

    public void OnGet()
    {

    }

    private static void InitFakeData()
    {
        var randomGen = new Random();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 1; j <= 12; j++)
            {
                var blocked = randomGen.Next(5) == 0;
                var nbBlocked = blocked ? randomGen.Next(2000) : 0;
                _forhrmPeriods.Add(new ForhrmPeriodModel(DateTime.Today.Year - i, j, blocked, nbBlocked));
            }
        }
    }

    public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
    {
        return new JsonResult(_forhrmPeriods.ToDataSourceResult(request));
    }

    public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, ForhrmPeriodModel period)
    {
        var modelPeriod = _forhrmPeriods.Single(x => x.Year == period.Year && x.Month == period.Month);
        modelPeriod.Blocked = period.Blocked;
        return new JsonResult(new[] { modelPeriod }.ToDataSourceResult(request, ModelState));
    }
}
