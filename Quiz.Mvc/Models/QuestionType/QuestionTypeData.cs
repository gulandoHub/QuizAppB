using System.ComponentModel.DataAnnotations;


namespace QuizMvc.Models
{
    public class QuestionTypeData
    {
        public int ID { get; set; }
          
        [Required]
        [Display(Name = "Quiz Name")]
        public int QuizID { get; set; }
        
        [Required]
        [Display(Name = "Question Type Name")]
        public string QuestionTypeName { get; set; }
        
        public string QuizName { get; set; }
    }
}