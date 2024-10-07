using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;
using PersonApi.Controllers;
using PersonApiDAL.Models;
using PersonApiDAL.Services.ProduceApiPersonService;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonApiTests
{
    public class PersonApiTests
    {
        private Mock<IPersonService> _serviceMock;
        private PersonsController? _personApiController;


        public PersonApiTests()
        {
            _serviceMock = new Mock<IPersonService>();
        }

        [Fact]
        public async void ConsumePersonController_GetPerson()
        {
            //arrange
            _serviceMock.Setup(x => x.GetPerson(1))
                .ReturnsAsync(new Person { Id = 1 });
            
            //act
            _personApiController = new PersonsController(_serviceMock.Object);
            var result = await _personApiController.GetPerson(1);

            var okResult = Assert.IsType<JsonResult>(result);
            var item = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(1, item.Id);
        }

        [Fact]
        public async void ApiPersonController_GetPeople()
        {
            //arrange
            _serviceMock.Setup(x => x.GetAllPeople())
                .ReturnsAsync(new List<Person> { new Person() { Id = 1, Name = "David", Age = 55 } });

            //act
            _personApiController = new PersonsController(_serviceMock.Object);
            var result = await _personApiController.GetAllPeople();
            var item = Assert.IsType<JsonResult>(result);
            Assert.Equivalent(new List<Person> { new Person() { Id = 1, Name = "David", Age = 55 } }, item.Value, strict: true);
        }

        [Fact]
        public async void ApiPersonController_CreatePerson()
        {
            //arrange
            var person = new Person() { Id = 1, Name = "David", Age = 55 };
            _serviceMock = new Mock<IPersonService>(MockBehavior.Loose);
       
            //act
            _personApiController = new PersonsController(_serviceMock.Object);
            var result = await _personApiController.CreatePerson(person);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task ApiPersonController_UpdatePerson()
        {
            //Arrange
            _serviceMock.Setup(x => x.GetPerson(1))
              .ReturnsAsync(new Person { Id = 1 });
            _personApiController = new PersonsController(_serviceMock.Object);
            var personId = 1;
            //Act
            var existingPost = await _personApiController.GetPerson(personId);

            var person = new Person
            {
                Id = 1,
                Name = "Joe",
                Age = 55,
            };
            var updatedData = await _personApiController.UpdatePerson(person);
            //Assert
            Assert.IsType<JsonResult>(updatedData);
            Assert.Equivalent(person, updatedData.Value);
        }

        [Fact]
        public async Task ApiPersonController_DeletePerson()
        {
            _serviceMock.Setup(x => x.GetPerson(1))
              .ReturnsAsync(new Person { Id = 1 });
            _personApiController = new PersonsController(_serviceMock.Object);
          
            var updatedData = await _personApiController.DeletePerson(1);
            //Assert
            Assert.IsType<JsonResult>(updatedData);
            Assert.Equal("Person Deleted: 1:", updatedData.Value);
        }
    }
}