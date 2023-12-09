namespace FizzBuzzProblem.Models
{
    /// <summary>
    /// Represents the key value pair for how to map the numbers to their values.
    /// </summary>
    public class FizzBuzzMap
    {
        /// <summary>
        /// Represents the number to replace every time itself, or a multiple of it, is found
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// Represents the value used to replace the number from <see cref="Key"/>.
        /// </summary>
        public string Value { get; set; }
    }
}
