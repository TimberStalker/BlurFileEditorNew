using System.Reflection;
using BlurFileFormats.XtFlask.Types;
using BlurFileFormats.XtFlask.Values;
#if DEBUG
[assembly: System.Reflection.Metadata.MetadataUpdateHandlerAttribute(typeof(Editor.Drawers.HotReloadService))]
namespace Editor.Drawers
{
    public static class HotReloadService
    {
        public static event Action<Type[]?>? UpdateApplicationEvent;

        internal static void ClearCache(Type[]? types) { }
        internal static void UpdateApplication(Type[]? types)
        {
            UpdateApplicationEvent?.Invoke(types);
        }
    }
}
#endif
namespace Editor.Drawers
{
    public static class TypeDrawer
    {
        static Dictionary<string, ITypeDrawer> Drawers { get; } = [];

        public static bool HasDrawer(IXtValue value) => HasDrawer(value.Type);
        public static bool HasDrawer(IXtType type) => HasDrawer(type.Name);
        public static bool HasDrawer(string type) => Drawers.ContainsKey(type);

        public static bool HasTitle(IXtValue value) => HasTitle(value.Type);
        public static bool HasTitle(IXtType type) => HasTitle(type.Name);
        public static bool HasTitle(string type) => HasDrawer(type) && GetDrawer(type) is ITypeTitleDrawer;

        public static bool HasContent(IXtValue value) => HasContent(value.Type) || value is IXtMultiValue || (value is IXtReferenceValue r && HasContent(r.Reference));
        public static bool HasContent(IXtType type) => HasContent(type.Name);
        public static bool HasContent(string type) => HasDrawer(type) && GetDrawer(type) is ITypeContentDrawer;

        public static ITypeDrawer? GetDrawer(IXtType type) => GetDrawer(type.Name);
        public static ITypeDrawer? GetDrawer(string type) => Drawers.TryGetValue(type, out var drawer) ? drawer : null;

        public static bool DrawTitle(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue value)
        {
            if (value is XtNullValue) return false;
            var drawer = GetDrawer(value.Type);
            if (drawer is not ITypeTitleDrawer d) return false;
            d.Draw(xtDb, value);
            return true;
        }
        public static bool DrawContent(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue value)
        {
            if (value is XtNullValue) return false;
            var drawer = GetDrawer(value.Type);
            if (drawer is not ITypeContentDrawer d) return false;

            d.DrawContent(xtDb, value);

            return true;
        }

        static TypeDrawer()
        {
#if (DEBUG)
            HotReloadService.UpdateApplicationEvent += _ => LoadDrawers();
#endif
            LoadDrawers();
        }

        private static void LoadDrawers()
        {
            Drawers.Clear();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsInterface && !t.IsAbstract && t.IsAssignableTo(typeof(ITypeDrawer)));
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<DrawAtribute>();
                if (attr is null) continue;
                Drawers[attr.TypeName] = (ITypeDrawer)Activator.CreateInstance(type)!;
            }
        }
    }
}
