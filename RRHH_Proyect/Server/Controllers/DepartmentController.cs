using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Departments")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly RRHHDbContext DbContext;

        public DepartmentController(RRHHDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost("PostDepartment")]
        public async Task<ActionResult<Department>> CreateAsync(Department department)
        {
            try
            {
                if (department == null)
                    throw new Exception("El objeto a registrar es nulo");

                department.IsActive = true;
                DbContext.Department.Add(department);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(department) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDepartments")]
        public async Task<ActionResult<List<Department>>> ReadAsync()
        {
            try
            {
                var Departments = await DbContext.Department.ToListAsync();

                return Ok(Departments.FindAll(x => x.IsActive == true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDepartmentsById/{id}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            try
            {
                var Departments = await DbContext.Department.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(Departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateDepartment")]
        public async Task<ActionResult<Department>> UpdateAsync(Department department)
        {
            try
            {
                var result = await DbContext.Department.FindAsync(department.Id);

                if (result == null)
                    return NotFound("Department Not Found");

                result.Name = department.Name;
                result.Description = department.Description;
                result.IsActive = true;


                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<ActionResult<Department>> DeleteAsync(int id)
        {
            try
            {
                var result = await DbContext.Department.FindAsync(id);

                if (result == null)
                    return NotFound("Department Not Found");

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
