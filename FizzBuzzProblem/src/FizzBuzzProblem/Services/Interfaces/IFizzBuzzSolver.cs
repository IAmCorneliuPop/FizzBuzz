using FizzBuzzProblem.Models;

namespace FizzBuzzProblem.Services.Interfaces
{
    /// <summary>
    /// Solving a Fizz Buzz problem.
    /// </summary>
    public interface IFizzBuzzSolver
    {
        /// <summary>
        /// It will do the necessary replacement of values based on the model passed in.
        /// </summary>
        /// <param name="model">The model to use when solving the Fizz Buzz.</param>
        /// <returns>A list representation of the replaced / solved Fizz Buzz model passed in.</returns>
        /// <remarks>If the model passed in does not satisfy the service validation, null should be returned.</remarks>
        List<string>? Solve(FizzBuzzRequestModel model);
    }
}
