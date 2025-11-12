using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.DTOs.Quiz;
using Quiz.Application.Services.Quiz;
using SharedLibrary;
using SharedLibrary.Wrapper;

namespace Quiz.API.Controllers;

[ApiController]
[Route("api/quiz")]
[Authorize]
public class QuizController : BaseController
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<QuizDto>>> Create([FromBody] QuizDto dto)
    {
        var userId = GetUserIdFromJwt();
        var result = await _quizService.CreateQuizAsync(dto, userId);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Data);
    }

    [HttpGet("public/quizzes")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<QuizDto>>>> GetAll()
    {
        var result = await _quizService.GetAllQuizzesAsync(onlyPublic: true);
        return Ok(result.Data);
    }

    [HttpGet("user/quizzes")]
    public async Task<ActionResult<ApiResponse<IEnumerable<QuizDto>>>> GetUserQuizzes()
    {
        var userId = GetUserIdFromJwt();
        var result = await _quizService.GetAllQuizzesAsync(userId: userId);
        return Ok(result.Data);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<QuizDto>>> GetById(Guid id)
    {
        var result = await _quizService.GetQuizByIdAsync(id);
        if (!result.Success) return NotFound(result.Message);
        return Ok(result.Data);
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse>> Update([FromBody] QuizDto dto)
    {
        var result = await _quizService.UpdateQuizAsync(dto);
        if (!result.Success) return BadRequest(result.Message);
        return Ok(result.Message);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse>> Delete(Guid id)
    {
        var result = await _quizService.DeleteQuizAsync(id);
        if (!result.Success) return NotFound(result.Message);
        return Ok(result.Message);
    }
}