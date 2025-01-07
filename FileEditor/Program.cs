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