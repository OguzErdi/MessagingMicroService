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
            CreateMap<MessageEntity, MessageEntityViewModel>()
                .ForMember(x => x.Time, cd => cd.MapFrom(map => map.Time.ToString("dd/MM/yyyy")));
        }
    }
}
