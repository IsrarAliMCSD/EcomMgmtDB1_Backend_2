using Code_EcomMgmtDB1.JwtHelper;
using Code_EcomMgmtDB1.JWTToken1;
using Code_EcomMgmtDB1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Code_EcomMgmtDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        EcomMgmtDb1Context ecomMgmtContext;
        public RoleController(EcomMgmtDb1Context company_DbContext)
        {
            ecomMgmtContext = company_DbContext;

        }

        // GET: api/Roles
        [HttpGet]
        [Route("GetRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await ecomMgmtContext.Roles.Where(a=>a.IsActive).ToListAsync();
        }


        // GET: api/GetRoleById
        [HttpGet]
        [Route("GetRoleById")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoleById(int roleId)
        {
            return await ecomMgmtContext.Roles.Where(a => a.RoleId==roleId).ToListAsync();
        }
    }
}
