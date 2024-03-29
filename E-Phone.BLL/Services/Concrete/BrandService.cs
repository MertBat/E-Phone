﻿using AutoMapper;
using E_Phone.BLL.DTOs.Brand;
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
    public class BrandService : IBrandService
    {
        private readonly IBaseRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBaseRepository<Brand> brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(CreateBrandDTO createBrandDTO)
        {
            bool isBranchExist = await _brandRepository.AnyAsync(b => b.BrandName == createBrandDTO.BrandName);
            if (isBranchExist)
                throw new ArgumentException("Aynı isimde bir marka vardır");

            Brand newBrand = _mapper.Map<Brand>(createBrandDTO);
            await _brandRepository.CreateAsync(newBrand);
        }

        public async Task DeleteBrand(int id)
        {
            Brand brand = await _brandRepository.GetAsync(b => b.Id == id);
            if (brand == null)
                throw new ArgumentException("Brand bulunamadı.");

            _brandRepository.Delete(brand);
        }

        public async Task<List<GetBrandsDTO>> GetAllBrandsAsync()
        {
            List<Brand> brands = await _brandRepository.GetAllAsync();

            return _mapper.Map<List<GetBrandsDTO>>(brands);
        }

        public async Task<GetSingleBrandDTO> GetBrandAsync(int id)
        {
            Brand brand = await _brandRepository.GetAsync(b => b.Id == id);
            if (brand == null)
                throw new ArgumentException("Marka bulunamadı");

            return _mapper.Map<GetSingleBrandDTO>(brand);
        }

        public async Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO, int id)
        {
            Brand selectedBrand = await _brandRepository.GetAsync(b => b.Id == id);
            if (selectedBrand == null)
                throw new ArgumentException("Bu isimde bir marka yoktur");

            selectedBrand.BrandName = updateBrandDTO.BrandName;
            _brandRepository.Update(selectedBrand);
        }

    }
}
