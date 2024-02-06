using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;
using System;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypeController : ControllerBase
    {
        private readonly RRHHDbContext _context;

        public TrainingTypeController(RRHHDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TrainingTypes>>> GetAllTrainingTypes()
        {
            var list = await _context.TrainingTypes
                .Where(t => t.IsActive)
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<List<TrainingTypes>>> GetTrainingTypeById(int Id)
        {
            var trainingType = await _context.TrainingTypes
                .Where(t => t.Id == Id && t.IsActive) 
                .FirstOrDefaultAsync();

            if (trainingType == null)
            {
                return NotFound(" /");
            }
            return Ok(trainingType);
        }

        [HttpPost]
        public async Task<ActionResult<TrainingTypes>> CreateTrainingType(TrainingTypes trainingTypes)
        {

            trainingTypes.IsActive = true;

            _context.TrainingTypes.Add(trainingTypes);
            await _context.SaveChangesAsync();

            return Ok(await GetDBTrainingsType());
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult<List<TrainingTypes>>> UpdateTrainingType(TrainingTypes trainingTypes)
        {
            var obj = await _context.TrainingTypes.FindAsync(trainingTypes.Id);
            if (obj == null)
                return BadRequest("No se encuentra el Objeto");
            obj.Type = trainingTypes.Type;


            await _context.SaveChangesAsync();

            return Ok(await _context.TrainingTypes.ToListAsync());
        }

        [HttpDelete]
        [Route("{Id}")]
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
