using DomainProperty.Models.DataModels;

namespace DomainProperty.Repositories
{
    public interface IDataRepository
    {
        Task<List<Property>> GetPropertiesAsync();
    }
}
