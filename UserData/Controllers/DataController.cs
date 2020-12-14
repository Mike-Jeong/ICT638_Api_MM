using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserData.Models;
using UserData.Configurations;
using UserData.NotificationHubs;

namespace UserData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataContext _context;
        NotificationHubConfiguration _hubConfiguration;

        public DataController(DataContext context)
        {
            _context = context;
           

        }

        // GET: api/Data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data>>> Getdatas()
        {
            return await _context.datas.ToListAsync();
        }

        // GET: api/Data/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data>> GetData(int id)
        {
            var data = await _context.datas.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        // PUT: api/Data/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutData(int id, Data data)
        {
            if (id != data.id)
            {
                return BadRequest();
            }

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataExists(id))
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




        // POST: api/Data
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data>> PostData(Data data)
        {
            _context.datas.Add(data);
            await _context.SaveChangesAsync();

            Notification.sendNotification("New Data Posted", "New Data ID: " + data.id);

            return CreatedAtAction("GetData", new { id = data.id }, data);
        }

        // DELETE: api/Data/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data>> DeleteData(int id)
        {
            var data = await _context.datas.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _context.datas.Remove(data);
            await _context.SaveChangesAsync();

            Notification.sendNotification("Data Deleted", "Data ID: " + data.id);

            return CreatedAtAction("DeletData", new { id = data.id }, data);

            
        }

        private bool DataExists(int id)
        {
            return _context.datas.Any(e => e.id == id);
        }
    }
}
