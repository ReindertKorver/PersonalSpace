using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalSpaceUI.Entities
{
    public class NavItemClickedEventArgs : EventArgs
    {
        public string Type { get; set; }
    }
}