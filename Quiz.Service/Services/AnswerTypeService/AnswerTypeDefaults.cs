namespace QuizService
{
    public static class AnswerTypeDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string AnswerTypeAllCacheKey => "Quiz.answerType.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : answer ID
        /// </remarks>
        public static string AnswerTypeByIdCacheKey => "Quiz.answerType.ByID";
    }
}