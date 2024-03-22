namespace QuizService
{
    public static class RoleRightDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string RoleRightAllCacheKey => "Quiz.RoleRight.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string RoleRightByIdCacheKey => "Quiz.RoleRight.ByID";
    }
}