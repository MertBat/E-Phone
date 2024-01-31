using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.Services.Abstract;
using E_Phone.BLL.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    /// <summary>
    /// Model işlemleri
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        /// <summary>
        /// Modellerin listelenmesi
        /// </summary>
        /// <param name="brandId">Marka id ye göre modellerin listelenmesi </param>
        [HttpGet("brands/{brandId}/models", Name = "GetAllModels")]
        public async Task<IActionResult> GetAllModels(int brandId)
        {
            List<GetModelsDTO> getModelsDTOs = await _modelService.GetAllModelsAsync(brandId);

            return Ok(getModelsDTOs);
        }

        /// <summary>
        /// Belirli bir modelin detayının görüntülenmesi
        /// </summary>
        /// <param name="modelId">Id ye göre model detaylarını görüntüleme</param>
        [HttpGet]
        [Route("models/{modelId}")]
        public async Task<IActionResult> GetModel(int modelId)
        {
            GetSingleModelDTO getSingleModelDTO = await _modelService.GetModelAsync(modelId);

            return Ok(getSingleModelDTO);
        }

        /// <summary>
        /// Yeni model oluşturulması
        /// </summary>
        /// <param name="brandId">Modelin hangi markaya ekleneceğini belirtir.</param>
        /// <param name="createModelDTO"><strong>modelName (model ismi):</strong> Maksimum 50 karekter içerebilir.</param>
        [HttpPost("brands/{brandId}/models", Name = "CreateModel")]
        public async Task<IActionResult> CreateModel(int brandId, [FromBody] CreateModelDTO createModelDTO)
        {
            await _modelService.CreateModelAsync(createModelDTO, brandId);

            return Ok();
        }

        /// <summary>
        /// Belirli bir modelin güncellenmesi
        /// </summary>
        /// <param name="modelId">Id ye göre modelin güncellenmesi</param> 
        /// <param name="updateModelDTO"><strong>modelName (model ismi):</strong> Maksimum 50 karekter içerebilir.</param>
        [HttpPut]
        [Route("models/{modelId}")]
        public async Task<IActionResult> UpdateModel(int modelId, [FromBody] UpdateModelDTO updateModelDTO)
        {
            await _modelService.UpdateModelAsync(updateModelDTO, modelId);

            return Ok();
        }

        /// <summary>
        /// Belirli bir markanın silinmesi
        /// </summary>
        /// <param name="modelId">Id ye göre markanın silinmesi</param>
        [HttpDelete]
        [Route("models/{modelId}")]
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            await _modelService.DeleteModel(modelId);

            return Ok();
        }
    }
}
