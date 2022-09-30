namespace DeGierigeHollander.Gui.Shared;

public class QuestionBase
{
    public string QuestionString { get; set; } = string.Empty;
    public string ResponseUrl { get; set; } = string.Empty;
    public Type AnswerType { get; set; }
}