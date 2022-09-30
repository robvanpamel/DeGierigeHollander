namespace DeGierigeHollander.Gui.Server.Services;

public class CalculationService
{
    public List<ICaculator> Calculators { get; set; } = new();
    public CalculationService()
    {
        
    }

    public void AddCalculation()
    {
        
    }
    
    public decimal Calculate()
    {
        decimal total = 0;
        
        foreach (var calculator in Calculators)
        {
            total+=calculator.Calculate();
        }

        return total;
    }
}

public interface ICaculator
{
    decimal Calculate();
}