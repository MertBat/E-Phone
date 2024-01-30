using E_Phone.BLL.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Abstract
{
    public interface IBrandService
    {
        Task<List<GetBrandsDTO>> GetAllBrandsAsync();
        Task<GetSingleBrandDTO> GetBrandAsync(int id);
        Task UpdateBrandAsync(UpdateBrandDTO brand, int id);
        Task CreateBrandAsync(CreateBrandDTO brand);
        Task DeleteBrand(int id);
    }
}
