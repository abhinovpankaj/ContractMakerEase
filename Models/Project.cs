using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged.Models
{
    public class Project : TermAndCondition
    {
        public List<Paragraph> Paragraphs { get; set; }

        public List<SubParagraph> SubParagraphs { get; set; }

        public Guid ProjectID { get; set; } = new Guid();
    }
}
