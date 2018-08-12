using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FertilabApi.Models;

namespace FertilabApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CentersApi")]
    public class CentersApiController : Controller
    {
        private readonly FertilabContext _context;

        public CentersApiController(FertilabContext context)
        {
            _context = context;
        }

        [HttpPost("GetCenterByLocation")]
        public async Task<IActionResult> GetCenterByLocation([FromBody] LatLng coord)
        {
            var Offices = await _context.Center.ToListAsync();
            for (int i = 0; i < Offices.Count; i++)
            {
                Offices[i].Distance = HaversineDistance(
                                        coord,
                                        new LatLng(Offices[i].Lat, Offices[i].Lng),
                                        DistanceUnit.Kilometers);
            }

            var closestOffice = Offices.OrderBy(x => x.Distance).ToList();
            return Ok(closestOffice);
        }

        public double HaversineDistance(LatLng pos1, LatLng pos2, DistanceUnit unit)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            var lat = (pos2.Latitude - pos1.Latitude).ToRadians();
            var lng = (pos2.Longitude - pos1.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(pos1.Latitude.ToRadians()) * Math.Cos(pos2.Latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }

        public enum DistanceUnit { Miles, Kilometers };

        // GET: api/CentersApi
        [HttpGet]
        public IEnumerable<Center> GetCenter()
        {
            return _context.Center;
        }

        // GET: api/CentersApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCenter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var center = await _context.Center.SingleOrDefaultAsync(m => m.Id == id);

            if (center == null)
            {
                return NotFound();
            }

            return Ok(center);
        }

        // PUT: api/CentersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenter([FromRoute] int id, [FromBody] Center center)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != center.Id)
            {
                return BadRequest();
            }

            _context.Entry(center).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenterExists(id))
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

        // POST: api/CentersApi
        [HttpPost]
        public async Task<IActionResult> PostCenter([FromBody] Center center)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Center.Add(center);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCenter", new { id = center.Id }, center);
        }

        // DELETE: api/CentersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var center = await _context.Center.SingleOrDefaultAsync(m => m.Id == id);
            if (center == null)
            {
                return NotFound();
            }

            _context.Center.Remove(center);
            await _context.SaveChangesAsync();

            return Ok(center);
        }

        private bool CenterExists(int id)
        {
            return _context.Center.Any(e => e.Id == id);
        }
    }
}