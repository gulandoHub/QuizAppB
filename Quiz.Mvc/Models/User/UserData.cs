using System.ComponentModel.DataAnnotations;


namespace QuizMvc.Models
{
    public class UserLoginData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "UserName")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    
    public class UserData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "UserName")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
    }
    
    public class UserRoleData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "User Name")]
        public int UserID { get; set; }
        
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Role Name")]
        public int RoleID { get; set; }
        
        public string RoleName { get; set; }
        
    }
    
    public class UserRightData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "User Name")]
        public int UserID { get; set; }
        
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Right Name")]
        public int RightID { get; set; }
        
        public string RightName { get; set; }
        
    }

    public class RoleRightData
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Role Name")]
        public int RoleID { get; set; }
        
        public string RoleName { get; set; }
        
        [Required]
        [Display(Name = "Right Name")]
        public int RightID { get; set; }
        
        public string RightName { get; set; }

    }
    
}