using Editor.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.OpenGL;
public class FrameBuffer : IDisposable
{
    uint handle;
    //Bind? bind;

    public FrameBuffer()
    {
        GL.glGenFramebuffers(1, out handle);
    }

    public void AttacthTexture(Texture2D texture)
    {
        //GL.glBindFramebuffer(GL.GL_FRAMEBUFFER, handle);
        GL.glFramebufferTexture2D(GL.GL_FRAMEBUFFER, GL.GL_COLOR_ATTACHMENT0, GL.GL_TEXTURE_2D, texture, 0);
    }

    //public Bind Bind()
    //{
    //    if(bind is not null) return bind;
    //}

    public static implicit operator uint(FrameBuffer texture)
    {
        return texture.handle;
    }
    public static implicit operator nint(FrameBuffer texture)
    {
        return (nint)texture.handle;
    }

    bool disposed;
    private void Dispose(bool disposing)
    {
        if (disposed) return;
        disposed = true;
        if (disposing)
        {

        }
        unsafe
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);
            fixed (uint* ptr = &handle)
            {
                GL.glDeleteTextures(1, (uint)&ptr);
            }
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    ~FrameBuffer()
    {
        Program.ExecuteOnMainThread(Dispose, false);
    }
}

public class Bind : IDisposable
{
    Action unbind;

    public Bind(Action unbind)
    {
        this.unbind = unbind;
    }

    public void Dispose()
    {
        unbind();
    }
    ~Bind()
    {
        throw new Exception("Bind must be disposed manually.");
    }
}