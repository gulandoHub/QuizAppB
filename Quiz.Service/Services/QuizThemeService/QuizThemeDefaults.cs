namespace QuizService
{
    public static class QuizThemeDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string QuizThemeAllCacheKey => "Quiz.QuizTheme.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : question ID
        /// </remarks>
        public static string QuizThemeIdCacheKey => "Quiz.QuizTheme.ByID";
    }
}