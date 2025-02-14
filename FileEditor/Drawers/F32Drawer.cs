using BlurFileFormats.FlaskReflection;
using ImGuiNET;
using System.ComponentModel.DataAnnotations;

namespace Editor.Drawers
{
    //[DrawAtribute("RP_LinearFunction")]
    //public class LinearFunctionDrawer
    //{
    //    public void DrawContent(XtDatabase xtDb, IXtValue xtValue)
    //    {
    //        ;
    //        if (xtValue is not XtStructValue value) return;
    //        var x0 = value.GetField<float>("x0");
    //        var x1 = value.GetFieldItem("x1");
    //        var yS = value.GetFieldItem("yScale");
    //        XtEditorWindow.DrawField(xtDb, x0Field);
    //        XtEditorWindow.DrawField(xtDb, x1Field);
    //        XtEditorWindow.DrawField(xtDb, yScale);
    //        var dataPoints = value.GetFieldItem("dataPoints");
    //        if (dataPoints.Value is not XtArrayValue points)
    //        {
    //            return;
    //        }
    //        int i = 0;
    //        float[] fPoints = new float[points.Values.Count];
    //        foreach (var item in points.Values)
    //        {
    //            if (item.Value is XtAtomValue<float> f)
    //            {
    //                fPoints[i] = f.Value;
    //            }
    //            i++;
    //        }
    //
    //        ImGui.PlotLines("Graph", ref fPoints[0], i, 0, "", 0, 1, new System.Numerics.Vector2(0, 100));
    //    }
    //}
    //[DrawAtribute("Render_Curve")]
    //public class CurveDrawer : ITypeContentDrawer
    //{
    //    public void DrawContent(XtDb xtDb, IXtValue xtValue)
    //    {
    //        ;
    //    }
    //}
}