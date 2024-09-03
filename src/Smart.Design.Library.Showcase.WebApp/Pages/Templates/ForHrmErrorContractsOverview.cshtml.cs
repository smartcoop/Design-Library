using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Design.Library.Showcase.Pages.Templates;

public class ForHrmErrorContractsOverviewModel : PageModel
{
    public static readonly List<ErrorQueue> errorQueue = new List<ErrorQueue>();

    public void InitializeWithRandomData(int numberOfRecords)
    {
        var random = new Random();
        string[] errorMessages = {
            "Sample Error Message 1",
            "Sample Error Message 2",
            "Sample Error Message 3",
            "Database connection lost",
            "Invalid input detected",
            "Timeout occurred while processing",
            "Permission denied",
            "Resource not found",
            "Unexpected end of input",
            "Operation completed with warnings"
        };

        for (int i = 0; i < numberOfRecords; i++)
        {
            var refPrest = $"REF{random.Next(1000, 9999)}"; // Random reference number
            var firstDate = DateTime.Now.AddDays(-random.Next(1, 30)); // Random date within the last 30 days
            var eSMartBEEntity = random.Next(1, 10); // Random entity ID between 1 and 10
            var eStatus = random.Next(0, 2); // Random status: 0 or 1
            var errorMessage = errorMessages[random.Next(errorMessages.Length)]; // Random error message

            errorQueue.Add(new ErrorQueue
            {
                refPrest = refPrest,
                firstDate = firstDate,
                eSMartBEEntity = eSMartBEEntity,
                eStatus = eStatus,
                errorMessage = errorMessage
            });
        }
    }
    public IActionResult OnGet()
    {
        // Initialize with some sample data
        //errorQueue.Add(new ErrorQueue { refPrest = "REF001", firstDate = DateTime.Now.AddDays(-5), eSMartBEEntity = 1, eStatus = 0, errorMessage = "Sample Error Message 1" });
        //errorQueue.Add(new ErrorQueue { refPrest = "REF002", firstDate = DateTime.Now.AddDays(-5), eSMartBEEntity = 1, eStatus = 0, errorMessage = "Sample Error Message 2" });
        //errorQueue.Add(new ErrorQueue { refPrest = "REF003", firstDate = DateTime.Now.AddDays(-5), eSMartBEEntity = 1, eStatus = 0, errorMessage = "Sample Error Message 3" });
        InitializeWithRandomData(100);
        return Page();
    }

    public JsonResult OnGetErrorQueueData()
    {
        return new JsonResult(errorQueue);
    }

    public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
    {
        return new JsonResult(errorQueue.ToDataSourceResult(request));
    }
}

public class ErrorQueue
{
public string refPrest {  get; set; } = string.Empty;
public DateTime firstDate { get; set;}

public int eSMartBEEntity { get; set; }

public int eStatus {  get; set; }
public string errorMessage { get; set; } = string.Empty;


}
