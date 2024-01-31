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
    /// <summary>
    /// Versiyon işlemleri
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class VersionController : ControllerBase
    {
        private readonly IVersionService _versionService;

        public VersionController(IVersionService versionService)
        {
            _versionService = versionService;
        }

        /// <summary>
        /// Versiyonların listelenmesi
        /// </summary>
        /// <param name="modelId">Model id ye göre versiyonların listelenmesi</param>
        /// <returns></returns>
        [HttpGet("models/{modelId}/versions", Name = "GetAllVersions")]
        public async Task<IActionResult> GetAllVersions(int modelId)
        {
            List<GetVersionsDTO> getVersionsDTOs = await _versionService.GetAllVersionsAsync(modelId);

            return Ok(getVersionsDTOs);
        }

        /// <summary>
        /// Belirli bir versiyon detayının görüntülenmesi
        /// </summary>
        /// <param name="versionId">Id ye göre versiyon detaylarını görüntüleme</param>
        [HttpGet]
        [Route("versions/{versionId}")]
        public async Task<IActionResult> GetVersion(int versionId)
        {
            GetSingleVersionDTO getSingleVersionDTO = await _versionService.GetVersionAsync(versionId);

            return Ok(getSingleVersionDTO);
        }

        /// <summary>
        /// Yeni versiyon oluşturulması
        /// </summary>
        /// <param name="modelId">Versiyonun hangi modele ekleneceğini belirtir.</param>
        /// <param name="createVersionDTO"><strong>storageCapacity (depo kapesitesi):</strong> 0 dan büyük olmalıdır. <br/>
        /// <strong>price (fiyat):</strong> 0 dan büyük olmalıdır.<br/>
        /// <strong>stock (stok):</strong> 0 dan büyük olmalıdır. Depo kapasitesinden küçük olmalıdır.<br/>
        /// </param>
        [HttpPost("models/{modelId}/versions", Name = "CreateVersion") ]
        public async Task<IActionResult> CreateVersion(int modelId, [FromBody] CreateVersionDTO createVersionDTO)
        {
            await _versionService.CreateVersionAsync(createVersionDTO, modelId);

            return Ok();
        }

        /// <summary>
        /// Yeni versiyon oluşturulması
        /// </summary>
        /// <param name="versionId">Id ye göre versiyon güncellenmesi</param>
        /// <param name="updateVersionDTO"><strong>storageCapacity (depo kapesitesi):</strong> 0 dan büyük olmalıdır. <br/>
        /// <strong>price (fiyat):</strong> 0 dan büyük olmalıdır.<br/>
        /// <strong>stock (stok):</strong> 0 dan büyük olmalıdır. Depo kapasitesinden küçük olmalıdır.<br/>
        /// </param>
        [HttpPut]
        [Route("versions/{versionId}")]
        public async Task<IActionResult> UpdateVersion(int versionId, [FromBody] UpdateVersionDTO updateVersionDTO)
        {
            await _versionService.UpdateVersionAsync(updateVersionDTO, versionId);

            return Ok();
        }

        /// <summary>
        /// Belirli bir markanın silinmesi
        /// </summary>
        /// <param name="versionId">Id ye göre versiyonun silinmesi</param>
        [HttpDelete]
        [Route("versions/{versionId}")]
        public async Task<IActionResult> DeleteVersion(int versionId)
        {
            await _versionService.DeleteVersion(versionId);

            return Ok();
        }
    }
}
