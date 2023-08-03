using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Vca.Data.Entities;
using Vca.Data.Entities.Identity;
using Vca.Models;

namespace Vca.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserContactEntity, UserContactModel>()
               .ReverseMap()
                    .ForMember(x => x.Id, d => d.Ignore())
                    .ForMember(x => x.CreatedAt, d => d.Ignore())
                    .ForMember(x => x.UserId, d => d.Ignore())
                    .ForMember(x => x.User, d => d.Ignore());
            CreateMap<AuthorizedUserModel, UserModel>()
                .ReverseMap();
        }
    }
}
