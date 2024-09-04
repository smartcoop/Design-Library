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
        var smartsBEEntities = Enum.GetValues(typeof(enmSMartBEEntity)).Cast<enmSMartBEEntity>().ToList();
        var statuses = Enum.GetValues(typeof(enmStatus)).Cast<enmStatus>().ToList();

        for (int i = 0; i < numberOfRecords; i++)
        {
            var refPrest = $"REF{random.Next(1000, 9999)}"; // Random reference number
            var firstDate = DateTime.Now.AddDays(-random.Next(1, 30)); // Random date within the last 30 days
            var eSMartBEEntity = smartsBEEntities[random.Next(smartsBEEntities.Count)]; // Random entity from enum
            var eStatus = statuses[random.Next(statuses.Count)]; // Random status from enum
            var errorMessage = errorMessages[random.Next(errorMessages.Length)]; // Random error message

            errorQueue.Add(new ErrorQueue
            {
                refPrest = refPrest,
                firstDate = firstDate,
                eSMartBEEntity = (int)eSMartBEEntity,
                eStatus = (int)eStatus,
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

    public string eSMartBEEntityName => ((enmSMartBEEntity)eSMartBEEntity).ToString();
    public string eStatusName => ((enmStatus)eStatus).ToString();
}

public enum enmStatus
{
    netGroupProcess = 90, //utiliser pr changer le statut quand on parcour les prestations jusqu a la derniere
    ignoreSocioQ = 93,
    holdOn = 94, //when working on the dmfa, prest from the previous trimester are put on hold !
    ignoreRefParentNotNull = 95,
    ignoreOldProcess = 96, //ignorer car déjà traité par l'ancienne version de forhrm
    ignoreRPI = 97,
    ignore = 98,
    processed = 99,
    errorState = 100
}

public enum enmSMartBEEntity
{
    /*ATTENTION: Les valeurs de cette énum sont reprises dans 
    une lookup column (SMartBE.MasterData.SMartGroupCompanies), il faut donc
    mettre à jour cette table lorsque l'on modifie ici l'énumération. */
    nonAttribue = 0,
    PU = 1,
    APMC = 3,
    UBIK = 8,
    PA = 16,
    SI = 17,
    SmartBE = 18,
    MatLease = 19,
    SmartImmo = 20,
    InterimPaleis = 21,
    FA = 22,
    TS = 31,
    SMartCoop = 23,
    SmartProductions = 25,
    SmartSol = 30
}
