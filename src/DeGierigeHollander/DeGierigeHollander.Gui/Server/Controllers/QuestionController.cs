using System.Globalization;
using DeGierigeHollander.Gui.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeGierigeHollander.Gui.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController
{
    public decimal PricePerKiloWatt { get; set; }
    public decimal LaptopConsumptionStandBy { get; set; }
    
    [HttpGet]
    public IEnumerable<Question> Get()
    {
        return new List<Question>()
        {
            new Question()
            {
                QuestionString = "Hoeveel uur laat je je laptop aan per week?  ",
                ResponseUrl = "/laptopQuestion",
                AnswerType = typeof(Answer<int>)
            }
        };
    }
    
    [HttpPost("/laptopQuestion")]
    public AnswerResponse LaptopQuestion(Answer<int> answer)
    {
        var responseText = (answer.Value * PricePerKiloWatt * LaptopConsumptionStandBy * 52).ToString(CultureInfo.InvariantCulture);
        return new AnswerResponse(responseText);
    }
}