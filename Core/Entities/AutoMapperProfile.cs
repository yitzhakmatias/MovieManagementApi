using AutoMapper;
using MovieManagementApi.Core.Dtos;

namespace MovieManagementApi.Core.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors));

        CreateMap<Actor, ActorDto>();
        CreateMap<MovieRating, RatingDto>();
    }
}