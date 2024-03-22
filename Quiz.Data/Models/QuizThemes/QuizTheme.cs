using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class QuizTheme : EntityBase
    {
        [Column("QuizThemeID")]
        public override int ID { get; set; }

        [Required]
        public string QuizThemeName { get; set; }

        [Required]
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }

        //public Quiz Quiz { get; set; }
    }

    public class QuizThemeSummary : QuizTheme
    {
        public string QuizName { get; set; }
    }
}