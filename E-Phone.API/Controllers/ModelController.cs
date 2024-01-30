using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.Services.Abstract;
using E_Phone.BLL.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("brands/{brandId}/models", Name = "GetAllModels")]
        public async Task<IActionResult> GetAllModels(int brandId)
        {
            List<GetModelsDTO> getModelsDTOs = await _modelService.GetAllModelsAsync(brandId);

            return Ok(getModelsDTOs);
        }

        [HttpGet]
        [Route("models/{modelId}")]
        public async Task<IActionResult> GetModel(int modelId)
        {
            GetSingleModelDTO getSingleModelDTO = await _modelService.GetModelAsync(modelId);

            return Ok(getSingleModelDTO);
        }

        [HttpPost("brands/{brandId}/models", Name = "CreateModel")]
        public async Task<IActionResult> CreateModel(int brandId, [FromBody] CreateModelDTO createModelDTO)
        {
            await _modelService.CreateModelAsync(createModelDTO, brandId);

            return Ok();
        }

        [HttpPut]
        [Route("models/{modelId}")]
        public async Task<IActionResult> UpdateModel(int modelId, [FromBody] UpdateModelDTO updateModelDTO)
        {
            await _modelService.UpdateModelAsync(updateModelDTO, modelId);

            return Ok();
        }

        [HttpDelete]
        [Route("models/{modelId}")]
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            await _modelService.DeleteModel(modelId);

            return Ok();
        }
    }
}
