using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRCT14_Server.Models;

namespace PRCT14_Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AcademicLoadsController : ControllerBase
    {
        private readonly CollegeContext _context;

        // GET: api/AcademicLoads/Info
        [HttpGet("Info")]
        public async Task<ActionResult<IEnumerable<AcademicLoadDTO>>> GetAcademicLoadInfoDTO()
        {            
            await _context.Teachers.LoadAsync();
            await _context.Disciplines.LoadAsync();
            
            return await _context.AcademicLoads
                .Select(p => new AcademicLoadDTO(p))
                .ToListAsync();
        }

        public AcademicLoadsController(CollegeContext context)
        {
            _context = context;
        }

        // GET: api/AcademicLoads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcademicLoad>>> GetAcademicLoads()
        {
            return await _context.AcademicLoads.ToListAsync();
        }

        // GET: api/AcademicLoads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AcademicLoad>> GetAcademicLoad(int id)
        {
            var academicLoad = await _context.AcademicLoads.FindAsync(id);

            if (academicLoad == null)
            {
                return NotFound();
            }

            return academicLoad;
        }

        //// PUT: api/AcademicLoads/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAcademicLoad(int id, AcademicLoad academicLoad)
        //{
        //    if (id != academicLoad.AcademLoadCode)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(academicLoad).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AcademicLoadExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/AcademicLoads
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<AcademicLoad>> PostAcademicLoad(AcademicLoad academicLoad)
        //{
        //    _context.AcademicLoads.Add(academicLoad);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAcademicLoad", new { id = academicLoad.AcademLoadCode }, academicLoad);
        //}       

        // POST: api/AcademicLoads
        [HttpPost]
        public async Task<ActionResult<AcademicLoad>> PostAcademicLoad(AcademicLoadEditDTO academicLoadDTO)
        {            
            if (!_context.Teachers.Any(t => t.ServiceNumber == academicLoadDTO.TeacherCode))
            {
                return BadRequest("Преподаватель не найден");
            }

            if (!_context.Disciplines.Any(d => d.DisciplineCode == academicLoadDTO.DisciplineCode))
            {
                return BadRequest("Дисциплина не найдена");
            }
            
            AcademicLoad academicLoad = new AcademicLoad(academicLoadDTO);

            _context.AcademicLoads.Add(academicLoad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcademicLoad", new { id = academicLoad.AcademLoadCode }, academicLoad);
        }

        // PUT: api/AcademicLoads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcademicLoad(int id, AcademicLoadEditDTO academicLoadDTO)
        {
            if (id != academicLoadDTO.AcademLoadCode)
            {
                return BadRequest("Неверный запрос!");
            }
            
            var academicLoad = await _context.AcademicLoads.FindAsync(id);
            if (academicLoad == null)
            {
                return NotFound($"Запись id={id} не обнаружена!");
            }
            
            if (!_context.Teachers.Any(t => t.ServiceNumber == academicLoadDTO.TeacherCode))
            {
                return BadRequest("Преподаватель не найден");
            }

            if (!_context.Disciplines.Any(d => d.DisciplineCode == academicLoadDTO.DisciplineCode))
            {
                return BadRequest("Дисциплина не найдена");
            }
            
            academicLoad.Update(academicLoadDTO);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicLoadExists(id))
                {
                    return NotFound($"Запись id={id} не обнаружена!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AcademicLoads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicLoad(int id)
        {
            var academicLoad = await _context.AcademicLoads.FindAsync(id);
            if (academicLoad == null)
            {
                return NotFound();
            }

            _context.AcademicLoads.Remove(academicLoad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcademicLoadExists(int id)
        {
            return _context.AcademicLoads.Any(e => e.AcademLoadCode == id);
        }
    }
}
