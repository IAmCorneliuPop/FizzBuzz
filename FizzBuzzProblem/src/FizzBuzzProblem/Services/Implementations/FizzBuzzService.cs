using FizzBuzzProblem.Models;
using FizzBuzzProblem.Services.Interfaces;
using System.Text;

namespace FizzBuzzProblem.Services.Implementations
{
    /// <summary>
    /// An implementation of <see cref="IFizzBuzzSolver"/>.
    /// </summary>
    public class FizzBuzzService : IFizzBuzzSolver
    {
        #region Private Properties

        private ILogger Logger { get; }

        #endregion Private Properties

        #region Constructor

        public FizzBuzzService(ILogger<FizzBuzzService> logger) 
        { 
            Logger = logger;
        }

        #endregion Constructor

        #region IFizzBuzzSolver Implementation

        public List<string>? Solve(FizzBuzzRequestModel model)
        {
            List<string>? output = null;

            if (Validator(model))
            {
                output = new List<string>();

                for (int i = 1; i <= model.TopLimit; i++)
                {
                    StringBuilder builder = new StringBuilder();

                    foreach (FizzBuzzMap map in model.Maps)
                    {
                        if (i % map.Key == 0)
                        {
                            builder.Append(map.Value);
                        }
                    }

                    if (builder.Length > 0)
                    {
                        output.Add(builder.ToString());
                    }
                    else
                    {
                        output.Add($"{i}");
                    }
                }
            }
            else
            {
                Logger.LogWarning($"The model passed in is not valid.");
            }

            return output;
        }

        #endregion IFizzBuzzSolver Implementation

        #region Private Methods

        private static bool Validator(FizzBuzzRequestModel model)
        {
            bool isValid = true;

            if (model == null)
            {
                isValid = false;
            }
            // Top limit is 250 max. Implicit bottom limit of 1
            else if (model.TopLimit > 250 || model.TopLimit < 1)
            {
                isValid = false;
            }

            return isValid;
        }

        #endregion Private Methods
    }
}
