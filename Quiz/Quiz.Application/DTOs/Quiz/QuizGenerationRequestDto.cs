namespace Quiz.Application.DTOs.Quiz;

public class QuizGenerationRequestDto
{
    public int NumQuestions { get; set; }
    public string Topic { get; set; }
    public string Subject { get; set; }
    public string Language { get; set; }
}