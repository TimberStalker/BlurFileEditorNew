using Editor.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Editor.OpenGL
{
    public abstract class GLObject : IDisposable
    {
        public abstract uint Handle { get; }
        public static implicit operator uint(GLObject obj)
        {
            return obj.Handle;
        }
        public static implicit operator nint(GLObject obj)
        {
            return (nint)obj.Handle;
        }

        bool disposed;
        protected abstract void Dispose(bool disposing);
        public void Dispose()
        {
            Dispose(true);
            disposed = true;
            GC.SuppressFinalize(this);
        }
        ~GLObject()
        {
            Program.ExecuteOnMainThread(Dispose, false);
            disposed = true;
        }
    }
}
