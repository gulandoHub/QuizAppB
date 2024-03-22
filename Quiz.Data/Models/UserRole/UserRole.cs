using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class UserRole : EntityBase
    {
        [Column("UserRoleID")]
        public override int ID { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }
        
        [Required]
        [ForeignKey("Role")]
        public int RoleID { get; set; }
    }

    public class UserRoleSummary : UserRole
    {
        public string UserName { get; set; }
        
        public string RoleName { get; set; }
    }
}