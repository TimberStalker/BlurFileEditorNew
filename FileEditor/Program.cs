using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;
using Editor.Rendering;
using Editor.Windows.Popups;
using ImGuiNET;
using static Editor.Rendering.GL;
using ImGuiController = Editor.Rendering.IMGUI.ImGuiController;

namespace Editor
{
    /// <summary>
    /// The entry point of the editor.
    /// </summary>
    static class Program 
    {
        [AllowNull]
        static SynchronizationContext synchronizationContext;
        static GuiWindowManager windows = new();

        public static void ExecuteOnMainThread(Action action) => ExecuteOnMainThread(_ => action(), null);
        public static void ExecuteOnMainThread<T1, T2>(Action<T1, T2> action, T1 state1, T2 state2)
            => ExecuteOnMainThread(c => action(c.state1, c.state2), (state1, state2));
        
        public static void ExecuteOnMainThread<T1, T2, T3>(Action<T1, T2, T3> action, T1 state1, T2 state2, T3 state3)
            => ExecuteOnMainThread(c => action(c.state1, c.state2, c.state3), (state1, state2, state3));

        public static void ExecuteOnMainThread<T>(Action<T> action, T state)
        {
            if(SynchronizationContext.Current != synchronizationContext)
            {
                synchronizationContext.Post(s => action((T)s!), state);
            }
            else
            {
                action(state);
            }
        }
        public static void ExecuteOnMainThread(Action<object?> action, object state)
        {
            if(SynchronizationContext.Current != synchronizationContext)
            {
                synchronizationContext.Post(s => action(s), state);
            }
            else
            {
                action(state);
            }
        }
        static void Main(string[] _args) {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext = new SynchronizationContext());
            WindowCreationProps _winProps = new WindowCreationProps() {
                Title = "BLUR FILE EDITOR",
                IsResizable = true,
            };
            Window _window = new Window(_winProps);
            new ImGuiController();
            ImGui.CreateContext();

            _window.OnDraw += GLDrawings.Draw;

            _window.OnUpdate += () => {
                ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
                ImGui.SetNextWindowSize(Window.Instance.WindowSize);
                ImGui.BeginMainMenuBar();
                if(ImGui.BeginMenu("File"))
                {
                    if(ImGui.MenuItem("Open Standalone"))
                    {
                        Console.WriteLine("Open Standalone File");
                        FileDialogue.OpenPopup("standaloneOpen");
                    }
                    if(ImGui.MenuItem("Create Project"))
                    {
                        Console.WriteLine("Open Create Project Popup");
                        FileDialogue.OpenPopup("createProjectOpen");
                    }
                    ImGui.EndMenu();
                }
                ImGui.EndMainMenuBar();

                if (FileDialogue.OpenFile("standaloneOpen", out string file))
                {
                    Console.WriteLine(file);
                    if(Path.GetExtension(file) == ".bin" || Path.GetExtension(file) == ".xt")
                    {
                        windows.AddWindow(new XtEditorWindow(windows, file));
                    }
                }
                if (FileDialogue.OpenFile("createProjectOpen", out string directory, "*.exe"))
                {
                    Console.WriteLine(directory);
                    windows.AddWindow(new ProjectExplorerWindow(Path.GetDirectoryName(directory)!, windows));
                }

                ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.5f);
                ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.5f);
                ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
                ImGui.SetNextWindowSize(Window.Instance.WindowSize, ImGuiCond.Always);
                ImGui.Begin("DOCK-SPACE TEST", ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoTitleBar |
                                               ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove | 
                                               ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus );
                ImGui.PopStyleVar(2);
                ImGui.DockSpace(ImGui.GetID("Dockspace"));
                
                ImGui.End();

                //ImGui.ShowAboutWindow();
                //ImGui.ShowDemoWindow();
                windows.Draw();
            };
            
            _window.Loop();
        }
        
    }
}
static class GLDrawings
{
    public static Action? Execute;

    public static void Draw()
    {
        if (Execute is null) return;

        glGetIntegerv(GL_ACTIVE_TEXTURE, out int _lastActiveTexture);
        glActiveTexture(GL_TEXTURE0);
        glGetIntegerv(GL_CURRENT_PROGRAM, out int _lastProgram);
        glGetIntegerv(GL_TEXTURE_BINDING_2D, out int _lastTexture);
        glGetIntegerv(GL_SAMPLER_BINDING, out int _lastSampler);
        glGetIntegerv(GL_VERTEX_ARRAY_BINDING, out int _lastVertexArrayObject);
        glGetIntegerv(GL_ARRAY_BUFFER_BINDING, out int _lastArrayBuffer);
        glGetIntegerv(GL_POLYGON_MODE, out int _lastPolygonMode);
        glGetIntegerv(GL_VIEWPORT, out int _lastViewport);
        glGetIntegerv(GL_SCISSOR_BOX, out int _lastScissorBox);
        glGetIntegerv(GL_BLEND_SRC_RGB, out int _lastBlendSrcRgb);
        glGetIntegerv(GL_BLEND_DST_RGB, out int _lastBlendDstRgb);
        glGetIntegerv(GL_BLEND_SRC_ALPHA, out int _lastBlendSrcAlpha);
        glGetIntegerv(GL_BLEND_DST_ALPHA, out int _lastBlendDstAlpha);
        glGetIntegerv(GL_BLEND_EQUATION_RGB, out int _lastBlendEquationRgb);
        glGetIntegerv(GL_BLEND_EQUATION_ALPHA, out int _lastBlendEquationAlpha);

        //Keep track of the old stuff
        bool _lastEnableBlend = glIsEnabled(GL_BLEND);
        bool _lastEnableCullFace = glIsEnabled(GL_CULL_FACE);
        bool _lastEnableDepthTest = glIsEnabled(GL_DEPTH_TEST);
        bool _lastEnableStencilTest = glIsEnabled(GL_STENCIL_TEST);
        bool _lastEnableScissorTest = glIsEnabled(GL_SCISSOR_TEST);
        bool _lastEnablePrimitiveRestart = glIsEnabled(GL_PRIMITIVE_RESTART);

        Execute();

        glUseProgram((uint)_lastProgram);
        glBindTexture(GL_TEXTURE_2D, (uint)_lastTexture);
        glBindSampler(0, (uint)_lastSampler);
        glActiveTexture((uint)_lastActiveTexture);
        glBindVertexArray((uint)_lastVertexArrayObject);
        glBindBuffer(GL_ARRAY_BUFFER, (uint)_lastArrayBuffer);
        glBlendEquationSeparate((uint)_lastBlendEquationRgb, (uint)_lastBlendEquationAlpha);
        glBlendFuncSeparate((uint)_lastBlendSrcRgb, (uint)_lastBlendDstRgb, (uint)_lastBlendSrcAlpha, (uint)_lastBlendDstAlpha);

        if (_lastEnableBlend) glEnable(GL_BLEND); else glDisable(GL_BLEND);
        if (_lastEnableCullFace) glEnable(GL_CULL_FACE); else glDisable(GL_CULL_FACE);
        if (_lastEnableDepthTest) glEnable(GL_DEPTH_TEST); else glDisable(GL_DEPTH_TEST);
        if (_lastEnableStencilTest) glEnable(GL_STENCIL_TEST); else glDisable(GL_STENCIL_TEST);
        if (_lastEnableScissorTest) glEnable(GL_SCISSOR_TEST); else glDisable(GL_SCISSOR_TEST);
        if (_lastEnablePrimitiveRestart) glEnable(GL_PRIMITIVE_RESTART); else glDisable(GL_PRIMITIVE_RESTART);

        glPolygonMode(GL_FRONT_AND_BACK, (uint)_lastPolygonMode);
        glViewport(_lastViewport, _lastViewport, _lastViewport, _lastViewport);
        glScissor(_lastScissorBox, _lastScissorBox, _lastScissorBox, _lastScissorBox);
    }
}