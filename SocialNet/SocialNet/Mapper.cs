﻿using AutoMapper;
using SocialNet.Models;

namespace SocialNet
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
            CreateMap<LoginViewModel, User>();

            CreateMap<EditViewModel, User>();
            CreateMap<User, EditViewModel>().ForMember(x => x.UserId, opt => opt.MapFrom(c => c.Id));

            CreateMap<UserWithFriendExt, User>();
            CreateMap<User, UserWithFriendExt>();
        }
    }
}
