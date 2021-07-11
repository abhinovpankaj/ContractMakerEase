using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DragAndDropSampleManaged.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace DragAndDropSampleManaged
{
    public class ContractDocCreator
    {
        private const JustificationValues ParaAlignment= JustificationValues.Distribute;

        private static List<Project> _projects;
        private static bool applyJustification;
        public static async Task<string> CreateNewDoc(List<Project> projects)
        {
            _projects = projects;
            string str;
            using (MemoryStream mem = new MemoryStream())
            {
                // Create Document
                using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document,true))
                {
                    // Add a main document part. 
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    Body docBody = new Body();

                    foreach (var item in projects)
                    {
                        var para = getProjectParagraph(item);
                        docBody.Append(para);
                    }
                    
                }
                mem.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(mem);
                str= Encoding.ASCII.GetString(mem.ToArray());
                str = await reader.ReadToEndAsync();

            }
            return str;
        }

        public static Paragraph getProjectParagraph( Models.Project pr)
        {
            Paragraph p = new Paragraph();
            if (applyJustification)
            {
                ParagraphProperties pp = new ParagraphProperties();
                pp.Justification = new Justification() { Val = ParaAlignment };
                // Add paragraph properties to your paragraph
                p.Append(pp);
            }
            // Paragraph properties
            SpacingBetweenLines sblUl = new SpacingBetweenLines() { After = "0" };  // Get rid of space between bullets  
            Indentation iUl = new Indentation() { Left = "indentation" , Hanging = "360" };  // correct indentation  
            NumberingProperties npUl = new NumberingProperties(
                new NumberingLevelReference() { Val = 1 },
                new NumberingId() { Val = 2 }
            );
            ParagraphProperties ppUnordered = new ParagraphProperties(npUl, sblUl, iUl);
            ppUnordered.ParagraphStyleId = new ParagraphStyleId() { Val = "ListParagraph" };
            
            //Project Header
            Run r2 = new Run();
            RunProperties rp2 = new RunProperties();
            rp2.Bold = new Bold();
            // Always add properties first
            r2.AppendChild(rp2);
            Text t2 = new Text(pr.Header) { Space = SpaceProcessingModeValues.Preserve };
            r2.AppendChild(t2);
            r2.AppendChild(new Break());
            //Project Description
            Run r3 = new Run();
            
            Text t3 = new Text(pr.Content) { Space = SpaceProcessingModeValues.Preserve };
            r3.Append(t3);
            
            //p.ParagraphProperties = new ParagraphProperties(ppUnordered.OuterXml);
            p.AppendChild(r2);
            p.AppendChild(r3);

            return p;
        }
        private static void getSubParaParagraph(Models.SubParagraph pr)
        {
            Paragraph p = new Paragraph();
            if (applyJustification)
            {
                ParagraphProperties pp = new ParagraphProperties();
                pp.Justification = new Justification() { Val = ParaAlignment };
                // Add paragraph properties to your paragraph
                p.Append(pp);
            }
            //Project Header
            Run r2 = new Run();
            RunProperties rp2 = new RunProperties();
            rp2.Bold = new Bold();
            // Always add properties first
            r2.Append(rp2);
            Text t2 = new Text(pr.Header) { Space = SpaceProcessingModeValues.Preserve };
            r2.Append(t2);

            //Project Description
            Run r3 = new Run();

            Text t3 = new Text(pr.Content) { Space = SpaceProcessingModeValues.Preserve };
            r3.Append(t3);
            p.Append(r2);
            p.Append(r3);

        }

        private static void getParaParagraph()
        {

        }

        private static void AddHeaders(MainDocumentPart mainPart)
        {
            // Heading 1
            StyleRunProperties styleRunPropertiesH1 = new StyleRunProperties();
            Color color1 = new Color() { Val = "2F5496" };
            // Specify a 16 point size. 16x2 because it’s half-point size
            DocumentFormat.OpenXml.Wordprocessing.FontSize fontSize1 = new DocumentFormat.OpenXml.Wordprocessing.FontSize();
            fontSize1.Val = new StringValue("16");

            styleRunPropertiesH1.Append(color1);
            styleRunPropertiesH1.Append(fontSize1);
            // Check above at the begining of the word creation to check where mainPart come from
            AddStyleToDoc(mainPart, "heading1", "Project", styleRunPropertiesH1);

            // Heading 2
            StyleRunProperties styleRunPropertiesH2 = new StyleRunProperties();
            Color color2 = new Color() { Val = "2F5496" };
            // Specify a 13 point size. 16x2 because it’s half-point size
            DocumentFormat.OpenXml.Wordprocessing.FontSize fontSize2 = new DocumentFormat.OpenXml.Wordprocessing.FontSize();
            fontSize2.Val = new StringValue("14");

            styleRunPropertiesH1.Append(color1);
            styleRunPropertiesH1.Append(fontSize1);
            AddStyleToDoc(mainPart, "heading2", "Paragraph", styleRunPropertiesH1);

            // Heading 3
            StyleRunProperties styleRunPropertiesH3 = new StyleRunProperties();
            Color color3 = new Color() { Val = "2F5496" };
            // Specify a 13 point size. 16x2 because it’s half-point size
            DocumentFormat.OpenXml.Wordprocessing.FontSize fontSize3 = new DocumentFormat.OpenXml.Wordprocessing.FontSize();
            fontSize3.Val = new StringValue("12");

            styleRunPropertiesH1.Append(color1);
            styleRunPropertiesH1.Append(fontSize1);
            AddStyleToDoc(mainPart, "heading3", "SubParagraph", styleRunPropertiesH1);
        }
        #region Styles
        public static void AddStyleToDoc(MainDocumentPart mainPart, string styleid, string stylename, StyleRunProperties styleRunProperties)
        {

            // Get the Styles part for this document.
            StyleDefinitionsPart part =
                mainPart.StyleDefinitionsPart;

            // If the Styles part does not exist, add it and then add the style.
            if (part == null)
            {
                part = AddStylesPartToPackage(mainPart);
                AddNewStyle(part, styleid, stylename, styleRunProperties);
            }
            else
            {
                // If the style is not in the document, add it.
                if (IsStyleIdInDocument(mainPart, styleid) != true)
                {
                    // No match on styleid, so let's try style name.
                    string styleidFromName = GetStyleIdFromStyleName(mainPart, stylename);
                    if (styleidFromName == null)
                    {
                        AddNewStyle(part, styleid, stylename, styleRunProperties);
                    }
                    else
                        styleid = styleidFromName;
                }
            }

        }
        private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename, StyleRunProperties styleRunProperties)
        {
            // Get access to the root element of the styles part.
            DocumentFormat.OpenXml.Wordprocessing.Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            DocumentFormat.OpenXml.Wordprocessing.Style style = new DocumentFormat.OpenXml.Wordprocessing.Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            style.Append(new StyleName() { Val = stylename });
            style.Append(new BasedOn() { Val = "Normal" });
            style.Append(new NextParagraphStyle() { Val = "Normal" });
            style.Append(new UIPriority() { Val = 900 });

            // Create the StyleRunProperties object and specify some of the run properties.


            // Add the run properties to the style.
            // --- Here we use the OuterXml. If you are using the same var twice, you will get an error. So to be sure just insert the xml and you will get through the error.
            style.Append(styleRunProperties);

            // Add the style to the styles part.
            styles.Append(style);
        }

        // Apply a style to a paragraph.
        public static void ApplyStyleToParagraph(WordprocessingDocument doc,
            string styleid, string stylename, Paragraph p)
        {
            // If the paragraph has no ParagraphProperties object, create one.
            if (p.Elements<ParagraphProperties>().Count() == 0)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get the paragraph properties element of the paragraph.
            ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();

            // Get the Styles part for this document.
            StyleDefinitionsPart part =
                doc.MainDocumentPart.StyleDefinitionsPart;

            // If the Styles part does not exist, add it and then add the style.
            if (part == null)
            {
                part = AddStylesPartToPackage(doc);
                AddNewStyle(part, styleid, stylename);
            }
            else
            {
                // If the style is not in the document, add it.
                if (IsStyleIdInDocument(doc, styleid) != true)
                {
                    // No match on styleid, so let's try style name.
                    string styleidFromName = GetStyleIdFromStyleName(doc, stylename);
                    if (styleidFromName == null)
                    {
                        AddNewStyle(part, styleid, stylename);
                    }
                    else
                        styleid = styleidFromName;
                }
            }

            // Set the style of the paragraph.
            pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
        }
        // Return true if the style id is in the document, false otherwise.
        public static bool IsStyleIdInDocument(WordprocessingDocument doc,
            string styleid)
        {
            // Get access to the Styles element for this document.
            Styles s = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

            // Check that there are styles and how many.
            int n = s.Elements<Style>().Count();
            if (n == 0)
                return false;

            // Look for a match on styleid.
            Style style = s.Elements<Style>()
                .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style == null)
                return false;

            return true;
        }
        // Return true if the style id is in the document, false otherwise.
        public static bool IsStyleIdInDocument(MainDocumentPart doc,
            string styleid)
        {
            // Get access to the Styles element for this document.
            Styles s = doc.StyleDefinitionsPart.Styles;

            // Check that there are styles and how many.
            int n = s.Elements<Style>().Count();
            if (n == 0)
                return false;

            // Look for a match on styleid.
            Style style = s.Elements<Style>()
                .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style == null)
                return false;

            return true;
        }

        public static string GetStyleIdFromStyleName(MainDocumentPart doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                .Where(s => s.Val.Value.Equals(styleName) &&
                    (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }
        public static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                .Where(s => s.Val.Value.Equals(styleName) &&
                    (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }
        // Create a new style with the specified styleid and stylename and add it to the specified style definitions part.
        
        private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart,
            string styleid, string stylename)
        {
            // Get access to the root element of the styles part.
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            StyleName styleName1 = new StyleName() { Val = stylename };
            BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            Bold bold1 = new Bold();
            Color color1 = new Color() { ThemeColor = ThemeColorValues.Accent2 };
            RunFonts font1 = new RunFonts() { Ascii = "Lucida Console" };
            Italic italic1 = new Italic();
            // Specify a 12 point size.
            FontSize fontSize1 = new FontSize() { Val = "24" };
            styleRunProperties1.Append(bold1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(font1);
            styleRunProperties1.Append(fontSize1);
            styleRunProperties1.Append(italic1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);

            // Add the style to the styles part.
            styles.Append(style);
        }

        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        public static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            StyleDefinitionsPart part;
            part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();
            root.Save(part);
            return part;
        }
        public static StyleDefinitionsPart AddStylesPartToPackage(MainDocumentPart doc)
        {
            StyleDefinitionsPart part;
            part = doc.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();
            root.Save(part);
            return part;
        }
        #endregion
    }
}
