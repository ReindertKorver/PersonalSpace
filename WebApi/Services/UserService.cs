using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);

        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);

        Task<User> NewUser(string userName, string password, string firstName, string lastName);
    }
}