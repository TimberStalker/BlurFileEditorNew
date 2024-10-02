using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using Editor;
using Editor.Rendering;
using SkiaSharp;

namespace Editor.OpenGL;
public class CubemapTexture : IDisposable
{
    uint handle;
    bool setPixels;

    public int Width
    {
        get
        {
            GL.glGetTextureLevelParameteriv(handle, 0, GL.GL_TEXTURE_WIDTH, out var width);
            return width;
        }
    }
    public int Height
    {
        get
        {
            GL.glGetTextureLevelParameteriv(handle, 0, GL.GL_TEXTURE_HEIGHT, out var height);
            return height;
        }
    }

    public CubemapTexture()
    {
        GL.glGenTextures(1, out handle);
    }
    public void SetParameter(uint parameter, int value)
    {
        GL.glTextureParameteri(handle, parameter, value);
    }
    public void SetParameter(uint parameter, float value)
    {
        GL.glTextureParameterf(handle, parameter, value);
    }


    public static CubemapTexture CreateFromBitmaps(SKBitmap[] bitmaps)
    {
        if (bitmaps.Length != 6) throw new NotSupportedException();
        var texture = new CubemapTexture();
        GL.glBindTexture(GL.GL_TEXTURE_CUBE_MAP, texture);
        texture.SetParameter(GL.GL_TEXTURE_WRAP_S, GL.GL_CLAMP_TO_EDGE);
        texture.SetParameter(GL.GL_TEXTURE_WRAP_T, GL.GL_CLAMP_TO_EDGE);
        texture.SetParameter(GL.GL_TEXTURE_WRAP_R, GL.GL_CLAMP_TO_EDGE);
        texture.SetParameter(GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
        texture.SetParameter(GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);

        for (int i = 0; i < bitmaps.Length; i++)
        {
            SKBitmap? item = bitmaps[i];
            texture.SetBits((uint)(GL.GL_TEXTURE_CUBE_MAP_POSITIVE_X + i), item);
        }

        return texture;
    }
    public void SetBits(uint face, SKBitmap bitmap)
    {
        GL.glBindTexture(GL.GL_TEXTURE_CUBE_MAP, handle);
        unsafe
        {
            using var destBimap = new SKBitmap(bitmap.Width, bitmap.Height, SKColorType.Bgra8888, SKAlphaType.Unpremul);
            bitmap.CopyTo(destBimap, SKColorType.Bgra8888);
            //if (!setPixels)
            //{
            GL.glTexImage2D(face, 0, GL.GL_RGBA32F, destBimap.Width, destBimap.Height, 0, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE, destBimap.GetPixels());
                //setPixels = true;
            //}
            //else
            //{
            //    GL.glTexSubImage2D(face, 0, GL.GL_RGBA32F, destBimap.Width, destBimap.Height, 0, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE, destBimap.GetPixels());
            //}
            GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
        }
    }

    public static implicit operator uint(CubemapTexture texture)
    {
        return texture.handle;
    }
    public static implicit operator nint(CubemapTexture texture)
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
            GL.glBindTexture(GL.GL_TEXTURE_CUBE_MAP, 0);
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
    ~CubemapTexture()
    {
        Program.ExecuteOnMainThread(Dispose, false);
    }
}
