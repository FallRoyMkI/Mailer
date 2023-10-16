using Mailer.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MailerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILetterManager _mailManager;

        public UserController(ILetterManager manager)
        {
            _mailManager = manager;
        }


        [HttpGet]
        [Route("/u")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_mailManager.GetAllUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}