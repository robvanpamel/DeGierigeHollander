namespace DeGierigeHollander.Gui.Shared;

public class Answer<T>
{
    public T Value { get; set; }
}

public class AnswerResponse
{
    public string ResponseText { get; set; }

    public AnswerResponse(string responseText)
    {
        ResponseText = responseText;
    }
}
