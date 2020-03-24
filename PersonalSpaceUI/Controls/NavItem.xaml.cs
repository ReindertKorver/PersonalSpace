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

namespace PersonalSpaceUI.Controls
{
    /// <summary>
    /// Interaction logic for NavItem.xaml
    /// </summary>
    public partial class NavItem : UserControl
    {
        public string Text { get; set; }

        public event EventHandler Clicked;

        public NavItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clicked?.Invoke(this, new NavItemClickedEventArgs() { Type = Text });
        }
    }
}