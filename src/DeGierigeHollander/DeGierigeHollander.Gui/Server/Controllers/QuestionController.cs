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

    [HttpGet]
    public IEnumerable<Question> Get()
    {
        return new List<Question>()
        {
            new()
            {
                QuestionString = "Hoeveel uur laat je je laptop aan per week?  ",
                ResponseUrl = "/laptopQuestion",
                AnswerType = typeof(int).ToString()
            },
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
            }
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
        
        var responseText = $"Het verbruik van je laptop per jaar is {calculation.ToString(CultureInfo.InvariantCulture)}EUR";
        _reportSession.TotalAsString.Add(responseText);
        
        var otherResponseText = $"De meerkost van thuiswerk voor verwarming per jaar (zonder maaltijdcheques) is: {result} EUR {Environment.NewLine}Het verbruik van je laptop per jaar is {calculation.ToString(CultureInfo.InvariantCulture)}EUR";
        _reportSession.TotalAsString.Add(otherResponseText);
        
        
        var toiletText = $"De meerkost van thuiswerk voor de grote boodschap per jaar is: {42} EUR {Environment.NewLine}";
        _reportSession.Toilet = 42;
        _reportSession.TotalAsString.Add(otherResponseText);

        return new AnswerResponse(responseText);
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

    [HttpGet("/report")]
    public ReportSession GetReport()
    {
        return _reportSession;
    }
}