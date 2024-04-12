using PawMates.Core.Models.Admin;
using PawMates.Infrastructure.Data.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Contracts.Admin
{
    public interface IUserService
    {
        ApplicationRole CreateRole(string roleName);
    }
}
