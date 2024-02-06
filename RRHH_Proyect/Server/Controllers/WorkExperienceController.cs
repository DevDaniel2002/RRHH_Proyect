using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        private readonly RRHHDbContext DbContext;

        public WorkExperienceController(RRHHDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost("PostWorkExperience")]
        public async Task<ActionResult<WorkExperience>> CreateAsync(WorkExperience workExperience)
        {
            try
            {
                if (workExperience == null)
                    throw new Exception("El objeto a registrar es nulo");

                DbContext.WorkExperience.Add(workExperience);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(workExperience) { StatusCode = StatusCodes.Status201Created };
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWorkExperiences")]
        public async Task<ActionResult<List<WorkExperience>>> ReadAsync()
        {
            try
            {
                var WorkExperiences = await DbContext.WorkExperience.ToListAsync();

                return Ok(WorkExperiences);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateWorkExperience")]
        public async Task<ActionResult<WorkExperience>> UpdateAsync(WorkExperience workExperience)
        {
            try
            {
                var result = await DbContext.WorkExperience.FindAsync(workExperience.Id);

                if (result == null)
                    throw new Exception("Work Experience Not Found");

                result = workExperience;

                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteWorkExperience/{id}")]
        public async Task<ActionResult<WorkExperience>> DeleteAsync(int id)
        {
            try
            {
                var result = await DbContext.WorkExperience.FindAsync(id);

                if(result == null)
                    throw new Exception("Work Experiene Not Found");
                result.IsActive = false;

                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
