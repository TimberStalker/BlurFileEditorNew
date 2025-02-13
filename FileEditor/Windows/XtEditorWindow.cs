using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using BlurFileFormats.FlaskReflection;
using Editor;
using Editor.Drawers;
using Editor.Windows;
using GLib;
using ImGuiNET;
using Pango;

public class XtEditorWindow : GuiWindow
{
    XtDatabase XtDb { get; }
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
            foreach (var item in XtDb.Refs)
            {
                if(item is XtRef xtRef)
                {
                    DrawXtItem(XtDb, xtRef);
                    //bool showContent = (ImGui.TreeNodeEx(item.Id.ToString(), ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap));
                    ////if (ImGui.BeginPopupContextItem($"recordContext{item.Id}"))
                    ////{
                    ////    if (ImGui.MenuItem("View Ref As Node Graph"))
                    ////    {
                    ////        Manager.AddWindow(new XtRefGraph((XtRef)item));
                    ////    }
                    ////    ImGui.EndPopup();
                    ////}
                    //ImGui.SameLine();
                    //ImGui.Text(item.Type.Name);
                    //if (showContent)
                    //{
                    //    ShowXtValue(XtDb, value);
                    //    ImGui.TreePop();
                    //}
                }
                else
                {
                    ImGui.TreeNodeEx($"[{item.Id}]", ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap);
                    ImGui.SameLine();
                    ImGui.Text(item.Type.Name);
                    ImGui.SameLine();
                    ImGui.Text("External Data");
                    ImGui.TreePop();
                }
            }
            ImGui.PopStyleVar();
            ImGui.End();
        }
        return open;
    }
    static bool DrawHeader(XtDatabase xtDb, IXtValueItem item)
    {
        string text = item switch
        {
            XtRef value => $"[{value.Id}]",
            XtFieldValueItem value => $"{value.Field.TargetType} {value.Field.Name}",
            XtArrayItem value => $"[{value.Index}]",
            _ => "Unknown"
        };
        if(item.Value is IXtValueContainer || 
            item.Value is XtPointerValue p && p.Value is IXtValueContainer || 
            item.Value is XtHandleValue h && h.XtRef is XtRef r && r.Value is IXtValueContainer || 
            item.Value is XtArrayValue a && a.Array is not null)
        {
            return ImGui.TreeNodeEx(text, ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap);
        }
        else
        {
            return ImGui.TreeNodeEx(text, ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.SpanFullWidth | ImGuiTreeNodeFlags.FramePadding | ImGuiTreeNodeFlags.AllowItemOverlap);
        }
    }
    static void DrawXtItem(XtDatabase xtDb, IXtValueItem item)
    {
        bool showContent = DrawHeader(xtDb, item);
        ImGui.SameLine(0, 10);
        ImGui.Text("=");
        DrawValue(xtDb, item.Value);
        if (showContent)
        {
            DrawContent(xtDb, item.Value);
            ImGui.TreePop();
        }
    }
    static void DrawContent(XtDatabase xtDb, IXtValue value)
    {
        if (value is XtStructValue v) {

            foreach (var item in v)
            {
                DrawXtItem(xtDb, item);
            }
        } else if(value is XtPointerValue p && p.Value is IXtValueContainer pv)
        {
            foreach (var item in pv)
            {
                DrawXtItem(xtDb, item);
            }
        } else if(value is XtHandleValue h && h.XtRef is XtRef r && r.Value is IXtValueContainer hv)
        {
            foreach (var item in hv)
            {
                DrawXtItem(xtDb, item);
            }
        } else if(value is XtArrayValue a && a.Array is not null)
        {
            foreach (var item in a.Array)
            {
                DrawXtItem(xtDb, item);
            }
            if (ImGui.Button("Append"))
            {
                
                a.Array.Add(a.Array.Type.BaseType.CreateValue());
            }
        }
    }
    static unsafe void DrawValue(XtDatabase xtDb, IXtValue value)
    {
        ImGui.SameLine(0, 10);
        switch (value)
        {
            case XtAtomValue<bool> v:
            {
                ImGui.SetNextItemWidth(80);
                bool edit = v.Value;
                ImGui.Checkbox("##sbyte", ref edit);
                v.Value = edit;
                break;
            }
            case XtAtomValue<sbyte> v:
            {
                ImGui.SetNextItemWidth(80);
                sbyte edit = v.Value;
                ImGui.InputScalar("##sbyte", ImGuiDataType.S8, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<short> v:
            {
                ImGui.SetNextItemWidth(80);
                short edit = v.Value;
                ImGui.InputScalar("##short", ImGuiDataType.S16, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<int> v:
            {
                ImGui.SetNextItemWidth(80);
                int edit = v.Value;
                ImGui.InputScalar("##int", ImGuiDataType.S32, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<long> v:
            {
                ImGui.SetNextItemWidth(80);
                long edit = v.Value;
                ImGui.InputScalar("##long", ImGuiDataType.S64, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<byte> v:
            {
                ImGui.SetNextItemWidth(80);
                byte edit = v.Value;
                ImGui.InputScalar("##byte", ImGuiDataType.U8, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<ushort> v:
            {
                ImGui.SetNextItemWidth(80);
                ushort edit = v.Value;
                ImGui.InputScalar("##ushort", ImGuiDataType.U16, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<uint> v:
            {
                ImGui.SetNextItemWidth(80);
                uint edit = v.Value;
                ImGui.InputScalar("##uint", ImGuiDataType.U32, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<ulong> v:
            {
                ImGui.SetNextItemWidth(80);
                ulong edit = v.Value;
                ImGui.InputScalar("##ulong", ImGuiDataType.U64, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<float> v:
            {
                ImGui.SetNextItemWidth(80);
                float edit = v.Value;
                ImGui.InputScalar("##float", ImGuiDataType.Float, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<decimal> v:
            {
                ImGui.SetNextItemWidth(80);
                decimal edit = v.Value;
                ImGui.InputScalar("##decimal", ImGuiDataType.Double, (nint)(&edit));
                v.Value = edit;
                break;
            }
            case XtAtomValue<string> v:
            {
                ImGui.PushItemWidth(460);
                string edit = v.Value;
                ImGui.InputText("##string", ref edit, 255);
                v.Value = edit;
                ImGui.PopItemWidth();
                break;
            }
            case XtAtomValue<LocId> v:
            {
                ImGui.PushItemWidth(460);
                uint edit = v.Value;
                ImGui.InputScalar("##locid", ImGuiDataType.U32, (nint)(&edit));
                v.Value = edit;
                ImGui.PopItemWidth();
                break;
            }
            case XtEnumValue v:
                if(v.Type.IsFlags)
                {
                    ImGui.SetNextItemWidth(80);
                    uint edit = v.Value;
                    MultiCombo("##flags", ref edit, v.Type.Labels);
                    v.Value = edit;
                }
                else
                {
                    ImGui.SetNextItemWidth(80);
                    int edit = (int)v.Value;
                    ImGui.Combo("##enum", ref edit, string.Join('\0', v.Type.Labels));
                    v.Value = (uint)edit;
                }
                break;
            case XtStructValue v:

                ImGui.SetNextItemWidth(80);
                ImGui.Text($"({v.Type.Name}){v.GetHashCode()}");
                break;
            case XtPointerValue v:

                if(v.Value is null)
                {
                    if (ImGui.Button("new", new Vector2(80, 0)))
                    {
                        ImGui.OpenPopup("newPop");
                    }
                    if(ImGui.BeginPopup("newPop"))
                    {
                        foreach (var item in xtDb.Types.Where(t => t == v.Type.BaseType || (t is XtStructType st && v.Type.BaseType is XtStructType pt && st.IsOfType(pt))))
                        {
                            if (ImGui.Button(item.Name))
                            {
                                v.Value = item.CreateValue();
                            }
                        }
                        ImGui.EndPopup();
                    }
                    ImGui.SameLine(0, 5);
                    if (ImGui.Button("use", new Vector2(80, 0)))
                    {

                    }
                }
                else
                {
                    DrawValue(xtDb, v.Value);
                }
                break;
            case XtHandleValue v:

                ImGui.SetNextItemWidth(80);
                if(v.XtRef is null)
                {
                    if (ImGui.Button("new", new Vector2(80, 0)))
                    {
                        
                    }
                    ImGui.SameLine(0, 5);
                    if (ImGui.Button("use", new Vector2(80, 0)))
                    {

                    }
                }
                else
                {
                    ImGui.Text($"({v.XtRef.Type})[{v.XtRef.GetHashCode()}]");
                }
                break;
            case XtArrayValue v:

                ImGui.SetNextItemWidth(80);
                if (v.Array is null)
                {
                    if(ImGui.Button("new", new Vector2(80, 0)))
                    {
                        v.Array = new XtArray(v.Type);
                    }
                    ImGui.SameLine(0, 5);
                    if (ImGui.Button("use", new Vector2(80, 0)))
                    {

                    }
                }
                else
                {
                    ImGui.Text($"({v.Array.Type}){v.Array.GetHashCode()}[{v.Array.Count}]");
                }
                break;
            default:
                ImGui.Text("Not Implemented");
                break;
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