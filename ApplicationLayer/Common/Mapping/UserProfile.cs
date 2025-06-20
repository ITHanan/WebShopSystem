using ApplicationLayer.Authorize.Commands.Register;
using DomainLayer.Models;
using AutoMapper;

namespace ApplicationLayer.Common.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterCommand, User>()
             .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // we hash it manually
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail.ToLower())); // normalize email
        }
    }
}
