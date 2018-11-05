using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans.Configuration;
using SmartCache.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartCache.Controllers
{
    [Route("")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IClusterClient orleansClient;
        public EmailController(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                var result = this.orleansClient.GetGrain<IEmailsGrain>(mailAddress.Host);

                bool resultEmail = await result.HasEmailAsync(email);
                if (!resultEmail)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (FormatException ex)
            {
                //log error
                ModelState.AddModelError("email", ex.Message);
                return ValidationProblem();
            }
            catch (Exception ex)
            {
                //log error
                return BadRequest();
            }
        }

        // POST: api/Email
        [HttpPost("{email}")]
        public async Task<IActionResult> Post(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                var result = this.orleansClient.GetGrain<IEmailsGrain>(mailAddress.Host);
     
                bool emailAdded = await result.AddEmailAsync(email);

                if (!emailAdded)
                {
                    return Conflict();
                }

                return Created(new Uri("/", UriKind.Relative), email);
            }
            catch (FormatException ex)
            {
                //log error
                ModelState.AddModelError("email", ex.Message);
                return ValidationProblem();
            }
            catch (Exception ex)
            {
                //log error
                return BadRequest();
            }
        }
    }
}
