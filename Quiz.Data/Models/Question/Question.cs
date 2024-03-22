using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Question : EntityBase
    {
        [Column("QuestionID")]
        public override int ID { get; set; }

        [Required]
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [Required]
        [ForeignKey("QuizTheme")]
        public int QuizThemeID { get; set; }
        
        [Required]
        [ForeignKey("AnswerType")]
        public int AnswerTypeID { get; set; }
        
        [Required]
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }

        [Required]
        public double QuestionNo { get; set; }

        [Required]         
        public string CorrectAnswer { get; set; }

        [Required]
        public string QuestionText { get; set; }
    }

    public class QuestionSummary : Question
    {
        //public int ImageID { get; set; }

        //public byte[] ImageData { get; set; }

        public string QuizName { get; set; }
        
        public string QuizThemeName { get; set; }
        
        public string QuestionTypeName { get; set; }
        
        public string AnswerTypeName { get; set; }

        public string ExamTypeName { get; set; }
    }
}