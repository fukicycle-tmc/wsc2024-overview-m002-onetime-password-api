using Microsoft.AspNetCore.Mvc;
using onetime_password_app.DTO;
using onetime_password_app.Services;

namespace onetime_password_app.Controllers;

[ApiController]
[Route("/api/v1/verify")]
public sealed class VerifyConteroller : ControllerBase
{
    private readonly OneTimePasswordService _oneTimePasswordService;
    public VerifyConteroller(OneTimePasswordService oneTimePasswordService)
    {
        _oneTimePasswordService = oneTimePasswordService;
    }

    [HttpPost]
    public IActionResult PostVerify([FromBody] VerifyRequestDTO verifyRequestDTO)
    {
        if (_oneTimePasswordService.Verify(verifyRequestDTO.Guid, verifyRequestDTO.Password))
        {
            return Ok();
        }
        return StatusCode(403);
    }
}
