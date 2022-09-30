namespace DeGierigeHollander.Gui.Server.Controllers;

public class ReportSession
{
    public decimal LaptopQuestionReponsePricePerYear { get; set; }
    public decimal HomeQuestionPricePerYear { get; set; }

    public decimal ShowerPerYear { get; set; }

    
    public decimal Sum => LaptopQuestionReponsePricePerYear + HomeQuestionPricePerYear;
}