using DragAndDropSampleManaged.Models;
using GalaSoft.MvvmLight;
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

        public ObservableCollection<Project> Projects { get; set; }

        public  ObservableCollection<SubParagraph> IndependentSubGraphs { get; set; }

        public CreateContractViewModel()
        {
            GetData();
        }
        private void GetData()
        {
            Projects = new ObservableCollection<Project>();
            Project Project1 = new Project()
            {
                Header = "Weather Wear",
                Type = NodeItemType.Project,
                Paragraphs =new List<Paragraph>()
                {
                    new Paragraph()
                    {
                        Header = "Functional Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating a random string in C# uses the same concepts that are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>()
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            new SubParagraph()
                            {
                                Header = "Sub Para spec 2 ",
                                Content = "  2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            new SubParagraph()
                            {
                                Header = "Sub Para spec 3",
                                Content = "3 : Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            }
                        }
                    },
                    new Paragraph()
                    {
                        Header = "Visual Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>()
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec1",
                                Content = "1: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },

                            new SubParagraph()
                            {
                                Header = "Sub Para spec 3",
                                Content = "3 : Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            }
                        }
                    },
                    new Paragraph()
                    {
                        Header = "Other Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>()
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
                        Header = "Feature Schedule Sub",
                        Content = "No Sub Para only Para",
                        Type = NodeItemType.Paragraph,
                    },
                    new Paragraph()
                    {
                        Header = "Feature Schedule Sub 2",
                        Content = "Both the projects above take recognize the disconnected entities when it's returned to the server, detect and save the changes, and return to the client affected data.",
                        Type = NodeItemType.Paragraph,
                    },

                },
                SubParagraphs =new List<SubParagraph>()
                {
                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub",
                        Content ="Sub Para only Sub",
                        Type = NodeItemType.SubParagraph
                    },
                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub 2",
                        Content ="The short answer is yes, every implementing type will have to create its own backing variable. This is because an interface is analogous to a contract. All it can do is specify particular publicly accessible pieces of code that an implementing type must make available; it cannot contain any code itself.",
                        Type = NodeItemType.SubParagraph
                    },
                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub 2",
                        Content="Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                        Type = NodeItemType.SubParagraph
                    },
                }
            };

            Project Project2 = new Project()
            {
                Header = "Pedestrian",
                Type = NodeItemType.Project,
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph()
                    {
                        Header = "Functional Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating a random string in C# uses the same concepts that are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            new SubParagraph()
                            {
                                Header = "Sub Para spec 2 ",
                                Content = "  2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },

                        }
                    },
                    new Paragraph()
                    {
                        Header = "Visual Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec1",
                                Content = "1: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },


                        }
                    },
                    new Paragraph()
                    {
                        Header = "Other Specifications",
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
                        Header = "Feature Schedule Sub",
                        Content = "No Sub Para only Para",
                        Type = NodeItemType.Paragraph,
                    },


                },
                SubParagraphs = new List<SubParagraph>
                {
                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub",
                        Content ="Sub Para only Sub",
                        Type = NodeItemType.SubParagraph
                    },

                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub 2",
                        Content="Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                        Type = NodeItemType.SubParagraph
                    },
                }
            };

            Project Project3 = new Project()
            {
                Header = "Dexotex",
                Type = NodeItemType.Project,
                Paragraphs = new List<Paragraph>
                {
                    new Paragraph()
                    {
                        Header = "Functional Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating a random string in C# uses the same concepts that are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec",
                                Content = "Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },
                            new SubParagraph()
                            {
                                Header = "Sub Para spec 2 ",
                                Content = "  2: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },

                        }
                    },
                    new Paragraph()
                    {
                        Header = "Visual Specifications",
                        Type = NodeItemType.Paragraph,
                        Content = "Generating are used to generate a random number in C#. The StringBuilder class and the NextDouble() method in the Random class are used to generate a random string.",
                        SubParagraphs = new List<SubParagraph>
                        {
                            new SubParagraph()
                            {
                                Header = "Sub Para spec1",
                                Content = "1: Use the Random.NextDouble() method to generate a float (flt) that is between 0.00.0 and 1.01.0 and is inclusive Multiply flt with 2525 and take the Floor of the result. This will return an integer(shift) that is between 00 and 2525 and is inclusive.",
                                Type = NodeItemType.SubParagraph,
                            },


                        }
                    },
                    
                    new Paragraph()
                    {
                        Header = "Feature Schedule Sub",
                        Content = "No Sub Para only Para",
                        Type = NodeItemType.Paragraph,
                    },


                },
                SubParagraphs = new List<SubParagraph>
                {
                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub",
                        Content ="Sub Para only Sub",
                        Type = NodeItemType.SubParagraph
                    },

                    new SubParagraph
                    {
                        Header = "Feature Schedule Sub 2",
                        Content="Here's my update method. Note that in a detached scenario, sometimes your code will read data and then update it, so it's not always detached.",
                        Type = NodeItemType.SubParagraph
                    },
                }
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
    }
}
