using ConsumePersonApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using PersonApiDAL.Models;
using PersonApiDAL.Services.ConsumeApiPersonService;
using PersonApiTests;
using System.Net.Http.Json;
using System.Net;


namespace ConsumePersonApiTests
{
    public class ConsumePersonApiTests
    {
        private Mock<PersonService> _serviceMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<PersonsController> _consumePersonApiController;


        public ConsumePersonApiTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _serviceMock = new Mock<PersonService>();
            _consumePersonApiController = new Mock<PersonsController>();
        }

        [Fact]
        public async Task GetPersonAsync()
        {
            //arrange
            const int personId = 1;
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var service = new Mock<PersonService>();

            var dto = new Person { Id = personId, Name = "Dave", Age = 42 };
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create<Person>(dto)
            };

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHandler.Object);

            var sut = new ClassUnderTest(httpClient);


            //act
            var actual = await sut.GetPersonAsync(personId);

            //assert
            Assert.NotNull(actual);
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetPeopleAsync()
        {
            //arrange
            const int personId = 1;
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var service = new Mock<PersonService>();

            var dto = new List<Person>() { new Person { Id = personId, Name = "Dave", Age = 42 } };
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create<List<Person>>(dto)
            };

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHandler.Object);

            var sut = new ClassUnderTest(httpClient);


            //act
            var actual = await sut.GetAllPeopleAsync();

            //assert
            Assert.NotNull(actual);
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task CreatePersonAsync()
        {
            //arrange
            const int personId = 1;
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var service = new Mock<PersonService>();

            var dto = new Person { Id = personId, Name = "Dave", Age = 42 };
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create<Person>(dto)
            };

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHandler.Object);

            var sut = new ClassUnderTest(httpClient);

            //act
            await sut.CreatePersonAsync(dto);

            //assert
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task UpdatePersonAsync()
        {
            //arrange
            const int personId = 1;
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var service = new Mock<PersonService>();

            var dto = new Person { Id = personId, Name = "Dave", Age = 42 };
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create<Person>(dto)
            };

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHandler.Object);

            var sut = new ClassUnderTest(httpClient);

            //act
            await sut.UpdatePersonAsync(dto);

            //assert
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Put),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task DeletePersonAsync()
        {
            //arrange
            const int personId = 1;
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var service = new Mock<PersonService>();

            var dto = new Person { Id = personId, Name = "Dave", Age = 42 };
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create<Person>(dto)
            };

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Delete),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Inject the handler or client into your application code
            var httpClient = new HttpClient(mockHandler.Object);

            var sut = new ClassUnderTest(httpClient);

            //act
            await sut.DeletePersonAsync(personId);

            //assert
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Delete),
                ItExpr.IsAny<CancellationToken>());
        }

    }
}