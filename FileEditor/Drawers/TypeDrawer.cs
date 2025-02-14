using BlurFileFormats.FlaskReflection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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
        static Dictionary<string, (object, MethodInfo drawMethod)> Drawers { get; } = [];

        public static bool HasDrawer(IXtValue value) => HasDrawer(value.Type);
        public static bool HasDrawer(IXtType type) => HasDrawer(type.Name);
        public static bool HasDrawer(string type) => Drawers.ContainsKey(type);


        public static (object, MethodInfo drawMethod) GetDrawer(IXtType type) => GetDrawer(type.Name);
        public static (object, MethodInfo drawMethod) GetDrawer(string type) => Drawers[type];

        public static void Draw(XtDatabase xtDb, IXtValue value, XtRef reference, IList<UndoCommand> commandBuffer)
        {
            var (drawer, drawMethod) = GetDrawer(value.Type);
            drawMethod.Invoke(drawer, [xtDb, value, reference, commandBuffer]);
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
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsInterface && !t.IsAbstract);
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<DrawAtribute>();
                if (attr is null) continue;
                Drawers[attr.TypeName] = (Activator.CreateInstance(type)!, type.GetMethod("DrawValue"));
            }
        }
    }
}
