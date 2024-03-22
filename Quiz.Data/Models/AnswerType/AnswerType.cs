using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace QuizData
{
    public enum RenderType
    {
        None = 0,
        CheckBox = 1,
        Input = 2,
        RadioGroup = 3
    }

    public class AnswerType : EntityBase
    {
        [Column("AnswerTypeID")]
        public override int ID { get; set; }
        
        [Required]
        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        
        [Required]
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }
        
        [Required]
        public string AnswerTypeName { get; set; }

        [Column(TypeName = "xml")]
        public string AnswerTypeDescription { get; set; }

        //[NotMapped]
        //public XElement AnswerTypeDescriptionElement => XElement.Parse(AnswerTypeDescription);
    }

    public class AnswerTypeSummary : AnswerType
    {
        public string QuizName { get; set; }
        
        public string QuestionTypeName { get; set; }

        public AnswerTypeConfiguration AnswerTypeConfiguration { get; set; }
    }

    [XmlRoot("AnswerType"), XmlType("AnswerType")]
    public class AnswerTypeConfiguration
    {
        public RenderType Type { get; set; }
        public int Count { get; set; }
        public int CorrectCount { get; set; }
        public int RowCount { get; set; }
    }

    public class AnswerTypeConfigurationSummary
    {
        public AnswerTypeConfiguration AnswerTypeConfiguration { get; set; }
        public string CorrectAnswer { get; set; }
    }
}