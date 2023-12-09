using FizzBuzzProblem.Controllers;
using FizzBuzzProblem.Models;
using FizzBuzzProblem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FizzBuzzProblemTests.Controller
{
    public class FizzBuzzControlerTests
    {
        [Fact]
        public void Controller_Null_Model_Returns_400()
        {
            // Setup
            Mock<ILogger<FizzBuzzController>> logger = new Mock<ILogger<FizzBuzzController>>();
            Mock<IFizzBuzzSolver> service = new Mock<IFizzBuzzSolver>();
            FizzBuzzController controller = new FizzBuzzController(service.Object, logger.Object);

            // Act
            var response = controller.Solve(null);

            // Assert

            // Response shouldn't be null
            Assert.NotNull(response);

            // Response type should be of type StatusCodeResult
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response.Result);

            // Response code should be 400
            Assert.Equal(400, statusCodeResult.StatusCode);

            // Verify method Solve wasn't called because the execution didn't get that far
            service.Verify(p => p.Solve(It.IsAny<FizzBuzzRequestModel>()), Times.Never());
        }

        [Fact]
        public void Controller_Service_Returns_Null_And_Controller_400()
        {
            // Setup
            Mock<ILogger<FizzBuzzController>> logger = new Mock<ILogger<FizzBuzzController>>();
            Mock<IFizzBuzzSolver> service = new Mock<IFizzBuzzSolver>();
            service.Setup(p => p.Solve(It.IsAny<FizzBuzzRequestModel>())).Returns((List<string>?)null);
            FizzBuzzController controller = new FizzBuzzController(service.Object, logger.Object);


            // Act
            var response = controller.Solve(new FizzBuzzRequestModel());

            // Assert

            // Response shouldn't be null
            Assert.NotNull(response);

            // Response type should be of type StatusCodeResult
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response.Result);

            // Response code should be 400
            Assert.Equal(400, statusCodeResult.StatusCode);

            // Verify method Solve wasn't called because the execution didn't get that far
            service.Verify(p => p.Solve(It.IsAny<FizzBuzzRequestModel>()), Times.Once());
        }

        [Fact]
        public void Controller_Service_Returns_Valid_Response()
        {
            // Setup
            Mock<ILogger<FizzBuzzController>> logger = new Mock<ILogger<FizzBuzzController>>();
            Mock<IFizzBuzzSolver> service = new Mock<IFizzBuzzSolver>();
            List<string> values = new List<string>() { "fizz", "buzz" };
            service.Setup(p => p.Solve(It.IsAny<FizzBuzzRequestModel>())).Returns(values);
            FizzBuzzController controller = new FizzBuzzController(service.Object, logger.Object);


            // Act
            var response = controller.Solve(new FizzBuzzRequestModel());

            // Assert

            // Response shouldn't be null
            Assert.NotNull(response);
            Assert.NotNull(response.Value);

            // Response type should be of type StatusCodeResult
            var resultObject = Assert.IsType<FizzBuzzResponseModel>(response.Value);
            Assert.NotNull(resultObject);

            // Verify method Solve wasn't called because the execution didn't get that far
            service.Verify(p => p.Solve(It.IsAny<FizzBuzzRequestModel>()), Times.Once());

            for (int i = 0; i < resultObject.Result.Count; i++)
            {
                Assert.Equal(response.Value.Result[i], values[i]);
            }
        }

        [Fact]
        public void Controller_Exception_Caught_Returns_500()
        {
            // Setup
            Mock<ILogger<FizzBuzzController>> logger = new Mock<ILogger<FizzBuzzController>>();
            Mock<IFizzBuzzSolver> service = new Mock<IFizzBuzzSolver>();
            service.Setup(p => p.Solve(It.IsAny<FizzBuzzRequestModel>())).Throws<Exception>();
            FizzBuzzController controller = new FizzBuzzController(service.Object, logger.Object);

            // Act
            var response = controller.Solve(new FizzBuzzRequestModel());

            // Assert

            // Response should not be null
            Assert.NotNull(response);

            // Check response type
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response.Result);

            // We expect 500 when an exception is thrown from the service
            Assert.Equal(500, statusCodeResult.StatusCode);

            // We expect the Solve method to be called once
            service.Verify(p => p.Solve(It.IsAny<FizzBuzzRequestModel>()), Times.Once());
        }
    }
}