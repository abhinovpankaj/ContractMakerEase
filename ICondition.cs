using DragAndDropSampleManaged.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged
{
   public interface ICondition
    {
        Guid ParentId { get; set; }
    }

    public interface IContractDocument
    {
        void CreateDocument();
    }
}
