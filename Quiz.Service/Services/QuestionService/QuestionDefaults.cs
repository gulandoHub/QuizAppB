namespace QuizService
{
    public static class QuestionDefaults
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static string QuestionAllCacheKey => "Quiz.question.all";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : question ID
        /// </remarks>
        public static string QuestionyIdCacheKey => "Quiz.question.ByID";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : question ID
        /// </remarks>
        public static string QuestionGetAll => "Quiz.question.GetAll";
    }
}