using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Role : EntityBase
    {
        [Column("RoleID")]
        public override int ID { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}