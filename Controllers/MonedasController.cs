using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        private readonly MonedaContext _context;
        Random random=new Random();

        public MonedasController(MonedaContext context)
        {
            _context = context;
        }

        // GET: api/Monedas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonedaItem>>> GetMonedaItems()
        {
            foreach (var item in _context.MonedaItems)
            {
                item.Ultimo=random.Next();

                if(item.Ultimo>item.Max){
                    item.Max=item.Ultimo;
                }
            }

            _context.SaveChanges();

            return  _context.MonedaItems;
        }

        // GET: api/Monedas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonedaItem>> GetMonedaItem(string id)
        {
            var monedaItem = await _context.MonedaItems.FindAsync(id);

            if (monedaItem == null)
            {
                return NotFound();
            }

            

            monedaItem.Ultimo=random.Next();

            if(monedaItem.Ultimo>monedaItem.Max){
                monedaItem.Max=monedaItem.Ultimo;
            }

            await PutMonedaItem(monedaItem.Nombre, monedaItem);

            return monedaItem;
        }

        // PUT: api/Monedas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonedaItem(string id, MonedaItem monedaItem)
        {
            if (id != monedaItem.Nombre)
            {
                return BadRequest();
            }

            monedaItem.Ultimo=random.Next();

            if(monedaItem.Ultimo>monedaItem.Max){
                monedaItem.Max=monedaItem.Ultimo;
            }
            

            _context.Entry(monedaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedaItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Monedas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonedaItem>> PostMonedaItem(MonedaItem monedaItem)
        {
            monedaItem.Ultimo=random.Next();

            if(monedaItem.Ultimo>monedaItem.Max){
                monedaItem.Max=monedaItem.Ultimo;
            }   

            _context.MonedaItems.Add(monedaItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonedaItemExists(monedaItem.Nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetMonedaItem), new { Nombre = monedaItem.Nombre }, monedaItem);
        }

        // DELETE: api/Monedas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonedaItem(string id)
        {
            var monedaItem = await _context.MonedaItems.FindAsync(id);
            if (monedaItem == null)
            {
                return NotFound();
            }

            _context.MonedaItems.Remove(monedaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonedaItemExists(string id)
        {
            return _context.MonedaItems.Any(e => e.Nombre == id);
        }
    }
}
