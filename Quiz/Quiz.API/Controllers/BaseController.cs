using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.API.Controllers;

public class BaseController : ControllerBase
{
    protected string GetUserIdFromJwt()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userIdClaim!;
    }
}