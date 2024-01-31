using AutoMapper;
using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.Services.Abstract;
using E_Phone.Core.Entities;
using E_Phone.Core.IRepositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Concrete
{
    public class ModelService : IModelService
    {
        private readonly IBaseRepository<Model> _modelRepository;
        private readonly IBaseRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public ModelService(IBaseRepository<Model> modelRepository, IBaseRepository<Brand> brandRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task CreateModelAsync(CreateModelDTO createModelDTO, int brandId)
        {
            bool isBranchExist = await _brandRepository.AnyAsync(b => b.Id == brandId);
            if (!isBranchExist)
                throw new ArgumentException("Marka bulunamadı.");

            bool isModelExist = await _modelRepository.AnyAsync(m => m.ModelName == createModelDTO.ModelName);
            if (isModelExist)
                throw new ArgumentException("Aynı isimde model vardır.");

            Model newModel = new()
            {
                BrandId = brandId,
                ModelName = createModelDTO.ModelName,
            };

            await _modelRepository.CreateAsync(newModel);
        }

        public async Task DeleteModel(int id)
        {
            Model model = await _modelRepository.GetAsync(m => m.Id == id);
            if (model == null)
                throw new ArgumentException("Model bulunamadı.");

            _modelRepository.Delete(model);
        }

        public async Task<List<GetModelsDTO>> GetAllModelsAsync(int brandId)
        {
            bool isBranchExist = await _brandRepository.AnyAsync(b => b.Id == brandId);
            if (!isBranchExist)
                throw new ArgumentException("Marka bulunamadı.");
            List<Model> models = await _modelRepository.GetAllAsync(m=> m.BrandId == brandId);

            return _mapper.Map<List<GetModelsDTO>>(models);
        }

        public async Task<GetSingleModelDTO> GetModelAsync(int id)
        {
            Model model = await _modelRepository.GetAsync(m => m.Id == id);
            if (model == null)
                throw new ArgumentException("Model bulunamadı.");

            GetSingleModelDTO getSingleModelDTO = _mapper.Map<GetSingleModelDTO>(model);

            return getSingleModelDTO;
        }

        public async Task UpdateModelAsync(UpdateModelDTO updateModelDTO, int id)
        {
            Model SelectModel = await _modelRepository.GetAsync(b => b.Id == id);
            if (SelectModel == null)
                throw new ArgumentException("Model bulunamadı.");

            SelectModel.ModelName = updateModelDTO.ModelName;
            _modelRepository.Update(SelectModel);
        }
    }
}
