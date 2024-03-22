namespace QuizService
{
    public static class UserRightDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string UserRightAllCacheKey => "Quiz.UserRight.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string UserRightByIdCacheKey => "Quiz.UserRight.ByID";
    }
}