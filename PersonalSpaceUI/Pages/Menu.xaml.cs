using PersonalSpaceUI.Entities;
using PersonalSpaceUI.Pages.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using WebApi.Entities;

namespace PersonalSpaceUI.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public User CurrentUser { get; set; }

        public Menu(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            UserNameTXT.Text = CurrentUser.Username;
        }

        private void NavItem_Clicked(object sender, EventArgs e)
        {
            if (e is NavItemClickedEventArgs ev)
            {
                Console.WriteLine(ev.Type);
                Page newView;
                switch (ev.Type)
                {
                    case "Dashboard":
                        newView = new Dashboard();

                        break;

                    case "Documents":
                        newView = new Documents();

                        break;

                    case "Tasks":
                        newView = new Tasks();

                        break;

                    case "Account":
                        newView = new Account(CurrentUser);

                        break;

                    default:
                        newView = new Dashboard();
                        break;
                }
                Main.NavigationService.RemoveBackEntry();

                Main.Navigate(newView);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            var signin = new SignIn();
            signin.SignedIn -= win.Signin_SignedIn;
            signin.SignedIn += win.Signin_SignedIn;
            win.Content = signin;
        }
    }
}