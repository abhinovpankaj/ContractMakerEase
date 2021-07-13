//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using System;
using Windows.Graphics.Imaging;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;
using System.Collections.ObjectModel;
using DragAndDropSampleManaged.ViewModels;
using System.Text;
using DragAndDropSampleManaged.Models;
using System.Linq;
using Windows.Storage.Streams;
using Windows.Storage;
using System.IO;
using Windows.UI.Xaml.Media;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.InteropServices.WindowsRuntime;
using DocumentFormat.OpenXml.Drawing.Charts;
using SautinSoft.Document;
using Microsoft.Toolkit.Uwp.UI.Controls.TextToolbarButtons;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Data;

namespace DragAndDropSampleManaged
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class CreateContract : Page
    {
        public CreateContract()
        {
            this.InitializeComponent();
            this.DataContext = new CreateContractViewModel();
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
            //ToolbarButton btnX = Toolbar.GetDefaultButton(ButtonType.Headers);
            //btnX.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //Toolbar.CustomButtons.Add(new ToolbarButton
            //{
            //    Name = "CustomButton",
            //    Icon = new SymbolIcon(Windows.UI.Xaml.Controls.Symbol.ReportHacked),
            //    Position = 1,
            //    Activation = x => System.Diagnostics.Debug.WriteLine($"{x.Name} Activated"),
            //    ShortcutKey = Windows.System.VirtualKey.S
            //});
            //Toolbar.CustomButtons.Add(new ToolbarSeparator { Position = 2 });

            
        }
        private Grid GetContractGrid(double top,double left,object tag)
        {
            string textType;
            //var binding = new Binding()
            //{
            //    Path = new PropertyPath("TnCId"),
            //    Source = (tag as TermAndCondition).TnCId
            //}; 
            
            Grid grid = new Grid();
            
            grid.Height = 60;
            grid.Width = 80;
            Rectangle rect = new Rectangle();
            
            rect.Height = 55;
            rect.Width = 80;
            if (tag is Project)
            {
                textType = "M";              
                //grid.Margin= new Windows.UI.Xaml.Thickness(0, 25, 0, 0);
            }
            else if (tag is Models.Paragraph)
            {
                textType = "P";
            }
            else
            {
                textType = "S";
            }
            rect.Fill = new SolidColorBrush(Colors.Red);
            rect.Stroke = new SolidColorBrush(Colors.White);
            rect.StrokeThickness = 2;
            rect.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            grid.Children.Add(rect);
            
            
            //TextBlock textBlock = new TextBlock();
            //textBlock.Text = (tag as TermAndCondition).Content;
            //textBlock.FontSize = 7;
            //textBlock.TextWrapping = TextWrapping.Wrap;
            //textBlock.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            //textBlock.Margin = new Windows.UI.Xaml.Thickness(2, 15, 0, 5);
            //grid.Children.Add(textBlock);

            TextBlock seqText = new TextBlock();
            seqText.Text = (tag as TermAndCondition).ParentId.ToString();           
            seqText.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            seqText.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
            grid.Children.Add(seqText);

            TextBlock typeText = new TextBlock();
            typeText.Text = textType;
            
            var fWeight = new Windows.UI.Text.FontWeight();
            fWeight.Weight = 700;
            typeText.Width = 20;
            typeText.Height = 20;
            typeText.FontWeight = fWeight;
            typeText.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            typeText.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            typeText.Margin = new Windows.UI.Xaml.Thickness(8,2,8,2);
            grid.Children.Add(typeText);

            Ellipse elipse = new Ellipse();
            elipse.Width = 20; elipse.Height = 20; elipse.Stroke = new SolidColorBrush(Colors.Black);
            elipse.Margin = new Windows.UI.Xaml.Thickness(2);
            elipse.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            elipse.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            
            grid.Children.Add(elipse);

            //Button btn = new Button();
            //btn.Padding = new Windows.UI.Xaml.Thickness(0);
            //btn.Height = 20 ;
            //btn.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            //btn.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
            //btn.Background = new SolidColorBrush(Colors.Transparent);
            //btn.Margin = new Windows.UI.Xaml.Thickness(3);

            //TextBlock actionText = new TextBlock();
            //actionText.Text = "+";
            //actionText.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            //btn.Content = actionText;

            Line L1 = new Line();
            L1.X1 = 27; L1.Y1 = 55; L1.X2 = 27; L1.Y2 = 60; L1.Stroke = new SolidColorBrush(Colors.Red);

            Line L2 = new Line();
            L2.X1 = 54; L2.Y1 = 55; L2.X2 = 54; L2.Y2 = 60; L2.Stroke = new SolidColorBrush(Colors.Red);
            grid.Children.Add(L1);
            grid.Children.Add(L2);
            

            Canvas.SetLeft(grid, left);
            Canvas.SetTop(grid, top);
            grid.PointerPressed += PolyLine2_PointerPressed;
            grid.CanDrag = true;
            grid.Tag = tag;
            grid.DoubleTapped += Grid_DoubleTapped;
            return grid;

        }
        //TODO, logic is not full proof.
        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //TODO, logic is not full proof.

            List<TermAndCondition> observedList = new List<TermAndCondition>();
            List<TermAndCondition> observedList1 = new List<TermAndCondition>();
            var proj = dragObject as Grid;
            
            int itemCount = 0;
            Visibility visibility=Visibility.Collapsed;
            if (proj != null)
            {
                TermAndCondition tc = proj.Tag as TermAndCondition;
                visibility = !tc.IsCollapsed ? Visibility.Collapsed : Visibility.Visible;
                tc.IsCollapsed = !tc.IsCollapsed;

                for (int i = 0; i < TcTargetlayout.Children.Count; i++)
                {

                    var item = TcTargetlayout.Children[i] as Grid;
                    if (item is Grid)
                    {
                        var left = Canvas.GetLeft(item as UIElement);
                        var top = Canvas.GetTop(item as UIElement);
                        if (absoluteLoc.Y < top)
                        {
                            var child = item.Tag as TermAndCondition;
                            if (tc.TnCId == child.ParentId)
                            {
                                item.Visibility = visibility;                                
                                itemCount++;
                                observedList.Add(child);
                            }
                            foreach (var condition in observedList)
                            {
                                if (condition.TnCId==child.ParentId)
                                {
                                    item.Visibility = visibility;
                                    itemCount++;
                                    observedList1.Add(child);
                                }
                            }
                            foreach (var condition1 in observedList1)
                            {
                                if (condition1.TnCId == child.ParentId)
                                {
                                    item.Visibility = visibility;
                                    itemCount++;
                                    
                                }
                            }
                        }

                    }
                }

                for (int i = 0; i < TcTargetlayout.Children.Count; i++)
                {

                    var item = TcTargetlayout.Children[i];
                    if (item is Grid)
                    {
                        var left = Canvas.GetLeft(item as UIElement);
                        var top = Canvas.GetTop(item as UIElement);
                        if (visibility == Visibility.Collapsed)
                        {
                            if (absoluteLoc.X - left >= 0 && absoluteLoc.X - left < 80 && top - absoluteLoc.Y > itemCount * 55)
                            {
                                Canvas.SetTop(item, top - itemCount * 55);
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                        else
                        {
                            if (absoluteLoc.X - left >=0 && absoluteLoc.X - left < 75 && top - absoluteLoc.Y > 0)
                            {
                                Canvas.SetTop(item, top + itemCount * 55);
                                System.Threading.Thread.Sleep(200);
                            }

                        }

                    }
                }
            }

        }


        private Rectangle GetRectangle(double top, double left, object tag)
        {
            var rect1 = new Rectangle();
            rect1.Height = 55;
            rect1.Width = 80;
            Canvas.SetLeft(rect1, left);
            Canvas.SetTop(rect1, top);
            rect1.StrokeLineJoin = PenLineJoin.Round;
            rect1.Fill = new SolidColorBrush(Colors.Green);
            rect1.Stroke = new SolidColorBrush(Colors.White);
            rect1.StrokeThickness = 2;
            Canvas.SetZIndex(rect1, 0);
            rect1.PointerPressed += PolyLine2_PointerPressed;
            rect1.CanDrag = true;
            rect1.Tag = tag;
            return rect1;
        }

        /// <summary>
        /// Start of the Drag and Drop operation: we set some content and change the DragUI
        /// depending on the selected options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        //private async void SourceGrid_DragStarting(Windows.UI.Xaml.UIElement sender, Windows.UI.Xaml.DragStartingEventArgs args)
        //{
        //    args.Data.SetText(SourceTextBox.Text);
        //    if ((bool)DataPackageRB.IsChecked)
        //    {
        //        // Standard icon will be used as the DragUIContent
        //        args.DragUI.SetContentFromDataPackage();
        //    }
        //    else if ((bool)CustomContentRB.IsChecked)
        //    {
        //        // Generate a bitmap with only the TextBox
        //        // We need to take the deferral as the rendering won't be completed synchronously
        //        var deferral = args.GetDeferral();
        //        var rtb = new RenderTargetBitmap();
        //        await rtb.RenderAsync(SourceTextBox);
        //        var buffer = await rtb.GetPixelsAsync();
        //        var bitmap = SoftwareBitmap.CreateCopyFromBuffer(buffer,
        //            BitmapPixelFormat.Bgra8,
        //            rtb.PixelWidth,
        //            rtb.PixelHeight,
        //            BitmapAlphaMode.Premultiplied);
        //        args.DragUI.SetContentFromSoftwareBitmap(bitmap);
        //        deferral.Complete();
        //    }
        //    // else just show the dragged UIElement
        //}

        /// <summary>
        /// Entering the Target, we'll change its background and optionally change the DragUI as well
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void TargetTextBox_DragEnter(object sender, Windows.UI.Xaml.DragEventArgs e)
        //{
        //    /// Change the background of the target
        //    VisualStateManager.GoToState(this, "Inside", true);
        //    bool hasText = e.DataView.Contains(StandardDataFormats.Text);
        //    e.AcceptedOperation = hasText ? DataPackageOperation.Copy : DataPackageOperation.None;
        //    if (hasText)
        //    {
        //        e.DragUIOverride.Caption = "Drop here to insert text";
        //        // Now customize the content
        //        if ((bool)HideRB.IsChecked)
        //        {
        //            e.DragUIOverride.IsGlyphVisible = false;
        //            e.DragUIOverride.IsContentVisible = false;
        //        }
        //        else if ((bool)CustomRB.IsChecked)
        //        {
        //            var bitmap = new BitmapImage(new Uri("ms-appx:///Assets/dropcursor.png", UriKind.RelativeOrAbsolute));
        //            // Anchor will define how to position the image relative to the pointer
        //            Point anchor = new Point(0,52); // lower left corner of the image
        //            e.DragUIOverride.SetContentFromBitmapImage(bitmap, anchor);
        //            e.DragUIOverride.IsGlyphVisible = false;
        //            e.DragUIOverride.IsCaptionVisible = false;
        //        }
        //        // else keep the DragUI Content set by the source
        //    }
        //}

        /// <summary>
        /// DragLeave: Restore previous background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetTextBox_DragLeave(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            VisualStateManager.GoToState(this, "Outside", true);
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private Project draggedProject;
        private Models.Paragraph draggedPara;

        private Models.SubParagraph draggedSubPara;

        private void resetDraggedData()
        {
            draggedProject = null;
            draggedPara = null;
            draggedSubPara = null;
        }
        private void SourceTCs_DragItemsStarting(TreeView sender, TreeViewDragItemsStartingEventArgs e)
        {
            resetDraggedData();
            string items = string.Empty;
            foreach (var item in e.Items)
            {
                if (item is Project)
                {
                    draggedProject = item as Project;
                    items = string.Join(",", e.Items.Cast<Project>().Select(i => i.TnCId));
                    e.Data.SetText(items.ToString());
                    
                }
                else if (item is Models.Paragraph)
                {
                    draggedPara = item as Models.Paragraph;
                    items = string.Join(",", e.Items.Cast<Models.Paragraph>().Select(i => i.TnCId));
                    e.Data.SetText(items.ToString());
                }
                else if (item is SubParagraph)
                {
                    draggedSubPara = item as SubParagraph;
                    items = string.Join(",", e.Items.Cast<SubParagraph>().Select(i => i.TnCId));
                    e.Data.SetText(items.ToString());
                }
            }
            
            e.Data.RequestedOperation = DataPackageOperation.Move;
            //Windows.Storage.Streams.RandomAccessStreamReference img =  Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/dropcursor.png", UriKind.RelativeOrAbsolute));

            //e.Data.SetBitmap(img);
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }
       
        
        private void TcTargetlayout_DragOver(object sender, DragEventArgs e)
        {

            e.AcceptedOperation = (e.DataView.Contains(StandardDataFormats.Text)) ? DataPackageOperation.Move : DataPackageOperation.None;
            TreeViewItem treeViewItem = FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);
            if (treeViewItem != null)
            {
                treeViewItem.Background = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
            }
        }

        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            // Search the VisualTree for specified type
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
        private async void TcTargetlayout_Drop(object sender, DragEventArgs e)
        {
            //var droppedProject = await e.DataView.GetDataAsync("Project");
            //var droppedPara = await e.DataView.GetDataAsync("Paragraph");
            //var droppedSubPara = await e.DataView.GetDataAsync("SubParagraph");

            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var id = await e.DataView.GetTextAsync();
                var itemIdsToMove = id.Split(',');

                var destinationTreeView = sender as TreeView;
                var treeViewItemsSource = destinationTreeView?.ItemsSource as ObservableCollection<Project>;

                if (draggedProject!=null)
                {
                    //treeViewItemsSource.Add(draggedProject);
                    //CreateDoc(treeViewItemsSource.ToList());
                    //Add rectangles 
                    double top=25, left = 40;
                    double height = 55;
                    double width = 100;
                    double indentation = 25;

                    var projRect = GetContractGrid(top, left, draggedProject);  // GetRectangle(top, left,draggedProject);

                    TcTargetlayout.Children.Add(projRect);
                    
                    left = 40;
                    top = 80;
                    foreach (var para in draggedProject.Paragraphs)
                    {
                        left = 68;
                        var paraRect = GetContractGrid(top,left,para);                           
                        top = top + 55;                               
                        TcTargetlayout.Children.Add(paraRect);
                                
                        if (para.SubParagraphs!=null)
                        {
                            foreach (var subpara in para.SubParagraphs)
                            {
                                left = 95;
                                var subParaRect = GetContractGrid(top, left,subpara);
                                top = top + 55;                                
                                //polyLine.Visibility = Visibility.Collapsed;
                                TcTargetlayout.Children.Add(subParaRect);
                                        
                            }
                        }
                                
                    }

                }
                if (draggedPara!=null)
                {
                    if (treeViewItemsSource.Count>0)
                    {
                            
                    }
                        
                }
                if (draggedSubPara!=null)
                {

                }
                
            }


        }
        UIElement dragObject=null;
        Point offset;
        Pointer draggedObjectPointer;
        Point absoluteLoc;

        #region word processing
        private MemoryStream _Ms;
        private WordprocessingDocument _Wpd;


        public void SaveToFile(string fullFileName)
        {
            _Wpd.MainDocumentPart.Document.Save();

            _Wpd.Package.Flush();

            _Ms.Position = 0;
            var buf = new byte[_Ms.Length];
            _Ms.Read(buf, 0, buf.Length);

            using (FileStream fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Create))
            {
                fs.Write(buf, 0, buf.Length);
            }
        } 
        public void Doc()
        {
            _Ms = new MemoryStream();
            _Wpd = WordprocessingDocument.Create(_Ms, WordprocessingDocumentType.Document, true);
            _Wpd.AddMainDocumentPart();
            _Wpd.MainDocumentPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            _Wpd.MainDocumentPart.Document.Body = new Body();
            _Wpd.MainDocumentPart.Document.Save();
            _Wpd.Package.Flush();
        }

        private async void CreateDoc(List<Project> projects)
        {
            StorageFolder folder = KnownFolders.PicturesLibrary;
            StorageFile sampleFile = await folder.GetFileAsync("hello.docx");

            Stream randomAccessStream = await sampleFile.OpenStreamForWriteAsync();
            var text = OpenextToWordDocument(randomAccessStream,projects);

            //randomAccessStream.Close();


            randomAccessStream.Seek(0,SeekOrigin.Begin);
            //Load a document.
           DocumentCore dc = DocumentCore.Load(randomAccessStream, new DocxLoadOptions());

            // Save the document to PDF format.
            using (MemoryStream outMs = new MemoryStream())
            {
                dc.Save(outMs, new RtfSaveOptions());

                IRandomAccessStream stream = outMs.AsRandomAccessStream();
                stream.Seek(0);
                textEditor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, stream);
                string str;
                textEditor.TextDocument.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out str);
                str = str.Replace("\\cf1\\b\\fs28 Created by unlicensed version of Document .Net 5.3.6.22!\\cf0\\b0\\fs22\\par\r\n\\cf1\\fs28 The unlicensed version inserts \"trial\" into random places.\\cf0\\fs22\\par\r\n\r\n\\pard\\widctlpar\\sa160\\sl252\\slmult1 {\\fs24{\\field{\\*\\fldinst{HYPERLINK \"https://www.sautinsoft.com/products/document/order.php\"}}{\\fldrslt{\\ul\\cf2\\cf2\\ulc2\\ul\\fs28 This text will disappear after purchasing the license.}}}}\\f0\\fs22\\par\r\n", "");
                textEditor.TextDocument.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, str);
                //ContentDialog errorDialog = new ContentDialog()
                //{
                //    Title = "File open error",
                //    Content = str,
                //    PrimaryButtonText = "Ok"
                //};

                //await errorDialog.ShowAsync();
            }
            randomAccessStream.Close();
            //string x = await FileIO.ReadTextAsync(sampleFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            //byte[] bytes = System.Text.Encoding.Unicode.GetBytes(x);
            //InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
            //await stream.WriteAsync(bytes.AsBuffer());
            //IRandomAccessStream stream2 = stream;
            //textEditor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, stream2);
            ////textEditor.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, text);
        }
        public static string OpenextToWordDocument(Stream stream, List<Project> projects)
        {
            var text = string.Empty;
            // Open a WordprocessingDocument for editing using the stream.
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(stream,true);
            // Assign a reference to the existing document body.
            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
            body.RemoveAllChildren();
            foreach (var item in projects)
            {
                var para = ContractDocCreator.getProjectParagraph(item);
                body.Append(para);
            }
            // Add new text.
            //DocumentFormat.OpenXml.Wordprocessing.Paragraph para = body.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Paragraph());
            //Run run = para.AppendChild(new Run());
            //run.AppendChild(new Text("Create text in body - CreateWordprocessingDocument"));

            text = body.InnerText;
            wordprocessingDocument.Save();
            wordprocessingDocument.Close();
            return text;
        }
        public static string OpenextToRTFDocument(Stream stream, List<Project> projects)
        {
            var text = string.Empty;
            // Open a WordprocessingDocument for editing using the stream.
            RichText rtf = new RichText();
            
            // Assign a reference to the existing document body.
            
            foreach (var item in projects)
            {
                var para = ContractDocCreator.getProjectParagraph(item);
                rtf.AppendChild(para);
                
            }
            // Add new text.
            //DocumentFormat.OpenXml.Wordprocessing.Paragraph para = body.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Paragraph());
            //Run run = para.AppendChild(new Run());
            //run.AppendChild(new Text("Create text in body - CreateWordprocessingDocument"));

            text = rtf.InnerText;
            
            return text;
        }
        public async Task<StorageFile> GetFileAsync(StorageFolder folder, string filename)
        {
            StorageFile file = null;
            if (folder != null)
            {
                file = await folder.GetFileAsync(filename);
            }
            return file;
        }
        Windows.Storage.Streams.IRandomAccessStream randAccStream;
        private async void LoadTextInControl(string fileName)
        {

            
            try
            {
                Windows.Storage.StorageFile file = await GetFileAsync(KnownFolders.DocumentsLibrary, fileName);
                randAccStream =  await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);

                // Load the file into the Document property of the RichEditBox.
                textEditor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                
            }
            catch (Exception)
            {
                ContentDialog errorDialog = new ContentDialog()
                {
                    Title = "File open error",
                    Content = "Sorry, I couldn't open the file.",
                    PrimaryButtonText = "Ok"
                };

                await errorDialog.ShowAsync();
            }
            
        }

        #endregion
        private void SourceTCs_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as TermAndCondition;
            TCTextBlock.Text = node.Content;
            myStoryboard.Begin();
        }

        private void TreeView_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {

            //var treeViewItemsSource = destinationTreeView?.ItemsSource as ObservableCollection<Project>;

            //var node = sender as TermAndCondition;

            
        }
        Point draggedItemLocation;
        private void TcTargetlayout_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (dragObject!=null)
            {
                var position = e.GetCurrentPoint(sender as UIElement);
                                
                Canvas.SetLeft(this.dragObject, position.Position.X - this.offset.X);
                Canvas.SetTop(this.dragObject, position.Position.Y - this.offset.Y);

                draggedItemLocation.X = position.Position.X - this.offset.X;
                draggedItemLocation.Y = position.Position.Y - this.offset.Y;
                int count = 0;
                foreach (var item in connectedRects)
                {
                    
                    Canvas.SetLeft(item, position.Position.X - this.offset.X);
                    Canvas.SetTop(item, position.Position.Y - this.offset.Y + 55*count);
                    count++;
                }
            }
        }

        private TermAndCondition getParent(double  myLeft )
        {
            double nearest = 20000;
            TermAndCondition parent =null;
            for (int j = 0; j < TcTargetlayout.Children.Count; j++)
            {
                var item1 = TcTargetlayout.Children[j];
                if (item1 is Grid)
                {
                    if (Math.Abs(myLeft - Canvas.GetLeft(item1))<3)
                    {
                        if (Canvas.GetTop(item1) < nearest)
                        {
                            nearest = Canvas.GetTop(item1);
                            parent = (item1 as Grid).Tag as TermAndCondition;
                        }

                    }
                }
            }
            return parent;
        }

        private void TcTargetlayout_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (draggedObjectPointer != null)
            {
                //Find other box nearby.
                if (dragObject == null)
                {
                    return;
                }
                TcTargetlayout.ReleasePointerCapture(draggedObjectPointer);
                var position = e.GetCurrentPoint(sender as UIElement);
                // draggedItemLocation.X = position.Position.X-offset.X ;
                // draggedItemLocation.Y = position.Position.Y-offset.Y ;
                if (position.Position.X == offset.X + absoluteLoc.X && position.Position.Y == offset.Y + absoluteLoc.Y)
                {
                    this.dragObject = null;
                    return;
                }

                double sx1 = Convert.ToDouble(dragObject.GetValue(Canvas.LeftProperty));
                double sy1 = Convert.ToDouble(dragObject.GetValue(Canvas.TopProperty));
                for (int i = TcTargetlayout.Children.Count - 1; i >= 0; i--)
                {

                    var item = TcTargetlayout.Children[i];
                    if (item is Grid)
                    {
                        double sx2 = Convert.ToDouble(item.GetValue(Canvas.LeftProperty));
                        double sy2 = Convert.ToDouble(item.GetValue(Canvas.TopProperty));
                        //set parentid
                        var dataObject = (dragObject as Grid).Tag as TermAndCondition;
                        var parntObject = (item as Grid).Tag as TermAndCondition;

                        if (sy1 - sy2 < 70 && sy1 - sy2 > 45)
                        {

                            double myLeft = Canvas.GetLeft(item);
                            double myTop = Canvas.GetTop(item);
                            if (overlap(sx1, sy1, 100, 55, sx2, sy2, 100, 55))
                            {
                                if (sx1 - myLeft > 25 && sx1 - myLeft < 55)
                                {
                                    myLeft = myLeft + 28;
                                    dataObject.ParentId = parntObject.TnCId;

                                }
                                else if (sx1 - myLeft < -25 && sx1 - myLeft > -55)
                                {
                                    myLeft = myLeft - 28;
                                    var parent = getParent(myLeft);
                                    dataObject.ParentId = parent.ParentId;

                                }
                                else if (sx1-myLeft<-55)
                                {
                                    myLeft = myLeft - 55;
                                    var parent = getParent(myLeft);
                                    dataObject.ParentId = parent.ParentId;
                                }
                                else if(sx1 - myLeft > 55)
                                {
                                    myLeft = myLeft + 55;
                                    dataObject.ParentId = parntObject.TnCId;
                                }
                                else
                                {
                                    var parent = getParent(myLeft);
                                    dataObject.ParentId = parent.ParentId;
                                }

                                
                                Canvas.SetLeft(dragObject, myLeft);
                                Canvas.SetTop(dragObject, myTop+55);

                                //set parentid
                                
                                ElementSoundPlayer.Play(ElementSoundKind.Show);
                                int count = 0;
                                if (connectedRects.Count>1)
                                {
                                    foreach (var rect in connectedRects)
                                    {
                                        count++;
                                        Canvas.SetLeft(rect, myLeft);
                                        Canvas.SetTop(rect, myTop + 55 * count);

                                        dataObject = (rect as Grid).Tag as TermAndCondition;
                                        parntObject = (item as Grid).Tag as TermAndCondition;
                                        dataObject.ParentId = parntObject.TnCId;

                                    }
                                }
                                
                                break;
                            }
                            
                        }
                        else
                        {
                            if (sy1 - sy2 < 55 && sy1-sy2>0)
                            {

                            }
                        }
                        
                    }
                }
                
                TcTargetlayout.ReleasePointerCapture(draggedObjectPointer);
            }
            this.dragObject = null;
        }

        private void setColor(Rectangle dragRect)
        {
            if (dragRect.Tag is Project)
                dragRect.Fill = new SolidColorBrush(Colors.Green);
            if (dragRect.Tag is Models.Paragraph)
                dragRect.Fill = new SolidColorBrush(Colors.Red);
            if (dragRect.Tag is Models.SubParagraph)
                dragRect.Fill = new SolidColorBrush(Colors.Blue);


        }
        List<Grid> connectedRects = new List<Grid>();
        private void PolyLine2_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            connectedRects.Clear();
            this.dragObject = sender as UIElement;
            this.offset = e.GetCurrentPoint(this.TcTargetlayout).Position;
            absoluteLoc.X = Canvas.GetLeft(this.dragObject);
            absoluteLoc.Y = Canvas.GetTop(this.dragObject);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.offset.Y -= Canvas.GetTop(this.dragObject);
            draggedObjectPointer = e.Pointer;
            this.TcTargetlayout.CapturePointer(draggedObjectPointer);

            double sx1 = Convert.ToDouble(dragObject.GetValue(Canvas.LeftProperty));
            double sy1 = Convert.ToDouble(dragObject.GetValue(Canvas.TopProperty));
            connectedRects.Add(dragObject as Grid);
            for (int i = 0; i < TcTargetlayout.Children.Count ; i++)
            {

                var item = TcTargetlayout.Children[i];
                if (item is Grid)
                {
                    double sy2 = Convert.ToDouble((item.GetValue(Canvas.TopProperty)));
                    if (sy1<sy2&&sy2 - sy1<60 && sx1 == Convert.ToDouble((item.GetValue(Canvas.LeftProperty))))
                    {
                        connectedRects.Add(item as Grid);
                        sy1 = Convert.ToDouble((item.GetValue(Canvas.TopProperty)));
                    }

                }
            }

        }
        /// <summary>
        /// Drop: restore the background and append the dragged textWindows.UI.Xaml.Controls.TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void TargetTextBox_Drop(object sender, Windows.UI.Xaml.DragEventArgs e)
        //{
        //    VisualStateManager.GoToState(this, "Outside", true);
        //    bool hasText = e.DataView.Contains(StandardDataFormats.Text);
        //    // if the result of the drop is not too important (and a text copy should have no impact on source)
        //    // we don't need to take the deferral and this will complete the operation faster
        //    e.AcceptedOperation = hasText ? DataPackageOperation.Copy : DataPackageOperation.None;
        //    if (hasText)
        //    {
        //        var text = await e.DataView.GetTextAsync();
        //        TargetTextBox.Text += text;
        //    }
        //}

        private bool overlap(double sx1, double sy1, int w1, int h1,
double sx2, double sy2, int w2, int h2)
        {
            double leastx, leasty, mostx, mosty;
            if (sx1 < sx2)
            {
                leastx = sx1;
                mostx = sx2;
            }
            else
            {
                leastx = sx2;
                mostx = sx1;
            }
            if (sy1 < sy2)
            {
                leasty = sy1;
                mosty = sy2;
            }
            else
            {
                leasty = sy2;
                mosty = sy1;
            }
            if ((mostx - leastx) > (w1 + w2)) return false;
            if ((mosty - leasty) > (h1 + h2)) return false;
            return true;

        }
    }
}
