using FizzBuzzProblem.Models;
using FizzBuzzProblem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FizzBuzzProblem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        #region Private Properties

        private IFizzBuzzSolver FizzBuzzService { get; }
        private ILogger Logger { get; }

        #endregion  Private Properties

        #region Constructor

        public FizzBuzzController(IFizzBuzzSolver fizzBuzzService, ILogger<FizzBuzzController> logger) 
        {
            FizzBuzzService = fizzBuzzService;
            Logger = logger;
        }

        #endregion Constructor

        #region Endpoints

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FizzBuzzResponseModel> Solve([FromBody] FizzBuzzRequestModel model)
        {
            Logger.LogInformation($"Request received to solve a Fizz Buzz problem.");

            ActionResult< FizzBuzzResponseModel> response;
            try
            {
                if (model == null)
                {
                    response = StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    List<string>? output = FizzBuzzService.Solve(model);

                    if (output == null)
                    {
                        response = StatusCode(StatusCodes.Status400BadRequest);
                    }
                    else
                    {
                        response = new FizzBuzzResponseModel()
                        {
                            Result = output
                        };
                    };
                }
            }

            catch (Exception ex)
            {
                // Log the exception
                Logger.LogError(ex, $"An error occurred while processing endpoing {nameof(Solve)}");

                // Return generic 500 error. We do not want to disclose more information to avoid security concerns
                response = StatusCode(StatusCodes.Status500InternalServerError);
            }

            Logger.LogInformation($"Request to solve Fizz Buzz completed.");
            return response;
        }

        #endregion Endpoints
    }
}
