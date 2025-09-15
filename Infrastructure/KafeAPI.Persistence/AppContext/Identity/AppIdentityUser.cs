using Microsoft.AspNetCore.Identity;

namespace KafeAPI.Persistence.AppContext.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
