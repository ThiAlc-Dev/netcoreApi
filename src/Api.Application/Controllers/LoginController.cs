using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Interfaces.Services;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> LoginAsync([FromBody] LoginDTO userentity, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                if (userentity == null)
                    return BadRequest();

                try
                {
                    var result = await service.FindByLogin(userentity);
                    if (result != null)
                        return Ok(result);
                    else
                        return NotFound();
                }
                catch (ArgumentException ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }
    }
}