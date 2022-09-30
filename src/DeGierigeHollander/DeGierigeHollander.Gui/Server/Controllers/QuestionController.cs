using System.Globalization;
using DeGierigeHollander.Gui.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeGierigeHollander.Gui.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController
{
    public decimal PricePerKiloWatt { get; set; } = 2;
    public decimal LaptopConsumptionStandBy { get; set; } = 2;
    
    public decimal HomeworkHeatingConsumption { get; set; } = 2;

    public int DailyVolumeInM3Gas = 3;

    public decimal PricePerM3Gas = 3.83m;
    
    [HttpGet]
    public IEnumerable<Question> Get()
    {
        return new List<Question>()
        {
            new()
            {
                QuestionString = "Hoeveel uur laat je je laptop aan per week?  ",
                ResponseUrl = "/laptopQuestion",
                AnswerType = typeof(Answer<int>).ToString()
            },
            new()
            {
                QuestionString = "Hoeveel dagen werk je van thuis per week?  ",
                ResponseUrl = "/homeworkQuestion",
                AnswerType = typeof(Answer<int>).ToString()
            }
        };
    }
    
    [HttpPost("/laptopQuestion")]
    public AnswerResponse LaptopQuestion(Answer<int> answer)
    {
        var responseText = $"Het verbruik van je laptop in slaapstand per jaar is {(answer.Value * PricePerKiloWatt * LaptopConsumptionStandBy * 52).ToString(CultureInfo.InvariantCulture)}EUR";
        
        return new AnswerResponse(responseText);
    }
    
    [HttpPost("/howeworkQuestion")]
    public AnswerResponse HomeworkQuestion(Answer<int> answer)
    {
        var priceperHourHomework = DailyVolumeInM3Gas / 16m * PricePerM3Gas;
        var uurHomeworkPerWeek = answer.Value * 8;
        var result = uurHomeworkPerWeek * priceperHourHomework * 52;
        var responseText = $"De kost van thuiswerk voor verwarming per jaar is: {result} EUR";
        
        return new AnswerResponse(responseText);
    }
}