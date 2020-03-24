using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models;

namespace PersonalSpaceUI.DAL
{
    public class UserConnection
    {
        public static async Task<User> AuthenticateUser(User user)
        {
            User userResponse = await BaseConnection.DoRequest<User>(user, HttpMethod.Post, headers: new (string, string)[] { ("Authorization", BaseConnection.GetAuth(user.Username, user.Password)) }, addToURL: user.APILocation + "authenticate");
            return userResponse;
        }

        public static async Task<User> GetUser()
        {
            User userResponse = await BaseConnection.DoRequest<User>(ApplicationRuntimeData.CurrentUser, HttpMethod.Get, headers: new (string, string)[] { ("Authorization", BaseConnection.GetAuth(ApplicationRuntimeData.AuthUser.Username, ApplicationRuntimeData.AuthUser.Password)) }, addToURL: new User().APILocation + ApplicationRuntimeData.CurrentUser.Id.ToString());
            return userResponse;
        }
    }
}