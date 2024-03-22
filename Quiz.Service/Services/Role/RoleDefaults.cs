namespace QuizService
{
    public static class RoleDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string RoleAllCacheKey => "Quiz.role.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string RoleByIdCacheKey => "Quiz.role.ByID";
    }
}