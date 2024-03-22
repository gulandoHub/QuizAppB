namespace QuizService
{
    public static class QuestionTypeDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string QuestionTypeAllCacheKey => "Quiz.questionType.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : question ID
        /// </remarks>
        public static string QuestionTypeIdCacheKey => "Quiz.questionType.ByID";
    }
}