using Microsoft.AspNetCore.Mvc;
using onetime_password_app.DTO;

namespace onetime_password_app.Controllers;

[ApiController]
[Route("/api/v1/register")]
public sealed class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult PostUser([FromBody] RegisterRequestDTO registerRequestDTO)
    {
        string guid = registerRequestDTO.Guid.ToString();
        string fullName = $"{registerRequestDTO.FirstName} {registerRequestDTO.LastName}";
        string fileName = $"{guid}.txt";
        if (!Directory.Exists("Debug"))
        {
            Directory.CreateDirectory("Debug");
        }
        string fullPath = System.IO.Path.Combine("Debug", fileName);
        if (System.IO.Directory.GetFiles("Debug").Any(a => a == fileName))
        {
            return BadRequest("Already registered.");
        }
        else
        {
            try
            {
                System.IO.File.WriteAllText(fullPath, fullName);
                return Created("user", guid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
