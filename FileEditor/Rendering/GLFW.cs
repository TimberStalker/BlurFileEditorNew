using Editor.Utils;
using System.Runtime.InteropServices;

namespace Editor.Rendering
{
    public unsafe static class GLFW
    {
        private const string LibWindows = "glfw3.dll";
        private const string LibLinux = "libglfw.so.3.3";
        private static readonly IntPtr NativeLibrary = GetNativeLibrary();

        private static IntPtr GetNativeLibrary() {
            return NativeFuncExecuter.LoadLibraryExt(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? LibWindows
                : LibLinux);
        }

        public const int GLFW_VERSION_MAJOR = 3;
        public const int GLFW_VERSION_MINOR = 3;
        public const int GLFW_VERSION_REVISION = 4;
        public const int GLFW_TRUE = 1;
        public const int GLFW_FALSE = 0;
        public const int GLFW_RELEASE = 0;
        public const int GLFW_PRESS = 1;
        public const int GLFW_REPEAT = 2;
        public const int GLFW_HAT_CENTERED = 0;
        public const int GLFW_HAT_UP = 1;
        public const int GLFW_HAT_RIGHT = 2;
        public const int GLFW_HAT_DOWN = 4;
        public const int GLFW_HAT_LEFT = 8;
        public const int GLFW_HAT_RIGHT_UP = (GLFW_HAT_RIGHT | GLFW_HAT_UP);
        public const int GLFW_HAT_RIGHT_DOWN = (GLFW_HAT_RIGHT | GLFW_HAT_DOWN);
        public const int GLFW_HAT_LEFT_UP = (GLFW_HAT_LEFT | GLFW_HAT_UP);
        public const int GLFW_HAT_LEFT_DOWN = (GLFW_HAT_LEFT | GLFW_HAT_DOWN);
        public const int GLFW_KEY_UNKNOWN = -1;
        public const int GLFW_KEY_SPACE = 32;
        public const int GLFW_KEY_APOSTROPHE = 39;
        public const int GLFW_KEY_COMMA = 44;
        public const int GLFW_KEY_MINUS = 45;
        public const int GLFW_KEY_PERIOD = 46;
        public const int GLFW_KEY_SLASH = 47;
        public const int GLFW_KEY_0 = 48;
        public const int GLFW_KEY_1 = 49;
        public const int GLFW_KEY_2 = 50;
        public const int GLFW_KEY_3 = 51;
        public const int GLFW_KEY_4 = 52;
        public const int GLFW_KEY_5 = 53;
        public const int GLFW_KEY_6 = 54;
        public const int GLFW_KEY_7 = 55;
        public const int GLFW_KEY_8 = 56;
        public const int GLFW_KEY_9 = 57;
        public const int GLFW_KEY_SEMICOLON = 59;
        public const int GLFW_KEY_EQUAL = 61;
        public const int GLFW_KEY_A = 65;
        public const int GLFW_KEY_B = 66;
        public const int GLFW_KEY_C = 67;
        public const int GLFW_KEY_D = 68;
        public const int GLFW_KEY_E = 69;
        public const int GLFW_KEY_F = 70;
        public const int GLFW_KEY_G = 71;
        public const int GLFW_KEY_H = 72;
        public const int GLFW_KEY_I = 73;
        public const int GLFW_KEY_J = 74;
        public const int GLFW_KEY_K = 75;
        public const int GLFW_KEY_L = 76;
        public const int GLFW_KEY_M = 77;
        public const int GLFW_KEY_N = 78;
        public const int GLFW_KEY_O = 79;
        public const int GLFW_KEY_P = 80;
        public const int GLFW_KEY_Q = 81;
        public const int GLFW_KEY_R = 82;
        public const int GLFW_KEY_S = 83;
        public const int GLFW_KEY_T = 84;
        public const int GLFW_KEY_U = 85;
        public const int GLFW_KEY_V = 86;
        public const int GLFW_KEY_W = 87;
        public const int GLFW_KEY_X = 88;
        public const int GLFW_KEY_Y = 89;
        public const int GLFW_KEY_Z = 90;
        public const int GLFW_KEY_LEFT_BRACKET = 91;
        public const int GLFW_KEY_BACKSLASH = 92;
        public const int GLFW_KEY_RIGHT_BRACKET = 93;
        public const int GLFW_KEY_GRAVE_ACCENT = 96;
        public const int GLFW_KEY_WORLD_1 = 161;
        public const int GLFW_KEY_WORLD_2 = 162;
        public const int GLFW_KEY_ESCAPE = 256;
        public const int GLFW_KEY_ENTER = 257;
        public const int GLFW_KEY_TAB = 258;
        public const int GLFW_KEY_BACKSPACE = 259;
        public const int GLFW_KEY_INSERT = 260;
        public const int GLFW_KEY_DELETE = 261;
        public const int GLFW_KEY_RIGHT = 262;
        public const int GLFW_KEY_LEFT = 263;
        public const int GLFW_KEY_DOWN = 264;
        public const int GLFW_KEY_UP = 265;
        public const int GLFW_KEY_PAGE_UP = 266;
        public const int GLFW_KEY_PAGE_DOWN = 267;
        public const int GLFW_KEY_HOME = 268;
        public const int GLFW_KEY_END = 269;
        public const int GLFW_KEY_CAPS_LOCK = 280;
        public const int GLFW_KEY_SCROLL_LOCK = 281;
        public const int GLFW_KEY_NUM_LOCK = 282;
        public const int GLFW_KEY_PRINT_SCREEN = 283;
        public const int GLFW_KEY_PAUSE = 284;
        public const int GLFW_KEY_F1 = 290;
        public const int GLFW_KEY_F2 = 291;
        public const int GLFW_KEY_F3 = 292;
        public const int GLFW_KEY_F4 = 293;
        public const int GLFW_KEY_F5 = 294;
        public const int GLFW_KEY_F6 = 295;
        public const int GLFW_KEY_F7 = 296;
        public const int GLFW_KEY_F8 = 297;
        public const int GLFW_KEY_F9 = 298;
        public const int GLFW_KEY_F10 = 299;
        public const int GLFW_KEY_F11 = 300;
        public const int GLFW_KEY_F12 = 301;
        public const int GLFW_KEY_F13 = 302;
        public const int GLFW_KEY_F14 = 303;
        public const int GLFW_KEY_F15 = 304;
        public const int GLFW_KEY_F16 = 305;
        public const int GLFW_KEY_F17 = 306;
        public const int GLFW_KEY_F18 = 307;
        public const int GLFW_KEY_F19 = 308;
        public const int GLFW_KEY_F20 = 309;
        public const int GLFW_KEY_F21 = 310;
        public const int GLFW_KEY_F22 = 311;
        public const int GLFW_KEY_F23 = 312;
        public const int GLFW_KEY_F24 = 313;
        public const int GLFW_KEY_F25 = 314;
        public const int GLFW_KEY_KP_0 = 320;
        public const int GLFW_KEY_KP_1 = 321;
        public const int GLFW_KEY_KP_2 = 322;
        public const int GLFW_KEY_KP_3 = 323;
        public const int GLFW_KEY_KP_4 = 324;
        public const int GLFW_KEY_KP_5 = 325;
        public const int GLFW_KEY_KP_6 = 326;
        public const int GLFW_KEY_KP_7 = 327;
        public const int GLFW_KEY_KP_8 = 328;
        public const int GLFW_KEY_KP_9 = 329;
        public const int GLFW_KEY_KP_DECIMAL = 330;
        public const int GLFW_KEY_KP_DIVIDE = 331;
        public const int GLFW_KEY_KP_MULTIPLY = 332;
        public const int GLFW_KEY_KP_SUBTRACT = 333;
        public const int GLFW_KEY_KP_ADD = 334;
        public const int GLFW_KEY_KP_ENTER = 335;
        public const int GLFW_KEY_KP_EQUAL = 336;
        public const int GLFW_KEY_LEFT_SHIFT = 340;
        public const int GLFW_KEY_LEFT_CONTROL = 341;
        public const int GLFW_KEY_LEFT_ALT = 342;
        public const int GLFW_KEY_LEFT_SUPER = 343;
        public const int GLFW_KEY_RIGHT_SHIFT = 344;
        public const int GLFW_KEY_RIGHT_CONTROL = 345;
        public const int GLFW_KEY_RIGHT_ALT = 346;
        public const int GLFW_KEY_RIGHT_SUPER = 347;
        public const int GLFW_KEY_MENU = 348;
        public const int GLFW_KEY_LAST = GLFW_KEY_MENU;
        public const int GLFW_MOD_SHIFT = 0x0001;
        public const int GLFW_MOD_CONTROL = 0x0002;
        public const int GLFW_MOD_ALT = 0x0004;
        public const int GLFW_MOD_SUPER = 0x0008;
        public const int GLFW_MOD_CAPS_LOCK = 0x0010;
        public const int GLFW_MOD_NUM_LOCK = 0x0020;
        public const int GLFW_MOUSE_BUTTON_1 = 0;
        public const int GLFW_MOUSE_BUTTON_2 = 1;
        public const int GLFW_MOUSE_BUTTON_3 = 2;
        public const int GLFW_MOUSE_BUTTON_4 = 3;
        public const int GLFW_MOUSE_BUTTON_5 = 4;
        public const int GLFW_MOUSE_BUTTON_6 = 5;
        public const int GLFW_MOUSE_BUTTON_7 = 6;
        public const int GLFW_MOUSE_BUTTON_8 = 7;
        public const int GLFW_MOUSE_BUTTON_LAST = GLFW_MOUSE_BUTTON_8;
        public const int GLFW_MOUSE_BUTTON_LEFT = GLFW_MOUSE_BUTTON_1;
        public const int GLFW_MOUSE_BUTTON_RIGHT = GLFW_MOUSE_BUTTON_2;
        public const int GLFW_MOUSE_BUTTON_MIDDLE = GLFW_MOUSE_BUTTON_3;
        public const int GLFW_JOYSTICK_1 = 0;
        public const int GLFW_JOYSTICK_2 = 1;
        public const int GLFW_JOYSTICK_3 = 2;
        public const int GLFW_JOYSTICK_4 = 3;
        public const int GLFW_JOYSTICK_5 = 4;
        public const int GLFW_JOYSTICK_6 = 5;
        public const int GLFW_JOYSTICK_7 = 6;
        public const int GLFW_JOYSTICK_8 = 7;
        public const int GLFW_JOYSTICK_9 = 8;
        public const int GLFW_JOYSTICK_10 = 9;
        public const int GLFW_JOYSTICK_11 = 10;
        public const int GLFW_JOYSTICK_12 = 11;
        public const int GLFW_JOYSTICK_13 = 12;
        public const int GLFW_JOYSTICK_14 = 13;
        public const int GLFW_JOYSTICK_15 = 14;
        public const int GLFW_JOYSTICK_16 = 15;
        public const int GLFW_JOYSTICK_LAST = GLFW_JOYSTICK_16;
        public const int GLFW_GAMEPAD_BUTTON_A = 0;
        public const int GLFW_GAMEPAD_BUTTON_B = 1;
        public const int GLFW_GAMEPAD_BUTTON_X = 2;
        public const int GLFW_GAMEPAD_BUTTON_Y = 3;
        public const int GLFW_GAMEPAD_BUTTON_LEFT_BUMPER = 4;
        public const int GLFW_GAMEPAD_BUTTON_RIGHT_BUMPER = 5;
        public const int GLFW_GAMEPAD_BUTTON_BACK = 6;
        public const int GLFW_GAMEPAD_BUTTON_START = 7;
        public const int GLFW_GAMEPAD_BUTTON_GUIDE = 8;
        public const int GLFW_GAMEPAD_BUTTON_LEFT_THUMB = 9;
        public const int GLFW_GAMEPAD_BUTTON_RIGHT_THUMB = 10;
        public const int GLFW_GAMEPAD_BUTTON_DPAD_UP = 11;
        public const int GLFW_GAMEPAD_BUTTON_DPAD_RIGHT = 12;
        public const int GLFW_GAMEPAD_BUTTON_DPAD_DOWN = 13;
        public const int GLFW_GAMEPAD_BUTTON_DPAD_LEFT = 14;
        public const int GLFW_GAMEPAD_BUTTON_LAST = GLFW_GAMEPAD_BUTTON_DPAD_LEFT;
        public const int GLFW_GAMEPAD_BUTTON_CROSS = GLFW_GAMEPAD_BUTTON_A;
        public const int GLFW_GAMEPAD_BUTTON_CIRCLE = GLFW_GAMEPAD_BUTTON_B;
        public const int GLFW_GAMEPAD_BUTTON_SQUARE = GLFW_GAMEPAD_BUTTON_X;
        public const int GLFW_GAMEPAD_BUTTON_TRIANGLE = GLFW_GAMEPAD_BUTTON_Y;
        public const int GLFW_GAMEPAD_AXIS_LEFT_X = 0;
        public const int GLFW_GAMEPAD_AXIS_LEFT_Y = 1;
        public const int GLFW_GAMEPAD_AXIS_RIGHT_X = 2;
        public const int GLFW_GAMEPAD_AXIS_RIGHT_Y = 3;
        public const int GLFW_GAMEPAD_AXIS_LEFT_TRIGGER = 4;
        public const int GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER = 5;
        public const int GLFW_GAMEPAD_AXIS_LAST = GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER;
        public const int GLFW_NO_ERROR = 0;
        public const int GLFW_NOT_INITIALIZED = 0x00010001;
        public const int GLFW_NO_CURRENT_CONTEXT = 0x00010002;
        public const int GLFW_INVALID_ENUM = 0x00010003;
        public const int GLFW_INVALID_VALUE = 0x00010004;
        public const int GLFW_OUT_OF_MEMORY = 0x00010005;
        public const int GLFW_API_UNAVAILABLE = 0x00010006;
        public const int GLFW_VERSION_UNAVAILABLE = 0x00010007;
        public const int GLFW_PLATFORM_ERROR = 0x00010008;
        public const int GLFW_FORMAT_UNAVAILABLE = 0x00010009;
        public const int GLFW_NO_WINDOW_CONTEXT = 0x0001000A;
        public const int GLFW_FOCUSED = 0x00020001;
        public const int GLFW_ICONIFIED = 0x00020002;
        public const int GLFW_RESIZABLE = 0x00020003;
        public const int GLFW_VISIBLE = 0x00020004;
        public const int GLFW_DECORATED = 0x00020005;
        public const int GLFW_AUTO_ICONIFY = 0x00020006;
        public const int GLFW_FLOATING = 0x00020007;
        public const int GLFW_MAXIMIZED = 0x00020008;
        public const int GLFW_CENTER_CURSOR = 0x00020009;
        public const int GLFW_TRANSPARENT_FRAMEBUFFER = 0x0002000A;
        public const int GLFW_HOVERED = 0x0002000B;
        public const int GLFW_FOCUS_ON_SHOW = 0x0002000C;
        public const int GLFW_RED_BITS = 0x00021001;
        public const int GLFW_GREEN_BITS = 0x00021002;
        public const int GLFW_BLUE_BITS = 0x00021003;
        public const int GLFW_ALPHA_BITS = 0x00021004;
        public const int GLFW_DEPTH_BITS = 0x00021005;
        public const int GLFW_STENCIL_BITS = 0x00021006;
        public const int GLFW_ACCUM_RED_BITS = 0x00021007;
        public const int GLFW_ACCUM_GREEN_BITS = 0x00021008;
        public const int GLFW_ACCUM_BLUE_BITS = 0x00021009;
        public const int GLFW_ACCUM_ALPHA_BITS = 0x0002100A;
        public const int GLFW_AUX_BUFFERS = 0x0002100B;
        public const int GLFW_STEREO = 0x0002100C;
        public const int GLFW_SAMPLES = 0x0002100D;
        public const int GLFW_SRGB_CAPABLE = 0x0002100E;
        public const int GLFW_REFRESH_RATE = 0x0002100F;
        public const int GLFW_DOUBLEBUFFER = 0x00021010;
        public const int GLFW_CLIENT_API = 0x00022001;
        public const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
        public const int GLFW_CONTEXT_VERSION_MINOR = 0x00022003;
        public const int GLFW_CONTEXT_REVISION = 0x00022004;
        public const int GLFW_CONTEXT_ROBUSTNESS = 0x00022005;
        public const int GLFW_OPENGL_FORWARD_COMPAT = 0x00022006;
        public const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
        public const int GLFW_OPENGL_PROFILE = 0x00022008;
        public const int GLFW_CONTEXT_RELEASE_BEHAVIOR = 0x00022009;
        public const int GLFW_CONTEXT_NO_ERROR = 0x0002200A;
        public const int GLFW_CONTEXT_CREATION_API = 0x0002200B;
        public const int GLFW_SCALE_TO_MONITOR = 0x0002200C;
        public const int GLFW_COCOA_RETINA_FRAMEBUFFER = 0x00023001;
        public const int GLFW_COCOA_FRAME_NAME = 0x00023002;
        public const int GLFW_COCOA_GRAPHICS_SWITCHING = 0x00023003;
        public const int GLFW_X11_CLASS_NAME = 0x00024001;
        public const int GLFW_X11_INSTANCE_NAME = 0x00024002;
        public const int GLFW_NO_API = 0;
        public const int GLFW_OPENGL_API = 0x00030001;
        public const int GLFW_OPENGL_ES_API = 0x00030002;
        public const int GLFW_NO_ROBUSTNESS = 0;
        public const int GLFW_NO_RESET_NOTIFICATION = 0x00031001;
        public const int GLFW_LOSE_CONTEXT_ON_RESET = 0x00031002;
        public const int GLFW_OPENGL_ANY_PROFILE = 0;
        public const int GLFW_OPENGL_CORE_PROFILE = 0x00032001;
        public const int GLFW_OPENGL_COMPAT_PROFILE = 0x00032002;
        public const int GLFW_CURSOR = 0x00033001;
        public const int GLFW_STICKY_KEYS = 0x00033002;
        public const int GLFW_STICKY_MOUSE_BUTTONS = 0x00033003;
        public const int GLFW_LOCK_KEY_MODS = 0x00033004;
        public const int GLFW_RAW_MOUSE_MOTION = 0x00033005;
        public const int GLFW_CURSOR_NORMAL = 0x00034001;
        public const int GLFW_CURSOR_HIDDEN = 0x00034002;
        public const int GLFW_CURSOR_DISABLED = 0x00034003;
        public const int GLFW_ANY_RELEASE_BEHAVIOR = 0;
        public const int GLFW_RELEASE_BEHAVIOR_FLUSH = 0x00035001;
        public const int GLFW_RELEASE_BEHAVIOR_NONE = 0x00035002;
        public const int GLFW_NATIVE_CONTEXT_API = 0x00036001;
        public const int GLFW_EGL_CONTEXT_API = 0x00036002;
        public const int GLFW_OSMESA_CONTEXT_API = 0x00036003;
        public const int GLFW_ARROW_CURSOR = 0x00036001;
        public const int GLFW_IBEAM_CURSOR = 0x00036002;
        public const int GLFW_CROSSHAIR_CURSOR = 0x00036003;
        public const int GLFW_HAND_CURSOR = 0x00036004;
        public const int GLFW_HRESIZE_CURSOR = 0x00036005;
        public const int GLFW_VRESIZE_CURSOR = 0x00036006;
        public const int GLFW_CONNECTED = 0x00040001;
        public const int GLFW_DISCONNECTED = 0x00040002;
        public const int GLFW_JOYSTICK_HAT_BUTTONS = 0x00050001;
        public const int GLFW_COCOA_CHDIR_RESOURCES = 0x00051001;
        public const int GLFW_COCOA_MENUBAR = 0x00051002;

        public struct GLFWvidmode
        {
        	public int width;
        	public int height;
        	public int redBits;
        	public int greenBits;
        	public int blueBits;
        	public int refreshRate;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWerrorfun(int errorcode, string description);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowposfun(IntPtr window, int posx, int posy);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowsizefun(IntPtr window, int width, int height);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowclosefun(IntPtr window);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowrefreshfun(IntPtr window);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowfocusfun(IntPtr window, int focused);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowiconifyfun(IntPtr window, int iconified);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowmaximizefun(IntPtr window, int maximized);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWframebuffersizefun(IntPtr window, int width, int height);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWwindowcontentscalefun(IntPtr window, float xscale, float yscale);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWmousebuttonfun(IntPtr window, int button, int action, int mods);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcursorposfun(IntPtr window, double mousex, double mousey);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcursorenterfun(IntPtr window, int entered);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWscrollfun(IntPtr window, double xoffset, double yoffset);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWkeyfun(IntPtr window, int key, int scancode, int action, int mods);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcharfun(IntPtr window, uint codepoint);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWcharmodsfun(IntPtr window, int codepoint, int mods);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWdropfun(IntPtr window, int count, string[] paths);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWmonitorfun(IntPtr window, int monitorevent);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GLFWjoystickfun(int jid, int ev);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void** glfwGetMonitorsDelegate(out int count);
        private static glfwGetMonitorsDelegate _glfwGetMonitors = NativeFuncExecuter.LoadFunction<glfwGetMonitorsDelegate>(NativeLibrary, nameof(glfwGetMonitors));
        public static IntPtr[] glfwGetMonitors(out int count)
        {
            var monitors = _glfwGetMonitors(out count);
            var ret = new IntPtr[count];
            for (var i = 0; i < count; i++)
                ret[i] = (IntPtr)monitors[i];
            return ret;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetVideoModeDelegate(IntPtr monitor);
        private static glfwGetVideoModeDelegate _glfwGetVideoMode = NativeFuncExecuter.LoadFunction<glfwGetVideoModeDelegate>(NativeLibrary, nameof(glfwGetVideoMode));
        public static GLFWvidmode glfwGetVideoMode(IntPtr monitor) => Marshal.PtrToStructure<GLFWvidmode>(_glfwGetVideoMode(monitor));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwInitDelegate();
        private static glfwInitDelegate _glfwInit = NativeFuncExecuter.LoadFunction<glfwInitDelegate>(NativeLibrary, nameof(glfwInit));
        public static int glfwInit() => _glfwInit();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwTerminateDelegate();
        private static glfwTerminateDelegate _glfwTerminate = NativeFuncExecuter.LoadFunction<glfwTerminateDelegate>(NativeLibrary, nameof(glfwTerminate));
        public static void glfwTerminate() => _glfwTerminate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwInitHintDelegate(int hint, int value);
        private static glfwInitHintDelegate _glfwInitHint = NativeFuncExecuter.LoadFunction<glfwInitHintDelegate>(NativeLibrary, nameof(glfwInitHint));
        public static void glfwInitHint(int hint, int value) => _glfwInitHint(hint, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetVersionDelegate(out int major, out int minor, out int rev);
        private static glfwGetVersionDelegate _glfwGetVersion = NativeFuncExecuter.LoadFunction<glfwGetVersionDelegate>(NativeLibrary, nameof(glfwGetVersion));
        public static void glfwGetVersion(out int major, out int minor, out int rev) => _glfwGetVersion(out major, out minor, out rev);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetVersionStringDelegate();
        private static glfwGetVersionStringDelegate _glfwGetVersionString = NativeFuncExecuter.LoadFunction<glfwGetVersionStringDelegate>(NativeLibrary, nameof(glfwGetVersionString));
        public static string glfwGetVersionString() => _glfwGetVersionString();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetErrorDelegate(out string description);
        private static glfwGetErrorDelegate _glfwGetError = NativeFuncExecuter.LoadFunction<glfwGetErrorDelegate>(NativeLibrary, nameof(glfwGetError));
        public static int glfwGetError(out string description) => _glfwGetError(out description);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWerrorfun glfwSetErrorCallbackDelegate(GLFWerrorfun callback);
        private static glfwSetErrorCallbackDelegate _glfwSetErrorCallback = NativeFuncExecuter.LoadFunction<glfwSetErrorCallbackDelegate>(NativeLibrary, nameof(glfwSetErrorCallback));
        public static GLFWerrorfun glfwSetErrorCallback(GLFWerrorfun callback) => _glfwSetErrorCallback(callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetPrimaryMonitorDelegate();
        private static glfwGetPrimaryMonitorDelegate _glfwGetPrimaryMonitor = NativeFuncExecuter.LoadFunction<glfwGetPrimaryMonitorDelegate>(NativeLibrary, nameof(glfwGetPrimaryMonitor));
        public static IntPtr glfwGetPrimaryMonitor() => _glfwGetPrimaryMonitor();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetMonitorPosDelegate(IntPtr monitor, out int xpos, out int ypos);
        private static glfwGetMonitorPosDelegate _glfwGetMonitorPos = NativeFuncExecuter.LoadFunction<glfwGetMonitorPosDelegate>(NativeLibrary, nameof(glfwGetMonitorPos));
        public static void glfwGetMonitorPos(IntPtr monitor, out int xpos, out int ypos) => _glfwGetMonitorPos(monitor, out xpos, out ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetMonitorWorkareaDelegate(IntPtr monitor, out int xpos, out int ypos, out int width, out int height);
        private static glfwGetMonitorWorkareaDelegate _glfwGetMonitorWorkarea = NativeFuncExecuter.LoadFunction<glfwGetMonitorWorkareaDelegate>(NativeLibrary, nameof(glfwGetMonitorWorkarea));
        public static void glfwGetMonitorWorkarea(IntPtr monitor, out int xpos, out int ypos, out int width, out int height) => _glfwGetMonitorWorkarea(monitor, out xpos, out ypos, out width, out height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetMonitorPhysicalSizeDelegate(IntPtr monitor, out int widthMM, out int heightMM);
        private static glfwGetMonitorPhysicalSizeDelegate _glfwGetMonitorPhysicalSize = NativeFuncExecuter.LoadFunction<glfwGetMonitorPhysicalSizeDelegate>(NativeLibrary, nameof(glfwGetMonitorPhysicalSize));
        public static void glfwGetMonitorPhysicalSize(IntPtr monitor, out int widthMM, out int heightMM) => _glfwGetMonitorPhysicalSize(monitor, out widthMM, out heightMM);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetMonitorContentScaleDelegate(IntPtr monitor, out float xscale, out float yscale);
        private static glfwGetMonitorContentScaleDelegate _glfwGetMonitorContentScale = NativeFuncExecuter.LoadFunction<glfwGetMonitorContentScaleDelegate>(NativeLibrary, nameof(glfwGetMonitorContentScale));
        public static void glfwGetMonitorContentScale(IntPtr monitor, out float xscale, out float yscale) => _glfwGetMonitorContentScale(monitor, out xscale, out yscale);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetMonitorNameDelegate(IntPtr monitor);
        private static glfwGetMonitorNameDelegate _glfwGetMonitorName = NativeFuncExecuter.LoadFunction<glfwGetMonitorNameDelegate>(NativeLibrary, nameof(glfwGetMonitorName));
        public static string glfwGetMonitorName(IntPtr monitor) => _glfwGetMonitorName(monitor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetMonitorUserPointerDelegate(IntPtr monitor, IntPtr pointer);
        private static glfwSetMonitorUserPointerDelegate _glfwSetMonitorUserPointer = NativeFuncExecuter.LoadFunction<glfwSetMonitorUserPointerDelegate>(NativeLibrary, nameof(glfwSetMonitorUserPointer));
        public static void glfwSetMonitorUserPointer(IntPtr monitor, IntPtr pointer) => _glfwSetMonitorUserPointer(monitor, pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetMonitorUserPointerDelegate(IntPtr monitor);
        private static glfwGetMonitorUserPointerDelegate _glfwGetMonitorUserPointer = NativeFuncExecuter.LoadFunction<glfwGetMonitorUserPointerDelegate>(NativeLibrary, nameof(glfwGetMonitorUserPointer));
        public static IntPtr glfwGetMonitorUserPointer(IntPtr monitor) => _glfwGetMonitorUserPointer(monitor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWmonitorfun glfwSetMonitorCallbackDelegate(GLFWmonitorfun callback);
        private static glfwSetMonitorCallbackDelegate _glfwSetMonitorCallback = NativeFuncExecuter.LoadFunction<glfwSetMonitorCallbackDelegate>(NativeLibrary, nameof(glfwSetMonitorCallback));
        public static GLFWmonitorfun glfwSetMonitorCallback(GLFWmonitorfun callback) => _glfwSetMonitorCallback(callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetVideoModesDelegate(IntPtr monitor, out int count);
        private static glfwGetVideoModesDelegate _glfwGetVideoModes = NativeFuncExecuter.LoadFunction<glfwGetVideoModesDelegate>(NativeLibrary, nameof(glfwGetVideoModes));
        public static IntPtr glfwGetVideoModes(IntPtr monitor, out int count) => _glfwGetVideoModes(monitor, out count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetGammaDelegate(IntPtr monitor, float gamma);
        private static glfwSetGammaDelegate _glfwSetGamma = NativeFuncExecuter.LoadFunction<glfwSetGammaDelegate>(NativeLibrary, nameof(glfwSetGamma));
        public static void glfwSetGamma(IntPtr monitor, float gamma) => _glfwSetGamma(monitor, gamma);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetGammaRampDelegate(IntPtr monitor);
        private static glfwGetGammaRampDelegate _glfwGetGammaRamp = NativeFuncExecuter.LoadFunction<glfwGetGammaRampDelegate>(NativeLibrary, nameof(glfwGetGammaRamp));
        public static IntPtr glfwGetGammaRamp(IntPtr monitor) => _glfwGetGammaRamp(monitor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetGammaRampDelegate(IntPtr monitor, IntPtr ramp);
        private static glfwSetGammaRampDelegate _glfwSetGammaRamp = NativeFuncExecuter.LoadFunction<glfwSetGammaRampDelegate>(NativeLibrary, nameof(glfwSetGammaRamp));
        public static void glfwSetGammaRamp(IntPtr monitor, IntPtr ramp) => _glfwSetGammaRamp(monitor, ramp);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwDefaultWindowHintsDelegate();
        private static glfwDefaultWindowHintsDelegate _glfwDefaultWindowHints = NativeFuncExecuter.LoadFunction<glfwDefaultWindowHintsDelegate>(NativeLibrary, nameof(glfwDefaultWindowHints));
        public static void glfwDefaultWindowHints() => _glfwDefaultWindowHints();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwWindowHintDelegate(int hint, int value);
        private static glfwWindowHintDelegate _glfwWindowHint = NativeFuncExecuter.LoadFunction<glfwWindowHintDelegate>(NativeLibrary, nameof(glfwWindowHint));
        public static void glfwWindowHint(int hint, int value) => _glfwWindowHint(hint, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwWindowHintStringDelegate(int hint, string value);
        private static glfwWindowHintStringDelegate _glfwWindowHintString = NativeFuncExecuter.LoadFunction<glfwWindowHintStringDelegate>(NativeLibrary, nameof(glfwWindowHintString));
        public static void glfwWindowHintString(int hint, string value) => _glfwWindowHintString(hint, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwCreateWindowDelegate(int width, int height, string title, IntPtr monitor, IntPtr share);
        private static glfwCreateWindowDelegate _glfwCreateWindow = NativeFuncExecuter.LoadFunction<glfwCreateWindowDelegate>(NativeLibrary, nameof(glfwCreateWindow));
        public static IntPtr glfwCreateWindow(int width, int height, string title, IntPtr monitor, IntPtr share) => _glfwCreateWindow(width, height, title, monitor, share);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwDestroyWindowDelegate(IntPtr window);
        private static glfwDestroyWindowDelegate _glfwDestroyWindow = NativeFuncExecuter.LoadFunction<glfwDestroyWindowDelegate>(NativeLibrary, nameof(glfwDestroyWindow));
        public static void glfwDestroyWindow(IntPtr window) => _glfwDestroyWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwWindowShouldCloseDelegate(IntPtr window);
        private static glfwWindowShouldCloseDelegate _glfwWindowShouldClose = NativeFuncExecuter.LoadFunction<glfwWindowShouldCloseDelegate>(NativeLibrary, nameof(glfwWindowShouldClose));
        public static int glfwWindowShouldClose(IntPtr window) => _glfwWindowShouldClose(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowShouldCloseDelegate(IntPtr window, int value);
        private static glfwSetWindowShouldCloseDelegate _glfwSetWindowShouldClose = NativeFuncExecuter.LoadFunction<glfwSetWindowShouldCloseDelegate>(NativeLibrary, nameof(glfwSetWindowShouldClose));
        public static void glfwSetWindowShouldClose(IntPtr window, int value) => _glfwSetWindowShouldClose(window, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowTitleDelegate(IntPtr window, string title);
        private static glfwSetWindowTitleDelegate _glfwSetWindowTitle = NativeFuncExecuter.LoadFunction<glfwSetWindowTitleDelegate>(NativeLibrary, nameof(glfwSetWindowTitle));
        public static void glfwSetWindowTitle(IntPtr window, string title) => _glfwSetWindowTitle(window, title);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowIconDelegate(IntPtr window, int count, IntPtr images);
        private static glfwSetWindowIconDelegate _glfwSetWindowIcon = NativeFuncExecuter.LoadFunction<glfwSetWindowIconDelegate>(NativeLibrary, nameof(glfwSetWindowIcon));
        public static void glfwSetWindowIcon(IntPtr window, int count, IntPtr images) => _glfwSetWindowIcon(window, count, images);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetWindowPosDelegate(IntPtr window, out int xpos, out int ypos);
        private static glfwGetWindowPosDelegate _glfwGetWindowPos = NativeFuncExecuter.LoadFunction<glfwGetWindowPosDelegate>(NativeLibrary, nameof(glfwGetWindowPos));
        public static void glfwGetWindowPos(IntPtr window, out int xpos, out int ypos) => _glfwGetWindowPos(window, out xpos, out ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowPosDelegate(IntPtr window, int xpos, int ypos);
        private static glfwSetWindowPosDelegate _glfwSetWindowPos = NativeFuncExecuter.LoadFunction<glfwSetWindowPosDelegate>(NativeLibrary, nameof(glfwSetWindowPos));
        public static void glfwSetWindowPos(IntPtr window, int xpos, int ypos) => _glfwSetWindowPos(window, xpos, ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetWindowSizeDelegate(IntPtr window, out int width, out int height);
        private static glfwGetWindowSizeDelegate _glfwGetWindowSize = NativeFuncExecuter.LoadFunction<glfwGetWindowSizeDelegate>(NativeLibrary, nameof(glfwGetWindowSize));
        public static void glfwGetWindowSize(IntPtr window, out int width, out int height) => _glfwGetWindowSize(window, out width, out height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowSizeLimitsDelegate(IntPtr window, int minwidth, int minheight, int maxwidth, int maxheight);
        private static glfwSetWindowSizeLimitsDelegate _glfwSetWindowSizeLimits = NativeFuncExecuter.LoadFunction<glfwSetWindowSizeLimitsDelegate>(NativeLibrary, nameof(glfwSetWindowSizeLimits));
        public static void glfwSetWindowSizeLimits(IntPtr window, int minwidth, int minheight, int maxwidth, int maxheight) => _glfwSetWindowSizeLimits(window, minwidth, minheight, maxwidth, maxheight);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowAspectRatioDelegate(IntPtr window, int numer, int denom);
        private static glfwSetWindowAspectRatioDelegate _glfwSetWindowAspectRatio = NativeFuncExecuter.LoadFunction<glfwSetWindowAspectRatioDelegate>(NativeLibrary, nameof(glfwSetWindowAspectRatio));
        public static void glfwSetWindowAspectRatio(IntPtr window, int numer, int denom) => _glfwSetWindowAspectRatio(window, numer, denom);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowSizeDelegate(IntPtr window, int width, int height);
        private static glfwSetWindowSizeDelegate _glfwSetWindowSize = NativeFuncExecuter.LoadFunction<glfwSetWindowSizeDelegate>(NativeLibrary, nameof(glfwSetWindowSize));
        public static void glfwSetWindowSize(IntPtr window, int width, int height) => _glfwSetWindowSize(window, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetFramebufferSizeDelegate(IntPtr window, out int width, out int height);
        private static glfwGetFramebufferSizeDelegate _glfwGetFramebufferSize = NativeFuncExecuter.LoadFunction<glfwGetFramebufferSizeDelegate>(NativeLibrary, nameof(glfwGetFramebufferSize));
        public static void glfwGetFramebufferSize(IntPtr window, out int width, out int height) => _glfwGetFramebufferSize(window, out width, out height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetWindowFrameSizeDelegate(IntPtr window, out int left, out int top, out int right, out int bottom);
        private static glfwGetWindowFrameSizeDelegate _glfwGetWindowFrameSize = NativeFuncExecuter.LoadFunction<glfwGetWindowFrameSizeDelegate>(NativeLibrary, nameof(glfwGetWindowFrameSize));
        public static void glfwGetWindowFrameSize(IntPtr window, out int left, out int top, out int right, out int bottom) => _glfwGetWindowFrameSize(window, out left, out top, out right, out bottom);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetWindowContentScaleDelegate(IntPtr window, out float xscale, out float yscale);
        private static glfwGetWindowContentScaleDelegate _glfwGetWindowContentScale = NativeFuncExecuter.LoadFunction<glfwGetWindowContentScaleDelegate>(NativeLibrary, nameof(glfwGetWindowContentScale));
        public static void glfwGetWindowContentScale(IntPtr window, out float xscale, out float yscale) => _glfwGetWindowContentScale(window, out xscale, out yscale);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float glfwGetWindowOpacityDelegate(IntPtr window);
        private static glfwGetWindowOpacityDelegate _glfwGetWindowOpacity = NativeFuncExecuter.LoadFunction<glfwGetWindowOpacityDelegate>(NativeLibrary, nameof(glfwGetWindowOpacity));
        public static float glfwGetWindowOpacity(IntPtr window) => _glfwGetWindowOpacity(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowOpacityDelegate(IntPtr window, float opacity);
        private static glfwSetWindowOpacityDelegate _glfwSetWindowOpacity = NativeFuncExecuter.LoadFunction<glfwSetWindowOpacityDelegate>(NativeLibrary, nameof(glfwSetWindowOpacity));
        public static void glfwSetWindowOpacity(IntPtr window, float opacity) => _glfwSetWindowOpacity(window, opacity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwIconifyWindowDelegate(IntPtr window);
        private static glfwIconifyWindowDelegate _glfwIconifyWindow = NativeFuncExecuter.LoadFunction<glfwIconifyWindowDelegate>(NativeLibrary, nameof(glfwIconifyWindow));
        public static void glfwIconifyWindow(IntPtr window) => _glfwIconifyWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwRestoreWindowDelegate(IntPtr window);
        private static glfwRestoreWindowDelegate _glfwRestoreWindow = NativeFuncExecuter.LoadFunction<glfwRestoreWindowDelegate>(NativeLibrary, nameof(glfwRestoreWindow));
        public static void glfwRestoreWindow(IntPtr window) => _glfwRestoreWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwMaximizeWindowDelegate(IntPtr window);
        private static glfwMaximizeWindowDelegate _glfwMaximizeWindow = NativeFuncExecuter.LoadFunction<glfwMaximizeWindowDelegate>(NativeLibrary, nameof(glfwMaximizeWindow));
        public static void glfwMaximizeWindow(IntPtr window) => _glfwMaximizeWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwShowWindowDelegate(IntPtr window);
        private static glfwShowWindowDelegate _glfwShowWindow = NativeFuncExecuter.LoadFunction<glfwShowWindowDelegate>(NativeLibrary, nameof(glfwShowWindow));
        public static void glfwShowWindow(IntPtr window) => _glfwShowWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwHideWindowDelegate(IntPtr window);
        private static glfwHideWindowDelegate _glfwHideWindow = NativeFuncExecuter.LoadFunction<glfwHideWindowDelegate>(NativeLibrary, nameof(glfwHideWindow));
        public static void glfwHideWindow(IntPtr window) => _glfwHideWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwFocusWindowDelegate(IntPtr window);
        private static glfwFocusWindowDelegate _glfwFocusWindow = NativeFuncExecuter.LoadFunction<glfwFocusWindowDelegate>(NativeLibrary, nameof(glfwFocusWindow));
        public static void glfwFocusWindow(IntPtr window) => _glfwFocusWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwRequestWindowAttentionDelegate(IntPtr window);
        private static glfwRequestWindowAttentionDelegate _glfwRequestWindowAttention = NativeFuncExecuter.LoadFunction<glfwRequestWindowAttentionDelegate>(NativeLibrary, nameof(glfwRequestWindowAttention));
        public static void glfwRequestWindowAttention(IntPtr window) => _glfwRequestWindowAttention(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetWindowMonitorDelegate(IntPtr window);
        private static glfwGetWindowMonitorDelegate _glfwGetWindowMonitor = NativeFuncExecuter.LoadFunction<glfwGetWindowMonitorDelegate>(NativeLibrary, nameof(glfwGetWindowMonitor));
        public static IntPtr glfwGetWindowMonitor(IntPtr window) => _glfwGetWindowMonitor(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowMonitorDelegate(IntPtr window, IntPtr monitor, int xpos, int ypos, int width, int height, int refreshRate);
        private static glfwSetWindowMonitorDelegate _glfwSetWindowMonitor = NativeFuncExecuter.LoadFunction<glfwSetWindowMonitorDelegate>(NativeLibrary, nameof(glfwSetWindowMonitor));
        public static void glfwSetWindowMonitor(IntPtr window, IntPtr monitor, int xpos, int ypos, int width, int height, int refreshRate) => _glfwSetWindowMonitor(window, monitor, xpos, ypos, width, height, refreshRate);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetWindowAttribDelegate(IntPtr window, int attrib);
        private static glfwGetWindowAttribDelegate _glfwGetWindowAttrib = NativeFuncExecuter.LoadFunction<glfwGetWindowAttribDelegate>(NativeLibrary, nameof(glfwGetWindowAttrib));
        public static int glfwGetWindowAttrib(IntPtr window, int attrib) => _glfwGetWindowAttrib(window, attrib);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowAttribDelegate(IntPtr window, int attrib, int value);
        private static glfwSetWindowAttribDelegate _glfwSetWindowAttrib = NativeFuncExecuter.LoadFunction<glfwSetWindowAttribDelegate>(NativeLibrary, nameof(glfwSetWindowAttrib));
        public static void glfwSetWindowAttrib(IntPtr window, int attrib, int value) => _glfwSetWindowAttrib(window, attrib, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetWindowUserPointerDelegate(IntPtr window, IntPtr pointer);
        private static glfwSetWindowUserPointerDelegate _glfwSetWindowUserPointer = NativeFuncExecuter.LoadFunction<glfwSetWindowUserPointerDelegate>(NativeLibrary, nameof(glfwSetWindowUserPointer));
        public static void glfwSetWindowUserPointer(IntPtr window, IntPtr pointer) => _glfwSetWindowUserPointer(window, pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetWindowUserPointerDelegate(IntPtr window);
        private static glfwGetWindowUserPointerDelegate _glfwGetWindowUserPointer = NativeFuncExecuter.LoadFunction<glfwGetWindowUserPointerDelegate>(NativeLibrary, nameof(glfwGetWindowUserPointer));
        public static IntPtr glfwGetWindowUserPointer(IntPtr window) => _glfwGetWindowUserPointer(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowposfun glfwSetWindowPosCallbackDelegate(IntPtr window, GLFWwindowposfun callback);
        private static glfwSetWindowPosCallbackDelegate _glfwSetWindowPosCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowPosCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowPosCallback));
        public static GLFWwindowposfun glfwSetWindowPosCallback(IntPtr window, GLFWwindowposfun callback) => _glfwSetWindowPosCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowsizefun glfwSetWindowSizeCallbackDelegate(IntPtr window, GLFWwindowsizefun callback);
        private static glfwSetWindowSizeCallbackDelegate _glfwSetWindowSizeCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowSizeCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowSizeCallback));
        public static GLFWwindowsizefun glfwSetWindowSizeCallback(IntPtr window, GLFWwindowsizefun callback) => _glfwSetWindowSizeCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowclosefun glfwSetWindowCloseCallbackDelegate(IntPtr window, GLFWwindowclosefun callback);
        private static glfwSetWindowCloseCallbackDelegate _glfwSetWindowCloseCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowCloseCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowCloseCallback));
        public static GLFWwindowclosefun glfwSetWindowCloseCallback(IntPtr window, GLFWwindowclosefun callback) => _glfwSetWindowCloseCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowrefreshfun glfwSetWindowRefreshCallbackDelegate(IntPtr window, GLFWwindowrefreshfun callback);
        private static glfwSetWindowRefreshCallbackDelegate _glfwSetWindowRefreshCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowRefreshCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowRefreshCallback));
        public static GLFWwindowrefreshfun glfwSetWindowRefreshCallback(IntPtr window, GLFWwindowrefreshfun callback) => _glfwSetWindowRefreshCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowfocusfun glfwSetWindowFocusCallbackDelegate(IntPtr window, GLFWwindowfocusfun callback);
        private static glfwSetWindowFocusCallbackDelegate _glfwSetWindowFocusCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowFocusCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowFocusCallback));
        public static GLFWwindowfocusfun glfwSetWindowFocusCallback(IntPtr window, GLFWwindowfocusfun callback) => _glfwSetWindowFocusCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowiconifyfun glfwSetWindowIconifyCallbackDelegate(IntPtr window, GLFWwindowiconifyfun callback);
        private static glfwSetWindowIconifyCallbackDelegate _glfwSetWindowIconifyCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowIconifyCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowIconifyCallback));
        public static GLFWwindowiconifyfun glfwSetWindowIconifyCallback(IntPtr window, GLFWwindowiconifyfun callback) => _glfwSetWindowIconifyCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowmaximizefun glfwSetWindowMaximizeCallbackDelegate(IntPtr window, GLFWwindowmaximizefun callback);
        private static glfwSetWindowMaximizeCallbackDelegate _glfwSetWindowMaximizeCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowMaximizeCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowMaximizeCallback));
        public static GLFWwindowmaximizefun glfwSetWindowMaximizeCallback(IntPtr window, GLFWwindowmaximizefun callback) => _glfwSetWindowMaximizeCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWframebuffersizefun glfwSetFramebufferSizeCallbackDelegate(IntPtr window, GLFWframebuffersizefun callback);
        private static glfwSetFramebufferSizeCallbackDelegate _glfwSetFramebufferSizeCallback = NativeFuncExecuter.LoadFunction<glfwSetFramebufferSizeCallbackDelegate>(NativeLibrary, nameof(glfwSetFramebufferSizeCallback));
        public static GLFWframebuffersizefun glfwSetFramebufferSizeCallback(IntPtr window, GLFWframebuffersizefun callback) => _glfwSetFramebufferSizeCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWwindowcontentscalefun glfwSetWindowContentScaleCallbackDelegate(IntPtr window, GLFWwindowcontentscalefun callback);
        private static glfwSetWindowContentScaleCallbackDelegate _glfwSetWindowContentScaleCallback = NativeFuncExecuter.LoadFunction<glfwSetWindowContentScaleCallbackDelegate>(NativeLibrary, nameof(glfwSetWindowContentScaleCallback));
        public static GLFWwindowcontentscalefun glfwSetWindowContentScaleCallback(IntPtr window, GLFWwindowcontentscalefun callback) => _glfwSetWindowContentScaleCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwPollEventsDelegate();
        private static glfwPollEventsDelegate _glfwPollEvents = NativeFuncExecuter.LoadFunction<glfwPollEventsDelegate>(NativeLibrary, nameof(glfwPollEvents));
        public static void glfwPollEvents() => _glfwPollEvents();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwWaitEventsDelegate();
        private static glfwWaitEventsDelegate _glfwWaitEvents = NativeFuncExecuter.LoadFunction<glfwWaitEventsDelegate>(NativeLibrary, nameof(glfwWaitEvents));
        public static void glfwWaitEvents() => _glfwWaitEvents();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwWaitEventsTimeoutDelegate(double timeout);
        private static glfwWaitEventsTimeoutDelegate _glfwWaitEventsTimeout = NativeFuncExecuter.LoadFunction<glfwWaitEventsTimeoutDelegate>(NativeLibrary, nameof(glfwWaitEventsTimeout));
        public static void glfwWaitEventsTimeout(double timeout) => _glfwWaitEventsTimeout(timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwPostEmptyEventDelegate();
        private static glfwPostEmptyEventDelegate _glfwPostEmptyEvent = NativeFuncExecuter.LoadFunction<glfwPostEmptyEventDelegate>(NativeLibrary, nameof(glfwPostEmptyEvent));
        public static void glfwPostEmptyEvent() => _glfwPostEmptyEvent();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetInputModeDelegate(IntPtr window, int mode);
        private static glfwGetInputModeDelegate _glfwGetInputMode = NativeFuncExecuter.LoadFunction<glfwGetInputModeDelegate>(NativeLibrary, nameof(glfwGetInputMode));
        public static int glfwGetInputMode(IntPtr window, int mode) => _glfwGetInputMode(window, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetInputModeDelegate(IntPtr window, int mode, int value);
        private static glfwSetInputModeDelegate _glfwSetInputMode = NativeFuncExecuter.LoadFunction<glfwSetInputModeDelegate>(NativeLibrary, nameof(glfwSetInputMode));
        public static void glfwSetInputMode(IntPtr window, int mode, int value) => _glfwSetInputMode(window, mode, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwRawMouseMotionSupportedDelegate();
        private static glfwRawMouseMotionSupportedDelegate _glfwRawMouseMotionSupported = NativeFuncExecuter.LoadFunction<glfwRawMouseMotionSupportedDelegate>(NativeLibrary, nameof(glfwRawMouseMotionSupported));
        public static int glfwRawMouseMotionSupported() => _glfwRawMouseMotionSupported();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetKeyNameDelegate(int key, int scancode);
        private static glfwGetKeyNameDelegate _glfwGetKeyName = NativeFuncExecuter.LoadFunction<glfwGetKeyNameDelegate>(NativeLibrary, nameof(glfwGetKeyName));
        public static string glfwGetKeyName(int key, int scancode) => _glfwGetKeyName(key, scancode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetKeyScancodeDelegate(int key);
        private static glfwGetKeyScancodeDelegate _glfwGetKeyScancode = NativeFuncExecuter.LoadFunction<glfwGetKeyScancodeDelegate>(NativeLibrary, nameof(glfwGetKeyScancode));
        public static int glfwGetKeyScancode(int key) => _glfwGetKeyScancode(key);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetKeyDelegate(IntPtr window, int key);
        private static glfwGetKeyDelegate _glfwGetKey = NativeFuncExecuter.LoadFunction<glfwGetKeyDelegate>(NativeLibrary, nameof(glfwGetKey));
        public static int glfwGetKey(IntPtr window, int key) => _glfwGetKey(window, key);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetMouseButtonDelegate(IntPtr window, int button);
        private static glfwGetMouseButtonDelegate _glfwGetMouseButton = NativeFuncExecuter.LoadFunction<glfwGetMouseButtonDelegate>(NativeLibrary, nameof(glfwGetMouseButton));
        public static int glfwGetMouseButton(IntPtr window, int button) => _glfwGetMouseButton(window, button);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwGetCursorPosDelegate(IntPtr window, out double xpos, out double ypos);
        private static glfwGetCursorPosDelegate _glfwGetCursorPos = NativeFuncExecuter.LoadFunction<glfwGetCursorPosDelegate>(NativeLibrary, nameof(glfwGetCursorPos));
        public static void glfwGetCursorPos(IntPtr window, out double xpos, out double ypos) => _glfwGetCursorPos(window, out xpos, out ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetCursorPosDelegate(IntPtr window, double xpos, double ypos);
        private static glfwSetCursorPosDelegate _glfwSetCursorPos = NativeFuncExecuter.LoadFunction<glfwSetCursorPosDelegate>(NativeLibrary, nameof(glfwSetCursorPos));
        public static void glfwSetCursorPos(IntPtr window, double xpos, double ypos) => _glfwSetCursorPos(window, xpos, ypos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwCreateCursorDelegate(IntPtr image, int xhot, int yhot);
        private static glfwCreateCursorDelegate _glfwCreateCursor = NativeFuncExecuter.LoadFunction<glfwCreateCursorDelegate>(NativeLibrary, nameof(glfwCreateCursor));
        public static IntPtr glfwCreateCursor(IntPtr image, int xhot, int yhot) => _glfwCreateCursor(image, xhot, yhot);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwCreateStandardCursorDelegate(int shape);
        private static glfwCreateStandardCursorDelegate _glfwCreateStandardCursor = NativeFuncExecuter.LoadFunction<glfwCreateStandardCursorDelegate>(NativeLibrary, nameof(glfwCreateStandardCursor));
        public static IntPtr glfwCreateStandardCursor(int shape) => _glfwCreateStandardCursor(shape);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwDestroyCursorDelegate(IntPtr cursor);
        private static glfwDestroyCursorDelegate _glfwDestroyCursor = NativeFuncExecuter.LoadFunction<glfwDestroyCursorDelegate>(NativeLibrary, nameof(glfwDestroyCursor));
        public static void glfwDestroyCursor(IntPtr cursor) => _glfwDestroyCursor(cursor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetCursorDelegate(IntPtr window, IntPtr cursor);
        private static glfwSetCursorDelegate _glfwSetCursor = NativeFuncExecuter.LoadFunction<glfwSetCursorDelegate>(NativeLibrary, nameof(glfwSetCursor));
        public static void glfwSetCursor(IntPtr window, IntPtr cursor) => _glfwSetCursor(window, cursor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWkeyfun glfwSetKeyCallbackDelegate(IntPtr window, GLFWkeyfun callback);
        private static glfwSetKeyCallbackDelegate _glfwSetKeyCallback = NativeFuncExecuter.LoadFunction<glfwSetKeyCallbackDelegate>(NativeLibrary, nameof(glfwSetKeyCallback));
        public static GLFWkeyfun glfwSetKeyCallback(IntPtr window, GLFWkeyfun callback) => _glfwSetKeyCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWcharfun glfwSetCharCallbackDelegate(IntPtr window, GLFWcharfun callback);
        private static glfwSetCharCallbackDelegate _glfwSetCharCallback = NativeFuncExecuter.LoadFunction<glfwSetCharCallbackDelegate>(NativeLibrary, nameof(glfwSetCharCallback));
        public static GLFWcharfun glfwSetCharCallback(IntPtr window, GLFWcharfun callback) => _glfwSetCharCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWcharmodsfun glfwSetCharModsCallbackDelegate(IntPtr window, GLFWcharmodsfun callback);
        private static glfwSetCharModsCallbackDelegate _glfwSetCharModsCallback = NativeFuncExecuter.LoadFunction<glfwSetCharModsCallbackDelegate>(NativeLibrary, nameof(glfwSetCharModsCallback));
        public static GLFWcharmodsfun glfwSetCharModsCallback(IntPtr window, GLFWcharmodsfun callback) => _glfwSetCharModsCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWmousebuttonfun glfwSetMouseButtonCallbackDelegate(IntPtr window, GLFWmousebuttonfun callback);
        private static glfwSetMouseButtonCallbackDelegate _glfwSetMouseButtonCallback = NativeFuncExecuter.LoadFunction<glfwSetMouseButtonCallbackDelegate>(NativeLibrary, nameof(glfwSetMouseButtonCallback));
        public static GLFWmousebuttonfun glfwSetMouseButtonCallback(IntPtr window, GLFWmousebuttonfun callback) => _glfwSetMouseButtonCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWcursorposfun glfwSetCursorPosCallbackDelegate(IntPtr window, GLFWcursorposfun callback);
        private static glfwSetCursorPosCallbackDelegate _glfwSetCursorPosCallback = NativeFuncExecuter.LoadFunction<glfwSetCursorPosCallbackDelegate>(NativeLibrary, nameof(glfwSetCursorPosCallback));
        public static GLFWcursorposfun glfwSetCursorPosCallback(IntPtr window, GLFWcursorposfun callback) => _glfwSetCursorPosCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWcursorenterfun glfwSetCursorEnterCallbackDelegate(IntPtr window, GLFWcursorenterfun callback);
        private static glfwSetCursorEnterCallbackDelegate _glfwSetCursorEnterCallback = NativeFuncExecuter.LoadFunction<glfwSetCursorEnterCallbackDelegate>(NativeLibrary, nameof(glfwSetCursorEnterCallback));
        public static GLFWcursorenterfun glfwSetCursorEnterCallback(IntPtr window, GLFWcursorenterfun callback) => _glfwSetCursorEnterCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWscrollfun glfwSetScrollCallbackDelegate(IntPtr window, GLFWscrollfun callback);
        private static glfwSetScrollCallbackDelegate _glfwSetScrollCallback = NativeFuncExecuter.LoadFunction<glfwSetScrollCallbackDelegate>(NativeLibrary, nameof(glfwSetScrollCallback));
        public static GLFWscrollfun glfwSetScrollCallback(IntPtr window, GLFWscrollfun callback) => _glfwSetScrollCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWdropfun glfwSetDropCallbackDelegate(IntPtr window, GLFWdropfun callback);
        private static glfwSetDropCallbackDelegate _glfwSetDropCallback = NativeFuncExecuter.LoadFunction<glfwSetDropCallbackDelegate>(NativeLibrary, nameof(glfwSetDropCallback));
        public static GLFWdropfun glfwSetDropCallback(IntPtr window, GLFWdropfun callback) => _glfwSetDropCallback(window, callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwJoystickPresentDelegate(int jid);
        private static glfwJoystickPresentDelegate _glfwJoystickPresent = NativeFuncExecuter.LoadFunction<glfwJoystickPresentDelegate>(NativeLibrary, nameof(glfwJoystickPresent));
        public static int glfwJoystickPresent(int jid) => _glfwJoystickPresent(jid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float* glfwGetJoystickAxesDelegate(int jid, out int count);
        private static glfwGetJoystickAxesDelegate _glfwGetJoystickAxes = NativeFuncExecuter.LoadFunction<glfwGetJoystickAxesDelegate>(NativeLibrary, nameof(glfwGetJoystickAxes));
        public static float* glfwGetJoystickAxes(int jid, out int count) => _glfwGetJoystickAxes(jid, out count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetJoystickButtonsDelegate(int jid, out int count);
        private static glfwGetJoystickButtonsDelegate _glfwGetJoystickButtons = NativeFuncExecuter.LoadFunction<glfwGetJoystickButtonsDelegate>(NativeLibrary, nameof(glfwGetJoystickButtons));
        public static string glfwGetJoystickButtons(int jid, out int count) => _glfwGetJoystickButtons(jid, out count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetJoystickHatsDelegate(int jid, out int count);
        private static glfwGetJoystickHatsDelegate _glfwGetJoystickHats = NativeFuncExecuter.LoadFunction<glfwGetJoystickHatsDelegate>(NativeLibrary, nameof(glfwGetJoystickHats));
        public static string glfwGetJoystickHats(int jid, out int count) => _glfwGetJoystickHats(jid, out count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetJoystickNameDelegate(int jid);
        private static glfwGetJoystickNameDelegate _glfwGetJoystickName = NativeFuncExecuter.LoadFunction<glfwGetJoystickNameDelegate>(NativeLibrary, nameof(glfwGetJoystickName));
        public static string glfwGetJoystickName(int jid) => _glfwGetJoystickName(jid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetJoystickGUIDDelegate(int jid);
        private static glfwGetJoystickGUIDDelegate _glfwGetJoystickGUID = NativeFuncExecuter.LoadFunction<glfwGetJoystickGUIDDelegate>(NativeLibrary, nameof(glfwGetJoystickGUID));
        public static string glfwGetJoystickGUID(int jid) => _glfwGetJoystickGUID(jid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetJoystickUserPointerDelegate(int jid, IntPtr pointer);
        private static glfwSetJoystickUserPointerDelegate _glfwSetJoystickUserPointer = NativeFuncExecuter.LoadFunction<glfwSetJoystickUserPointerDelegate>(NativeLibrary, nameof(glfwSetJoystickUserPointer));
        public static void glfwSetJoystickUserPointer(int jid, IntPtr pointer) => _glfwSetJoystickUserPointer(jid, pointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetJoystickUserPointerDelegate(int jid);
        private static glfwGetJoystickUserPointerDelegate _glfwGetJoystickUserPointer = NativeFuncExecuter.LoadFunction<glfwGetJoystickUserPointerDelegate>(NativeLibrary, nameof(glfwGetJoystickUserPointer));
        public static IntPtr glfwGetJoystickUserPointer(int jid) => _glfwGetJoystickUserPointer(jid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwJoystickIsGamepadDelegate(int jid);
        private static glfwJoystickIsGamepadDelegate _glfwJoystickIsGamepad = NativeFuncExecuter.LoadFunction<glfwJoystickIsGamepadDelegate>(NativeLibrary, nameof(glfwJoystickIsGamepad));
        public static int glfwJoystickIsGamepad(int jid) => _glfwJoystickIsGamepad(jid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GLFWjoystickfun glfwSetJoystickCallbackDelegate(GLFWjoystickfun callback);
        private static glfwSetJoystickCallbackDelegate _glfwSetJoystickCallback = NativeFuncExecuter.LoadFunction<glfwSetJoystickCallbackDelegate>(NativeLibrary, nameof(glfwSetJoystickCallback));
        public static GLFWjoystickfun glfwSetJoystickCallback(GLFWjoystickfun callback) => _glfwSetJoystickCallback(callback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwUpdateGamepadMappingsDelegate(string @string);
        private static glfwUpdateGamepadMappingsDelegate _glfwUpdateGamepadMappings = NativeFuncExecuter.LoadFunction<glfwUpdateGamepadMappingsDelegate>(NativeLibrary, nameof(glfwUpdateGamepadMappings));
        public static int glfwUpdateGamepadMappings(string @string) => _glfwUpdateGamepadMappings(@string);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetGamepadNameDelegate(int jid);
        private static glfwGetGamepadNameDelegate _glfwGetGamepadName = NativeFuncExecuter.LoadFunction<glfwGetGamepadNameDelegate>(NativeLibrary, nameof(glfwGetGamepadName));
        public static string glfwGetGamepadName(int jid) => _glfwGetGamepadName(jid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwGetGamepadStateDelegate(int jid, IntPtr state);
        private static glfwGetGamepadStateDelegate _glfwGetGamepadState = NativeFuncExecuter.LoadFunction<glfwGetGamepadStateDelegate>(NativeLibrary, nameof(glfwGetGamepadState));
        public static int glfwGetGamepadState(int jid, IntPtr state) => _glfwGetGamepadState(jid, state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetClipboardStringDelegate(IntPtr window, string @string);
        private static glfwSetClipboardStringDelegate _glfwSetClipboardString = NativeFuncExecuter.LoadFunction<glfwSetClipboardStringDelegate>(NativeLibrary, nameof(glfwSetClipboardString));
        public static void glfwSetClipboardString(IntPtr window, string @string) => _glfwSetClipboardString(window, @string);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string glfwGetClipboardStringDelegate(IntPtr window);
        private static glfwGetClipboardStringDelegate _glfwGetClipboardString = NativeFuncExecuter.LoadFunction<glfwGetClipboardStringDelegate>(NativeLibrary, nameof(glfwGetClipboardString));
        public static string glfwGetClipboardString(IntPtr window) => _glfwGetClipboardString(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate double glfwGetTimeDelegate();
        private static glfwGetTimeDelegate _glfwGetTime = NativeFuncExecuter.LoadFunction<glfwGetTimeDelegate>(NativeLibrary, nameof(glfwGetTime));
        public static double glfwGetTime() => _glfwGetTime();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSetTimeDelegate(double time);
        private static glfwSetTimeDelegate _glfwSetTime = NativeFuncExecuter.LoadFunction<glfwSetTimeDelegate>(NativeLibrary, nameof(glfwSetTime));
        public static void glfwSetTime(double time) => _glfwSetTime(time);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong glfwGetTimerValueDelegate();
        private static glfwGetTimerValueDelegate _glfwGetTimerValue = NativeFuncExecuter.LoadFunction<glfwGetTimerValueDelegate>(NativeLibrary, nameof(glfwGetTimerValue));
        public static ulong glfwGetTimerValue() => _glfwGetTimerValue();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong glfwGetTimerFrequencyDelegate();
        private static glfwGetTimerFrequencyDelegate _glfwGetTimerFrequency = NativeFuncExecuter.LoadFunction<glfwGetTimerFrequencyDelegate>(NativeLibrary, nameof(glfwGetTimerFrequency));
        public static ulong glfwGetTimerFrequency() => _glfwGetTimerFrequency();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwMakeContextCurrentDelegate(IntPtr window);
        private static glfwMakeContextCurrentDelegate _glfwMakeContextCurrent = NativeFuncExecuter.LoadFunction<glfwMakeContextCurrentDelegate>(NativeLibrary, nameof(glfwMakeContextCurrent));
        public static void glfwMakeContextCurrent(IntPtr window) => _glfwMakeContextCurrent(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetCurrentContextDelegate();
        private static glfwGetCurrentContextDelegate _glfwGetCurrentContext = NativeFuncExecuter.LoadFunction<glfwGetCurrentContextDelegate>(NativeLibrary, nameof(glfwGetCurrentContext));
        public static IntPtr glfwGetCurrentContext() => _glfwGetCurrentContext();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSwapBuffersDelegate(IntPtr window);
        private static glfwSwapBuffersDelegate _glfwSwapBuffers = NativeFuncExecuter.LoadFunction<glfwSwapBuffersDelegate>(NativeLibrary, nameof(glfwSwapBuffers));
        public static void glfwSwapBuffers(IntPtr window) => _glfwSwapBuffers(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void glfwSwapIntervalDelegate(int interval);
        private static glfwSwapIntervalDelegate _glfwSwapInterval = NativeFuncExecuter.LoadFunction<glfwSwapIntervalDelegate>(NativeLibrary, nameof(glfwSwapInterval));
        public static void glfwSwapInterval(int interval) => _glfwSwapInterval(interval);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwExtensionSupportedDelegate(string extension);
        private static glfwExtensionSupportedDelegate _glfwExtensionSupported = NativeFuncExecuter.LoadFunction<glfwExtensionSupportedDelegate>(NativeLibrary, nameof(glfwExtensionSupported));
        public static int glfwExtensionSupported(string extension) => _glfwExtensionSupported(extension);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr glfwGetProcAddressDelegate(string procname);
        private static glfwGetProcAddressDelegate _glfwGetProcAddress = NativeFuncExecuter.LoadFunction<glfwGetProcAddressDelegate>(NativeLibrary, nameof(glfwGetProcAddress));
        public static IntPtr glfwGetProcAddress(string procname) => _glfwGetProcAddress(procname);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int glfwVulkanSupportedDelegate();
        private static glfwVulkanSupportedDelegate _glfwVulkanSupported = NativeFuncExecuter.LoadFunction<glfwVulkanSupportedDelegate>(NativeLibrary, nameof(glfwVulkanSupported));
        public static int glfwVulkanSupported() => _glfwVulkanSupported();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate char** glfwGetRequiredInstanceExtensionsDelegate(out uint count);
        private static glfwGetRequiredInstanceExtensionsDelegate _glfwGetRequiredInstanceExtensions = NativeFuncExecuter.LoadFunction<glfwGetRequiredInstanceExtensionsDelegate>(NativeLibrary, nameof(glfwGetRequiredInstanceExtensions));
        public static char** glfwGetRequiredInstanceExtensions(out uint count) => _glfwGetRequiredInstanceExtensions(out count);
    }
}