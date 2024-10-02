using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static Editor.Rendering.GL;
using static Editor.Rendering.GLFW;
using Exception = System.Exception;

namespace Editor.Rendering {
    
    /// <summary>
    /// <see cref="Window"/> is a single-instance object designed to
    /// create and maintain a GLFW Window.
    /// </summary>
    public class Window {

        #region Events

        /// <summary>
        /// Invoked before <see cref="onUpdate"/>.
        /// </summary>
        private Action? onDraw = null;
        /// <summary>
        /// Invoked before <see cref="onUpdate"/>.
        /// </summary>
        private Action? onPreUpdate = null;
        /// <summary>
        /// Invoked when it's time to update.
        /// </summary>
        private Action? onUpdate = null;
        /// <summary>
        /// Invoked after <see cref="onUpdate"/>.
        /// </summary>
        private Action? onPostUpdate = null;

        #endregion

        #region Properties

        /// <summary>
        /// The current <see cref="Window"/> instance running.
        /// </summary>
        [AllowNull]
        public static Window Instance { get; private set; }

        /// <summary>
        /// The <see cref="WindowCreationProps"/> given to create this <see cref="Window"/> instance.
        /// </summary>
        public WindowCreationProps CreationProps { get; } = default;
        /// <summary>
        /// A direct instance of the <see cref="GLFW"/> Window created.
        /// </summary>
        public IntPtr GLFWWindow { get; private set; } = default;
        
        public event Action OnDraw { add { onDraw += value; } remove { onDraw -= value; } }

        /// <inheritdoc cref="onPreUpdate"/>
        public event Action OnPreUpdate { add { onPreUpdate += value; } remove { onPreUpdate -= value; } }
        /// <inheritdoc cref="onUpdate"/>
        public event Action OnUpdate { add { onUpdate += value; } remove { onUpdate -= value; } }
        /// <inheritdoc cref="onPostUpdate"/>
        public event Action OnPostUpdate { add { onPostUpdate += value; } remove { onPostUpdate -= value; } }
        
        #endregion

        /// <summary>
        /// Create the single <see cref="Window"/> instance and set up everything GLFW/GL related.
        /// </summary>
        public Window(WindowCreationProps _props) {
            if (Instance != null) throw new Exception($"Can't create more than one instance of '{nameof(Window)}'!");

            Instance = this;
            CreationProps = _props;
            
            Initialize();
        }

        #region Window's External Methods

        /// <summary>
        /// Launch the loop maintaining the <see cref="GLFWWindow"/> previously created.
        /// </summary>
        public void Loop() {
            while (glfwWindowShouldClose(GLFWWindow) == GL_FALSE) {

                onDraw?.Invoke();
                glfwPollEvents();
                onPreUpdate?.Invoke();
                
                glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
                
                onUpdate?.Invoke();
                onPostUpdate?.Invoke();


                glfwSwapBuffers(GLFWWindow);
            }
            
            glfwTerminate();
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> containing the width and height of the current window.<br/>
        /// X being the width, and Y the height.
        /// </summary>
        public Vector2 WindowSize
        {
            get
            {
                glfwGetWindowSize(GLFWWindow, out int _width, out int _height);
                return new Vector2(_width, _height);
            }
        }

        #endregion

        #region Window's Internal Methods

        /// <summary>
        /// Initialize and create the GLFW Window based on the <see cref="CreationProps"/> set.
        /// </summary>
        private void Initialize() {
            if (GLFW.glfwInit() == 0) throw new Exception("Failed to initialize GLFW!");
            
            //Configure GLFW
            GLFW.glfwDefaultWindowHints();
            GLFW.glfwWindowHint(GLFW.GLFW_VISIBLE, 0);
            GLFW.glfwWindowHint(GLFW.GLFW_FOCUS_ON_SHOW, 1);
            GLFW.glfwWindowHint(GLFW.GLFW_RESIZABLE, Convert.ToInt32(CreationProps.IsResizable));
            GLFW.glfwWindowHint(GLFW.GLFW_MAXIMIZED, Convert.ToInt32(CreationProps.IsMaximized));
            
            //Create the GLFWWindow itself
            GLFWWindow = GLFW.glfwCreateWindow(
                CreationProps.Width,
                CreationProps.Height,
                CreationProps.Title,
                IntPtr.Zero, IntPtr.Zero);
            if (GLFWWindow == IntPtr.Zero) throw new Exception($"Failed to initialize '{nameof(Window)}'!");
            
            //TODO: Setup here the GLFW's callbacks (Mouse, Keyboard, Resize, ...)
            //For now, ImGui manage these events on its own, so there's maybe no purpose 
            //of catching these callbacks here afterall...
            
            glfwMakeContextCurrent(GLFWWindow);
            GL.LoadEntryPoints();
            
            glfwSwapInterval(Convert.ToInt32(CreationProps.SwapInterval));
            
            //Make the GLFWWindow visible
            glfwShowWindow(GLFWWindow);
            
            //Enable to Alpha Blend
            glEnable(GL_BLEND);
            glBlendFunc(GL_ONE, GL_ONE_MINUS_SRC_ALPHA);
        }

        #endregion

    }
    
}