using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ExcelInterface.Application
{
    class AutoCleanup<T> : IDisposable
    {

        public T Resource
        {
            get;
            private set;
        }

        public AutoCleanup(T resource)
        {
            this.Resource = resource;
        }

        ~AutoCleanup()
        {
            this.Dispose();
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (this.Resource != null &&
                     Marshal.IsComObject(this.Resource))
                {
                    Marshal.FinalReleaseComObject(this.Resource);
                }
                else if (this.Resource is IDisposable)
                {
                    ((IDisposable)this.Resource).Dispose();
                }
                this.Resource = default(T);
            }
        }

    }

    static class ExtensionMethods
    {

        public static AutoCleanup<T> WithComCleanup<T>(this T target)
        {
            return new AutoCleanup<T>(target);
        }

    }
}
