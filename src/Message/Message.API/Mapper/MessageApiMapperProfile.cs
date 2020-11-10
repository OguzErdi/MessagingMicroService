using AutoMapper;
using Message.API.ViewModels;
using Message.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Mapper
{
    public class BasketApiMapperProfile : Profile
    {
        public BasketApiMapperProfile()
        {
            CreateMap<MessageEntity, MessageViewModel>().ReverseMap();
        }
    }
}
