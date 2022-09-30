namespace DeGierigeHollander.Gui.Shared;

public class ReportSession
{
    public decimal LaptopQuestionReponsePricePerYear { get; set; }
    public decimal HomeQuestionPricePerYear { get; set; }

    public decimal ShowerPerYear { get; set; }

    public decimal Total => LaptopQuestionReponsePricePerYear + HomeQuestionPricePerYear;
    
    public List<string> TotalAsString { get; set; } = new List<string>();
    public int Toilet { get; set; }
}