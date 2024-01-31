using AutoMapper;
using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.DTOs.Version;
using E_Phone.BLL.Services.Abstract;
using E_Phone.Core.Entities;
using E_Phone.Core.IRepositories.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Concrete
{
    public class VersionService : IVersionService
    {
        private readonly IBaseRepository<Core.Entities.Version> _versionRepository;
        private readonly IBaseRepository<Model> _modelRepository;
        private readonly IMapper _mapper;

        public VersionService(IBaseRepository<Core.Entities.Version> versionRepository, IBaseRepository<Model> modelRepository, IMapper mapper)
        {
            _versionRepository = versionRepository;
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task CreateVersionAsync(CreateVersionDTO createVersionDTO, int modelId)
        {
            bool isModelExist = await _modelRepository.AnyAsync(b => b.Id == modelId);
            if (!isModelExist)
                throw new ArgumentException("Model bulunamadı.");

            Core.Entities.Version newVersion = new()
            {
                ModelId = modelId,
                price = createVersionDTO.price,
                Stock = createVersionDTO.Stock,
                StorageCapacity = createVersionDTO.StorageCapacity
            };

            await _versionRepository.CreateAsync(newVersion);
        }

        public async Task DeleteVersion(int id)
        {
            Core.Entities.Version version = await _versionRepository.GetAsync(v => v.Id == id);
            if (version == null)
                throw new ArgumentException("Version bulunamadı.");

            _versionRepository.Delete(version);
        }

        public async Task<List<GetVersionsDTO>> GetAllVersionsAsync(int modelId)
        {
            bool isModelExist = await _modelRepository.AnyAsync(b => b.Id == modelId);
            if (!isModelExist)
                throw new ArgumentException("Model bulunamadı.");
            List<Core.Entities.Version> versions = await _versionRepository.GetAllAsync(v => v.ModelId == modelId);

            return _mapper.Map<List<GetVersionsDTO>>(versions);
        }

        public async Task<GetSingleVersionDTO> GetVersionAsync(int id)
        {
            Core.Entities.Version version = await _versionRepository.GetAsync(b => b.Id == id);
            if (version == null)
                throw new ArgumentException("Versiyon bulunamadı.");

            return _mapper.Map<GetSingleVersionDTO>(version);
        }

        public async Task UpdateVersionAsync(UpdateVersionDTO updateVersionDTO, int id)
        {
            Core.Entities.Version selectVersion = await _versionRepository.GetAsync(b => b.Id == id);
            if (selectVersion == null)
                throw new ArgumentException("Versiyon bulunamadı.");

            selectVersion.price = updateVersionDTO.price;
            selectVersion.StorageCapacity = updateVersionDTO.StorageCapacity;
            selectVersion.Stock = updateVersionDTO.Stock;
            _versionRepository.Update(selectVersion);
        }
    }
}
