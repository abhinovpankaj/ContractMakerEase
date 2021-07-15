using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged.Models
{
   public  class TermAndCondition: ViewModelBase,ICondition
    {
        public TermAndCondition()
        {
            TnCId = Guid.NewGuid();
        }
        public List<TermAndCondition> Children { get; set; }
        public bool IsCollapsed { get; set; }
        public string Header { get; set; }

        public string Content { get; set; }

        public Guid TnCId { get; set; }

        private NodeItemType _type;
        public NodeItemType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                RaisePropertyChanged("Type");
            }
        }
        private string _sequence;
        public string ConditionSequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
                RaisePropertyChanged("ConditionSequence");
            }
        }
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
