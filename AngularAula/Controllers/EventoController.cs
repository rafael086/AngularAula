using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AngularAula.DTO;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public EventoController(IDataRepository dataRepository, IMapper mapper)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<EventoDTO>>(await _dataRepository.GetAllEventosAsync(true)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if(file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", "").Trim());
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar executar upload");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var x = _mapper.Map<EventoDTO>(await _dataRepository.GetEventosByIdAsync(id, true));
                return Ok(x);
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
        public async Task<IActionResult> Post(EventoDTO evento)
        {
            try
            {
                var model = _mapper.Map<Evento>(evento);
                _dataRepository.Add(model);
                if (await _dataRepository.SaveChangesAsync())
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDTO>(model));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
                throw;
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDTO evento)
        {
            try
            {
                var eventoLoc = await _dataRepository.GetEventosByIdAsync(id, false);
                if (eventoLoc == null)
                    return NotFound();
                _mapper.Map(evento, eventoLoc);
                _dataRepository.Update(eventoLoc);
                if (await _dataRepository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDTO>(eventoLoc));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu erro");
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
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

