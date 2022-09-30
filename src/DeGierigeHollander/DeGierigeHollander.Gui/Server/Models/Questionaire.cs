namespace DeGierigeHollander.Gui.Server.Models;

public class UtilityPrices
{
    public decimal Electricity { get; set; } = 0;
    public decimal Gas { get; set; }
    public decimal Water { get; set; }
    public decimal Gasoline { get; set; }
}


public class IndividualOutput 
{
    public decimal Price { get; set; }
    public Unit Unit { get; set; }
}

public class Answer
{
    public IEnumerable<IndividualOutput> Outputs { get; }

    public Output(IEnumerable<IndividualOutput> outputs)
    {
        Outputs = outputs;
    }
}

public enum Unit
{
    PerDay,
    PerWeek,
    PerMonth,
    PerYear
}

/*


input = Aantal dagen thuiswerk 
        Utilityprice  
output = 


Laptop verbruik (sander & Pieter) 

Slaapstand buiten de werkuren -> 150 kwh/jaar -> €134,40  

300 kwh per jaar -> €268,8 

Heeft u verwarming op het werk? 

 => 2250 euro per jaar minimum 

Data: 

3 m³ gas per dag => 2u stoken * 3 m³ *  prijs gas => 4500 euro per jaar minimum 