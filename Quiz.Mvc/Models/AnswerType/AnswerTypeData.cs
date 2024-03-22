using System.ComponentModel.DataAnnotations;


namespace QuizMvc.Models
{
    public class AnswerTypeData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Name")]
        public int QuizID { get; set; }
                
        [Required]
        [Display(Name = "Question Type")]
        public int QuestionTypeID { get; set; }
        
        public string QuizName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        [Required]
        [Display(Name = "Answer Type")]
        public string AnswerTypeName { get; set; }

        public string AnswerTypeDescription { get; set; }
    }
}