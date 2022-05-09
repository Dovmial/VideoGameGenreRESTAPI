#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Data;
using VideoGameAPI.DTO;
using VideoGameAPI.Interfaces;
using VideoGameAPI.Models;
using VideoGameAPI.ViewModels;

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideogamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;

        public VideogamesController(IMapper mapper, IUnitRepository unitrepository)
        {
            _mapper = mapper;
            _unitRepository = unitrepository;
        }


        // GET: api/Videogames
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VideogameViewModel>))]
        public IActionResult GetVideogames()
        {
            var videoGames = _unitRepository.Videogame.GetAll();
            List<VideogameViewModel> result = new List<VideogameViewModel>();   
            foreach (var game in videoGames)
            {
                var genresDTO = _mapper.Map<List<GenreDTO>>(_unitRepository.Videogame.GetGenresByVideogame(game));
                result.Add(new VideogameViewModel(game, genresDTO));
            }
            return Ok(result);
        }

        // GET: api/Videogames/5
        
        [HttpGet("{id}")]
        [ProducesResponseType(200,Type = typeof(VideogameViewModel))]
        public IActionResult GetVideogame(int id)
        { 
            var videogame = _unitRepository.Videogame.GetbyId(id);
            
            if (videogame == null)
                return NotFound();

            var genresDTO = _mapper.Map<List<GenreDTO>>(_unitRepository.Videogame.GetGenresByVideogame(videogame));
            var videogameVM = new VideogameViewModel(videogame, genresDTO);
            return Ok(videogameVM);
        }

        // PUT: api/Videogames/5
        //Какая-то проблема с отслеживаием жанров
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateVideogame(int id, [FromBody]VideogameViewModel videogameVM)
        {
            if (id != videogameVM.Id)
                return BadRequest();
            if (_unitRepository.Videogame.IsExists(id) == false)
                return NotFound();

            var genres = _mapper.Map<IEnumerable<Genre>>(videogameVM.Genres);
            var videogame = _mapper.Map<Videogame>(videogameVM.GetVideogameDTO());

            _unitRepository.Videogame.Update(videogame, genres);

            if(! await _unitRepository.SaveAsync())
            {
                ModelState.AddModelError("", "Не удалось обновить запись");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Videogames
        // Скрытое в EF самопроизвольное добавление жанров (если их id = 0)
        [HttpPost]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PostVideogame([FromBody]VideogameViewModel videogameVM)
        {
            var videogameDTO = videogameVM.GetVideogameDTO();
            if(videogameDTO is null)
                return BadRequest(ModelState);
            var videogame = _mapper.Map<Videogame>(videogameDTO);
            if(_unitRepository.Videogame.IsExists(videogame))
            {
                ModelState.AddModelError("", "Такая звпись уже существует.");
                return StatusCode(422, ModelState);
            }

            var genres = _mapper.Map<IEnumerable<Genre>>(videogameVM.Genres);

            await _unitRepository.Videogame.CreateAsync(videogame, genres);
            if(! await _unitRepository.SaveAsync())
            {
                ModelState.AddModelError("", "Не удалось обновить запись");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        // DELETE: api/Videogames/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVideogame(int id)
        {
            var videogame = _unitRepository.Videogame.GetbyId(id);
            if (videogame is null)
                return NotFound();
            
            _unitRepository.Videogame.Delete(videogame);
            if(! await _unitRepository.SaveAsync())
            {
                ModelState.AddModelError("", "Не удалось удалить запись.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
