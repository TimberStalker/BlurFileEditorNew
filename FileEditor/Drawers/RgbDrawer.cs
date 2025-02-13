using BlurFileFormats.XtFlask.Values;
using ImGuiNET;

namespace Editor.Drawers
{
    [DrawAtribute("Rgb")]
    public class RgbDrawer : ITypeTitleDrawer, ITypeContentDrawer
    {
        public void Draw(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue xtValue)
        {
            if (xtValue is XtStructValue s)
            {
                var rValue = (XtAtomValue<float>)s.GetField("r");
                var gValue = (XtAtomValue<float>)s.GetField("g");
                var bValue = (XtAtomValue<float>)s.GetField("b");

                var color = new System.Numerics.Vector3(rValue.Value, gValue.Value, bValue.Value);

                ImGui.SameLine();
                ImGui.ColorEdit3($"##color {xtValue.GetHashCode()}", ref color, ImGuiColorEditFlags.NoInputs);

                rValue.Value = color.X;
                gValue.Value = color.Y;
                bValue.Value = color.Z;
            }
        }

        public void DrawContent(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue xtValue)
        {
            if (xtValue is XtStructValue s)
            {
                var rItem = s.GetFieldItem("r");
                var gItem = s.GetFieldItem("g");
                var bItem = s.GetFieldItem("b");

                ImGui.PushItemWidth(80);

                //if (XtEditorWindow.DrawFieldLabel(rItem))
                //{
                //    var rValue = (XtAtomValue<float>)rItem.Value;
                //    float r = rValue.Value;
                //
                //    ImGui.SameLine();
                //    ImGui.DragFloat("##r", ref r, 0.001f, 0, 1);
                //
                //    rValue.Value = r;
                //    ImGui.TreePop();
                //}
                //if (XtEditorWindow.DrawFieldLabel(gItem))
                //{
                //    var gValue = (XtAtomValue<float>)gItem.Value;
                //    float g = gValue.Value;
                //
                //    ImGui.SameLine();
                //    ImGui.DragFloat("##g", ref g, 0.001f, 0, 1);
                //
                //    gValue.Value = g;
                //    ImGui.TreePop();
                //}
                //if (XtEditorWindow.DrawFieldLabel(bItem))
                //{
                //    var bValue = (XtAtomValue<float>)bItem.Value;
                //    float b = bValue.Value;
                //
                //    ImGui.SameLine();
                //    ImGui.DragFloat("##b", ref b, 0.001f, 0, 1);
                //
                //    bValue.Value = b;
                //    ImGui.TreePop();
                //}

                ImGui.PopItemWidth();
            }
        }
    }
}