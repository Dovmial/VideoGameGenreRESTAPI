using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoGameAPI.DTO;
using VideoGameAPI.Interfaces;
using VideoGameAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public GenresController(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        // GET: api/<GenresController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDTO>>(_unitRepository.Genre.GetAll());
            return Ok(genres);
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        public IActionResult GetGenre(int id)
        {
            if (_unitRepository.Genre.IsExists(id) == false)
                return NotFound();
            var genre = _mapper.Map<GenreDTO>(_unitRepository.Genre.GetById(id));

            return Ok(genre);
        }

        // POST api/<GenresController>
        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO genreCreate)
        {
            var genre = _unitRepository.Genre.GetAll()
                .Where(g => g.Name == genreCreate.Name)
                .FirstOrDefault();

            if (genre is not null)
            {
                ModelState.AddModelError("", "Такой жанр уже существует!");
                return StatusCode(422, ModelState);
            }

            var genreMap = _mapper.Map<Genre>(genreCreate);
            await _unitRepository.Genre.CreateAsync(genreMap);

            if (! await _unitRepository.SaveAsync())
            {
                ModelState.AddModelError("", "Не удалось создать новую запись.");
                return StatusCode(500, ModelState);
            }

            return Ok("Запись добавлена.");
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreDTO genreUpdate)
        {
            if (genreUpdate is null)
                return BadRequest(ModelState);
            
            if (!_unitRepository.Genre.IsExists(genreUpdate.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var genreMap = _mapper.Map<Genre>(genreUpdate);
            _unitRepository.Genre.Update(genreMap);
            if (! await _unitRepository.SaveAsync())
            {
                ModelState.AddModelError("", "Не удалось обновить запись");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{genreId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            var genreUnit = _unitRepository.Genre;
            if (!genreUnit.IsExists(genreId))
                return NotFound();
            var genreToDelete = genreUnit.GetById(genreId);
            genreUnit.Delete(genreToDelete);
            if (! await _unitRepository.SaveAsync())
            {
                ModelState.AddModelError("", "Не удалось удалить запись.");
            }
            return NoContent();
        }
    }
}
