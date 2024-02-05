using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly RRHHDbContext _context;

        public TrainingController(RRHHDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Trainings>>> GetAllTrainings()
        {
            var list = await _context.Trainings.ToListAsync();

            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult<List<Trainings>>> GetTrainingById(int Id)
        {
            var training = await _context.Trainings.FirstOrDefaultAsync(ob => ob.Id == Id);

            if (training == null)
            {
                return NotFound(" /");
            }

            return Ok(training);
        }

        [HttpPost]
        public async Task<ActionResult<Trainings>> CreateTraining(Trainings trainings)
        {
            _context.Trainings.Add(trainings);
            await _context.SaveChangesAsync();

            return Ok(await GetDbTrainings());
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<List<Trainings>>> UpdateTrainings(Trainings trainings)
        {
            var obj = await _context.Trainings.FindAsync(trainings);
            if (obj == null)
            {
                return BadRequest("No se encuentra la competencia");
            }
            obj.Description = trainings.Description;

            await _context.SaveChangesAsync();

            return Ok(await _context.Trainings.ToListAsync());
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult<List<Trainings>>> DeleteTraining(int Id)
        {
            var obj = await _context.Trainings.FirstOrDefaultAsync(Ob => Ob.Id == Id);

            if (obj == null)
            {
                return NotFound("No existe :/");
            }

            _context.Trainings.Remove(obj);
            await _context.SaveChangesAsync();
            return Ok(await GetDbTrainings());
        }

        private async Task<List<Trainings>> GetDbTrainings()
        {
            return await _context.Trainings.ToListAsync();
        }
    }
}
