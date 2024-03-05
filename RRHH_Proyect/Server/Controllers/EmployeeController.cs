using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly RRHHDbContext DbContext;

        public EmployeeController(RRHHDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost("PostEmployee")]
        public async Task<ActionResult<Employee>> CreateAsync(Employee employee)
        {
            try
            {
                if (employee == null)
                    throw new Exception("El objeto a registrar es nulo");

                employee.IsActive = true;
                DbContext.Employee.Add(employee);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(employee) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEmployees")]
        public async Task<ActionResult<List<Employee>>> ReadAsync()
        {
            try
            {
                var Employees = await DbContext.Employee
                    .Include(e=>e.Department)
                    .Include(e=>e.Position)
                    .ToListAsync();

                return Ok(Employees.FindAll(x => x.IsActive == true));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEmployeesById/{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            try
            {
                var Employees = await DbContext.Employee.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(Employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult<Employee>> UpdateAsync(Employee employee)
        {
            try
            {
                var result = await DbContext.Employee.FindAsync(employee.Id);

                if (result == null)
                    return NotFound("Employee Not Found");
                
                result.IdentificationNumber = employee.IdentificationNumber;
                result.Name = employee.Name;
                result.Surname = employee.Surname;
                result.Position = employee.Position;
                result.Department = employee.Department;
                result.HiringDate = employee.HiringDate;
                result.Salary = employee.Salary;                
                
                result.IsActive = true;


                await DbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult<Employee>> DeleteAsync(int id)
        {
            try
            {
                var result = await DbContext.Employee.FindAsync(id);

                if (result == null)
                    return NotFound("Employee Not Found");
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
