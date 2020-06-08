using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Musify_Desktop_App.Controls
{
    public class MultiSelectedDataGrid : DataGrid
    {
        public MultiSelectedDataGrid()
        {
            SelectionChanged += CustomDataGrid_SelectionChanged;
        }

        void CustomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = SelectedItems;
        }
        #region SelectedItemsList

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
            DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(MultiSelectedDataGrid), new PropertyMetadata(null));

        #endregion
    }
}
