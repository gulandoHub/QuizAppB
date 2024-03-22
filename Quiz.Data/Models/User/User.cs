using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace QuizData
{
    public class User : EntityBase
    {
        [Column("UserID")]
        public override int ID { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public byte[] PasswordHash { get; set; }
        
        [Required]
        public byte[] PasswordSalt { get; set; }
        
        /// <summary>
        //This is only for Quiz.Mvc to check that all functional related with UserManagement works fine, for real application
        // we will use Quiz.Api and for that we already have correct(JWT).
        /// </summary>
        public string Password { get; set; }
    }

    public class AppUser : IdentityUser
    {

    }
}