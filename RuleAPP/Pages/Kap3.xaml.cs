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

namespace RuleAPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для Kap3.xaml
    /// </summary>
    public partial class Kap3 : Page
    {

        Frame fr = new Frame();
        public Kap3(Frame frame)
        {
            InitializeComponent();
            fr = frame;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (TextBoxKapcha.Text == "exej")
            {
                fr.Content = "";
            }
            else MessageBox.Show("Неверная капча");
        }
    }
}
