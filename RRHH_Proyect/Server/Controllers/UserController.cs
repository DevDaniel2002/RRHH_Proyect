using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Server.Data;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly RRHHDbContext DbContext;

        public UserController(RRHHDbContext dbContext)
        {
            DbContext = dbContext;
        }
        

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> CreateAsync(LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null)
                    throw new Exception("Provide login information");

                var user = await DbContext.User.FirstOrDefaultAsync(x=>x.Username == loginRequest.UserName &&  x.Password == loginRequest.Password);

                if(user != null)
                {
                    return Ok(new LoginResponse() { result = "true" });
                }

                return Unauthorized(new LoginResponse() { result = "false" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostUser")]
        public async Task<ActionResult<User>> CreateAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new Exception("El objeto a registrar es nulo");

                user.IsActive = true;
                DbContext.User.Add(user);
                await DbContext.SaveChangesAsync();

                return new ObjectResult(user) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
