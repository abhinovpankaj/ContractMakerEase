using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DragAndDropSampleManaged
{
    public class GridLines : DependencyObject
    {
        public static readonly DependencyProperty AreVisibleProperty =
            DependencyProperty.RegisterAttached(
                "AreVisible",
                typeof(Boolean),
                typeof(GridLines),
                new PropertyMetadata(false, OnPropertyChanged)
            );

        private static Grid owner;

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid && e.NewValue is bool showLines)
            {
                SetLineVisibility(grid, showLines);
            }
        }

        // Change these 3 values as per your preferences
        private static Windows.UI.Color LineColor = Colors.Blue;
        private const int LineThickness = 1;
        private const bool ShowOuterBorder = true;

        private const string GridLineTag = "GRIDLINE";

        protected static void SetLineVisibility(Grid grid, bool showLines)
        {
            if (showLines)
            {
                var cols = grid.ColumnDefinitions.Count;
                var rows = grid.RowDefinitions.Count;

                for (int col = 0; col < cols; col++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        var thickness = ShowOuterBorder
                            ? new Thickness(col == 0 ? LineThickness : 0, row == 0 ? LineThickness : 0, LineThickness, LineThickness)
                            : new Thickness(0, 0, col < grid.ColumnDefinitions.Count - 1 ? LineThickness : 0, row < grid.RowDefinitions.Count - 1 ? LineThickness : 0);

                        var border = new Border();
                        border.BorderBrush = new SolidColorBrush(LineColor);
                        border.BorderThickness = thickness;
                        border.Tag = GridLineTag;

                        Grid.SetColumn(border, col);
                        Grid.SetRow(border, row);

                        grid.Children.Add(border);
                    }
                }
            }
            else
            {
                foreach (var gridChild in grid.Children.Reverse())
                {
                    if (gridChild is Border border)
                    {
                        if (border.Tag.ToString() == GridLineTag)
                        {
                            grid.Children.Remove(border);
                        }
                    }
                }
            }
        }

        public static void SetAreVisible(Grid grid, Boolean value)
        {
            grid.SetValue(AreVisibleProperty, value);

            grid.Loaded += (sender, args) => { SetLineVisibility(grid, value); };
        }

        public static Boolean GetAreVisible(Grid grid)
        {
            return (Boolean)grid.GetValue(AreVisibleProperty);
        }
    }
}
