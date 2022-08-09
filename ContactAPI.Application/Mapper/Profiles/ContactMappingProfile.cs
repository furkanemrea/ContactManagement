using ContactAPI.Application.Response;
using ContactAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;
using ContactAPI.Application.Commands;

namespace ContactAPI.Application.Mapper.Profiles
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactAPI.Core.Entities.Contact, ContactResponse>().ReverseMap();
            CreateMap<ContactAPI.Core.Entities.Contact, CreateContactCommand>().ReverseMap();
        }
    }
}
