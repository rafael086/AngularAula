using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace AngularAula.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public EventoController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _dataRepository.GetAllEventosAsync(true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _dataRepository.GetEventosByIdAsync(id, true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
                throw;
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                return Ok(await _dataRepository.GetEventoByTemaAsync(tema, true));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                _dataRepository.Add(evento);
                if (await _dataRepository.SaveChangesAsync())
                    return Created($"/api/evento/{evento.Id}", evento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
                throw;
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Evento evento)
        {
            try
            {
                var eventoLoc = await _dataRepository.GetEventosByIdAsync(id, false);
                if (eventoLoc == null)
                    return NotFound();
                _dataRepository.Update(evento);
                if (await _dataRepository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", evento);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _dataRepository.GetEventosByIdAsync(id, false);
                if (evento == null)
                    return NotFound();
                _dataRepository.Delete(evento);
                if (await _dataRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
            }
            return BadRequest();
        }
    }

}

