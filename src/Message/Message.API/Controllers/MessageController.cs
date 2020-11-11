using System;
using System.Collections.Generic;
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
        [ProducesResponseType(typeof(MessageViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MessageViewModel>> GetAsync(string receiverUsername)
        {
            var messageEntity = await this.messageService.GetLastMessage("erdi", receiverUsername);
            var mapped = this.mapper.Map<MessageViewModel>(messageEntity);
            if (mapped == null)
            {
                throw new Exception($"Entity could not be mapped.");
            }

            return mapped;
        }

        [HttpGet("history/{receiverUsername}")]
        public async Task<MessageQueue> GeHistorytAsync(string receiverUsername)
        {
            var messageHistory = await this.messageService.GetMessageHistory("erdi", receiverUsername);
            

            //var mapped = this.mapper.Map<MessageViewModel>(messageHistory);
            //if (mapped == null)
            //{
            //    throw new Exception($"Entity could not be mapped.");
            //}

            return messageHistory;
        }

        [HttpPut("{receiverUsername}")]
        public async Task PostAsync(string receiverUsername, [FromBody] MessageViewModel messageViewModel)
        {
            var mapped = this.mapper.Map<MessageEntity>(messageViewModel);
            if (mapped == null)
            {
                throw new Exception($"Entity could not be mapped.");
            }

            var result = await this.messageService.AddMessage(mapped);
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
