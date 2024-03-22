using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    
    public class Quiz : EntityBase
    {
        [Column("QuizID")]
        public override int ID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Name")]
        public string QuizName { get; set; }

        public ICollection<QuizTheme> QuizThemes { get; set; }

        public ICollection<QuestionType> QuestionTypes { get; set; }

        public ICollection<AnswerType> AnswerTypes { get; set; } 

        public ICollection<Question> Questions { get; set; }

    }

    public class QuizSummary : Quiz
    {
        public int QuizThemeID { get; set; }
        
        public int QuestionTypeID { get; set; }
        
        public int AnswerTypeID { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }
    }
}