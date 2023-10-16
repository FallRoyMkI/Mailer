using Mailer.Contracts;
using Mailer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LetterController : ControllerBase
    {
        private readonly ILetterManager _mailManager;

        public LetterController(ILetterManager manager)
        {
            _mailManager = manager;
        }


        [HttpGet]
        [Route("/m{id}")]
        public IActionResult GetLetterById([FromRoute] int id)
        {
            try
            {
                return Ok(_mailManager.GetLetterById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("/s{id}")]
        public IActionResult GetAllLettersBySenderId([FromRoute] int id)
        {
            try
            {
                return Ok(_mailManager.GetAllLettersBySenderId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("/r{id}")]
        public IActionResult GetAllLettersByReceiverId([FromRoute] int id)
        {
            try
            {
                return Ok(_mailManager.GetAllLettersByReceiverId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("/send")]
        public IActionResult SendLetter(LetterRequestModel letter)
        {
            try
            {
                return Ok(_mailManager.SendLetter(letter.Name, letter.SenderId, letter.ReceiverId, letter.Content));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}