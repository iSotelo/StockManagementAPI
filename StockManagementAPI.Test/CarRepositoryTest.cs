using System;
using Xunit;
using Moq;
using StockManagementAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using StockManagementAPI.DAL;
using System.Collections.Generic;
using StockManagementAPI.Models;
using System.Threading.Tasks;

namespace StockManagementAPI.Test
{
    public class CarRepositoryTest
    {
        [Fact]
        public async Task GetCarByIdAsync()
        {
            // Arrange

            // Act

            // Assert


            //// Arrange
            //var moc = new Mock<ICarRepository>();
            //moc.Setup(mc => mc.GetCars().Result).Returns(IEnumerable<Car>);
            //var car = new Car(moc.Object);
            //car.Caller(null, 9);

            //Assert.Equal(200, car.mockingMethod());



            //var controller = new CarController(mock.Object);

            //// Act
            //var result = controller.Index().Result;

            //// Assert correct non-null View returned with no Model
            //var viewResult = Assert.IsType<ViewResult>(result);
            //Assert.NotNull(viewResult);
            ////Assert.Equal(nameof(controller.Index), viewResult.ViewName);
            ////Assert.NotNull(viewResult.ViewData);
            ////Assert.Null(viewResult.ViewData.Model);
        }

        
        public void UpdateTest()
        {
            //var mock = new Mock<ICarRepository>();
            //var controller = new CarController(mock.Object);
            //// Act
            //var result = controller.Update().Result;

            //// Assert correct non-null View returned with no Model
            //var viewResult = Assert.IsType<ViewResult>(result);
            //Assert.NotNull(viewResult);
            ////Assert.Equal(nameof(controller.Index), viewResult.ViewName);
            ////Assert.NotNull(viewResult.ViewData);
            //Assert.Null(viewResult.ViewData.Model);

        }
    }
}
