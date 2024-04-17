using PawMates.Infrastructure.Data.IdentityModels;

namespace PawMates.Core.Contracts.Admin
{
    public interface IUserService
    {
        ApplicationRole CreateRole(string roleName);
    }
}
