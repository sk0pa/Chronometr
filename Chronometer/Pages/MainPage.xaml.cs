using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Chronometer.Pages
{
    public sealed partial class MainPage : Page
    {
        bool editing = false;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var category  =e.ClickedItem as Category;
            bool add = category.Icon == "+";
            if (add)
            {
                (Window.Current.Content as Frame).Navigate(typeof(Pages.AddActivity));
            }
            else
            {
                if (category.IsStarted)
                {
                    category.IsStarted = false;
                    var now = DateTime.Now;
                    new MessageDialog(string.Format("Duration: {0}", now-category.Started)).ShowAsync();
                    Data.AddActivity(category.Id, category.Started, now);
                    Data.UpdateCategory(category);
                }
                else
                {
                    category.IsStarted = true;
                    category.Started = DateTime.Now;
                    Data.UpdateCategory(category);
                }
            }


            if (!add && editing)
            {
                Data.DeleteCategory(e.ClickedItem as Category);
            }
        }

        private void CategoriesGrid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started || e.HoldingState == Windows.UI.Input.HoldingState.Canceled)
            {
                editing = !editing;
                if (editing)
                {
                    CategoriesGrid.Background.Opacity = 0.6;
                }
                else
                {
                    CategoriesGrid.Background.Opacity = 0.0;
                }
            }
        }
    }
}
