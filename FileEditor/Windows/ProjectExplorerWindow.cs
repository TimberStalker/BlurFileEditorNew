using Editor;
using Editor.Drawers;
using Editor.Windows;
using ImGuiNET;
using System.Diagnostics.CodeAnalysis;

public class ProjectExplorerWindow : GuiWindow
{
    string filter = "";
    FileSystemObject parentObject;
    public GuiWindowManager WindowManager { get; }
    public ProjectExplorerWindow(string directory, GuiWindowManager windows)
    {
        parentObject = FileSystemObject.Create(directory);
        WindowManager = windows;
    }


    public bool Draw()
    {
        bool open = true;
        if(ImGui.Begin("Project Explorer", ref open, ImGuiWindowFlags.NoCollapse))
        {
            if(ImGui.InputText("##filterInput", ref filter, 100))
            {
            }
            DrawFileItem(parentObject);

            ImGui.End();
        }
        return true;
    }
    public void DrawFileItem(FileSystemObject fileObject)
    {
        ImGuiTreeNodeFlags isLeaf = fileObject.Contents is null || fileObject.Contents.Length > 0 ? ImGuiTreeNodeFlags.None : ImGuiTreeNodeFlags.Leaf;
        bool showContents = ImGui.TreeNodeEx($"##{fileObject.Name}", isLeaf | ImGuiTreeNodeFlags.AllowItemOverlap | ImGuiTreeNodeFlags.SpanFullWidth);
        if(ImGui.IsItemHovered() && ImGui.IsMouseDoubleClicked(ImGuiMouseButton.Left))
        {
            OpenFile(fileObject.FilePath);
        }
        ImGui.SameLine();
        ImGui.Image(fileObject.Icon, new System.Numerics.Vector2(12, 12));
        ImGui.SameLine();
        ImGui.Text(fileObject.Name);
        if (showContents)
        {
            if (fileObject.Contents is null) fileObject.PopulateContents();
            foreach (var content in fileObject.Contents)
            {
                DrawFileItem(content);
            }
            ImGui.TreePop();
        }
    }
    public void OpenFile(string file)
    {
        if (MatchesExtension(file, ".bin", ".xt"))
        {
            WindowManager.AddWindow(new XtEditorWindow(WindowManager, file));
        }
        else if(MatchesExtension(file, ".png", ".jpg"))
        {
            WindowManager.AddWindow(new ImageWindow(file));
        }
        else if(MatchesExtension(file, ".dds"))
        {
            WindowManager.AddWindow(new DirectXImageWindow(file));
        }
    }
    static bool MatchesExtension(string file, params string[] extensions)
    {
        string extension = Path.GetExtension(file);
        return extensions.Any(e => e == extension);
    }
}
public class FileSystemObject
{
    public string Name { get; }
    public string FilePath { get; }
    public Texture2D Icon { get; }
    public FileSystemObject[]? Contents { get; private set; }
    FileSystemObject(string path, Texture2D icon)
    {
        Name = Path.GetFileName(path);
        FilePath = path;
        Icon = icon;
        var attributes = File.GetAttributes(path);
        if(!attributes.HasFlag(FileAttributes.Directory))
        {
            Contents = [];
        }
    }
    [MemberNotNull(nameof(Contents))]
    public void PopulateContents()
    {
        var directories = Directory.GetDirectories(FilePath);
        var files = Directory.GetFiles(FilePath);
        int x = 0;
        Contents = new FileSystemObject[directories.Length + files.Length];
        for(int i = 0; i < directories.Length; i++)
        {
            Contents[x] = Create(directories[i]);
            x++;
        }
        for(int i = 0; i < files.Length; i++)
        {
            Contents[x] = Create(files[i]);
            x++;
        }
    }
    public static FileSystemObject Create(string path)
    {
        return new FileSystemObject(path, FileIcon.GetIcon(path));
    }
}