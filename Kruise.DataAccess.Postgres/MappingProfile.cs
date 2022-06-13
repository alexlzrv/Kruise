using AutoMapper;
using Kruise.DataAccess.Postgres.Entities;

namespace Kruise.DataAccess.Postgres;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostEntity, Domain.PostModel>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<AccountEntity, Domain.AccountModel>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
