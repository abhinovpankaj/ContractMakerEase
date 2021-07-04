using DragAndDropSampleManaged.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DragAndDropSampleManaged.ViewModels
{
    public class NodeTemplateSelector: DataTemplateSelector
    {
        public DataTemplate ProjectTemplate { get; set; }
        public DataTemplate ParagraphTemplate { get; set; }
        public DataTemplate SubParagraphTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            var explorerItem = (TermAndCondition)item;
            switch (explorerItem.Type)
            {
                case NodeItemType.Project:
                    return ProjectTemplate;
                case NodeItemType.Paragraph:
                    return ParagraphTemplate;
                case NodeItemType.SubParagraph:
                    return SubParagraphTemplate;
                default:
                    return SubParagraphTemplate; 
            }


        }
    }

    public class DraggedNodeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProjectTemplate { get; set; }
        public DataTemplate ParagraphTemplate { get; set; }
        public DataTemplate SubParagraphTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item)
        {
            var explorerItem = (TermAndCondition)item;
            switch (explorerItem.Type)
            {
                case NodeItemType.Project:
                    return ProjectTemplate;
                case NodeItemType.Paragraph:
                    return ParagraphTemplate;
                case NodeItemType.SubParagraph:
                    return SubParagraphTemplate;
                default:
                    return SubParagraphTemplate;
            }


        }
    }
}
