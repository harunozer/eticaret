using DataLayer.Models;
using HelperLayer.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer.Services
{
    public class CancelService : BaseService<Cancel>
    {
        public CancelService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor, PermissionModulEnum.Cancel) { }

        public override IQueryable<Cancel> getBaseQuery()
        {
            return base.getBaseQuery()
                .Include(c => c.CancelObj);
        }
        
        public override bool Add(Cancel data)
        {
            //Max ID 
            data.ID = _context.Cancel.Max(c => c.ID) + 1;
            return base.Add(data);
        }
        
        public override string getValidationError(Cancel data)
        {
            string errorValidation = base.getValidationError(data);
            if (!String.IsNullOrEmpty(errorValidation)) return errorValidation;

            //verisel tutarlılık kontrolleri
            //Aynı isimde kayıt olmamalı
            
            if (_context.Cancel
                .RetrieveAllNotDeleted()
                .Where(c => c.CancelName == data.CancelName && c.ID != data.ID)
                .Count() > 0
            )
                errorValidation = "Bu isimde iptal nedeni sistemde kayıtlı.";

            return errorValidation;
        }

    }
}