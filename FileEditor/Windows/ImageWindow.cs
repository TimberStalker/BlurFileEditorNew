using BlurFileFormats.SerializationFramework;
using DirectXTexNet;
using Editor.OpenGL;
using Editor.Rendering;
using ImGuiNET;
using Pango;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Windows;
public class ImageWindow : GuiWindow, IDisposable
{
    public string TexturePath { get; }
    Texture2D texture;
    public ImageWindow(string path)
    {
        TexturePath = path;

        texture = Texture2D.CreateFromFile(path);
    }

    public bool Draw()
    {
        bool open = true;
        if (ImGui.Begin($"{Path.GetFileName(TexturePath)}##{TexturePath}", ref open, ImGuiWindowFlags.NoCollapse))
        {
            var size = ImGui.GetWindowSize();
            var minSize = MathF.Min(size.X, size.Y);
            ImGui.Image(texture, new System.Numerics.Vector2(minSize, minSize));
            ImGui.End();
        }
        return open;
    }

    public void Dispose()
    {
        texture.Dispose();
    }
}
public class DirectXImageWindow : GuiWindow, IDisposable
{
    public string TexturePath { get; }
    TextureDisplay Display { get; }
    public DirectXImageWindow(string path)
    {
        TexturePath = path;

        var sImage = TexHelper.Instance.LoadFromDDSFile(path, DDS_FLAGS.NONE);
        var metadata = sImage.GetMetadata();
        if (TexHelper.Instance.IsCompressed(metadata.Format))
        {
            var temp = sImage.Decompress(DXGI_FORMAT.UNKNOWN);
            sImage.Dispose();
            sImage = temp;
            metadata = sImage.GetMetadata();
        }
        if(metadata.IsCubemap())
        {
            Display = new CubemapImage(sImage, metadata);
        }
        else
        {
            Display = new SingleImage(sImage, metadata);
        }
        sImage.Dispose();
    }

    public bool Draw()
    {
        bool open = true;
        if (ImGui.Begin($"{Path.GetFileName(TexturePath)}##{TexturePath}", ref open, ImGuiWindowFlags.NoCollapse))
        {
            var size = ImGui.GetWindowSize();
            ImGui.BeginChildFrame(2, size - new Vector2(20, 40), ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.NoScrollWithMouse);
            Display.Draw();
            ImGui.End();
        }
        return open;
    }


    public void Dispose()
    {
        if (Display is IDisposable d) d.Dispose();
    }
    interface TextureDisplay
    {
        public void Draw();
    }
    class SingleImage : TextureDisplay, IDisposable
    {
        Texture2D texture;
        float scale = 0;

        public SingleImage(ScratchImage sImage, TexMetadata metadata)
        {
            using var stream = sImage.SaveToWICMemory(0, WIC_FLAGS.NONE, TexHelper.Instance.GetWICCodec(WICCodecs.PNG));
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            texture = Texture2D.CreateFromBytes(bytes);
            sImage.Dispose();
        }
        Vector2 offset;
        Vector2 padding = new Vector2(20, 20);
        Vector2 halfPadding = new Vector2(10, 10);
        public void Draw()
        {
            ImGui.BeginChild("container", Vector2.Zero, true, ImGuiWindowFlags.HorizontalScrollbar | ImGuiWindowFlags.NoScrollWithMouse);

            var size = ImGui.GetWindowSize();
            var minSize = MathF.Max(MathF.Min(size.X, size.Y) - 60, 80);

            Vector2 offset = new Vector2(size.X / 2, size.Y / 2);

            var cursorScreenPos = ImGui.GetCursorScreenPos();
            if (ImGui.IsWindowHovered())
            {
                scale += ImGui.GetIO().MouseWheel / 4;

                if(ImGui.IsMouseDown(ImGuiMouseButton.Middle))
                {
                    var delta = ImGui.GetIO().MouseDelta;
                    ImGui.SetScrollX(ImGui.GetScrollX() - delta.X);
                    ImGui.SetScrollY(ImGui.GetScrollY() - delta.Y);
                }
            }

            int width = texture.Width;
            int height = texture.Height;

            if (width >= height)
            {
                float scale = minSize / width;
                width = (int)minSize;
                height = (int)(height * scale);
            }
            else
            {
                float scale = minSize / height;
                height = (int)minSize;
                width = (int)(width * scale);
            }

            ImGui.Image(texture, new Vector2(width, height) * MathF.Exp(scale));
            ImGui.EndChild();
        }

        public void Dispose()
        {
            texture.Dispose();
        }
    }
    class CubemapImage : TextureDisplay, IDisposable
    {
        CubemapTexture texture;
        Texture2D renderTexture;
        FrameBuffer frameBuffer;
        Shader shader;
        float pitch;
        float yaw;
        const float standardFov = 65 * MathF.PI / 180;
        float fov = standardFov;
        uint vao;
        uint vbo;
        public CubemapImage(ScratchImage sImage, TexMetadata metadata)
        {
            var bitmaps = new SKBitmap[metadata.ArraySize];
            
            for(int i = 0; i < metadata.ArraySize; i++)
            {
                using var stream = sImage.SaveToWICMemory(i, WIC_FLAGS.NONE, TexHelper.Instance.GetWICCodec(WICCodecs.PNG));
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                bitmaps[i] = SKBitmap.Decode(bytes);
            }
            texture = CubemapTexture.CreateFromBitmaps(bitmaps);


            GL.glGenVertexArrays(1, out vao);
            GL.glBindVertexArray(vao);
            
            GL.glGenBuffers(1, out vbo);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, vbo);
            GL.glBufferData(GL.GL_ARRAY_BUFFER, (ulong)(sizeof(float) * skyboxVertices.Length), skyboxVertices, GL.GL_STATIC_DRAW); 
            GL.glVertexAttribPointer(0, 3, GL.GL_FLOAT, false, 0, 0);
            GL.glEnableVertexAttribArray(0);
            
            GL.glBindVertexArray(0);
            
            shader = Shader.Create(Path.Combine(Environment.CurrentDirectory, "Shaders", "skybox"));
            
            frameBuffer = new FrameBuffer();
            GL.glBindFramebuffer(GL.GL_FRAMEBUFFER, frameBuffer);
            
            renderTexture = new Texture2D();
            GL.glBindTexture(GL.GL_TEXTURE_2D, renderTexture);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, GL.GL_RGB, 1, 1, 0, GL.GL_RGB, GL.GL_UNSIGNED_BYTE, 0);
            frameBuffer.AttacthTexture(renderTexture);
            if (GL.glCheckFramebufferStatus(GL.GL_FRAMEBUFFER) != GL.GL_FRAMEBUFFER_COMPLETE)
            {
                throw new Exception("Cubemap framebuffer is not complete");
            }
            GL.glBindFramebuffer(GL.GL_FRAMEBUFFER, 0);
            
        }
        Vector2 lastSize;
        Vector2 padding = new Vector2(20, 20);
        Vector2 halfPadding = new Vector2(10, 10);
        public void Draw()
        {
            ImGui.BeginChild("container", Vector2.Zero, true);
            var size = ImGui.GetWindowSize() - padding;
            if(lastSize != size)
            {
                GL.glBindTexture(GL.GL_TEXTURE_2D, renderTexture);
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, GL.GL_RGB, (int)size.X, (int)size.Y, 0, GL.GL_RGB, GL.GL_UNSIGNED_BYTE, 0);
                lastSize = size;
            }
            var minSize = MathF.Max(MathF.Min(size.X, size.Y) - 60, 80);

            if(ImGui.IsWindowHovered())
            {
                if (ImGui.IsMouseDown(ImGuiMouseButton.Middle))
                {
                    var mouseDrag = ImGui.GetIO().MouseDelta / 6 * standardFov;
                    yaw -= mouseDrag.X;
                    pitch -= mouseDrag.Y;
                    pitch = (float)Math.Clamp(pitch, -90, 90);
                    //rotation *= Quaternion.CreateFromYawPitchRoll(mouseDrag.X, mouseDrag.Y, 0);
                    //ImGui.GetIO().WantSetMousePos = true;
                    //ImGui.GetIO().MousePos = ImGui.GetIO().MousePosPrev;
                }
                fov += (ImGui.GetIO().MouseWheel / 50);
                fov = Math.Clamp(fov, 0.1f, 3.13f);
            }

            var drawList = ImGui.GetWindowDrawList();

            int width = texture.Width;
            int height = texture.Height;

            if (width >= height)
            {
                float scale = minSize / width;
                width = (int)minSize;
                height = (int)(height * scale);
            }
            else
            {
                float scale = minSize / height;
                height = (int)minSize;
                width = (int)(width * scale);
            }
            var pitchQuat = Quaternion.CreateFromYawPitchRoll(0, pitch * MathF.PI / 180, 0);
            var rotation = pitchQuat * Quaternion.CreateFromYawPitchRoll(yaw * MathF.PI / 180, 0, 0);
            
            var viewMatrix = Matrix4x4.CreateFromQuaternion(rotation);
            var perspective = Matrix4x4.CreatePerspectiveFieldOfView(fov, size.X/size.Y, 0.1f, 100f);
            
            
            GL.glBindFramebuffer(GL.GL_FRAMEBUFFER, frameBuffer);
            GL.glClipControl(GL.GL_LOWER_LEFT, GL.GL_NEGATIVE_ONE_TO_ONE);
            GL.glActiveTexture(GL.GL_TEXTURE0);
            GL.glBindTexture(GL.GL_TEXTURE_CUBE_MAP, texture);
            shader.Use();
            shader.SetMatrix("projection", perspective);
            shader.SetMatrix("view", viewMatrix);
            shader.SetInt("skybox", 0);
            
            GL.glBindVertexArray(vao);
            
            GL.glDisable(GL.GL_DEPTH_TEST);
            GL.glViewport(0, 0, (int)size.X, (int)size.Y);
            GL.glClearColor(0, 0, 0, 1);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            GL.glDrawArrays(GL.GL_TRIANGLES, 0, 36);
            
            GL.glUseProgram(0);
            GL.glBindVertexArray(0);
            GL.glBindFramebuffer(GL.GL_FRAMEBUFFER, 0);

            ImGui.SetCursorPos(halfPadding);
            ImGui.Image(renderTexture, new Vector2(size.X, size.Y));
            ImGui.EndChild();
        }

        public void Dispose()
        {
            GL.glDeleteFramebuffers(1, frameBuffer);
            GL.glDeleteTextures(1, texture);
            renderTexture.Dispose();
        }

        float[] skyboxVertices = {
    -10.0f,  10.0f, -10.0f,
    -10.0f, -10.0f, -10.0f,
     10.0f, -10.0f, -10.0f,
     10.0f, -10.0f, -10.0f,
     10.0f,  10.0f, -10.0f,
    -10.0f,  10.0f, -10.0f,

    -10.0f, -10.0f,  10.0f,
    -10.0f, -10.0f, -10.0f,
    -10.0f,  10.0f, -10.0f,
    -10.0f,  10.0f, -10.0f,
    -10.0f,  10.0f,  10.0f,
    -10.0f, -10.0f,  10.0f,

     10.0f, -10.0f, -10.0f,
     10.0f, -10.0f,  10.0f,
     10.0f,  10.0f,  10.0f,
     10.0f,  10.0f,  10.0f,
     10.0f,  10.0f, -10.0f,
     10.0f, -10.0f, -10.0f,

    -10.0f, -10.0f,  10.0f,
    -10.0f,  10.0f,  10.0f,
     10.0f,  10.0f,  10.0f,
     10.0f,  10.0f,  10.0f,
     10.0f, -10.0f,  10.0f,
    -10.0f, -10.0f,  10.0f,

    -10.0f,  10.0f, -10.0f,
     10.0f,  10.0f, -10.0f,
     10.0f,  10.0f,  10.0f,
     10.0f,  10.0f,  10.0f,
    -10.0f,  10.0f,  10.0f,
    -10.0f,  10.0f, -10.0f,

    -10.0f, -10.0f, -10.0f,
    -10.0f, -10.0f,  10.0f,
     10.0f, -10.0f, -10.0f,
     10.0f, -10.0f, -10.0f,
    -10.0f, -10.0f,  10.0f,
     10.0f, -10.0f,  10.0f
};
    }
}
