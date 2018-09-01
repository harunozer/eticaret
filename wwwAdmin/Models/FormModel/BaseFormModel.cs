using System;

namespace wwwAdmin.Models.FormModel
{
    public class BaseFormModel<T> : IDisposable
        where T : class
    {
        public T Data { get; set; }
        public FormType FormType { get; set; }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
