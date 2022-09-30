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
    
    [HttpGet]
    public IEnumerable<Question> Get()
    {
        return new List<Question>()
        {
            new Question()
            {
                QuestionString = "Hoeveel uur laat je je laptop aan per week?  ",
                ResponseUrl = "/laptopQuestion",
                AnswerType = typeof(Answer<int>).ToString()
            },
            new Question()
            {
                QuestionString = "Hoeveel uur laat je je laptop aan per week?  ",
                ResponseUrl = "/laptopQuestion",
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
}