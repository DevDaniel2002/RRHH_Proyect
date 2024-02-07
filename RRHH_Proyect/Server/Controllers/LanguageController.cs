using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Language")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly RRHHDbContext DbContext;

        public LanguageController(RRHHDbContext dbcontext)
        {
            DbContext = dbcontext;
        }

        // GET
        [HttpGet("GetLanguage")]
        public async Task<ActionResult<List<Language>>> ReadAsync()
        {
            try
            {
                var Languages = await DbContext.Language.ToListAsync();

                return Ok(Languages.FindAll(x => x.IsActive == true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLanguageById/{id}")]
        public async Task<ActionResult<Language>> GetById(int id)
        {
            try
            {
                var Language = await DbContext.Language.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(Language);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST
        [HttpPost("PostLanguage")]
        public async Task<ActionResult<Language>> CreateAsync(Language language)
        {
            try
            {
                if (language == null)
                    throw new Exception("El objeto a registrar es nulo");

                language.IsActive = true;
                DbContext.Language.Add(language);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(language) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT
        [HttpPut("UpdateLanguage")]
        public async Task<ActionResult<Language>> UpdateAsync(Language language)
        {
            try
            {
                var result = await DbContext.Language.FindAsync(language.Id);

                if (result == null)
                    throw new Exception("Language Not Found");
                result.Name = language.Name;
                result.Level = language.Level;
                result.IsActive = true;


                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE
        [HttpDelete("DeleteLanguage")]
        public async Task<ActionResult<Language>> DeleteAsync(int id)
        {
            try
            {
                var result = await DbContext.Language.FindAsync(id);

                if (result == null)
                    throw new Exception("Language Not Found");
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
