namespace MoviesApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<AddMovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}