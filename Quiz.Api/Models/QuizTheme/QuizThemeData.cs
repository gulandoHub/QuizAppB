using System.ComponentModel.DataAnnotations;


namespace QuizApi.Models
{
    public class QuizThemeData
    {
        public int ID { get; set; }
              
        [Required]
        [Display(Name = "Quiz Name")]
        public int QuizID { get; set; }
        
        [Required]
        [Display(Name = "Quiz Theme Name")]
        public string QuizThemeName { get; set; }
        
        public string QuizName { get; set; }
    }
}