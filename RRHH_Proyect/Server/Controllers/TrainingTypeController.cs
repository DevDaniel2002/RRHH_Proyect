using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;
using System;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/TrainingTypes")]
    [ApiController]
    public class TrainingTypeController : ControllerBase
    {
        private readonly RRHHDbContext _context;

        public TrainingTypeController(RRHHDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllTrainingTypes")]
        public async Task<ActionResult<List<TrainingTypes>>> GetAllTrainingTypes()
        {
            try
            {
                var list = await _context.TrainingTypes
                    .Where(t => t.IsActive)
                    .ToListAsync();

                return Ok(list);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTrainingTypeById")]
        [Route("{Id}")]
        public async Task<ActionResult<TrainingTypes>> GetTrainingTypeById(int Id)
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

        [HttpPost("CreateTrainingType")]
        public async Task<ActionResult<TrainingTypes>> CreateTrainingType(TrainingTypes trainingTypes)
        {

            trainingTypes.IsActive = true;

            _context.TrainingTypes.Add(trainingTypes);
            await _context.SaveChangesAsync();

            return Ok(await GetDBTrainingsType());
        }


        [HttpPut("UpdateTrainingType/{Id}")]
        public async Task<ActionResult<List<TrainingTypes>>> UpdateTrainingType(TrainingTypes trainingTypes)
        {
            try
            {
                var result = await _context.TrainingTypes.FindAsync(trainingTypes.Id);
                if (result == null)
                    return BadRequest("No se encuentra el Objeto");
                result.Type = trainingTypes.Type;


                await _context.SaveChangesAsync();

                return Ok(await _context.TrainingTypes.ToListAsync());

            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTrainingType/{id}")]
        public async Task<ActionResult<List<TrainingTypes>>> DeleteTrainingType(int Id)
        {
            var obj = await _context.TrainingTypes.FirstOrDefaultAsync(Ob => Ob.Id == Id);

            if (obj == null)
            {
                return NotFound("No existe :/");
            }

            obj.IsActive = false;

            await _context.SaveChangesAsync();

            return Ok(await GetDBTrainingsType());
        }


        private async Task<List<TrainingTypes>> GetDBTrainingsType()
        {
            return await _context.TrainingTypes.ToListAsync();
        }

    }
}
