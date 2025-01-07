using BlurFileFormats.XtFlask;
using BlurFileFormats.XtFlask.Values;
using ImGuiNET;
using System.ComponentModel.DataAnnotations;

namespace Editor.Drawers
{
    //[DrawAtribute("RP_LinearFunction")]
    //public class LinearFunctionDrawer : ITypeContentDrawer
    //{
    //    public void DrawContent(XtDb xtDb, IXtValue xtValue)
    //    {
    //        ;
    //        if (xtValue is not XtStructValue value) return;
    //        var x0Field = value.GetFieldItem("x0");
    //        var x1Field = value.GetFieldItem("x1");
    //        var yScale = value.GetFieldItem("yScale");
    //        XtEditorWindow.DrawField(xtDb, x0Field);
    //        XtEditorWindow.DrawField(xtDb, x1Field);
    //        XtEditorWindow.DrawField(xtDb, yScale);
    //        var dataPoints = value.GetFieldItem("dataPoints");
    //        if(dataPoints.Value is not XtArrayValue points)
    //        {
    //            return;
    //        }
    //        int i = 0;
    //        float[] fPoints = new float[points.Values.Count];
    //        foreach (var item in points.Values)
    //        {
    //            if(item.Value is XtAtomValue<float> f)
    //            {
    //                fPoints[i] = f.Value;
    //            }
    //            i++;
    //        }
            
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
    [DrawAtribute("f32")]
    public class F32Drawer : ITypeTitleDrawer
    {
        public void Draw(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue xtValue)
        {
            if (xtValue is XtAtomValue<float> a)
            {
                ImGui.PushItemWidth(60);

                ImGui.SameLine(ImGui.GetColumnOffset(), 20);
                ImGui.InputFloat("##float", ref a.GetReference());

                ImGui.PopItemWidth();
            }
        }
    }
    [DrawAtribute("f64")]
    public class F64Drawer : ITypeTitleDrawer
    {
        public void Draw(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue xtValue)
        {
            if (xtValue is XtAtomValue<double> a)
            {
                double v = a.Value;

                ImGui.PushItemWidth(60);

                ImGui.SameLine(ImGui.GetColumnOffset(), 20);
                ImGui.InputDouble("##double", ref v);
                ImGui.PopItemWidth();

                a.Value = v;
            }
        }
    }
}