using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiStart.DTOs;
using WebApiStart.Models;
using WebApiStart.Services;

namespace WebApiStart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GyogyszerController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAllGyogyszer()
        {
            try
            {
                List<Gyogyszer> result = new GyogyszerService().GetAllGyogyszer();
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest("Hiba történt"+ ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetByIdGyogyszer(int id)
        {
            try
            {
                Gyogyszer result = new GyogyszerService().GetById(id);
                if (result == null)
                {
                    return Ok(result);
                }
                return NotFound("Nincs megfelelő gyógyszer.");
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba!" + ex.Message);
            }
        }

        [HttpGet("NevHatoanyagLista")]
        public IActionResult NevHatoanyagLista()
        {
            try
            {
                List<NevHatoanyagDTO> result = new GyogyszerService().GetAllGyogyszer().Select(gy => new NevHatoanyagDTO()
                {
                    Id = gy.Id,
                    Nev = gy.Nev,
                    Hatoanyag = gy.Hatoanyag
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Hiba történt" + ex.Message);
            }
        }

        [HttpGet("NevHatoanyagById/{id}")]
        public IActionResult NevHatoanyagById(int id)
        {
            try
            {
                Gyogyszer valasztott = new GyogyszerService().GetById(id);

                if (valasztott == null)
                {
                    return NotFound("Nincs megfelelő gyógyszer.");
                }

                NevHatoanyagDTO result = new NevHatoanyagDTO()
                {
                    Id = valasztott.Id,
                    Nev = valasztott.Nev,
                    Hatoanyag = valasztott.Hatoanyag
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba történt: " + ex.Message);
            }
        }
    }
}