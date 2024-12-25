using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace MerenkovVar_1
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private Frame _mainFrame;

        public RegistrationPage(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            DateTime? birthDate = BirthDatePicker.SelectedDate;
            string gender = GenderComboBox.Text;
            string region = RegionComboBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            // Проверка на заполненность всех полей
            if (string.IsNullOrWhiteSpace(fullName) ||
                birthDate == null ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(region) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Пожалуйста, заполните все поля.",
                    "Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection("your_connection_string"))
                {
                    conn.Open();
                    string query = @"
                INSERT INTO Users (FullName, BirthDate, Gender, Region, Login, Password) 
                VALUES (@FullName, @BirthDate, @Gender, @Region, @Login, @Password)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Region", region);
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Password", password);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Регистрация успешна!",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                // Переход на LauncherPage
                _mainFrame.Navigate(new LauncherPage(region, login, _mainFrame));
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(
                    $"Ошибка базы данных: {sqlEx.Message}",
                    "Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Произошла ошибка: {ex.Message}",
                    "Ошибка регистрации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}