using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Entities;

namespace PersonalSpaceUI.Entities
{
    public class SignedInEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}