using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            List<GetBrandsDTO> brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrand(int brandId)
        {
            GetSingleBrandDTO brand = await _brandService.GetBrandAsync(brandId);
            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO createBrand)
        {
            await _brandService.CreateBrandAsync(createBrand);
            return Ok();
        }

        [HttpPut("{brandId}")]
        public async Task<IActionResult> UpdateBrand(int brandId, UpdateBrandDTO updateBrand)
        {
            await _brandService.UpdateBrandAsync(updateBrand, brandId);
            return Ok();
        }

        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            _brandService.DeleteBrand(brandId);
            return Ok();
        }
    }
}
