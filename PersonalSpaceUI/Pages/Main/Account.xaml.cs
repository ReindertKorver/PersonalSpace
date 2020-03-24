using PersonalSpaceUI.DAL;
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

namespace PersonalSpaceUI.Pages.Main
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public User CurrentUser { get; set; }

        public Account(User user)
        {
            CurrentUser = user;
            InitializeComponent();
            Loader.Visibility = Visibility.Visible;
            this.DataContext = this;
            GetUser();
        }

        public async void GetUser()
        {
            try
            {
                //because my User doesnt implement INotifyChange class the binding doesnt update
                CurrentUser = await UserConnection.GetUser();
                FirstNameTB.Text = CurrentUser.FirstName;
                LastNameTB.Text = CurrentUser.LastName;
                UsernameTB.Text = CurrentUser.Username;
                ApplicationRuntimeData.CurrentUser = CurrentUser;
            }
            catch (Exception e)
            {
                MessageBox.Show("Couldn't gather updated account data\n" + e.Message);
            }
            Loader.Visibility = Visibility.Hidden;
        }
    }
}