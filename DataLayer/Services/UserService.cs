using DataLayer.Models;
using DataLayer.Models.NotMapped;
using HelperLayer;
using HelperLayer.Permissions;
using HelperLayer.web;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(eTicaretDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor, PermissionModulEnum.User) { }
        
        #region Login-Logout
        public User Login(LoginForm form)
        {
            User user = _context.User.RetrieveAll()
                .Where(u => u.EMail == form.EMail && u.Password == form.Password)
                .Include(u => u.UserRole)
                .SingleOrDefault();

            if (user == null)
                return user;

            user.ProcessBaseModel = false;

            user.LastLoginIP = base._httpContextAccessor.HttpContext.GetClientIP();
            user.LastLoginTime = DateTime.Now;
            user.LastLogoutTime = null;
            user.LoginCount++;
            user.IsLogin = true;

            _context.Update<User>(user);
            _context.SaveChanges();

            //session bind
            _httpContextAccessor.HttpContext.SessionSetLoginUser<User>(user);

            //cookie bind
            if (form.RememberMe)
                _httpContextAccessor.HttpContext.CookieObjectSet<LoginForm>(Consts.CookieNameLoginUser, form);
            else
                _httpContextAccessor.HttpContext.CookieRemove(Consts.CookieNameLoginUser);

            return user;
        }

        public void Logout()
        {
            //Update
            User LoginUser = _httpContextAccessor.HttpContext.SessionGetLoginUser<User>();
            LoginUser.IsLogin = false;
            LoginUser.LastLogoutTime = DateTime.Now;
            LoginUser.ProcessBaseModel = false;
            _context.Update(LoginUser);
            _context.SaveChanges();

            //Session Clear
            _httpContextAccessor.HttpContext.SessionAbandon();

        }
        #endregion

        public override IQueryable<User> getBaseQuery()
        {
            return base.getBaseQuery()
                .Include(u => u.UserRole)
                .Include(u => u.CreatedUser)
                .Include(u => u.UpdatedUser)
                .Include(u => u.CanceledUser)
                .Include(u => u.Cancel)
                .Where(u => u.UserRoleID >= _context.CurrentUser.UserRoleID);
        }
        
        public override bool CheckUpdatePermission(User data)
        {
            bool Auth = base.CheckUpdatePermission(data);
            if (!Auth) return false;
            return (data.UserRoleID >= _context.CurrentUser.UserRoleID);
        }
        public override bool CheckDeletePermission(User data)
        {
            bool Auth = base.CheckUpdatePermission(data) && _context.CurrentUserID != data.ID;
            if (!Auth) return false;
            return (data.UserRoleID >= _context.CurrentUser.UserRoleID);
        }
        public override bool CheckViewPermission(User data)
        {
            bool Auth = base.CheckViewPermission(data);
            if (!Auth) return false;
            return (data.UserRoleID >= _context.CurrentUser.UserRoleID);
        }
        public override string getValidationError(User data)
        {
            string errorValidation = base.getValidationError(data);
            if (!String.IsNullOrEmpty(errorValidation)) return errorValidation;

            //verisel tutarlılık kontrolleri
            //Aynı e-posta da kayıt olmamalı
            if (_context.User.RetrieveAllNotDeleted()
                .Where(u => u.EMail == data.EMail && u.ID != data.ID).Count() > 0
            )
                errorValidation = "Bu E-Mail sistemde kayıtlı";

            return errorValidation;
        }

        public void UserAddTest(int count)
        {
            //TODO SİL
            for (int i = 0; i < count; i++)
            {
                string EMail = Guid.NewGuid().ToString();

                User u = new User();
                u.EMail = EMail;
                u.Gsm = "546 278 5454";
                u.IsLogin = false;
                u.Name = "test" + i.ToString();
                u.Password = "12345" + i.ToString();
                u.Surname = "test" + i.ToString();
                u.UserRoleID = 1;

                u.ProcessBaseModel = true;

                _context.User.Add(u);
            }

            _context.SaveChanges();
        }
    }
}