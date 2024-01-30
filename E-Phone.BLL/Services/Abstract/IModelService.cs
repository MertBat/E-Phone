using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.DTOs.Model;
using E_Phone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Abstract
{
    public interface IModelService
    {
        Task<List<GetModelsDTO>> GetAllModelsAsync(int brandId);
        Task<GetSingleModelDTO> GetModelAsync(int id);
        Task UpdateModelAsync(UpdateModelDTO model, int id);
        Task CreateModelAsync(CreateModelDTO model, int brandId);
        Task DeleteModel(int id);
    }
}
