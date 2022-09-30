using System.Globalization;
using DeGierigeHollander.Gui.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DeGierigeHollander.Gui.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController
{
    private readonly ReportSession _reportSession;

    public QuestionController(ReportSession reportSession)
    {
        _reportSession = reportSession;
    }

    public decimal PricePerKiloWatt { get; set; } = 0.896m;
    public decimal LaptopConsumptionStandByKWH { get; set; } = 0.1m;

    public decimal HomeworkHeatingConsumption { get; set; } = 2;

    public decimal DailyHeatingVolumeInM3Gas = 2.1m;
    public decimal DailyShowerVolumeInM3Gas = 0.13m;

    public decimal PricePerM3Gas = 3.83m;
    
    public int YearlyDishwasherKWH = 305;

    [HttpGet]
    public IEnumerable<Question> Get()
    {
        return new List<Question>()
        {
            // new()
            // {
            //     QuestionString = "Hoeveel uur laat je je laptop aan per week?  ",
            //     ResponseUrl = "/laptopQuestion",
            //     AnswerType = typeof(int).ToString()
            // },
            new()
            {
                QuestionString = "Hoeveel dagen werk je van thuis per week?  ",
                ResponseUrl = "/homeworkQuestion",
                AnswerType = typeof(int).ToString()
            },
            new()
            {
                QuestionString = "Kan je douchen op het werk?",
                ResponseUrl = "/doucheQuestion",
                AnswerType = typeof(bool).ToString()
            },
            
            new()
            {
                QuestionString = "Hoeveel keer per jaar volgt u een opleiding of cc bij Axxes?",
                ResponseUrl = "/ccQuestion",
                AnswerType = typeof(int).ToString()
            },
            new()
            {
                QuestionString = "Neem je u vuile vaat mee naar het werk?",
                ResponseUrl = "/dishwasherQuestion",
                AnswerType = typeof(bool).ToString()
            },

        };
    }

    [HttpPost("/laptopQuestion")]
    public AnswerResponse LaptopQuestion(Answer<int> answer)
    {
        var calculation = answer.Value * PricePerKiloWatt * LaptopConsumptionStandByKWH * 52;
        _reportSession.LaptopQuestionReponsePricePerYear = calculation;
        var responseText = $"Het verbruik van je laptop per jaar is {calculation.ToString(CultureInfo.InvariantCulture)}EUR";
        _reportSession.TotalAsString.Add(responseText);
        return new AnswerResponse(responseText);
    }
    
    [HttpPost("/homeworkQuestion")]
    public AnswerResponse HomeworkQuestion(Answer<int> answer)
    {
        var priceperHourHomework = DailyHeatingVolumeInM3Gas / 14m * PricePerM3Gas;
        var uurHomeworkPerWeek = answer.Value * 8;
        var result = uurHomeworkPerWeek * priceperHourHomework * 52;
        _reportSession.HomeQuestionPricePerYear = result;

        var calculation = answer.Value * PricePerKiloWatt * LaptopConsumptionStandByKWH * 52;
        _reportSession.LaptopQuestionReponsePricePerYear = calculation;
        
        var laptopText = $"Het verbruik van je laptop per jaar is {calculation.ToString(CultureInfo.InvariantCulture)}EUR";
        _reportSession.TotalAsString.Add(laptopText);
        
        
        
        var toiletText = $"De meerkost van thuiswerk voor de grote boodschap per jaar is: {42} EUR {Environment.NewLine}";
        _reportSession.Toilet = 42;
        _reportSession.TotalAsString.Add(toiletText);

        var heatingText = $"De meerkost van thuiswerk voor verwarming per jaar (zonder maaltijdcheques) is: {result} EUR ";
        _reportSession.TotalAsString.Add(heatingText);

        return new AnswerResponse($"{laptopText} {Environment.NewLine} {heatingText} {Environment.NewLine} {toiletText}");
    }

    [HttpPost("/doucheQuestion")]
    public AnswerResponse DoucheQuestion(Answer<bool> answer)
    {
        var result = 0.0m;
        if (answer.Value)
        {
            var pricePerShowerPerDay = DailyShowerVolumeInM3Gas * PricePerM3Gas;
            result = pricePerShowerPerDay * 5 * 52;
            _reportSession.ShowerPerYear = result;
            var response = $"Door het douchen op werk bespaar je per jaar: {result} EUR";
            _reportSession.TotalAsString.Add(response);
            return new AnswerResponse(response);

        }

        var responseText = $"Dommerik, Aangezien je geen douche neemt op het werk bespaar je hier niets!";  
        _reportSession.TotalAsString.Add(responseText);
        return new AnswerResponse(responseText);
    }

    
    [HttpPost("/ccQuestion")]
    public AnswerResponse CcQuestion(Answer<int> answer)
    {
        var result = 0.0m;
 
        result = answer.Value * 5;
        _reportSession.CcQuestionPricePerYear = result;
 
        var responseText = $"Door te eten op de CC bespaar je per jaar: {result} EUR";
        _reportSession.TotalAsString.Add(responseText);
        return new AnswerResponse(responseText);
    }
 
    [HttpPost("/dishwasherQuestion")]
    public AnswerResponse DishwasherQuestion(Answer<bool> answer)
    {
        var result = 0.0m;
        if (answer.Value)
        {
            result = YearlyDishwasherKWH * PricePerKiloWatt;
        }
 
        _reportSession.DishwasherQuestionPerYear = result;
 
        var responseText = $"Door je vuile vaat achter te laten op kantoor bespaar je per jaar: {result} EUR";
        _reportSession.TotalAsString.Add(responseText);
     
        return new AnswerResponse(responseText);
    }
    
    
    [HttpGet("/report")]
    public ReportSession GetReport()
    {
        return _reportSession;
    }
}