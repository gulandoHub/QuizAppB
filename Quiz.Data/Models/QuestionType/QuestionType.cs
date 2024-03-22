using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class QuestionType : EntityBase
    {
        [Column("QuestionTypeID")]
        public override int ID { get; set; }

        [Required]
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [Required]
        public string QuestionTypeName { get; set; }
    }

    public class QuestionTypeSummary : QuestionType
    {
        public string QuizName { get; set; }
        
    }
}