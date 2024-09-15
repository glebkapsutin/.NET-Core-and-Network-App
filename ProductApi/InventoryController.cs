using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public InventoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task <IActionResult> GetInventory()
        {
            var inventory = await _dbContext.Inventory.ToListAsync();
            return Ok(inventory);
        }

        [HttpPut]

        public async Task<IActionResult> PutInventory(Inventory inventory)
        {
            try
            {
                _dbContext.Inventory.Add(inventory);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetInventory),new {id = inventory.Id_Inventory},inventory);

            }
            catch (Exception ex)
            {
                Debug.Assert(ex.Message.Contains("Упсс!! Ошибочка добавления :)"));
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _dbContext.Inventory.FindAsync(id);
            if(inventory == null)
            {
                return NotFound();
            }
            _dbContext.Inventory.Remove(inventory);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
