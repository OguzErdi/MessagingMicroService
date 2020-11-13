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

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            this.mapper = mapper;
        }

        [HttpGet("{senderUsername}")]
        [ProducesResponseType(typeof(MessageLineViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MessageLineViewModel>> GetAsync(string senderUsername)
        {
            var receiverUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var messageLine = await this.messageService.GetLastMessage(senderUsername, receiverUsername);
            var messageLineViewModel = new MessageLineViewModel(receiverUsername, messageLine);

            return messageLineViewModel;
        }

        [HttpGet("history/{withWhom}")]
        public async Task<MessageHistroy> GeHistorytAsync(string withWhom)
        {
            var senderUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var messageHistory = await this.messageService.GetMessageHistory(senderUsername, withWhom);
            

            return messageHistory;
        }

        [HttpPost]
        public async Task PostAsync([FromBody]MessageLineViewModel messageLineViewModel)
        {
            var senderUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await this.messageService.AddMessage(messageLineViewModel.MessageLine, senderUsername, messageLineViewModel.ReceiverUsername);
            if (result)
            {
                Ok();
            }
            else
            {
                BadRequest();
            }
        }

    }
}
