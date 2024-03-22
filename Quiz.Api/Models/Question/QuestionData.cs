using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class QuestionData
    {
        public int ID { get; set; }
        
        //public int QuizID { get; set; }
        
        //public int QuizThemeID { get; set; }

        public int QuestionTypeID { get; set; }

        public int AnswerTypeID { get; set; }

        //public string QuizName { get; set; }
        
        //public string QuizThemeName { get; set; }

        public string QuestionText { get; set; }

        //public double QuestionNo { get; set; }

        //public string CorrectAnswer { get; set; }        
       
    }
}