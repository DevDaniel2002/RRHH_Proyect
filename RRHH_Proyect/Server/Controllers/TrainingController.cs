using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Trainings")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly RRHHDbContext _context;

        public TrainingController(RRHHDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllTrainings")]
        public async Task<ActionResult<List<Trainings>>> GetAllTrainings()
        {
            try
            {
                var list = await _context.Trainings
                    .Where(t => t.IsActive)
                    .ToListAsync();

                return Ok(list);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTrainingById/{Id}")]
        public async Task<ActionResult<List<Trainings>>> GetTrainingById(int Id)
        {
            try
            {
                var trainingType = await _context.TrainingTypes
                    .Where(t => t.Id == Id && t.IsActive)
                    .FirstOrDefaultAsync();
                return Ok(trainingType);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateTraining")]
        public async Task<ActionResult<Trainings>> CreateTraining(Trainings trainings)
        {
            try
            {
                trainings.IsActive = true;

                _context.Trainings.Add(trainings);
                await _context.SaveChangesAsync();

                return Ok(await GetDbTrainings());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateTraining/{Id}")]
        public async Task<ActionResult<List<Trainings>>> UpdateTrainings(Trainings training)
        {
            try
            {
                var result = await _context.Trainings.FindAsync(training.Id);
                if (result == null)
                {
                    return BadRequest("No se encuentra la competencia");
                }
                result.Description = training.Description;
                result.TrainingTypeId = training.TrainingTypeId;
                result.DateFrom = training.DateFrom;
                result.DateTo = training.DateTo;
                result.Institution = training.Institution;
                result.IsActive = true;


                await _context.SaveChangesAsync();

                return Ok(await _context.Trainings.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTraining/{Id}")]
        //[Route("{Id}")]
        public async Task<ActionResult<List<Trainings>>> DeleteTraining(int Id)
        {
            try
            {
                var obj = await _context.Trainings.FirstOrDefaultAsync(Ob => Ob.Id == Id);

                if (obj == null)
                {
                    return NotFound("No existe :/");
                }

                obj.IsActive = false;
                await _context.SaveChangesAsync();
                return Ok(await GetDbTrainings());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<List<Trainings>> GetDbTrainings()
        {
            return await _context.Trainings.ToListAsync();
        }
    }
}
