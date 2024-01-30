using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    [Route("brands")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
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
            List<GetBrandsDTO> getBrandsDTOs = await _brandService.GetAllBrandsAsync();

            return Ok(getBrandsDTOs);
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrand(int brandId)
        {
            GetSingleBrandDTO getSingleBrandDTO = await _brandService.GetBrandAsync(brandId);

            return Ok(getSingleBrandDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDTO createBrandDTO)
        {
            await _brandService.CreateBrandAsync(createBrandDTO);

            return Ok();
        }

        [HttpPut("{brandId}")]
        public async Task<IActionResult> UpdateBrand(int brandId, [FromBody] UpdateBrandDTO updateBrandDTO)
        {
            await _brandService.UpdateBrandAsync(updateBrandDTO, brandId);

            return Ok();
        }

        [HttpDelete("{brandId}")]
        public IActionResult DeleteBrand(int brandId)
        {
            _brandService.DeleteBrand(brandId);

            return Ok();
        }
    }
}
