using Quartz;
using QuizSession.Application.Services.BackgroundProcessor;

namespace QuizSession.Application.Jobs.Quiz;

public class StartQuizJob(IQuizSessionBackgroundProcessor processor) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var sessionId = Guid.Parse(context.JobDetail.Key.Name);
        await processor.StartQuizSessionAsync(sessionId);
    }
}

public class EndQuizJob(IQuizSessionBackgroundProcessor processor) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var sessionId = Guid.Parse(context.JobDetail.Key.Name);
        await processor.EndQuizSessionAsync(sessionId);
    }
}