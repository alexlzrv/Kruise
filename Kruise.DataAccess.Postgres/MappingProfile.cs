using AutoMapper;
using Kruise.DataAccess.Postgres.Entities;

namespace Kruise.DataAccess.Postgres;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, Domain.Post>();
    }
}
