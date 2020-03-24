using System.Collections.Generic;

namespace WebApi.Entities
{
    public class User : IAPIRequestable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string APILocation { get; } = "api/users/";
        public List<PSTask> Tasks { get; set; }
        public List<Document> Documents { get; set; }
    }
}