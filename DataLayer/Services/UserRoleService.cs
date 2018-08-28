using DataLayer.Models;
using HelperLayer.Permissions;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DataLayer.Services
{
    public class UserRoleService : BaseService<UserRole>
    {
        public UserRoleService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor, PermissionModulEnum.UserRole) { }

        public override IQueryable<UserRole> getBaseQuery()
        {
            return base.getBaseQuery()
                .Where(r => r.ID >= _context.CurrentUser.UserRoleID);
        }
        
    }
}
