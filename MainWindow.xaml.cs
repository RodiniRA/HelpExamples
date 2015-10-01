using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using NeedHelpExamples.ViewModels;
using MahApps.Metro;

namespace NeedHelpExamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainViewModel _vm;
        public MainWindow()
        {
            _vm = new MainViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        public void ShowFileFlyout(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ApplyInitialAccent(object sender, RoutedEventArgs e)
        {
            // add custom accent and theme resource dictionaries
            ThemeManager.AddAccent("DefaultBlue", new Uri("Styles\\Accents\\Blue.xaml", UriKind.Relative));

            // get the theme from the current application
            var theme = ThemeManager.DetectAppStyle(Application.Current);

            // now use the custom accent
            ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent("DefaultBlue"),
                                    theme.Item1);
        }
    }
}
