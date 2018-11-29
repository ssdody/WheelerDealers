using Dealership.Data.Models.Contracts;

namespace Dealership.Data.Models
{
    public class UserSession : IUserSession
    {
        public User CurrentUser { get; set; }
    }
}
