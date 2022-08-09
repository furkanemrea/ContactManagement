﻿using AutoMapper;
using ContactAPI.Application.Commands;
using ContactAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Mapper.Profiles
{
    public class EmailMappingProfile : Profile
    {
        public EmailMappingProfile()
        {
            CreateMap<Email, EmailCommand>().ReverseMap();
        }
    }
}
