using EvoNaplo.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace EvoNaplo.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){

            var config = new MapperConfiguration(cfg => {
                CreateMap<StudentDto, IdentityUser<Guid>>().ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
                CreateMap<IdentityUser<Guid>, StudentDto>().ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.UserName));
                CreateMap<MentorDto, IdentityUser<Guid>>().ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
                CreateMap<IdentityUser<Guid>, MentorDto>().ForMember(u => u.Email, opt => opt.MapFrom(ur => ur.UserName));
            });
            
        }
    }
}
