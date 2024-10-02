using System.Numerics;
using System.Text;
using System.Xml.Linq;
using BlurFileFormats.XtFlask;
using BlurFileFormats.XtFlask.Values;
using Editor;
using Editor.Drawers;
using Editor.Windows;
using ImGuiNET;

public class XtEditorWindow : GuiWindow
{
    XtDb XtDb { get; }
    public GuiWindowManager Manager { get; }
    string File { get; }
    string Name { get; }
    public XtEditorWindow(GuiWindowManager manager, string path)
    {
        XtDb = Flask.Import(path);
        Manager = manager;
        File = path;
        Name = Path.GetFileName(path);
    }
    public bool Draw()
    {
        bool open = true;
        if (ImGui.Begin($"{Name}##{File}", ref open, ImGuiWindowFlags.NoCollapse))
        {
            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(5, 5));
            foreach (var item in XtDb.References)
            {
                var value = item.Value;
                if (value == XtNullValue.Instance)
                {
                    ImGui.TreeNodeEx(item.Id.ToString(), ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap);
                    ImGui.SameLine();
                    ImGui.Text(item.Type.Name);
                    ImGui.SameLine();
                    ImGui.Text("External Data");
                    ImGui.TreePop();
                }
                else
                {
                    bool showContent = (ImGui.TreeNodeEx(item.Id.ToString(), ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap));
                    if (ImGui.BeginPopupContextItem($"recordContext{item.Id}"))
                    {
                        if(ImGui.MenuItem("View Ref As Node Graph"))
                        {
                            Manager.AddWindow(new XtRefGraph((XtRef)item));
                        }
                        ImGui.EndPopup();
                    }
                    ImGui.SameLine();
                    ImGui.Text(item.Type.Name);
                    if (showContent)
                    {
                        ShowXtValue(XtDb, value);
                        ImGui.TreePop();
                    }
                }
            }
            ImGui.PopStyleVar();
            ImGui.End();
        }
        return open;
    }

    static void ShowXtValue(XtDb xtDb, IXtValue xtValue)
    {
        if (xtValue is XtStructValue s)
        {
            foreach (var item in s.Values)
            {
                DrawField(xtDb, item);
            }
        }
        else if (xtValue is XtArrayValue a)
        {
            for (int i = 0; i < a.Values.Count; i++)
            {
                var value = a.Values[i];
                DrawArrayItem(xtDb, i, value);
            }
            ImGui.Indent();
            if(ImGui.Button("Add", new Vector2(180, 0)))
            {
                a.Values.Add(new XtArrayValueItem(a.Type.ElementType.CreateDefault()));
            }
            ImGui.Unindent();
        }
    }
    private static bool DrawArrayItem(XtDb xtDb, int i, XtArrayValueItem value)
    {
        ImGuiTreeNodeFlags flags = default;
        bool hasContent = TypeDrawer.HasContent(value.Value);
        if (!hasContent)
        {
            flags = ImGuiTreeNodeFlags.Leaf;
        }

        bool showContent = ImGui.TreeNodeEx($"[{i}]", flags | ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap);
        ImGui.SameLine();
        ImGui.Text(value.Type.Name);

        DrawTitleContent(xtDb, value.Value);
        if (showContent)
        {
            DrawContent(xtDb, value.Value);
            ImGui.TreePop();
        }
        return showContent;
    }
    public static bool DrawField(XtDb xtDb, XtStructValueItem item)
    {
        var field = item.Field;
        var fieldValue = item.Value;

        bool showContent = DrawFieldLabel(item);
        DrawTitleContent(xtDb, fieldValue);
        if (showContent)
        {
            DrawContent(xtDb, fieldValue);
            ImGui.TreePop();
        }
        return showContent;
    }

    public static bool DrawFieldLabel(XtStructValueItem item, ImGuiTreeNodeFlags? overrideFlags = null)
    {
        var field = item.Field;
        var fieldValue = item.Value;

        bool hasContent = TypeDrawer.HasContent(fieldValue);

        ImGuiTreeNodeFlags flags = default;
        if (overrideFlags is ImGuiTreeNodeFlags f)
        {
            flags = f;
        }
        else
        {
            if (!hasContent)
            {
                flags = ImGuiTreeNodeFlags.Leaf;
            }
        }
        bool showContent = ImGui.TreeNodeEx(field.Name, flags | ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap, field.Type.Name);

        ImGui.SameLine();
        ImGui.Text(field.Name);

        return showContent;
    }

    private static void DrawTitleContent(XtDb xtDb, IXtValue value)
    {
        if(value is IXtReferenceValue refVal)
        {
            value = refVal.Reference;
        }
        if (!TypeDrawer.DrawTitle(xtDb, value))
        {
            if (value is XtEnumValue v)
            {
                int c = (int)v.Value;
                ImGui.SameLine();
                ImGui.SetNextItemWidth(160);
                ImGui.Combo("##enum", ref c, v.XtType.Labels.ToArray(), v.XtType.Labels.Count);
                v.Value = (uint)c;
            } else if(value is XtFlagsValue f)
            {
                uint c = f.Value;
                ImGui.SameLine();
                ImGui.SetNextItemWidth(160);
                MultiCombo("##flags", ref c, f.XtType.Labels);
                f.Value = c;
            } else if(value is XtNullValue)
            {
                ImGui.SameLine();
                ImGui.Text("null");
            }
        }
    }
    public static void DrawContent(XtDb xtDb, IXtValue value)
    {
        if (value is IXtReferenceValue refVal)
        {
            DrawContent(xtDb, refVal.Reference);
            return;
        }
        if (!TypeDrawer.DrawContent(xtDb, value))
        {
            if (value is IXtMultiValue)
            {
                ShowXtValue(xtDb, value);
            }
        }
    }
    static void MultiCombo(string label, ref uint flags, IEnumerable<string> flagNames)
    {
        string text = "";
        if (flags == 0)
        {
            text = "None";
        }
        else
        {
            if (flags > 0)
            {
                uint nameFlags = flags;
                StringBuilder builder = new();
                foreach (var item in flagNames)
                {
                    if (nameFlags == 0) break;
                    if ((nameFlags & 1) == 1)
                    {
                        builder.Append(item);
                        builder.Append(',');
                    }
                    nameFlags >>= 1;
                }
                text = builder.Remove(builder.Length - 1, 1).ToString();
            }
        }
        if (ImGui.BeginCombo(label, text))
        {
            uint i = 1;
            foreach (var flag in flagNames)
            {
                //bool isSet = (flags & 1 << i) > 0;
                ImGui.CheckboxFlags(flag, ref flags, i);
                //int x = 1;
                //if (isSet) x = 0;
                //flags ^= x << i;
                i <<= 1;
            }
            ImGui.EndCombo();
        }
    }
}