using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using System.Xml.Linq;

namespace QuizData
{
    public class ExamType : EntityBase
    {
        [Column("ExamTypeID")]
        public override int ID { get; set; }

        public string ExamTypeName { get; set; }

        [Column(TypeName = "xml")]
        public string ExtendedData { get; set; }

        [NotMapped]
        public XElement ExtendedDataElement
        {
            get => XElement.Parse(ExtendedData);
            set => ExtendedData = value.ToString();
        }
    }
}
