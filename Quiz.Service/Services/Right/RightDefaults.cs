namespace QuizService
{
    public static class RightDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string RightAllCacheKey => "Quiz.right.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string RightByIdCacheKey => "Quiz.right.ByID";
    }
}