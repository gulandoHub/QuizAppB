namespace QuizService
{
    public static class UserRoleDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string UserRoleAllCacheKey => "Quiz.UserRole.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string UserRoleByIdCacheKey => "Quiz.UserRole.ByID";
    }
}