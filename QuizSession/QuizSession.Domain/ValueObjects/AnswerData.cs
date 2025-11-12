namespace QuizSession.Domain.ValueObjects;

public class AnswerData : ValueObject
{
    public string? TextAnswer { get; private set; }
    public List<Guid>? SelectedOptionIds { get; private set; }

    private AnswerData() { }

    private AnswerData(string? textAnswer, List<Guid>? selectedOptionIds)
    {
        TextAnswer = textAnswer;
        SelectedOptionIds = selectedOptionIds;
    }

    public static AnswerData FromText(string text) =>
        new AnswerData(text, null);

    public static AnswerData FromOptions(IEnumerable<Guid> options) =>
        new AnswerData(null, options.ToList());

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return TextAnswer;

        if (SelectedOptionIds is null) yield break;
        foreach (var id in SelectedOptionIds.OrderBy(x => x))
            yield return id;
    }
}