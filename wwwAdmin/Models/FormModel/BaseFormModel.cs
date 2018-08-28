using System;

namespace wwwAdmin.Models.FormModel
{
    public class BaseFormModel : IDisposable
    {
        public FormType FormType { get; set; }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
