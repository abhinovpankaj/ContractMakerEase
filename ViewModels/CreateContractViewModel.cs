﻿using DragAndDropSampleManaged.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropSampleManaged.ViewModels
{
    public class CreateContractViewModel : ViewModelBase
    {

        public ObservableCollection<Project> Conditions { get; set; }

        private void setTestData()
        {
            Projects = new ObservableCollection<Project>();
            Project P1 = new Project();
            P1.Header = "WW";
            P1.Content = "Place holder text for WW content";
            
            
            P1.Paragraphs = new List<Paragraph>();
            Paragraph para1 = new Paragraph();
            para1.Header = " WW Para1";
            para1.Content = "An Ellipse is a shape with a curved perimeter. To create a basic Ellipse, specify a Width, Height, and a Brush for the Fill.The next example creates an Ellipse with a Width of 200 and a Height of 200,and uses a SteelBlue colored SolidColorBrush as its Fill.";
            para1.ParentId = P1.TnCId;
            para1.InputFields = new Dictionary<string, string>();
            para1.InputFields.Add("SQFT", "0");
            para1.InputFields.Add("Stair", "");
            P1.Paragraphs.Add(para1);

            Paragraph para2 = new Paragraph();
            para2.Header = " WW Para2";
            para2.Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result";
            para2.ParentId = P1.TnCId;
            para2.InputFields = new Dictionary<string, string>();
            para2.InputFields.Add("VerticalSQFT", "0");
            para2.InputFields.Add("RiserCount", "");
            P1.Paragraphs.Add(para2);

            para2.SubParagraphs = new List<SubParagraph>();
            SubParagraph subPara = new SubParagraph();
            subPara.Header = " WW Para2 Sub para 1";
            subPara.Content = "inclusive Multiply flt with 2525 and take the Floor of the result";
            subPara.ParentId = para2.TnCId;
            para2.SubParagraphs.Add(subPara);

            SubParagraph subPara1 = new SubParagraph();
            subPara1.Header = " WW Para2 Sub para 2";
            subPara1.Content = "Sub Para, really small content.";
            subPara1.ParentId = para2.TnCId;
            para2.SubParagraphs.Add(subPara1);


            Project P2 = new Project();
            P2.Header = "Dexo";
            P2.Content = "Place holder text for Dexo content";


            P2.Paragraphs = new List<Paragraph>();
            Paragraph para3 = new Paragraph();
            para3.Header = " Dexo Para1";
            para3.Content = "An Ellipse is a shape with a curved perimeter. To create a basic Ellipse, specify a Width, Height, and a Brush for the Fill.The next example creates an Ellipse with a Width of 200 and a Height of 200,and uses a SteelBlue colored SolidColorBrush as its Fill.";
            para3.ParentId = P2.TnCId;
            para3.InputFields = new Dictionary<string, string>();
            para3.InputFields.Add("SQFT", "0");
            para3.InputFields.Add("Stair", "");
            P2.Paragraphs.Add(para3);

            Paragraph para4 = new Paragraph();
            para4.Header = " Dexo Para2";
            para4.Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result";
            para4.ParentId = P2.TnCId;
           
            P2.Paragraphs.Add(para4);

            para3.SubParagraphs = new List<SubParagraph>();
            SubParagraph subPara2 = new SubParagraph();
            subPara2.Header = " Dexo Para2 Sub para 1";
            subPara2.Content = "inclusive Multiply flt with 2525 and take the Floor of the result";
            subPara2.ParentId = para3.TnCId;
            para3.SubParagraphs.Add(subPara2);

            SubParagraph subPara4 = new SubParagraph();
            subPara4.Header = " Dexo Para2 Sub para 2";
            subPara4.Content = "Sub Para, really small content.";
            subPara4.ParentId = para3.TnCId;
            para3.SubParagraphs.Add(subPara4);

            Projects.Add(P1);
            Projects.Add(P2);

        }

        #region OldCode



        public ObservableCollection<Project> Projects { get; set; }

        public  ObservableCollection<SubParagraph> IndependentSubGraphs { get; set; }

        private bool _enabled;
        public bool IsOn
        {
            get
            {
                    return _enabled;
            }
            set
            {
                _enabled = value;
                ShowDetails = value == true ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                RaisePropertyChanged("IsOn");
                RaisePropertyChanged("ShowDetails");
            }
        }

        private Windows.UI.Xaml.Visibility _showDetails= Windows.UI.Xaml.Visibility.Collapsed;
        public Windows.UI.Xaml.Visibility ShowDetails
        {
            get
            {
                return _showDetails;
            }
            set
            {
                if (value!=_showDetails)
                {
                    _showDetails = value;
                    RaisePropertyChanged("ShowDetails");
                }
            }
        }

        public CreateContractViewModel()
        {
            //GetData();
            setTestData();
            Messenger.Default.Register<Guid>(this, DeleteNode);
        }

        
        private void DeleteNode(Guid obj)
        {
            //TODO
            return; //Need to work on this 
            var tobeDeleted=ArrangedContracts.FirstOrDefault(x => x.TnCId == obj);
            if (tobeDeleted!=null)
            {
                ArrangedContracts.Remove(tobeDeleted);
            }
            foreach (var item in ArrangedContracts)
            {
                var delPara = item.Paragraphs.FirstOrDefault(x => x.TnCId == obj);
                if (delPara != null)
                {
                    item.Paragraphs.Remove(delPara);
                }
                foreach (var para in item.Paragraphs)
                {
                    var delSubPara = para.SubParagraphs.FirstOrDefault(x => x.TnCId == obj);
                    if (delSubPara != null)
                    {
                        para.SubParagraphs.Remove(delSubPara);
                    }
                }
            }
           
           
           
        }

        public ObservableCollection<Project> ArrangedContracts { get; set; }
        private void GetData()
        {
            Projects = new ObservableCollection<Project>();
            ArrangedContracts = new ObservableCollection<Project>();
            Project Project1 = new Project()
            {
                Header = "Weather Wear",
                Content = "An Ellipse is a shape with a curved perimeter. To create a basic Ellipse, specify a Width, Height, and a Brush for the Fill.The next example creates an Ellipse with a Width of 200 and a Height of 200,and uses a SteelBlue colored SolidColorBrush as its Fill.",
                Type = NodeItemType.Project,
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph()
                    {
                        Header = "WW Para 1: Functional Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating a random string in C# uses the same concepts that are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>()
                        {
                            new SubParagraph()
                            {
                                Header = "WW SubPara 1:Sub Para spec",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                                
                            },
                            new SubParagraph()
                            {
                                Header = "WW SubPara 2: Sub Para spec",
                                Content = "  2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            //new SubParagraph()
                            //{
                            //    Header = "WW SubPara 3:Sub Para spec",
                            //    Content = "3 : Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                            //    Type = NodeItemType.SubParagraph,
                            //}
                        }
                    },
                    //new Paragraph()
                    //{
                    //    Header = "WW Para 2: Visual Specifications",
                    //    Type = NodeItemType.Paragraph,
                    //    Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                    //    SubParagraphs = new List<SubParagraph>()
                    //    {
                    //        new SubParagraph()
                    //        {
                    //            Header = "WW Subpara1: Sub Para spec1",
                    //            Content = "1: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                    //            Type = NodeItemType.SubParagraph,
                    //        },

                    //        new SubParagraph()
                    //        {
                    //            Header = "WW SubPara2: Sub Para spec 2",
                    //            Content = "2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                    //            Type = NodeItemType.SubParagraph,
                    //        }
                    //    }
                    //},
                    //new Paragraph()
                    //{
                    //    Header = "WW  Para 3: Other Specifications",
                    //    Type = NodeItemType.Paragraph,
                    //    Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                    //    SubParagraphs = new List<SubParagraph>()
                    //    {
                    //        new SubParagraph()
                    //        {
                    //            Header = "WW Subpara1: Sub Para spec",
                    //            Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                    //            Type = NodeItemType.SubParagraph,
                    //        }
                    //    }
                    //},
                    new Paragraph()
                    {
                        Header = "WW Para 4:Feature Schedule Sub",
                        Content = "No Sub Para only Para",
                        Type = NodeItemType.Paragraph,
                    },
                    new Paragraph()
                    {
                        Header = "WW Para 5:Feature Schedule Sub 2",
                        Content = "Both the projects above take recognize the disconnected entities when it's returned to the server, detect and save the changes, and return to the client affected data.",
                        Type = NodeItemType.Paragraph,
                    },

                },
                //SubParagraphs =new List<SubParagraph>()
                //{
                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub",
                //        Content ="Sub Para only Sub",
                //        Type = NodeItemType.SubParagraph
                //    },
                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub 2",
                //        Content ="The short answer is yes, every implementing type will have to create its own backing variable. This is because an interface is analogous to a contract. All it can do is specify particular publicly accessible pieces of code that an implementing type must make available; it cannot contain any code itself.",
                //        Type = NodeItemType.SubParagraph
                //    },
                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub 2",
                //        Content="Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                //        Type = NodeItemType.SubParagraph
                //    },
                //}
            };

            Project Project2 = new Project()
            {
                Header = "Pedestrian",
                Type = NodeItemType.Project,
                Content= @"// When you create a XAML element in code, you have to add // it to the XAML visual tree. This example assumes you have // a panel named 'layoutRoot' in your XAML file, like this:// <Grid x:Name= layoutRoot>layoutRoot.Children.Add(ellipse1) ",
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph()
                    {
                        Header = "Pede Para1: Functional Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating a random string in C# uses the same concepts that are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Pede Sub Para spec1",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            new SubParagraph()
                            {
                                Header = "Pede Sub Para spec 2 ",
                                Content = "  2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },

                        }
                    },
                    new Paragraph()
                    {
                        Header = "Pede Para2: Visual Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Pede Sub Para spec1",
                                Content = "1: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },


                        }
                    },
                    new Paragraph()
                    {
                        Header = "Pede Para 3:Other Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            }
                        }
                    },
                    new Paragraph()
                    {
                        Header = "Pede Para 4: Feature Schedule Sub",
                        Content = "No Sub Para only Para",
                        Type = NodeItemType.Paragraph,
                    },


                },
                //SubParagraphs = new List<SubParagraph>
                //{
                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub",
                //        Content ="Sub Para only Sub",
                //        Type = NodeItemType.SubParagraph
                //    },

                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub 2",
                //        Content="Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                //        Type = NodeItemType.SubParagraph
                //    },
                //}
            };

            Project Project3 = new Project()
            {
                Header = "Dexotex",
                Type = NodeItemType.Project,
                Content= "You can round the corners of a Rectangle. To create rounded corners, specify a value for the RadiusX and RadiusY properties. These properties specify the x-axis and y-axis of an ellipse that defines the curve of the corners. The maximum allowed value of RadiusX is the Width divided by two and the maximum allowed value of RadiusY is the Height divided by two.",
                Paragraphs = new List<Paragraph>
                {
                    new Paragraph()
                    {
                        Header = " dexo Para1: Functional Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating a random string in C# uses the same concepts that are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Dexo sub1:Sub Para spec",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            new SubParagraph()
                            {
                                Header = "DExo sub2:Sub Para spec 2 ",
                                Content = "  2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },

                        }
                    },
                    new Paragraph()
                    {
                        Header = "Dexo Para2:Visual Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "DExo Sub Para spec1",
                                Content = "1: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },


                        }
                    },
                    
                    new Paragraph()
                    {
                        Header = "Dexo Para3: Feature Schedule Sub",
                        Content = "No Sub Para only Para",
                        Type = NodeItemType.Paragraph,
                    },


                },
                //SubParagraphs = new List<SubParagraph>
                //{
                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub",
                //        Content ="Sub Para only Sub",
                //        Type = NodeItemType.SubParagraph
                //    },

                //    new SubParagraph
                //    {
                //        Header = "Feature Schedule Sub 2",
                //        Content="Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                //        Type = NodeItemType.SubParagraph
                //    },
                //}
            };

            Projects.Add(Project1);
            Projects.Add(Project2);
            Projects.Add(Project3);

            IndependentSubGraphs = new ObservableCollection<SubParagraph>()

                {
                new SubParagraph
                {
                    Header = "Feature Schedule Sub",
                    Content = "Sub Para only Sub",
                    Type = NodeItemType.SubParagraph
                },
                new SubParagraph
                {
                    Header = "Feature Schedule Sub 2",
                    Content = "The short answer is yes, every implementing type will have to create its own backing variable. This is because an interface is analogous to a contract. All it can do is specify particular publicly accessible pieces of code that an implementing type must make available; it cannot contain any code itself.",
                    Type = NodeItemType.SubParagraph
                },
                new SubParagraph
                {
                    Header = "Feature Schedule Sub 2",
                    Content = "Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                    Type = NodeItemType.SubParagraph
                },
                };
        }

        #endregion
    }
}
