using DataLayer.Models;
using HelperLayer.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer.Services
{
    public class CountryService : BaseService<Country>
    {
        public CountryService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor, PermissionModulEnum.Country) { }

        public override IQueryable<Country> getBaseQuery()
        {
            return base.getBaseQuery()
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CanceledUser)
                .Include(c => c.Cancel);
        }

        public override string getValidationError(Country data)
        {
            string errorValidation = base.getValidationError(data);
            if (!String.IsNullOrEmpty(errorValidation)) return errorValidation;

            //verisel tutarlılık kontrolleri
            //Aynı e-posta da kayıt olmamalı
            if (_context.Country.RetrieveAllNotDeleted()
                .Where(c => c.CountryName == data.CountryName && c.ID != data.ID).Count() > 0
            )
                errorValidation = "Bu Ülke sistemde kayıtlı";

            return errorValidation;
        }
    }
}
