using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuizData
{
    public class Image : EntityBase
    {
        [Column("ImageID")]
        public override int ID { get; set; }
        
        [Required]
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public long Length { get; set; }

        [Required]
        public string ContentType { get; set; }

    }
}