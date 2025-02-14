using BlurFileFormats.FlaskReflection;
using GLib;
using ImGuiNET;
using System.Diagnostics.CodeAnalysis;

namespace Editor.Drawers
{
    [DrawAtribute("Vec3")]
    public class Vec3Drawer
    {
        public void DrawValue(XtDatabase xtDb, XtStructValue value, XtRef reference, IList<UndoCommand> commandBuffer)
        {
            var xItem = value.GetFieldItem("vx");
            var yItem = value.GetFieldItem("vy");
            var zItem = value.GetFieldItem("vz");

            ImGui.Text("(");

            ImGui.SameLine(0, 0);
            XtEditorWindow.DrawValue(xtDb, xItem.Value, reference, commandBuffer);

            ImGui.SameLine(0, 0);
            ImGui.Text(",");

            ImGui.SameLine(0, 0);
            XtEditorWindow.DrawValue(xtDb, yItem.Value, reference, commandBuffer);

            ImGui.SameLine(0, 0);
            ImGui.Text(",");

            ImGui.SameLine(0, 0);
            XtEditorWindow.DrawValue(xtDb, zItem.Value, reference, commandBuffer);

            ImGui.SameLine();
            ImGui.Text(")");
        }
    }
    [DrawAtribute("Vec2")]
    public class Vec2Drawer
    {
        public void DrawValue(XtDatabase xtDb, XtStructValue value, XtRef reference, IList<UndoCommand> commandBuffer)
        {
            var xItem = value.GetFieldItem("vx");
            var yItem = value.GetFieldItem("vy");

            ImGui.Text("(");

            ImGui.SameLine(0, 0);
            XtEditorWindow.DrawValue(xtDb, xItem.Value, reference, commandBuffer);

            ImGui.SameLine(0, 0);
            ImGui.Text(",");

            ImGui.SameLine(0, 0);
            XtEditorWindow.DrawValue(xtDb, yItem.Value, reference, commandBuffer);

            ImGui.SameLine();
            ImGui.Text(")");
        }
    }
    [DrawAtribute("RangeI8")]
    public class RangeI8Drawer
    {
        public void DrawValue(XtDatabase xtDb, XtStructValue value, XtRef reference, IList<UndoCommand> commandBuffer)
        {
            var fromItem = value.GetFieldItem("from");
            var toItem = value.GetFieldItem("to");

            XtEditorWindow.DrawValue(xtDb, fromItem.Value, reference, commandBuffer);

            ImGui.SameLine(0, 0);
            ImGui.Text("-");

            ImGui.SameLine(0, 0);
            XtEditorWindow.DrawValue(xtDb, toItem.Value, reference, commandBuffer);
        }
    }
}