using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.DTOs.Question;
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
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public QuizController(IQuizService quizService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _quizService = quizService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
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
    
    [HttpPost("generate-quiz")]
    public async Task<ActionResult<QuizDto>> GenerateQuiz([FromBody] QuizGenerationRequestDto request)
    {
        var client = _httpClientFactory.CreateClient("AIGeneratorMicroservice");
        var endpoint = _configuration["AIGeneratorMicroservice:Endpoints:GenerateQuiz"];
        var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response;
        try
        {
            response = await client.PostAsync(endpoint, content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { error = "AI Microservice is not reachable" });
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, new { error = "Eroare de la microserviciu", details = errorContent });
        }

        var responseJson = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var listOfQuestions = JsonSerializer.Deserialize<List<AIQuizQuestionDto>>(responseJson, options);
        return Ok(listOfQuestions);
    }
    
    [HttpGet("test-client")]
    public IActionResult TestClient()
    {
        if (_httpClientFactory == null)
            return BadRequest("IHttpClientFactory este NULL!");

        var client = _httpClientFactory.CreateClient("AIGeneratorMicroservice");
        return Ok(new { 
            BaseAddress = client.BaseAddress,
            Headers = client.DefaultRequestHeaders.ToString()
        });
    }
    
}