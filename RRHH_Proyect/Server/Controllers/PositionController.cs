using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Positions")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly RRHHDbContext DbContext;

        public PositionController(RRHHDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost("PostPosition")]
        public async Task<ActionResult<Position>> CreateAsync(Position position)
        {
            try
            {
                if (position == null)
                    throw new Exception("El objeto a registrar es nulo");

                position.IsActive = true;
                DbContext.Position.Add(position);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(position) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPositions")]
        public async Task<ActionResult<List<Position>>> ReadAsync()
        {
            try
            {
                var Positions = await DbContext.Position.ToListAsync();

                return Ok(Positions.FindAll(x => x.IsActive == true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPositionsById/{id}")]
        public async Task<ActionResult<Position>> GetById(int id)
        {
            try
            {
                var Position = await DbContext.Position.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(Position);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePosition({id})")]
        public async Task<ActionResult<Position>> UpdateAsync(Position position, int id)
        {
            try
            {
                var result = await DbContext.Position.FindAsync(position.Id);

                if (result == null)
                    return NotFound("Position Not Found");

                result.Name = position.Name;
                result.Description = position.Description;
                result.IsActive = true;


                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePosition/{id}")]
        public async Task<ActionResult<Position>> DeleteAsync(int id)
        {
            try
            {
                var result = await DbContext.Position.FindAsync(id);

                if (result == null)
                    return NotFound("Position Not Found");

                result.IsActive = false;

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
