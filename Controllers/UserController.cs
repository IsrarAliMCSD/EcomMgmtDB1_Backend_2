using Code_EcomMgmtDB1.JwtHelper;
using Code_EcomMgmtDB1.JWTToken1;
using Code_EcomMgmtDB1.Models;
using Code_EcomMgmtDB1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Code_EcomMgmtDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        EcomMgmtDb1Context ecomMgmtContext;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        JWTToken jWTToken;
        public UserController(EcomMgmtDb1Context company_DbContext, JWTToken jWTToken1, IJwtTokenProvider jwtTokenProvider)
        {
            ecomMgmtContext = company_DbContext;
            jWTToken = jWTToken1;
            _jwtTokenProvider = jwtTokenProvider;
        }


        [HttpPost]
        [Route("LoginDemo")]
        [AllowAnonymous]
        //[EnableCors]
        public async Task<IActionResult> LoginDemo(VMUserLogin vMUserLogin)
        {
            vMUserLogin.UserName = "a";
            vMUserLogin.Password = "a";
            try
            {
                string ErrorMessage = "";
                //ecomMgmtContext = new EcomMgmtContext();
                var result = ecomMgmtContext.Users.Include("Role").FirstOrDefault(a =>
                a.UserName == vMUserLogin.UserName
                && a.Password == vMUserLogin.Password);
                if (result != null)
                {
                    // return Ok(jWTToken.GetToken(result));
                    return Ok(_jwtTokenProvider.GenerateToken(result));
                    // return Ok(result);
                }
                else
                {
                    return Ok("The credentials is invalid");
                    return NotFound("The credentials is invalid");
                }
            }
            catch (Exception exx)
            {
                return BadRequest(new { message = exx.Message }); // 400
            }
            return Ok("The credentials is invalid");

        }

        // GET: api/Users
        [Route("GetAuthUser")]
        [HttpGet]
        public async Task<ActionResult<object>> GetAuthUser()
        {
            var request = Convert.ToString(HttpContext.Request.Headers["Authorization"]).Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(request);
            var userId = jwtSecurityToken.Claims.First(claim => claim.Type == "UserId").Value;

            var user = await ecomMgmtContext.Users
        .Include(u => u.Role)
        .Where(u => u.UserId.ToString() == userId)
        .Select(u => new
        {
            u.UserId,
            u.UserName,
            u.EmailAddress,
            RoleName = u.Role.RoleName
        })
        .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        //// GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    return await _context.Users
        //        .Include(u => u.Role)
        //        .ToListAsync();
        //}

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users
        //        .Include(u => u.Role)
        //        .FirstOrDefaultAsync(u => u.UserId == id);

        //    if (user == null)
        //        return NotFound();

        //    return user;
        //}

        //// POST: api/Users
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    user.CreatdDate = DateTime.UtcNow;
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        //}

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.UserId)
        //        return BadRequest();

        //    var existingUser = await _context.Users.FindAsync(id);
        //    if (existingUser == null)
        //        return NotFound();

        //    // Update properties
        //    existingUser.UserName = user.UserName;
        //    existingUser.EmailAddress = user.EmailAddress;
        //    existingUser.Password = user.Password;
        //    existingUser.Dob = user.Dob;
        //    existingUser.RoleId = user.RoleId;
        //    existingUser.LastModifiedBy = user.LastModifiedBy;
        //    existingUser.LastModifiedDate = DateTime.UtcNow;
        //    existingUser.IsActive = user.IsActive;

        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //        return NotFound();

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
