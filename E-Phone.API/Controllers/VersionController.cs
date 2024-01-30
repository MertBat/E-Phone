using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.DTOs.Version;
using E_Phone.BLL.Services.Abstract;
using E_Phone.BLL.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    [Route("[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class VersionController : ControllerBase
    {
        private readonly IVersionService _versionService;

        public VersionController(IVersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet]
        [Route("vesions/{modelId}")]
        [ActionName("models")]
        public async Task<IActionResult> GetAllVersions(int modelId)
        {
            List<GetVersionsDTO> getVersionsDTOs = await _versionService.GetAllVersionsAsync(modelId);

            return Ok(getVersionsDTOs);
        }

        [HttpGet]
        [Route("{versionId}")]
        [ActionName("versions")]
        public async Task<IActionResult> GetVersion(int versionId)
        {
            GetSingleVersionDTO getSingleVersionDTO = await _versionService.GetVersionAsync(versionId);

            return Ok(getSingleVersionDTO);
        }

        [HttpPost]
        [Route("versions/{modelId}")]
        [ActionName("models")]
        public async Task<IActionResult> CreateVersion(int modelId, [FromBody] CreateVersionDTO createVersionDTO)
        {
            await _versionService.CreateVersionAsync(createVersionDTO, modelId);

            return Ok();
        }

        [HttpPut]
        [Route("{versionId}")]
        [ActionName("versions")]
        public async Task<IActionResult> UpdateVersion(int versionId, [FromBody] UpdateVersionDTO updateVersionDTO)
        {
            await _versionService.UpdateVersionAsync(updateVersionDTO, versionId);

            return Ok();
        }

        [HttpDelete]
        [Route("{versionId}")]
        [ActionName("versions")]
        public async Task<IActionResult> DeleteVersion(int versionId)
        {
            await _versionService.DeleteVersion(versionId);

            return Ok();
        }
    }
}
