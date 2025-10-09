using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Identity;
using Shared.Identity;

namespace Service.Mapping
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<RegisterDto,User>().ReverseMap();
        }

    }
}
