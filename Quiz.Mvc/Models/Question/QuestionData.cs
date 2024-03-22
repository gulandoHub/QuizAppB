using System.ComponentModel.DataAnnotations;
using QuizData;

namespace QuizMvc.Models
{
    public class QuestionData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Name")]
        public int QuizID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Theme Name")]
        public int QuizThemeID { get; set; }
        
        [Required]
        [Display(Name = "Answer Type")]
        public int AnswerTypeID { get; set; }
        
        [Required]
        [Display(Name = "Question Type")]
        public int QuestionTypeID { get; set; }

        [Required]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        [Required]
        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }

        [Required]
        [Display(Name = "Question No")]
        public double QuestionNo { get; set; }

        public int ImageID { get; set; }
        
        public string QuizName { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }

        public string ExamTypeName { get; set; }

        public AnswerTypeConfigurationSummary AnswerTypeConfigurationSummary { get; set; }
    }
}