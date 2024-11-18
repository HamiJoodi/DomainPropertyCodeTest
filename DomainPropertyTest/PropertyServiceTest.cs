using Newtonsoft.Json.Linq;
using System.Data.Common;
using DomainProperty.Repositories;
using Microsoft.Extensions.Caching.Memory;
using DomainProperty.Services;
using DomainProperty.Models.DataModels;
using AutoMapper;
using Moq;
using DomainProperty.Models;

namespace DomainPropertyTest
{
    public class PropertyServiceTest
    {
        [Fact]
        public async void GetSuburbAverage_CorrectAverage()
        {
            // Arrange
            var dataSource = new Mock<IDataRepository>();
            dataSource.Setup(   ds =>  ds.GetPropertiesAsync()).ReturnsAsync( new List<Property>
            {
            new Property { Suburb = "SuburbA", Value = 1000, Type = "unit" },
            new Property { Suburb = "SuburbA", Value = 2000, Type = "house" },
            new Property { Suburb = "SuburbA", Value = 3000, Type = "house" },
        });

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
            var cache = new MemoryCache(new MemoryCacheOptions());
            var service = new PropertyService(dataSource.Object, cache , mapper );

            // Act
            var result = await service.GetSuburbAverageAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(1000, result.First().Units);
            Assert.Equal(2500, result.First().Houses);
        }
    }
}