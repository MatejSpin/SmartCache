using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans.Configuration;
using SmartCache.Contracts;
using SmartCache.Contracts.Grains;
using SmartCache.Service;
using SmartCache.Service.Models.Exceptions;
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
        private EmailsService emailsService;
        public EmailController(EmailsService emailsService)
        {
            this.emailsService = emailsService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                bool resultEmail = await emailsService.HasEmailAsync(email);
                if (!resultEmail)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (EmailsServiceException ex)
            {
                //log error, maybe provide info to client
                return BadRequest();
            }
        }

        [HttpPost("{email}")]
        public async Task<IActionResult> Post(string email)
        {
            try
            {
                bool emailAdded = await emailsService.AddEmailAsync(email);

                if (!emailAdded)
                {
                    return Conflict();
                }

                return Created(new Uri("/", UriKind.Relative), email);
            }
            catch (EmailsServiceException ex)
            {
                //log error, maybe provide info to client
                return BadRequest();
            }
        }
    }
}
