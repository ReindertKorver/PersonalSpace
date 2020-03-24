using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.DAL
{
    public class UserConnection : IUserService
    {
        private readonly UserContext userContext;

        public UserConnection(UserContext context)
        {
            userContext = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => userContext.User.SingleOrDefault(u => u.Username == username && u.Password == password));

            if (user == null)
                return null;

            return user;
        }

        public async Task<User> NewUser(string userName, string password, string firstName, string lastName)
        {
            var user = new User() { FirstName = firstName, LastName = lastName, Username = userName, Password = password };
            await userContext.User.AddAsync(user);
            await userContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => userContext.User);
        }

        public async Task<User> Get(int id)
        {
            User res = await Task.Run(() => userContext.User.FirstOrDefault(u => u.Id == id));
            return res;
        }
    }
}