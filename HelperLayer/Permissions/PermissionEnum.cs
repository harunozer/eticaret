using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLayer.Permissions
{
    public enum PermissionEnum
    {
        Insert = 1,
        Update = 2,
        Delete = 4,
        List = 8,
        View = 16
    }
}
