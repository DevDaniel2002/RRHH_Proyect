using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Candidates")]
    [ApiController]
    public class CandidateController : Controller
    {
        private readonly RRHHDbContext DbContext;

        public CandidateController(RRHHDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("GetAllCandidates")]
        public async Task<ActionResult<List<Candidate>>> GetAllCandidate()
        {
            try
            {
                var candidates = await DbContext.Candidates
                    .Where(c => c.IsActive)
                    .ToListAsync();

                return Ok(candidates);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCandidateById/{id}")]
        public async Task<ActionResult<Candidate>> GetCandidateById(int id)
        {
            try
            {
                var candidates = await DbContext.Candidates
                    .FirstOrDefaultAsync(c => c.Id == id);

                return Ok(candidates);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateCandidate")]
        public async Task<ActionResult<Candidate>> CreateCandidate(Candidate candidate)
        {
            try
            {
                if (candidate == null)
                    throw new Exception("El objeto a registrar es nulo");

                candidate.IsActive = true;
                DbContext.Candidates.Add(candidate);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(candidate) 
                { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCandidate/{id}")]
        public async Task<ActionResult<Candidate>> DeleteCandidate(int id)
        {
            try
            {
                var result = await DbContext.Candidates
                    .FindAsync(id);

                if (result == null)
                    return NotFound("El candidato no fue encontrado");

                result.IsActive = false;

                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message) ;
            }
        }

        [HttpPut("UpdateCandidate")]
        public async Task<ActionResult<Candidate>> UpdateCandidate(Candidate candidate)
        {
            try
            {
                var result = await DbContext.Candidates
                    .FindAsync(candidate.Id);

                if (result == null)
                    return NotFound("Candidato No encontrado");

                result.Cedula = candidate.Cedula;
                result.Name = candidate.Name;
                result.Surname = candidate.Surname;
                result.DepartmentId = candidate.DepartmentId;
                result.DesiredPosition = candidate.DesiredPosition;
                result.DesiredSalary = candidate.DesiredSalary;
                result.TrainingId = candidate.TrainingId;
                result.WorkExperienceId= candidate.WorkExperienceId;
                result.LanguagueId = candidate.LanguagueId;
                result.RecommendFor = candidate.RecommendFor;
                result.IsActive = true;

                await DbContext.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
