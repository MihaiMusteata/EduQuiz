using EduQuiz.Application.DTOs.QuizSession;
using EduQuiz.Application.Services.QuizSession;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduQuiz.API.Controllers;

[ApiController]
[Route("api/quiz-session")]
public class QuizSessionController(IQuizSessionService _quizSessionService) : BaseController
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateNewSession([FromBody] QuizSessionDto quizSessionDto)
    {
        var hostUserId = GetUserIdFromJwt();
        var response = await _quizSessionService.CreateNewSession(quizSessionDto, hostUserId);
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    [HttpGet("{sessionId:guid}")]
    public async Task<IActionResult> GetSessionById([FromRoute] Guid sessionId)
    {
        var response = await _quizSessionService.GetSessionByIdAsync(sessionId);
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    [HttpGet("host/{sessionId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetSessionByIdAsHost([FromRoute] Guid sessionId)
    {
        var hostUserId = GetUserIdFromJwt();
        var response = await _quizSessionService.GetSessionByIdAsync(sessionId, hostUserId);
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    [HttpPost("{sessionId:guid}/update-status")]
    [Authorize]
    public async Task<IActionResult> UpdateSessionStatus([FromRoute] Guid sessionId)
    {
        var hostUserId = GetUserIdFromJwt();
        var response = await _quizSessionService.UpdateSessionStatusAsync(sessionId, hostUserId);
        return response.Success ? Ok(response.Message) : BadRequest(response.Message);
    }

}