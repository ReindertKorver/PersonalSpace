using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Entities;
using WebApi.Models;

namespace PersonalSpaceUI.DAL
{
    public static class ApplicationRuntimeData
    {
        public static AuthUser AuthUser { get; set; }
        public static User CurrentUser { get; set; }
    }
}