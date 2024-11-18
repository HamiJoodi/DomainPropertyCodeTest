using DomainProperty.Controllers;
using DomainProperty.Models.DtoModels;
using DomainProperty.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainPropertyTest
{
    public class SuburbControllerTest
    {
        private readonly SuburbController _controller;
        private readonly Mock<IPropertyService> _service;

        public SuburbControllerTest()
        {
            _service = new Mock<IPropertyService>();
            _controller = new SuburbController(_service.Object);
        }

        [Fact]
        public async void GetSuburbAverage_OkResult()
        {
            // Arrange
            _service.Setup(s => s.GetSuburbAverageAsync())
                .ReturnsAsync(new List<SuburbAverageDTO> { new SuburbAverageDTO { Name = "Kings Park", Units = 995750, Houses = 1583750 } });

            // Act
            var result = await _controller.GetSuburbs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var suburbs = Assert.IsType<List<SuburbAverageDTO>>(okResult.Value);
            Assert.Single(suburbs);
        }
    }
}
