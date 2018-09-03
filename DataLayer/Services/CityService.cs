using DataLayer.Models;
using HelperLayer.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer.Services
{
    public class CityService : BaseService<City>
    {
        public CityService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor, PermissionModulEnum.City) { }

        public override IQueryable<City> getBaseQuery()
        {
            return base.getBaseQuery()
                .Include(c => c.Country)
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CanceledUser)
                .Include(c => c.Cancel);
        }

        public override string getValidationError(City data)
        {
            string errorValidation = base.getValidationError(data);
            if (!String.IsNullOrEmpty(errorValidation)) return errorValidation;

            //Country - CityName unique
            if (_context.City.Where(d => d.CountryID == data.CountryID && d.CityName == data.CityName && d.ID != data.ID).Count() > 0)
                errorValidation = "Bu ülkede bu şehir tanımlı.";

            return errorValidation;
        }
    }
}
