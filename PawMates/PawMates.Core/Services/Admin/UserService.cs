using PawMates.Core.Contracts.Admin;
using PawMates.Core.Models.Admin;
using PawMates.Infrastructure.Data.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
