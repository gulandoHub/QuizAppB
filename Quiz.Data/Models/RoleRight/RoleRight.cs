using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizData
{
    public class RoleRight : EntityBase
    {
        [Column("RoleRightID")]
        public override int ID { get; set; }
        
        [Required]
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        
        [Required]
        [ForeignKey("Right")]
        public int RightID { get; set; }
        
    }

    public class RoleRightSummary : RoleRight
    {
        public string RoleName { get; set; }
        
        public string RightName { get; set; }
    }
}