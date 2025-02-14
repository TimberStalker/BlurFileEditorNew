using BlurFileFormats.FlaskReflection;
using ImGuiNET;

namespace Editor.Drawers
{
    [DrawAtribute("Rgb")]
    public class RgbDrawer
    {
        public void DrawValue(XtDatabase xtDb, XtStructValue value, XtRef reference, IList<UndoCommand> commandBuffer)
        {
            var rValue = value.GetField<float>("r");
            var gValue = value.GetField<float>("g");
            var bValue = value.GetField<float>("b");

            var color = new System.Numerics.Vector3(
                rValue.Value,
                gValue.Value,
                bValue.Value);

            ImGui.SameLine();
            if (ImGui.ColorEdit3($"##color {value.GetHashCode()}", ref color, ImGuiColorEditFlags.NoInputs))
            {
                commandBuffer.Add(UndoCommand.Create(
                    (target: (rValue, gValue, bValue),
                    oldValues: (r: rValue.Value, g: gValue.Value, b: bValue.Value),
                    newValues: (r: color.X, g: color.Y, b: color.Z)),
                    t => (t.target.rValue.Value, t.target.gValue.Value, t.target.bValue.Value) = t.newValues,
                    t => (t.target.rValue.Value, t.target.gValue.Value, t.target.bValue.Value) = t.oldValues
                    ));
            }
        }
    }
}