using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.DTOs.Question;
using Quiz.Application.Services.Question;
using SharedLibrary;

namespace Quiz.API.Controllers;

[ApiController]
[Route("api/question")]
[Authorize]
public class QuestionController : BaseController
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost("{quizId:guid}/add")]
    public async Task<ActionResult> AddQuestionToQuiz(Guid quizId, [FromBody] QuestionDto dto)
    {
        var result = await _questionService.AddQuestionToQuizAsync(dto, quizId);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdateQuestion([FromBody] QuestionDto dto)
    {
        var result = await _questionService.UpdateQuestionAsync(dto);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Message);
    }
    
    [HttpDelete("{questionId:guid}/delete")]
    public async Task<ActionResult> DeleteQuestion(Guid questionId)
    {
        var result = await _questionService.DeleteQuestionAsync(questionId);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Message);
    }

    [HttpGet("{questionId:guid}")]
    public async Task<ActionResult> GetQuestionById(Guid questionId)
    {
        var result = await _questionService.GetQuestionByIdAsync(questionId);
        if (!result.Success) return NotFound(result.Message);
        return Ok(result.Data);
    }
}