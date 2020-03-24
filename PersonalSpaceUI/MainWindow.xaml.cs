using PersonalSpaceUI.DAL;
using PersonalSpaceUI.Entities;
using PersonalSpaceUI.Pages;
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

namespace PersonalSpaceUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var signin = new SignIn();
            signin.SignedIn -= Signin_SignedIn;
            signin.SignedIn += Signin_SignedIn;
            this.Content = signin;
        }

        public void Signin_SignedIn(object sender, EventArgs e)
        {
            Pages.Menu menu = new Pages.Menu(((SignedInEventArgs)e).User);
            this.Content = menu;
        }
    }
}