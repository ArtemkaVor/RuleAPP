using RuleAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using static System.Net.Mime.MediaTypeNames;



namespace RuleAPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для Avtoriz.xaml
    /// </summary>
    public partial class Avtoriz : Page
    {
        private int countUnsuccessful = 0; //Количество неверных попыток входа
        Frame fr = new Frame();
        public Avtoriz(Frame frame)
        {
            InitializeComponent();
                fr = frame;
            KapchaFrame.Content = "";

        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null));
        }

        private async void btnEnter_Click(object sender, RoutedEventArgs e)
        {


            string login = txtLogin.Text.Trim(); //Объявляем переменную, в которую будет записываться значения с TextBlock'a логина
            string password = txtPassword.Text.Trim(); //Объявляем переменную, в которую будет записываться значения с TextBlock'а пароля

            User user = new User(); //создаем пустой объект пользователя

            user = Controller.DbConnection.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).FirstOrDefault(); //Условие на нахождение пользователя с введенными логином и паролем
            int userCount = Controller.DbConnection.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).Count(); //Находим количество пользователей

            if (countUnsuccessful < 1)
            {
                if (userCount > 0 && KapchaFrame.Content == "")
                {

                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(),user);

                }
                else
                {
                        btnEnter.IsEnabled = false;
                        MessageBox.Show("Неверный логин или пароль");
                        await Task.Delay(3000);
                        btnEnter.IsEnabled = true;
                        Random random = new Random();
                        int randmNum = random.Next(1, 4);
                        switch (randmNum)
                        {
                            case 1:
                            KapchaFrame.Content = new Kap1(KapchaFrame);
                            break;
                            case 2:
                            KapchaFrame.Content = new Kap2(KapchaFrame);
                            break;
                            case 3:
                            KapchaFrame.Content = new Kap3(KapchaFrame);
                            break;
                        }
                    MessageBox.Show("Введите капчу!");
                }


            }
        }
       
        private void LoadForm(string _role, User user)
        {
            switch(_role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "Менеджер":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "Администратор":
                    NavigationService.Navigate(new Admin(user));
                    break;
            }
        }

    }
}
