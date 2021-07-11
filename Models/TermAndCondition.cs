using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged.Models
{
   public  class TermAndCondition: ICondition
   {
        public TermAndCondition()
        {
            TnCId = Guid.NewGuid();
        }
        public string Header { get; set; }

        public string Content { get; set; }

        public Guid TnCId { get; set; }

        public NodeItemType Type { get; set; }

        public Dictionary<string,string> InputFields { get; set; }
        public Guid ParentId
        { get; set; }


        public RelayCommand RemoveCommand
        {
            get
            {
                return new RelayCommand(FireRemove, true);
            }
        }

        private void FireRemove()
        {
            Messenger.Default.Send<Guid>(TnCId);
        }

        public override string ToString()
        {
            return Header;
        }
    }
}
