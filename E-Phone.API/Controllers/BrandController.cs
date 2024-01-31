using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    /// <summary>
    /// Marka işlemleri
    /// </summary>
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

        /// <summary>
        /// Tüm markaların listelenmesi
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            List<GetBrandsDTO> getBrandsDTOs = await _brandService.GetAllBrandsAsync();

            return Ok(getBrandsDTOs);
        }

        /// <summary>
        /// Belirli bir markanın detaylarını görüntülenmesi
        /// </summary>
        /// <param name="brandId">Id ye göre marka detaylarını görüntüleme</param>
        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrand(int brandId)
        {
            GetSingleBrandDTO getSingleBrandDTO = await _brandService.GetBrandAsync(brandId);

            return Ok(getSingleBrandDTO);
        }

        /// <summary>
        /// Yeni marka oluşturulması
        /// </summary>
        /// <param name="createBrandDTO"><strong>brandName (marka ismi):</strong> Maksimum 50 karekter içerebilir.</param>
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDTO createBrandDTO)
        {
            await _brandService.CreateBrandAsync(createBrandDTO);

            return Ok();
        }

        /// <summary>
        /// Belirli bir markanın güncellenmesi
        /// </summary>
        /// <param name="brandId">Id ye göre markanın güncellenmesi</param> 
        /// <param name="updateBrandDTO"><strong>brandName (marka ismi):</strong> Maksimum 50 karekter içerebilir.</param>
        [HttpPut("{brandId}")]
        public async Task<IActionResult> UpdateBrand(int brandId, [FromBody] UpdateBrandDTO updateBrandDTO)
        {
            await _brandService.UpdateBrandAsync(updateBrandDTO, brandId);

            return Ok();
        }

        /// <summary>
        /// Belirli bir markanın silinmesi
        /// </summary>
        /// <param name="brandId">Id ye göre markanın silinmesi</param>
        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
           await _brandService.DeleteBrand(brandId);

            return Ok();
        }
    }
}
