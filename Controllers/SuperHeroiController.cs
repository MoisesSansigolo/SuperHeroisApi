using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroisApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroiController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHeroi>>> Get()
        {
            return Ok(await _context.SuperHerois.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroi>> GetById(int id)
        {
            var heroi = await _context.SuperHerois.FindAsync(id);
            if (heroi == null)
            {
                return BadRequest("Heroi não encontrado");
            }
            return Ok(heroi);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroi>>> AddHero(SuperHeroi heroi)
        {
            _context.SuperHerois.Add(heroi);            
            await _context.SaveChangesAsync();

            return Ok("Heroi criado com sucesso");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHeroi>>> UpdateHero(SuperHeroi request)
        {
            var heroi = await _context.SuperHerois.FindAsync(request.Id);
            if (heroi == null)

                return BadRequest("Heroi não encontrado");

            heroi.Name = request.Name;
            heroi.FirstName = request.FirstName;
            heroi.LastNmae = request.LastNmae;
            heroi.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(heroi);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHeroi>>> Delete(int id)
        {
            var heroi = await _context.SuperHerois.FindAsync(id);
            if (heroi == null)
                return BadRequest("Heroi não encontrado");

            _context.SuperHerois.Remove(heroi);
            await _context.SaveChangesAsync();
            return Ok("Heroi excluido com sucesso.");
        }
    }
}
