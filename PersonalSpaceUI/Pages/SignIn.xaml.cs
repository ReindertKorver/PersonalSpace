using PersonalSpaceUI.DAL;
using PersonalSpaceUI.Entities;
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
using WebApi.Entities;

namespace PersonalSpaceUI.Pages
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        public event EventHandler SignedIn;

        private async void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageTB.Text = "";
            Loader.Visibility = Visibility.Visible;
            var user = UserNameTB.Text;
            var password = PasswordTB.Password;
            try
            {
                User signedInUser = await UserConnection.AuthenticateUser(new User() { Username = user, Password = password });
                SignedIn?.Invoke(this, new SignedInEventArgs() { User = signedInUser });
                ApplicationRuntimeData.AuthUser = new WebApi.Models.AuthUser() { Username = user, Password = password };
                ApplicationRuntimeData.CurrentUser = signedInUser;
            }
            catch (Exception ex)
            {
                MessageTB.Text = ex.Message;
            }
            Loader.Visibility = Visibility.Hidden;
        }
    }
}