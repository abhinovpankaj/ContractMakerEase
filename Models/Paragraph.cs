using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged.Models
{
    public class Paragraph :TermAndCondition
    {
        public List<SubParagraph> SubParagraphs { get; set; }

    }
}
