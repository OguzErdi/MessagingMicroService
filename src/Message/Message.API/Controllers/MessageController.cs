using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Message.API.ViewModels;
using Message.Application.Interfaces;
using Message.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Message.API.Controllers
{
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

        [HttpGet("{receiverUsername}")]
        [ProducesResponseType(typeof(MessageLineViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MessageLineViewModel>> GetAsync(string senderUsername, string receiverUsername)
        {
            var messageLine = await this.messageService.GetLastMessage(senderUsername, receiverUsername);
            var messageLineViewModel = new MessageLineViewModel(senderUsername, receiverUsername, messageLine);

            return messageLineViewModel;
        }

        [HttpGet("history/{withWhom}")]
        public async Task<MessageHistroy> GeHistorytAsync(string senderUsername, string withWhom)
        {
            var messageHistory = await this.messageService.GetMessageHistory(senderUsername, withWhom);
            

            //var mapped = this.mapper.Map<MessageViewModel>(messageHistory);
            //if (mapped == null)
            //{
            //    throw new Exception($"Entity could not be mapped.");
            //}

            return messageHistory;
        }

        [HttpPost]
        public async Task PostAsync([FromBody]MessageLineViewModel messageLineViewModel)
        {
            var result = await this.messageService.AddMessage(messageLineViewModel.MessageLine, messageLineViewModel.SenderUsername, messageLineViewModel.ReceiverUsername);
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
