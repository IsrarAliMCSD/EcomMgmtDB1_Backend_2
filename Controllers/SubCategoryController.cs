using Code_EcomMgmtDB1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Code_EcomMgmtDB1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        EcomMgmtDb1Context ecomMgmtContext;
        public SubCategoryController(EcomMgmtDb1Context company_DbContext)
        {
            ecomMgmtContext = company_DbContext;
        }

        [HttpGet]
        [Route("GetSubCategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.SubCategories.Include("Category").Where(a => a.IsActive == true).Take(100).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSubCategoryById")]
        public async Task<IActionResult> GetSubCategoryById(int subCategoryId)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.SubCategories.Where(a => a.SubCategoryId == subCategoryId).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost]
        [Route("AddSubCategory")]
        public async Task<IActionResult> AddSubCategory(SubCategory subCategory)
        {
            string ErrorMessage = "";
            subCategory.CreatdDate = DateTime.UtcNow;
            subCategory.CreatedBy = "israr";
            subCategory.IsActive = true;
            ecomMgmtContext.SubCategories.Add(subCategory);
            await ecomMgmtContext.SaveChangesAsync();
            return Ok("Record Save");
        }

        [HttpPost]
        [Route("UpdateSubCategory")]
        public async Task<IActionResult> UpdateSubCategory(SubCategory subCategory)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.SubCategories.Where(a => a.SubCategoryId == subCategory.SubCategoryId).FirstOrDefault();
            if (result != null)
            {
                result.SubCategoryName = subCategory.SubCategoryName;
            }

            await ecomMgmtContext.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteSubCategory")]
        public async Task<IActionResult> DeleteSubCategory(int subCategoryId)
        {
            string ErrorMessage = "";
            var result = ecomMgmtContext.SubCategories.Where(a => a.SubCategoryId == subCategoryId).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
            }
            await ecomMgmtContext.SaveChangesAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSubCategoriesByCategoryId")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId(int categoryId)
        {
            string ErrorMessage = "";
            //var result = await ecomMgmtContext.SubCategories
            //.Where(a => categoryId > 0 ? a.CategoryId == categoryId : true).ToListAsync();

            var query = ecomMgmtContext.SubCategories.AsQueryable();
            if (categoryId > 0)
            {
                query = query.Where(a => a.CategoryId == categoryId);
            }
            var result = await query.ToListAsync();

            return Ok(result);
        }
    }
}
