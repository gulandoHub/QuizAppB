using System.ComponentModel.DataAnnotations;


namespace QuizData
{
    public class EntityBase
    {
        [Key]
        [Required]
        public virtual int ID { get; set; }
    }
}