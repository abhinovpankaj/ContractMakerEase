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
        private Paragraph draggedPara;

        private SubParagraph draggedSubPara;

        private void SourceTCs_DragItemsStarting(TreeView sender, TreeViewDragItemsStartingEventArgs e)
        {
            string items = string.Empty;
            foreach (var item in e.Items)
            {
                if (item is Project)
                {
                    draggedProject = item as Project;
                    items = string.Join(",", e.Items.Cast<Project>().Select(i => i.TnCId));
                    e.Data.SetText(items.ToString());
                    
                }
                else if (item is Paragraph)
                {
                    draggedPara = item as Paragraph;
                    items = string.Join(",", e.Items.Cast<Paragraph>().Select(i => i.TnCId));
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

                if (treeViewItemsSource != null)
                {
                    //foreach (var itemId in itemIdsToMove)
                    //{
                    //    var itemToMove = this..First(i => i.Id.ToString() == itemId);

                    //    listViewItemsSource.Add(itemToMove);
                    //    this.MyItems.Remove(itemToMove);
                    //}
                    if (draggedProject!=null)
                    {
                        treeViewItemsSource.Add(draggedProject);

                        string str = await ContractDocCreator.CreateNewDoc(treeViewItemsSource.ToList());
                        //// Load the file into the Document property of the RichEditBox.
                        textEditor.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf,str);
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


        }

        private void SourceTCs_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as TermAndCondition;
            TCTextBlock.Text = node.Content;
            myStoryboard.Begin();
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
    }
}
