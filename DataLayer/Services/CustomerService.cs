using DataLayer.Models;
using HelperLayer.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer.Services
{
    public class CustomerService : BaseService<Customer>
    {
        public CustomerService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor, PermissionModulEnum.CustomerPermission) { }

        public override IQueryable<Customer> getBaseQuery()
        {
            return _context.Customer.RetrieveAllNotDeleted()
                .Include(c => c.Cancel)
                .Include(c => c.CreatedUser)
                .Include(c => c.CanceledUser)
                .Include(c => c.UpdatedUser);
        }

        public override string getValidationError(Customer data)
        {
            string errorValidation = base.getValidationError(data);
            if (!String.IsNullOrEmpty(errorValidation)) return errorValidation;

            if (_context.Customer.Where(c => c.EMail == data.EMail && c.ID != data.ID).Count() > 0)
                errorValidation = "By E-Posta sistemde kayıtlıdır.";

            return errorValidation;
        }

    }

}
