namespace QuizService
{
    public static class QuizDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string QuizAllCacheKey => "Quiz.quiz.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : question ID
        /// </remarks>
        public static string QuizIdCacheKey => "Quiz.quiz.ByID";
    }
}