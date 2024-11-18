using AutoMapper;
using DomainProperty.Models.DataModels;
using DomainProperty.Models.DtoModels;
using DomainProperty.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Data.Common;

namespace DomainProperty.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly string _PropertiesCacheKey = "PropertyData";
        private readonly string _suburbCacheKey = "SuburbData";


        public PropertyService(IDataRepository dataRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _cache = memoryCache;
            _mapper = mapper;
        }

        private async Task<List<PropertyDTO>> LoadPropertiesDataAsync()
        {
            if (!_cache.TryGetValue(_PropertiesCacheKey, out List<PropertyDTO> properties))
            {

                var list = await _dataRepository.GetPropertiesAsync();
                properties = _mapper.Map<List<PropertyDTO>>(list);
                _cache.Set(_PropertiesCacheKey, properties, TimeSpan.FromMinutes(10));
            }

            return properties;
        }



        public async Task<List<PropertyDTO>> GetAllPropertiesAsync(int page, int pageSize)
        {

            var properties = await LoadPropertiesDataAsync();
            if (page == 0)
                return properties;
            return properties.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<PropertyDTO?> GetPropertyByIdAsync(int id)
        {
            var properties = await LoadPropertiesDataAsync();
            return properties.FirstOrDefault(p => p.Id == id);
        }

        public async Task<List<SuburbAverageDTO>> GetSuburbAverageAsync()
        {

            if (!_cache.TryGetValue(_suburbCacheKey, out List<SuburbAverageDTO> suburbInfo))
            {

                var properties = await LoadPropertiesDataAsync();

                suburbInfo = properties
                    .GroupBy(p => p.Suburb)
                    .Select(g => new SuburbAverageDTO
                    {
                        Name = g.Key,
                        Units = g.Where(p => p.Type == "unit").DefaultIfEmpty().Average(p => p?.Value ?? 0),
                        Houses = g.Where(p => p.Type == "house").DefaultIfEmpty().Average(p => p?.Value ?? 0)
                    })
                    .ToList();
                _cache.Set(_suburbCacheKey, suburbInfo, TimeSpan.FromMinutes(10));
            }

            return suburbInfo;

        }
    }
}
