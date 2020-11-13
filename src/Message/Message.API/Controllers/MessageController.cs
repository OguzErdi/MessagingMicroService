using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Message.API.ViewModels;
using Message.Application.Interfaces;
using Message.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Message.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;
        private readonly ILogger<MessageController> logger;

        public MessageController(IMessageService messageService, IMapper mapper, ILogger<MessageController> logger)
        {
            this.messageService = messageService;
            this.mapper = mapper;
            this.logger = logger;
            logger.LogInformation("Hello first log in MessageController Constructor");
        }

        [HttpGet("{senderUsername}")]
        [ProducesResponseType(typeof(MessageLineViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(string senderUsername)
        {

            var receiverUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await this.messageService.GetLastMessage(senderUsername, receiverUsername);
            if (result.Success)
            {
                var messageLineViewModel = new MessageLineViewModel(receiverUsername, result.Data);
                return Ok(messageLineViewModel);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("history/{withWhom}")]
        public async Task<IActionResult> GeHistorytAsync(string withWhom)
        {
            var senderUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await this.messageService.GetMessageHistory(senderUsername, withWhom);
            if (result.Success)
            {
                var vmList = mapper.Map<List<MessageEntityViewModel>>(result.Data.MessageEntities);
                return Ok(vmList);
            }

            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MessageLineViewModel messageLineViewModel)
        {
            var senderUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await this.messageService.AddMessage(messageLineViewModel.MessageLine, senderUsername, messageLineViewModel.ReceiverUsername);
            
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

    }
}
