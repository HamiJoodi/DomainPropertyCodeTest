using DomainProperty.Models.DtoModels;

namespace DomainProperty.Services
{
    public interface IPropertyService
    {
        Task<List<SuburbAverageDTO>> GetSuburbAverageAsync();
        Task<List<PropertyDTO>> GetAllPropertiesAsync(int page, int pageSize);
        Task<PropertyDTO?> GetPropertyByIdAsync(int id);
    }


}
