using Code_EcomMgmtDB1.JwtHelper;
using Code_EcomMgmtDB1.Models;
using Code_EcomMgmtDB1.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Code_EcomMgmtDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        EcomMgmtDb1Context ecomMgmtContext;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public ProductController(EcomMgmtDb1Context company_DbContext, IJwtTokenProvider jwtTokenProvider)
        {
            ecomMgmtContext = company_DbContext;
            _jwtTokenProvider = jwtTokenProvider;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Products
      .Where(p => p.IsActive == true)
      .Include(p => p.SubCategory)
          .ThenInclude(sc => sc.Category)
      .Select(p => new
      {
          p.ProductId,
          p.ProductName,
          p.Detail,
          p.ProductPrice,
          p.CreatdDate,
          p.Image,
          p.ImageName,
          p.ImageFormat,
          // Add other Product fields here...
          SubCategoryName = p.SubCategory.SubCategoryName,
          CategoryName = p.SubCategory.Category.CategoryName
      })
      .Take(30)
      .ToList();

            return Ok(result);
        }



        [HttpPost]
        [Route("GetAllProductsFilter")]
        public async Task<IActionResult> GetAllProductsFilter(VMProductFilter vMProductFilter)
        {
            var query = ecomMgmtContext.Products
                .Where(p => p.IsActive == true)
                .Include(p => p.SubCategory)
                .ThenInclude(sc => sc.Category)
                .AsQueryable();

            if (vMProductFilter.CategoryId.HasValue && vMProductFilter.CategoryId > 0)
            {
                query = query.Where(p => p.SubCategory.CategoryId == vMProductFilter.CategoryId);
            }

            if (vMProductFilter.SubcategoryId.HasValue && vMProductFilter.SubcategoryId > 0)
            {
                query = query.Where(p => p.SubCategoryId == vMProductFilter.SubcategoryId);
            }

            //if (vMProductFilter.ProductIds != null && vMProductFilter.ProductIds.Any())
            //{
            //    query = query.Where(p => vMProductFilter.ProductIds.ToArray().Contains(p.ProductId));
            //}
            try
            {

                var result = await query.ToListAsync();
                var t2 = result.Where(a => vMProductFilter.ProductIds.Contains(a.ProductId));
                var temp = t2.Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Detail,
                    p.ProductPrice,
                    p.CreatdDate,
                    p.Image,
                    p.ImageName,
                    p.ImageFormat,
                    SubCategoryName = p.SubCategory.SubCategoryName,
                    CategoryName = p.SubCategory.Category.CategoryName
                })
                .Take(30);

                return Ok(temp.ToList());

            }
            catch (Exception exx)
            {

                throw;
            }

            return NotFound();
        }



        [HttpGet]

        [Route("GetProductById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int productId)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Products.Where(a => a.ProductId == productId).FirstOrDefault();
            return Ok(result);
        }

        [HttpGet]

        [Route("GetProductBySubcategoryId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductBySubcategoryId(int subCategoryId)
        {
            string ErrorMessage = "";
            var result =await ecomMgmtContext.Products.Where(a => a.SubCategoryId == subCategoryId)
                .Select(a=> new { a.ProductId, a.ProductName })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            string ErrorMessage = "";
            product.CreatdDate = DateTime.UtcNow;
            product.CreatedBy = "israr";
            product.IsActive = true;
            ecomMgmtContext.Products.Add(product);
            await ecomMgmtContext.SaveChangesAsync();
            return Ok("Record Save");
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Products.Where(a => a.ProductId == product.ProductId).FirstOrDefault();
            if (result != null)
            {
                result.ProductName = product.ProductName;
            }

            await ecomMgmtContext.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.Products.Where(a => a.ProductId == productId).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
            }
            await ecomMgmtContext.SaveChangesAsync();
            return Ok(result);
        }
    }
}
