namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresRepository _genresRepository;

        public GenresController(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _genresRepository.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create(GenreDto genreDto)
        {
            var genre = new Genre { Name = genreDto.Name };

            await _genresRepository.AddAsync(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]byte id ,[FromBody] GenreDto genreDto)
        {

           var genre = await _genresRepository.GetByIdAsync(id);
            if(genre == null)
            {
                return NotFound($"No genre was found with ID: {id}");
            }
            genre.Name = genreDto.Name;
            _genresRepository.Update(genre);
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresRepository.GetByIdAsync(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            _genresRepository.Delete(genre);

            return Ok(genre);
        }


    }
}
