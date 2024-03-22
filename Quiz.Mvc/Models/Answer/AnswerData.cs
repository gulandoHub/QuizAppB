namespace QuizMvc.Models
{
    public class AnswerData
    {
        public int ID { get; set; }
        
        public int QuestionID { get; set; }
        
        public string QuestionName { get; set; }
        
        public int AnswerTypeID { get; set; }
        
        public string AnswerTypeName { get; set; }
        
        public string AnswerText { get; set; }
    }
}