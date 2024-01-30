using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.DTOs.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Abstract
{
    public interface IVersionService
    {
        Task<List<GetVersionsDTO>> GetAllVersionsAsync(int modelId);
        Task<GetSingleVersionDTO> GetVersionAsync(int id);
        Task UpdateVersionAsync(UpdateVersionDTO version, int id);
        Task CreateVersionAsync(CreateVersionDTO version, int modelId);
        Task DeleteVersion(int id);
    }
}
