

using Microsoft.AspNetCore.Identity;

namespace Brainbox.Domain.Models
{
    public class Customer : IdentityUser
    {
        public string FullName { get; set; }
    }
}
