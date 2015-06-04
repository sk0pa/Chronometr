using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Chronometer.Pages
{
    public sealed partial class AddActivity : Page
    {
        ColorPicker colorPicker;

        public AddActivity()
        {
            this.InitializeComponent();

            colorPicker = new ColorPicker();
            this.DataContext = colorPicker;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Length > 0)
            {
                Data.AddCategory(NameTextBox.Text, colorPicker.Color);
            }

            (Window.Current.Content as Frame).GoBack();
        }
    }
}
