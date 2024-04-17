using PawMates.Core.Contracts.Admin;
using PawMates.Infrastructure.Data.IdentityModels;

namespace PawMates.Core.Services.Admin
{
    public class UserService : IUserService
    {
        public ApplicationRole CreateRole(string roleName) => new ApplicationRole()
        {
            Name = roleName,
            NormalizedName = roleName.ToUpper()
        };
    }
}
