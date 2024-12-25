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
using System.Windows.Threading;

namespace MerenkovVar_1
{
    /// <summary>
    /// Логика взаимодействия для LauncherPage.xaml
    /// </summary>
        public partial class LauncherPage : Page
        {
            private string _region;
            private string _login;
            private Frame _mainFrame;

            public LauncherPage(string region, string login, Frame mainFrame)
            {
                _region = region;
                _login = login;
                _mainFrame = mainFrame;
                LoadServers();
                StartClock();
            }

            private void LoadServers()
            {
                ServerComboBox.Items.Clear();
                for (int i = 1; i <= 5; i++)
                {
                    ServerComboBox.Items.Add($"{_region}-Server-{i}");
                }

                ServerComboBox.SelectionChanged += (s, e) =>
                {
                    PlayButton.IsEnabled = ServerComboBox.SelectedItem != null;
                };
            }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) =>
            {
                TimeZoneInfo zone;

                switch (_region)
                {
                    case "EU":
                        zone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                        break;
                    case "AS":
                        zone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
                        break;
                    case "AM":
                        zone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                        break;
                    default:
                        zone = TimeZoneInfo.Utc;
                        break;
                }

                TimeTextBlock.Text = TimeZoneInfo.ConvertTime(DateTime.UtcNow, zone).ToString("HH:mm:ss");
            };

            timer.Start();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
            {
                if (ServerComboBox.SelectedItem != null)
                {
                    MessageBox.Show($"Игра запускается на сервере: {ServerComboBox.SelectedItem}");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите сервер перед запуском игры.");
                }
            }
            private void ChangeRegionButton_Click(object sender, RoutedEventArgs e)
            {
                _mainFrame.Navigate(new RegistrationPage(_mainFrame));
            }

            private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Учетная запись удалена.");
            }
        }
    }
