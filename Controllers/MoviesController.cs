namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMoviesRepository _moviesRepository;
        private readonly IGenresRepository _genresRepository;

        private readonly List<string> _allowedExtensions = [".jpg", ".png"];
        private readonly long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMoviesRepository moviesRepository, IGenresRepository genresRepository, IMapper mapper)
        {
            _moviesRepository = moviesRepository;
            _genresRepository = genresRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesRepository.GetAllAsync();

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesRepository.GetByIdAsync(id);

            if (movie == null)
                return NotFound();

            var dto = _mapper.Map<MovieDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var isValidGenre = await _genresRepository.IsValidGenreAsync(genreId);

            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            var movies = await _moviesRepository.GetAllAsync(genreId);

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] AddMovieDto dto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _genresRepository.IsValidGenreAsync(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            using var dataStream = new MemoryStream();

            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);

            movie.Poster = dataStream.ToArray();

            await _moviesRepository.AddAsync(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateMovieDto dto)
        {
            var movie = await _moviesRepository.GetByIdAsync(id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            var isValidGenre = await _genresRepository.IsValidGenreAsync(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            if (dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;
            movie.Storeline = dto.Storeline;
            movie.Rate = dto.Rate;

            _moviesRepository.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesRepository.GetByIdAsync(id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            _moviesRepository.Delete(movie);

            return Ok(movie);
        }
    }
}