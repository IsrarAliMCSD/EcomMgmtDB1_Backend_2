using Code_EcomMgmtDB1.JwtHelper;
using Code_EcomMgmtDB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Code_EcomMgmtDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("Role=Admin")]
    // [Authorize(Roles = ("Admin"))]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        EcomMgmtDb1Context ecomMgmtContext;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public CategoryController(EcomMgmtDb1Context company_DbContext, IJwtTokenProvider jwtTokenProvider)
        {
            ecomMgmtContext = company_DbContext;
            _jwtTokenProvider = jwtTokenProvider;
        }

        [HttpGet]
        [Route("GetCategories")]
      
        public async Task<IActionResult> GetCategories()
        {
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(tokenString);
            //token.Payload.Where(a => a.Key == "scope").FirstOrDefault().Value;
           // var request = Convert.ToString(HttpContext.Request.Headers["Authorization"]).Split(' ')[1];
            ///var handler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = handler.ReadJwtToken(request);
            //var userRole = jwtSecurityToken.Claims.First(claim => claim.Type == "Role").Value;

            //var user = HttpContext.User;
            //var role = user.FindFirst("Role");


            string ErrorMessage = "";
            // ecomMgmtContext = new EcomMgmtContext();
            var result = ecomMgmtContext.Categories.Where(a => a.IsActive == true).Take(100).ToList();
            //var prod = ecomMgmtContext.Products.ToList();
            //var produ1 = ecomMgmtContext.Products.Where(a=> a.IsActive==true).ToList();
            //var produ2 = ecomMgmtContext.Products.Where(a => a.IsActive == true).FirstOrDefault();

            //var produ4 = ecomMgmtContext.Products.Where(a => a.ProductId == 2).First();
            //var produ5 = ecomMgmtContext.Products.Where(a => a.ProductId == 100).FirstOrDefault();
            //if(produ5!=null)
            //{

            //}


            return Ok(result);
        }

        [HttpGet]

        [Route("GetCategoryById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Categories.Where(a => a.CategoryId == categoryId).FirstOrDefault();
            return Ok(result);
            
        }
        [HttpPost]
        [Route("AddCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCategory(Category category)
        {
            string ErrorMessage = "";
            category.CreatdDate = DateTime.UtcNow;
            category.CreatedBy = "israr";
            category.IsActive = true;
            ecomMgmtContext.Categories.Add(category);
            await ecomMgmtContext.SaveChangesAsync();
            return Ok("Record Save");
        }

        [HttpPost]
        [Route("UpdateCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Categories.Where(a => a.CategoryId == category.CategoryId).FirstOrDefault();
            if (result != null)
            {
                result.CategoryName = category.CategoryName;
                result.Detail = category.Detail;
                category.LastModifiedDate = DateTime.UtcNow;
                category.LastModifiedBy = "israr";
                if (category.ImageFormat != null && category.ImageName != null && category.Image != null)
                {
                    result.ImageFormat = category.ImageFormat;
                    result.ImageName = category.ImageName;
                    result.Image = category.Image;
                }
               
            }

            await ecomMgmtContext.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Categories.Where(a => a.CategoryId == categoryId).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
            }
            await ecomMgmtContext.SaveChangesAsync();
            return Ok(result);
        }
    }
}
