using System;
using Microsoft.AspNetCore.Http;
using DataLayer.Models;
using HelperLayer.web;
using HelperLayer.Permissions;
using System.Linq;
using HelperLayer.Exceptions;
using System.Collections.Generic;
using System.Reflection;
using DataLayer.Models.NotMapped;
using HelperLayer;

namespace DataLayer.Services
{
    public class BaseService<T> : IDisposable where T : class
    {
        public eTicaretDbContext _context;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public PermissionModulEnum _permissionModul;

        public BaseService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor, PermissionModulEnum permissionModul)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _permissionModul = permissionModul;

            if (_httpContextAccessor != null)
            {
                _context.CurrentUser = _httpContextAccessor.HttpContext.SessionGetLoginUser<User>();
            }
        }

        #region Permissions
        public virtual bool CheckInsertPermission()
        {
            return CheckUserRolePermission(PermissionEnum.Insert);
        }

        public virtual bool CheckUpdatePermission(T data)
        {
            return CheckUserRolePermission(PermissionEnum.Update);
        }

        public virtual bool CheckDeletePermission(T data)
        {
            return CheckUserRolePermission(PermissionEnum.Delete);
        }

        public virtual bool CheckListPermission()
        {
            return CheckUserRolePermission(PermissionEnum.List);
        }

        public virtual bool CheckViewPermission(T data)
        {
            return CheckUserRolePermission(PermissionEnum.View);
        }

        private bool CheckUserRolePermission(PermissionEnum permission)
        {
            return _httpContextAccessor.HttpContext.CheckPermission(_permissionModul, permission);
        }

        #endregion

        #region List
        public virtual IQueryable<T> getBaseQuery()
        {
            return _context.Set<T>().RetrieveAllNotDeleted();
        }

        public virtual IQueryable<T> getListQuery()
        {
            if (!CheckListPermission())
                throw new PermissionException();
            return getBaseQuery();
        }

        public virtual List<T> getList()
        {
            return getBaseQuery().ToList();
        }
        #endregion

        private bool HasProperty(T data, string PropertyName)
        {
            return (data.GetType().GetProperty(PropertyName) != null);
        }

        public DataPermissions GetDataPermission(T data)
        {
            //Permissions property varmı kontrolü yap.
            if (!HasProperty(data, "Permissions"))
                throw new PropertyNotFoundException();

            return (DataPermissions)data.GetType().GetProperty("Permissions").GetValue(data);
        }

        public virtual T Get(int ID)
        {
            T data = getBaseQuery().RetrieveByIdNotDeleted(ID).SingleOrDefault();

            if (data == null) throw new NotFoundDataException();

            BindPermissions(data);

            //view yetkisi olmayan kayıtda güncelleme,silme yetkisi de olmaz.
            if (!GetDataPermission(data).View) throw new PermissionException();

            return data;
        }
        public virtual bool Add(T data)
        {
            if (!CheckInsertPermission())
                throw new PermissionException();

            string validationError = getValidationError(data);
            if (!String.IsNullOrEmpty(validationError))
                throw new ValidationException(validationError);

            _context.Set<T>().Add(data);
            return _context.SaveChanges() > 0;
        }
        public bool Update(T data)
        {
            if (HasProperty(data, "ID"))
            {
                //ID < 0 ise sistemsel kayıttır güncellenecez Developer role izin verilebilir?
                if(((int)data.GetType().GetProperty("ID").GetValue(data)) < 1)
                    throw new NotFoundDataException();

            }

            //eski data update permission check data 
            //zaten get ile alınmış olduğundan özellikle ezilmediği sürece doğru çalışır.
            if (!GetDataPermission(data).Update)
                throw new PermissionException();

            //güncellenmiş hali de update permission check yapılmalı. (güncellenmiş hali update permission ı olmayan değerlerde olamaz )
            if (!CheckUpdatePermission(data))
                throw new PermissionException();

            //Data validation check
            string validationError = getValidationError(data);
            if (!String.IsNullOrEmpty(validationError))
                throw new ValidationException(validationError);

            _context.Set<T>().Update(data);
            return _context.SaveChanges() > 0;
        }
        public virtual bool Delete(int ID)
        {
            //ID < 0 ise sistemsel kayıttır güncellenecez Developer role izin verilebilir?
            if (ID < 1)
                throw new NotFoundDataException();

            T data = Get(ID);

            //T de CancelID varmı kontrolü
            if (!HasProperty(data, "CancelID"))
                throw new PropertyNotFoundException();

            if (!GetDataPermission(data).Delete)
                throw new PermissionException();
            
            data.GetType().GetProperty("CancelID").SetValue(data, Consts.DeleteCancelID);

            _context.Set<T>().Update(data);

            return _context.SaveChanges() > 0;
        }

        public void BindPermissions(T data)
        {
            if (data == null) return;

            if (!HasProperty(data, "Permissions"))
                throw new PropertyNotFoundException();
            
            data.GetType().GetProperty("Permissions").SetValue(data, new DataPermissions
            {
                View = CheckViewPermission(data),
                Update = CheckUpdatePermission(data),
                Delete = CheckDeletePermission(data)
            });

        }

        public void BindPermissionsList(List<T> dataList)
        {
            foreach (T item in dataList)
            {
                BindPermissions(item);
            }
        }

        public virtual string getValidationError(T data)
        { return null; }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}