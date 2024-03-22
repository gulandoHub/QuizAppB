namespace QuizService
{
    public static class UserDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string UserAllCacheKey => "Quiz.user.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string UserByIdCacheKey => "Quiz.user.ByID";
    }
}