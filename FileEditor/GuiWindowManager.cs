using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor;
public class GuiWindowManager
{
    List<GuiWindow> windows = [];
    List<GuiWindow> pendingWindows = [];
    public void AddWindow(GuiWindow window)
    {
        pendingWindows.Add(window);
    }
    public void RemoveWindow(GuiWindow window)
    {
        pendingWindows.Remove(window);
    }
    public void Draw()
    {
        GuiWindow? removeWindow = null;
        foreach(var pendingWindow in pendingWindows)
        {
            windows.Add(pendingWindow);
        }
        pendingWindows.Clear();
        foreach (var window in windows)
        {
            if (!window.Draw())
            {
                removeWindow = window;
            }
        }
        if (removeWindow is not null)
        {
            windows.Remove(removeWindow);
            if(removeWindow is IDisposable d)
            {
                d.Dispose();
            }
        }
    }
}
