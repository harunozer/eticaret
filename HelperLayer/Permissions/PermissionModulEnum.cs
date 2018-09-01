using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLayer.Permissions
{
    //Veritabanı kayıtları da bu şekilde olmalı.
    //Yeni yetkilendirmelerde zaten kod değişmeli
    public enum PermissionModulEnum
    {
        Cancel = 1,
        User = 2,
        UserRole = 3,
        Customer = 4,
        Country = 5,
        City = 6,
        District = 7
    }
}
