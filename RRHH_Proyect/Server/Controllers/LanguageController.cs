using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly RRHHDbContext DbContext;

        public LanguageController(RRHHDbContext dbcontext)
        {
            DbContext = dbcontext;
        }

        // GET: api/Language
        [HttpGet("GetLanguage")]
        public IActionResult Get()
        {
            var languages = DbContext.Language.ToList();
            return Ok(languages);
        }

        // GET: api/Language/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var language = DbContext.Language.Find(id);
            if (language == null)
            {
                return NotFound(); //  Not Found
            }

            return Ok(language);
        }

        // POST: api/Language
        [HttpPost]
        public IActionResult Post([FromBody] Language newLanguage)
        {
            DbContext.Language.Add(newLanguage);
            DbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newLanguage.Id }, newLanguage);
        }

        // PUT: api/Language/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Language updatedLanguage)
        {
            var language = DbContext.Language.Find(id);
            if (language == null)
            {
                return NotFound(); //  Not Found
            }

            // Actualizar propiedades del idioma
            language.Name = updatedLanguage.Name;
            language.Level = updatedLanguage.Level;
            language.IsActive = updatedLanguage.IsActive;

            DbContext.SaveChanges();

            return NoContent(); //  No Content
        }

        // DELETE: api/Language/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var language = DbContext.Language.Find(id);
            if (language == null)
            {
                return NotFound(); //  Not Found
            }

            DbContext.Language.Remove(language);
            DbContext.SaveChanges();

            return NoContent(); //  No Content



        }

     }
}
