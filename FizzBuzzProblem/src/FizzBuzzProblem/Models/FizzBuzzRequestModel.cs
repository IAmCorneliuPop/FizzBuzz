namespace FizzBuzzProblem.Models
{
    /// <summary>
    /// Represents the model for the request coming from the consumer.
    /// </summary>
    public class FizzBuzzRequestModel
    {
        /// <summary>
        /// A list of <see cref="FizzBuzzMap"/> that indicates how replacing should occur.
        /// </summary>
        public List<FizzBuzzMap> Maps { get; set; }

        /// <summary>
        /// The upper limit to how far the Fizz Buzz test should go.
        /// </summary>
        public long TopLimit { get; set; }
    }
}
