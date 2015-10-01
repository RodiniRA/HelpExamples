using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
//using AmTrustExample.Models;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using NeedHelpExamples.UserControls;
//using AmTrustExample.Command;
//using AmTrustExample.UserControls;
//using AmTrustExample.Extensions;

namespace NeedHelpExamples.ViewModels
{
        public class MainViewModel : INotifyPropertyChanged
        {

            #region "Privates"

            private ObservableCollection<RibbonButton> _testButtons = null;
            private ObservableCollection<RibbonButton> _testQuickAccessButtons = null;

            #endregion

            #region "Properties"

            public ObservableCollection<RibbonButton> TestButtons
            {
                get
                {
                    if (_testButtons == null)
                    {
                        _testButtons = new ObservableCollection<RibbonButton>();
                        for (int i = 0; i < 3; i++)
                        {
                            var b = new RibbonButton();
                            b.Content = new Image { Source = new BitmapImage(new Uri("/NeedHelpExamples;component/Images/testImage.jpg", UriKind.Relative)) };
                            b.ToolTip = "Testing RibbonButton UserControl";

                            b.ContextMenu = new ContextMenu();
                            var mItem = new MenuItem();
                            mItem.Header = "Add to Quick Access Bar";
                            mItem.Click += (s, e) => MoveToQuickAccessBar(b, e);
                            b.ContextMenu.Items.Add(mItem);
                            _testButtons.Add(b);
                            b.Click += (s, e) => MessageBox.Show("You done clicked me");
                        }
                    }
                    return _testButtons;
                }
            }

            public ObservableCollection<RibbonButton> TestQuickAccessButtons
            {
                get
                {
                    if (_testQuickAccessButtons == null)
                    {
                        _testQuickAccessButtons = new ObservableCollection<RibbonButton>();
                        for (int i = 0; i < 3; i++)
                        {
                            var b = new RibbonButton();
                            b.Content = new Image { Source = new BitmapImage(new Uri("/NeedHelpExamples;component/Images/testImage.jpg", UriKind.Relative)) };
                            b.ToolTip = "Testing RibbonButton UserControl";

                            b.ContextMenu = new ContextMenu();
                            var mItem = new MenuItem();
                            mItem.Header = "Remove from Quick Access Bar";
                            mItem.Click += (s, e) => RemoveFromQuickAccessBar(b, e);
                            b.ContextMenu.Items.Add(mItem);

                            _testQuickAccessButtons.Add(b);
                        }
                    }
                    return _testQuickAccessButtons;
                }
            }
            #endregion

            #region "Enums"
            
            #endregion

            #region "Constructor"
            public MainViewModel()
            {
                          }
            #endregion

            #region "Events"
            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }


            #endregion

            #region "Methods"
            private Grid CreateDetailTooltip(string header, string ribbonGroupName, string text, string uri = "")
            {
                Grid tooltipGrid = new Grid();
                ColumnDefinition gridCol1 = new ColumnDefinition();
                ColumnDefinition gridCol2 = new ColumnDefinition();
                tooltipGrid.ColumnDefinitions.Add(gridCol1);
                tooltipGrid.ColumnDefinitions.Add(gridCol2);

                RowDefinition gridRow1 = new RowDefinition();
                RowDefinition gridRow2 = new RowDefinition();
                RowDefinition gridRow3 = new RowDefinition();
                RowDefinition gridRow4 = new RowDefinition();
                tooltipGrid.RowDefinitions.Add(gridRow1);
                tooltipGrid.RowDefinitions.Add(gridRow2);
                tooltipGrid.RowDefinitions.Add(gridRow3);
                tooltipGrid.RowDefinitions.Add(gridRow4);

                var headerTextBlock = new TextBlock();
                headerTextBlock.Text = header;
                headerTextBlock.FontWeight = FontWeights.Bold;
                headerTextBlock.Margin = new Thickness(3);
                headerTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                Grid.SetRow(headerTextBlock, 0);
                Grid.SetColumn(headerTextBlock, 1);
                tooltipGrid.Children.Add(headerTextBlock);

                var ribbonGroupNameTextBlock = new TextBlock();
                ribbonGroupNameTextBlock.Text = ribbonGroupName;
                ribbonGroupNameTextBlock.Margin = new Thickness(3);
                ribbonGroupNameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                ribbonGroupNameTextBlock.FontSize = 10;
                ribbonGroupNameTextBlock.FontWeight = FontWeights.SemiBold;
                Grid.SetRow(ribbonGroupNameTextBlock, 1);
                Grid.SetColumn(ribbonGroupNameTextBlock, 1);
                tooltipGrid.Children.Add(ribbonGroupNameTextBlock);

                var contentTextBlock = new TextBlock();
                contentTextBlock.Text = text;
                contentTextBlock.Margin = new Thickness(3);
                Grid.SetRow(contentTextBlock, 3);
                Grid.SetColumn(contentTextBlock, 1);
                tooltipGrid.Children.Add(contentTextBlock);

                var seperator = new Separator();
                seperator.Margin = new Thickness(3);
                Grid.SetRow(seperator, 2);
                Grid.SetColumn(seperator, 1);
                tooltipGrid.Children.Add(seperator);

                if (uri != "")
                {
                    var image = new Image { Source = new BitmapImage(new Uri(uri, UriKind.Relative)) };
                    image.Margin = new Thickness(3);
                    Grid.SetRow(image, 0);
                    Grid.SetColumn(image, 0);
                    Grid.SetRowSpan(image, 4);
                    tooltipGrid.Children.Add(image);
                }

                return tooltipGrid;
            }

            private void RemoveFromQuickAccessBar(object sender, RoutedEventArgs e)
            {
                var rb = (RibbonButton)sender;
                _testQuickAccessButtons.Remove(rb);
                rb.ContextMenu = new ContextMenu();
                var mItem = new MenuItem();
                mItem.Header = "Add to Quick Access Bar";
                mItem.Click += (s, f) => MoveToQuickAccessBar(rb, f);
                rb.ContextMenu.Items.Add(mItem);
            }

            private void MoveToQuickAccessBar(object sender, RoutedEventArgs e)
            {
                var rb = (RibbonButton)sender;
                var rb2 = CopyButton(rb);
                //var rb2 = rb.Clone();
                _testQuickAccessButtons.Add(rb2);
                rb2.ContextMenu = new ContextMenu();
                var mItem = new MenuItem();
                mItem.Header = "Remove from Quick Access Bar";
                mItem.Click += (s, f) => RemoveFromQuickAccessBar(rb2, f);
                rb2.ContextMenu.Items.Add(mItem);
            }

            private RibbonButton CopyButton(RibbonButton b)
            {
                var newB = new RibbonButton();

                newB.Content = b.Content;//new Image { Source = new BitmapImage(new Uri("/AmTrustExample;component/Images/testImage.jpg", UriKind.Relative)) };
                newB.ToolTip = b.ToolTip;

                foreach (CommandBinding binding in b.CommandBindings)
                {
                    newB.CommandBindings.Add(binding);
                }
                //newB.Click += b.Click;
                return newB;
            }
            #endregion
        }
}
