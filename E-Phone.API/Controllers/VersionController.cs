using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.DTOs.Version;
using E_Phone.BLL.Services.Abstract;
using E_Phone.BLL.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace E_Phone.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class VersionController : ControllerBase
    {
        private readonly IVersionService _versionService;

        public VersionController(IVersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet("models/{modelId}/versions", Name = "GetAllVersions")]
        public async Task<IActionResult> GetAllVersions(int modelId)
        {
            List<GetVersionsDTO> getVersionsDTOs = await _versionService.GetAllVersionsAsync(modelId);

            return Ok(getVersionsDTOs);
        }

        [HttpGet]
        [Route("versions/{versionId}")]
        public async Task<IActionResult> GetVersion(int versionId)
        {
            GetSingleVersionDTO getSingleVersionDTO = await _versionService.GetVersionAsync(versionId);

            return Ok(getSingleVersionDTO);
        }

        [HttpPost("models/{modelID}/versions", Name = "CreateVersion") ]
        public async Task<IActionResult> CreateVersion(int modelId, [FromBody] CreateVersionDTO createVersionDTO)
        {
            await _versionService.CreateVersionAsync(createVersionDTO, modelId);

            return Ok();
        }

        [HttpPut]
        [Route("versions/{versionId}")]
        public async Task<IActionResult> UpdateVersion(int versionId, [FromBody] UpdateVersionDTO updateVersionDTO)
        {
            await _versionService.UpdateVersionAsync(updateVersionDTO, versionId);

            return Ok();
        }

        [HttpDelete]
        [Route("versions/{versionId}")]
        public async Task<IActionResult> DeleteVersion(int versionId)
        {
            await _versionService.DeleteVersion(versionId);

            return Ok();
        }
    }
}
