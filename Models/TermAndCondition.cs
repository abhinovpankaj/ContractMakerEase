using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged.Models
{
   public  class TermAndCondition: ICondition
   {
        public string Header { get; set; }

        public string Content { get; set; }

        public Guid TnCId { get; set; }= new Guid();

        public NodeItemType Type { get; set; }
        public Guid ParentId
        { get; set; } 
    }
}
