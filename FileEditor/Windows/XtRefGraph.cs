using BlurFileFormats.FlaskReflection;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Windows;
public class XtRefGraph : GuiWindow
{
    public List<Node> Nodes { get; } = [];
    public XtRef XtRef { get; }
    public XtRefGraph(XtRef xtRef)
    {
        XtRef = xtRef;
        Nodes.Add(new Node(XtRef.Value));
    }

    public bool Draw()
    {
        bool isOpen = true;

        if(ImGui.Begin("Ref Viewer", ref isOpen, ImGuiWindowFlags.NoCollapse))
        {
            if(ImGui.BeginChild("scrollingRegion", Vector2.Zero, true, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoMove))
            {
                //ImGui.PushClipRect(ImGui.GetItemRectMin(), ImGui.GetItemRectMax(), true);
                foreach (var node in Nodes)
                {
                    node.Draw();
                }
                //ImGui.PopClipRect();
                ImGui.EndChild();
            }
            ImGui.End();
        }

        return isOpen;
    }
    public class Node
    {
        Vector2 position;
        Vector2 padding = new Vector2(5, 5);
        public IXtValue Value { get; }

        public Node(IXtValue value)
        {
            Value = value;
        }
        public void Draw()
        {
            ImGui.SetCursorScreenPos(position + padding);
            ImGui.BeginGroup();

            ImGui.EndGroup();


            ImGui.SetCursorScreenPos(position);
            if (ImGui.BeginChild(GetHashCode().ToString(), Vector2.One * 150, true, ImGuiWindowFlags.NoTitleBar  | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.AlwaysAutoResize))
            {
                ImGui.Text(Value.ToString());
                ImGui.EndChild();
            }
        }
    }
}
