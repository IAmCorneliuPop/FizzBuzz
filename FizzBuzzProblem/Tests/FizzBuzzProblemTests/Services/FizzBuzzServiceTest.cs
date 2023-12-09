using FizzBuzzProblem.Models;
using FizzBuzzProblem.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;

namespace FizzBuzzProblemTests.Services
{
    public class FizzBuzzServiceTest
    {
        [Fact]
        public void Service_Null_Model_Returns_Null()
        {
            // Setup
            Mock<ILogger<FizzBuzzService>> logger = new Mock<ILogger<FizzBuzzService>>();
            FizzBuzzService service = new FizzBuzzService(logger.Object);

            // Act
            var response = service.Solve(null);

            // Assert

            // The response should be null
            Assert.Null(response);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(256)]
        public void Service_Invalid_Model_Returns_Null(int topLimit)
        {
            // Setup
            Mock<ILogger<FizzBuzzService>> logger = new Mock<ILogger<FizzBuzzService>>();
            FizzBuzzService service = new FizzBuzzService(logger.Object);

            // Act
            var response = service.Solve(new FizzBuzzProblem.Models.FizzBuzzRequestModel()
            {
                TopLimit = topLimit
            });

            // Assert

            // The response should be null since the model passed in doesn't pass validation
            Assert.Null(response);
        }

        [Fact]
        public void Service_Throws_Exception()
        {
            // Setup
            Mock<ILogger<FizzBuzzService>> logger = new Mock<ILogger<FizzBuzzService>>();
            FizzBuzzService service = new FizzBuzzService(logger.Object);

            // Act + Assert
            Assert.Throws<DivideByZeroException>(() => service.Solve(new FizzBuzzProblem.Models.FizzBuzzRequestModel()
            {
                TopLimit = 1,
                Maps = new List<FizzBuzzProblem.Models.FizzBuzzMap>()
                {
                    new FizzBuzzProblem.Models.FizzBuzzMap() { Key = 0, Value = "Fuzz" }
                }
            }));
        }

        [Fact]
        public void Service_Returns_Correct_Response()
        {
            // Setup
            Mock<ILogger<FizzBuzzService>> logger = new Mock<ILogger<FizzBuzzService>>();
            FizzBuzzService service = new FizzBuzzService(logger.Object);
            FizzBuzzRequestModel model = new FizzBuzzRequestModel();
            model.TopLimit = 60;
            model.Maps = new List<FizzBuzzMap>()
            {
                new FizzBuzzMap() { Key = 3, Value = "Fizz" },
                new FizzBuzzMap() { Key = 4, Value = "Buzz" },
                new FizzBuzzMap() { Key = 5, Value = "Fuzz" },
            };

            // Act
            var response = service.Solve(model);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(model.TopLimit, response.Count);
            Assert.Equal("Fizz", response[2]);
            Assert.Equal("Buzz", response[3]);
            Assert.Equal("Fuzz", response[4]);
            // This is a multiple for all 3 words
            Assert.Equal("FizzBuzzFuzz", response[59]);

        }
    }
}
