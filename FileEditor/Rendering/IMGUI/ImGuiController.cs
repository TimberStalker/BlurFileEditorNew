using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiNET;
using static Editor.Rendering.GLFW;
using static Editor.Rendering.GL;
using static Editor.Utils.GLUtils;

namespace Editor.Rendering.IMGUI {

    /// <summary>
    /// <see cref="ImGuiController"/> is a single-instance object designed to
    /// initialize and manage the core of IMGUI.NET.
    /// </summary>
    /// <remarks>
    /// <see cref="ImGuiController"/> must be initialized after <see cref="Window"/>'s initialization.
    /// </remarks>
    public class ImGuiController {

        #region Shaders (TODO: Implement that somewhere else)

        /// <summary>
        /// The (<i>literal</i>) vertex shader code used by ImGui.
        /// </summary>
        private readonly string vertexShaderSrc = @"#version 330 core
            layout (location = 0) in vec2 Position;
            layout (location = 1) in vec2 UV;
            layout (location = 2) in vec4 Color;
            uniform mat4 ProjMtx;
            out vec2 Frag_UV;
            out vec4 Frag_Color;
            void main()
            {
                Frag_UV = UV;
                Frag_Color = Color;
                gl_Position = ProjMtx * vec4(Position.xy,0,1);
            }";
        
        /// <summary>
        /// The (<i>literal</i>) fragment shader code used by ImGui.
        /// </summary>
        private readonly string fragmentShaderSrc = @"#version 330 core
            in vec2 Frag_UV;
            in vec4 Frag_Color;
            uniform sampler2D Texture;
            layout (location = 0) out vec4 Out_Color;
            void main()
            {
                Out_Color = Frag_Color * texture(Texture, Frag_UV.st);
            }";

        #endregion
        
        #region Attributes

        /// <summary>
        /// Pointer to the ImGui's IO.
        /// </summary>
        private ImGuiIOPtr io;

        private double time;
        private readonly bool[] justPressedMouseBtns;
        private readonly IntPtr[] mouseCursors;

        private uint fontTexture;
        private uint shaderHandle, vertHandle, fragHandle;
        private int attribLocationTex, attribLocationProjMtx;
        private uint attribLocationVtxPos, attribLocationVtxUv, attribLocationVtxColor;
        private uint vboHandle, elementsHandle;
        
        //Marshal forced me to use this, otherwise everything will crash upon init
        private GLFWmousebuttonfun? userCallbackMouseButton;
        private GLFWscrollfun? userCallbackScroll;
        private GLFWkeyfun? userCallbackKey;
        private GLFWcharfun? userCallbackChar;

        private delegate void SetClipboardTextFunc(IntPtr _userData, string _text);
        private delegate void GetClipboardTextFunc(IntPtr _userData);

        private readonly SetClipboardTextFunc setClipboardTextFunc;
        private readonly GetClipboardTextFunc getClipboardTextFunc;

        #endregion

        #region Properties

        /// <summary>
        /// The current <see cref="ImGuiController"/> instance running.
        /// </summary>
        public static ImGuiController Instance { get; private set; } = null;

        #endregion

        /// <summary>
        /// Create the single <see cref="ImGuiController"/> instance and set up everything ImGui-related.
        /// </summary>
        public ImGuiController() {
            if (Instance != null) throw new Exception($"Can't create more than one instance '{nameof(ImGuiController)}'!");
            if (Window.Instance == null) throw new Exception($"'{nameof(Window)}' must be initialized in order to setup '{nameof(ImGuiController)}'!");

            Instance = this;

            ImGui.CreateContext();
            ImGui.StyleColorsDark();
            
            io = ImGui.GetIO();
            justPressedMouseBtns = new bool[(int)ImGuiMouseButton.COUNT];
            mouseCursors = new IntPtr[(int)ImGuiMouseCursor.COUNT];
            
            setClipboardTextFunc = (_data, _text) => glfwSetClipboardString(_data, _text);
            getClipboardTextFunc = (_data) => glfwGetClipboardString(_data);
            
            SetupImGuiIOFlags();
            SetupInputCallbacks();
            SetupDeviceObjects();
            
            Window.Instance.OnPreUpdate += InternalUpdate;
            Window.Instance.OnPostUpdate += Render;
        }

        #region ImGuiController's Internal Methods

        /// <summary>
        /// Update the <see cref="ImGuiController"/>.
        /// </summary>
        private void InternalUpdate() {
            if (!io.Fonts.IsBuilt()) throw new Exception("ImGui Fonts aren't built!");
            
            glfwGetWindowSize(Window.Instance.GLFWWindow, out int _width, out int _height);
            glfwGetFramebufferSize(Window.Instance.GLFWWindow, out int _fbWidth, out int _fbHeight);
            
            io.DisplaySize = new Vector2(_width, _height);
            if (_width > 0 && _height > 0) io.DisplayFramebufferScale = new Vector2((float)_fbWidth/_width, (float)_fbHeight/_height);

            double _currentTime = glfwGetTime();
            io.DeltaTime = time > 0.0 ? (float)(_currentTime - time) : 1.0f / 60.0f;
            time = _currentTime;
            
            UpdateMousePosAndButtons();
            UpdateMouseCursor();
            
            ImGui.NewFrame();
        }

        /// <summary>
        /// Render ImGui's stuff.
        /// </summary>
        /// <remarks>
        /// Must be done after <see cref="InternalUpdate"/>.
        /// </remarks>
        private void Render() {
            ImGui.Render();
            RenderDrawData(ImGui.GetDrawData());
        }

        /// <summary>
        /// Set the "<i>Backend Flags</i>" of the <see cref="io"/>.
        /// </summary>
        private void SetupImGuiIOFlags() {
            io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors;
            io.BackendFlags |= ImGuiBackendFlags.HasSetMousePos;
            io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;
            io.BackendFlags |= ImGuiBackendFlags.RendererHasViewports;
            
            io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
        }

        /// <summary>
        /// Set up GLFW-related callbacks used by ImGui.
        /// </summary>
        private void SetupInputCallbacks() {
            //Set up the IO's KeyMap
            io.KeyMap[(int)ImGuiKey.Tab] = GLFW_KEY_TAB;
            
            io.KeyMap[(int)ImGuiKey.LeftArrow] = GLFW_KEY_LEFT;
            io.KeyMap[(int)ImGuiKey.RightArrow] = GLFW_KEY_RIGHT;
            io.KeyMap[(int)ImGuiKey.UpArrow] = GLFW_KEY_RIGHT;
            io.KeyMap[(int)ImGuiKey.DownArrow] = GLFW_KEY_DOWN;
            
            io.KeyMap[(int)ImGuiKey.PageUp] = GLFW_KEY_PAGE_UP;
            io.KeyMap[(int)ImGuiKey.PageDown] = GLFW_KEY_PAGE_DOWN;
            
            io.KeyMap[(int)ImGuiKey.Home] = GLFW_KEY_HOME;
            io.KeyMap[(int)ImGuiKey.End] = GLFW_KEY_END;
            io.KeyMap[(int)ImGuiKey.Insert] = GLFW_KEY_INSERT;
            io.KeyMap[(int)ImGuiKey.Delete] = GLFW_KEY_DELETE;
            
            io.KeyMap[(int)ImGuiKey.Backspace] = GLFW_KEY_BACKSPACE;
            io.KeyMap[(int)ImGuiKey.Space] = GLFW_KEY_SPACE;
            io.KeyMap[(int)ImGuiKey.Enter] = GLFW_KEY_ENTER;
            io.KeyMap[(int)ImGuiKey.Escape] = GLFW_KEY_ESCAPE;
            io.KeyMap[(int)ImGuiKey.KeyPadEnter] = GLFW_KEY_KP_ENTER;
            
            io.KeyMap[(int)ImGuiKey.A] = GLFW_KEY_A;
            io.KeyMap[(int)ImGuiKey.C] = GLFW_KEY_C;
            io.KeyMap[(int)ImGuiKey.V] = GLFW_KEY_V;
            io.KeyMap[(int)ImGuiKey.X] = GLFW_KEY_X;
            io.KeyMap[(int)ImGuiKey.Y] = GLFW_KEY_Y;
            io.KeyMap[(int)ImGuiKey.Z] = GLFW_KEY_Z;
            
            //Set up ImGui's clipboard stuff
            io.SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(setClipboardTextFunc);
            io.GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(getClipboardTextFunc);
            io.ClipboardUserData = Window.Instance.GLFWWindow;
            
            //Set up the Mouse Cursors
            mouseCursors[(int)ImGuiMouseCursor.Arrow] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.TextInput] = glfwCreateStandardCursor(GLFW_IBEAM_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.ResizeNS] = glfwCreateStandardCursor(GLFW_VRESIZE_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.ResizeEW] = glfwCreateStandardCursor(GLFW_HRESIZE_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.Hand] = glfwCreateStandardCursor(GLFW_HAND_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.ResizeAll] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.ResizeNESW] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.ResizeNWSE] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
            mouseCursors[(int)ImGuiMouseCursor.NotAllowed] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
            
            //Set up GLFW's Inputs Callbacks
            //Yes, I'm forced to use this method (delegates) because otherwise
            //"Marshal" will shit himself...
            userCallbackMouseButton = MouseButtonCallback;
            userCallbackScroll = ScrollCallback;
            userCallbackKey = KeyCallback;
            userCallbackChar = CharCallback;
            
            glfwSetMouseButtonCallback(Window.Instance.GLFWWindow, userCallbackMouseButton);
            glfwSetScrollCallback(Window.Instance.GLFWWindow, userCallbackScroll);
            glfwSetKeyCallback(Window.Instance.GLFWWindow, userCallbackKey);
            glfwSetCharCallback(Window.Instance.GLFWWindow, userCallbackChar);
        }

        /// <summary>
        /// Set up and create the GL Objects that ImGui'll later use to render itself.
        /// </summary>
        private void SetupDeviceObjects() {
            glGetIntegerv(GL_TEXTURE_BINDING_2D, out int lastTexture);
            glGetIntegerv(GL_ARRAY_BUFFER_BINDING, out int lastArrayBuffer);
            glGetIntegerv(GL_VERTEX_ARRAY_BINDING, out int lastVertexArray);

            //Create the Vertex shader from the source
            vertHandle = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vertHandle, 1, new[] {vertexShaderSrc}, vertexShaderSrc.Length);
            glCompileShader(vertHandle);
            CheckShader(vertHandle); //Verify our result

            //Create the Fragment shader from the source
            fragHandle = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fragHandle, 1, new[] {fragmentShaderSrc}, fragmentShaderSrc.Length);
            glCompileShader(fragHandle);
            CheckShader(fragHandle); //Verify our result

            //Create our ShaderProgram that'll reunite our frag/vert shaders :)
            shaderHandle = glCreateProgram();
            glAttachShader(shaderHandle, vertHandle);
            glAttachShader(shaderHandle, fragHandle);
            glLinkProgram(shaderHandle);
            CheckProgram(shaderHandle); //Verify our result

            //Now, get the location of the attributes set in our frag/vert shaders
            attribLocationTex = glGetUniformLocation(shaderHandle, "Texture");
            attribLocationProjMtx = glGetUniformLocation(shaderHandle, "ProjMtx");
            attribLocationVtxPos = (uint)glGetAttribLocation(shaderHandle, "Position");
            attribLocationVtxUv = (uint)glGetAttribLocation(shaderHandle, "UV");
            attribLocationVtxColor = (uint)glGetAttribLocation(shaderHandle, "Color");

            //Generate some buffer (?) (idk tbh)
            glGenBuffers(1, out vboHandle);
            glGenBuffers(1, out elementsHandle);

            CreateFontTexture(); //Create the texture for the ImGui's font

            glBindTexture(GL_TEXTURE_2D, (uint) lastTexture);
            glBindBuffer(GL_ARRAY_BUFFER, (uint) lastArrayBuffer);
            glBindVertexArray((uint) lastVertexArray);
        }

        /// <summary>
        /// Create the font texture ImGui will use to display beautiful letters to our users :).
        /// </summary>
        private void CreateFontTexture() {
            io.Fonts.GetTexDataAsRGBA32(out IntPtr _pixels, out int _width, out int _height);
            
            glGetIntegerv(GL_TEXTURE_BINDING_2D, out int _lastTexture);
            glGenTextures(1, out fontTexture);
            glBindTexture(GL_TEXTURE_2D, fontTexture);
            
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            
            glPixelStorei(GL_UNPACK_ROW_LENGTH, 0);
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, _width, _height, 0, GL_RGBA, GL_UNSIGNED_BYTE, _pixels);
            io.Fonts.SetTexID((IntPtr)fontTexture);
            glBindTexture(GL_TEXTURE_2D, (uint)_lastTexture);
        }

        /// <summary>
        /// Update (or synchronize) the mouse buttons/position between ImGui's IO and GLFW
        /// and what's been previously detected with GLFW's callbacks.
        /// </summary>
        private void UpdateMousePosAndButtons() {
            for (int i=0; i<io.MouseDown.Count; i++) { //Sync every mouse button, and clear this shit up!
                io.MouseDown[i] = justPressedMouseBtns[i] || glfwGetMouseButton(Window.Instance.GLFWWindow, i) != 0;
                justPressedMouseBtns[i] = false;
            }
            
            //Set this stuff about uh... the uh... mouse pos thingy
            Vector2 _previousMousePos = io.MousePos;
            io.MousePos = new Vector2(-float.MaxValue, -float.MaxValue);
            if (glfwGetWindowAttrib(Window.Instance.GLFWWindow, GLFW_FOCUSED) == GL_FALSE) return; //If unfocused

            if (io.WantSetMousePos) {
                glfwSetCursorPos(Window.Instance.GLFWWindow, _previousMousePos.X, _previousMousePos.Y);
            } else {
                glfwGetCursorPos(Window.Instance.GLFWWindow, out double _mouseX, out double _mouseY);
                io.MousePos = new Vector2((float)_mouseX, (float)_mouseY);
            }
        }

        /// <summary>
        /// Update (or synchronize) the mouse cursor between ImGui's IO and GLFW.
        /// </summary>
        private void UpdateMouseCursor() {
            bool _flag = (io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0;
            if (_flag || glfwGetInputMode(Window.Instance.GLFWWindow, GLFW_CURSOR) == GLFW_CURSOR_DISABLED) return;

            ImGuiMouseCursor _imguiCursor = ImGui.GetMouseCursor();

            if (_imguiCursor == ImGuiMouseCursor.None || io.MouseDrawCursor) {
                glfwSetInputMode(Window.Instance.GLFWWindow, GLFW_CURSOR, GLFW_CURSOR_HIDDEN);
            } else {
                glfwSetCursor(Window.Instance.GLFWWindow,
                    mouseCursors[(int)_imguiCursor] != IntPtr.Zero ? mouseCursors[(int)_imguiCursor] : mouseCursors[(int)ImGuiMouseCursor.Arrow]);
                glfwSetInputMode(Window.Instance.GLFWWindow, GLFW_CURSOR, GLFW_CURSOR_NORMAL);
            }
        }

        /// <summary>
        /// Set up the context for ImGui's rendering.
        /// </summary>
        private void SetupRenderContext(ImDrawDataPtr _drawData, int _fbWidth, int _fbHeight, uint _vertexArrayObject) {
            glEnable(GL_BLEND);
            glBlendEquation(GL_FUNC_ADD);
            glBlendFuncSeparate(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA, GL_ONE, GL_ONE_MINUS_SRC_ALPHA);
            glDisable(GL_CULL_FACE);
            glDisable(GL_DEPTH_TEST);
            glDisable(GL_STENCIL_TEST);
            glEnable(GL_SCISSOR_TEST);
            glDisable(GL_PRIMITIVE_RESTART);
            glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
            
            glViewport(0, 0, _fbWidth, _fbHeight);
            float _left = _drawData.DisplayPos.X;
            float _right = _drawData.DisplayPos.X + _drawData.DisplaySize.X;
            float _top = _drawData.DisplayPos.Y;
            float _bottom = _drawData.DisplayPos.Y + _drawData.DisplaySize.Y;

            float[,] _orthoProj = new[,] {
                {2.0f / (_right - _left), 0.0f, 0.0f, 0.0f},
                {0.0f, 2.0f / (_top - _bottom), 0.0f, 0.0f},
                {0.0f, 0.0f, -1.0f, 0.0f},
                {(_right + _left) / (_left - _right), (_top + _bottom) / (_bottom - _top), 0.0f, 1.0f},
            };
            
            //Input our values into the shader previously made
            glUseProgram(shaderHandle);
            glUniform1i(attribLocationTex, 0);
            glUniformMatrix4fv(attribLocationProjMtx, 1, false, _orthoProj[0, 0]);
            
            glBindSampler(0, 0);

            glBindVertexArray(_vertexArrayObject);

            glBindBuffer(GL_ARRAY_BUFFER, vboHandle);
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementsHandle);
            glEnableVertexAttribArray(attribLocationVtxPos);
            glEnableVertexAttribArray(attribLocationVtxUv);
            glEnableVertexAttribArray(attribLocationVtxColor);
            
            glVertexAttribPointer(attribLocationVtxPos, 2, GL_FLOAT, false, Unsafe.SizeOf<ImDrawVert>(),
                Marshal.OffsetOf<ImDrawVert>("pos"));
            glVertexAttribPointer(attribLocationVtxUv, 2, GL_FLOAT, false, Unsafe.SizeOf<ImDrawVert>(),
                Marshal.OffsetOf<ImDrawVert>("uv"));
            glVertexAttribPointer(attribLocationVtxColor, 4, GL_UNSIGNED_BYTE, true, Unsafe.SizeOf<ImDrawVert>(),
                Marshal.OffsetOf<ImDrawVert>("col"));
        }

        /// <summary>
        /// Render the <see cref="ImDrawData"/> kept from us by ImGui...
        /// </summary>
        private void RenderDrawData(ImDrawDataPtr _drawData) {
            if (_drawData.CmdListsCount == 0) return; //Nothing to render
            
            int _fbWidth = (int)(_drawData.DisplaySize.X * _drawData.FramebufferScale.X);
            int _fbHeight = (int)(_drawData.DisplaySize.Y * _drawData.FramebufferScale.Y);
            if (_fbWidth <= 0 || _fbHeight <= 0) return; //Too smol to render anything :(
            
            glGetIntegerv(GL_ACTIVE_TEXTURE, out int _lastActiveTexture);
            glActiveTexture(GL_TEXTURE0);
            glGetIntegerv(GL_CURRENT_PROGRAM, out int _lastProgram);
            glGetIntegerv(GL_TEXTURE_BINDING_2D, out int _lastTexture);
            glGetIntegerv(GL_SAMPLER_BINDING, out int _lastSampler);
            glGetIntegerv(GL_VERTEX_ARRAY_BINDING, out int _lastVertexArrayObject);
            glGetIntegerv(GL_ARRAY_BUFFER_BINDING, out int _lastArrayBuffer);
            glGetIntegerv(GL_POLYGON_MODE, out int _lastPolygonMode);
            glGetIntegerv(GL_VIEWPORT, out int _lastViewport);
            glGetIntegerv(GL_SCISSOR_BOX, out int _lastScissorBox);
            glGetIntegerv(GL_BLEND_SRC_RGB, out int _lastBlendSrcRgb);
            glGetIntegerv(GL_BLEND_DST_RGB, out int _lastBlendDstRgb);
            glGetIntegerv(GL_BLEND_SRC_ALPHA, out int _lastBlendSrcAlpha);
            glGetIntegerv(GL_BLEND_DST_ALPHA, out int _lastBlendDstAlpha);
            glGetIntegerv(GL_BLEND_EQUATION_RGB, out int _lastBlendEquationRgb);
            glGetIntegerv(GL_BLEND_EQUATION_ALPHA, out int _lastBlendEquationAlpha);
            
            //Keep track of the old stuff
            bool _lastEnableBlend = glIsEnabled(GL_BLEND);
            bool _lastEnableCullFace = glIsEnabled(GL_CULL_FACE);
            bool _lastEnableDepthTest = glIsEnabled(GL_DEPTH_TEST);
            bool _lastEnableStencilTest = glIsEnabled(GL_STENCIL_TEST);
            bool _lastEnableScissorTest = glIsEnabled(GL_SCISSOR_TEST);
            bool _lastEnablePrimitiveRestart = glIsEnabled(GL_PRIMITIVE_RESTART);
            
            glGenVertexArrays(1, out uint _vertexArrayObject);
            SetupRenderContext(_drawData, _fbWidth, _fbHeight, _vertexArrayObject);
            
            Vector2 _clipOff = _drawData.DisplayPos;
            Vector2 _clipScale = _drawData.FramebufferScale;

            for (int i=0; i<_drawData.CmdListsCount; i++) {
                ImDrawListPtr _cmdList = _drawData.CmdListsRange[i];
                
                glBufferData(GL_ARRAY_BUFFER, (ulong) (_cmdList.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>()),
                    _cmdList.VtxBuffer.Data, GL_STREAM_DRAW);
                glBufferData(GL_ELEMENT_ARRAY_BUFFER, (ulong) _cmdList.IdxBuffer.Size * sizeof(ushort),
                    _cmdList.IdxBuffer.Data, GL_STREAM_DRAW);

                for (int y=0; y<_cmdList.CmdBuffer.Size; y++) {
                    ImDrawCmdPtr _cmdPtr = _cmdList.CmdBuffer[y];
                    
                    if (_cmdPtr.UserCallback != IntPtr.Zero) throw new NotImplementedException("ImDrawCmdPtr doesn't currently support 'UserCallback'!");

                    Vector4 _clipRect = new Vector4() {
                        X = (_cmdPtr.ClipRect.X - _clipOff.X) * _clipScale.X,
                        Y = (_cmdPtr.ClipRect.Y - _clipOff.Y) * _clipScale.Y,
                        Z = (_cmdPtr.ClipRect.Z - _clipOff.X) * _clipScale.X,
                        W = (_cmdPtr.ClipRect.W - _clipOff.Y) * _clipScale.Y
                    };
                    
                    if (!(_clipRect.X < _fbWidth) || !(_clipRect.Y < _fbHeight) || 
                        !(_clipRect.Z >= 0.0f) || !(_clipRect.W >= 0.0f)) continue;
                    
                    glScissor((int) _clipRect.X, (int) (_fbHeight - _clipRect.W), (int) (_clipRect.Z - _clipRect.X), (int) (_clipRect.W - _clipRect.Y));
                    
                    glBindTexture(GL_TEXTURE_2D, (uint)_cmdPtr.TextureId);
                    glDrawElementsBaseVertex(GL_TRIANGLES, (int)_cmdPtr.ElemCount, GL_UNSIGNED_SHORT, (IntPtr)(_cmdPtr.IdxOffset * sizeof(ushort)), (int) _cmdPtr.VtxOffset);
                }
            }
            
            //Set everything as before :)
            glDeleteVertexArrays(1, _vertexArrayObject);

            glUseProgram((uint) _lastProgram);
            glBindTexture(GL_TEXTURE_2D, (uint)_lastTexture);
            glBindSampler(0, (uint)_lastSampler);
            glActiveTexture((uint)_lastActiveTexture);
            glBindVertexArray((uint)_lastVertexArrayObject);
            glBindBuffer(GL_ARRAY_BUFFER, (uint)_lastArrayBuffer);
            glBlendEquationSeparate((uint)_lastBlendEquationRgb, (uint)_lastBlendEquationAlpha);
            glBlendFuncSeparate((uint)_lastBlendSrcRgb, (uint)_lastBlendDstRgb, (uint)_lastBlendSrcAlpha, (uint)_lastBlendDstAlpha);
            
            if (_lastEnableBlend) glEnable(GL_BLEND); else glDisable(GL_BLEND);
            if (_lastEnableCullFace) glEnable(GL_CULL_FACE); else glDisable(GL_CULL_FACE);
            if (_lastEnableDepthTest) glEnable(GL_DEPTH_TEST); else glDisable(GL_DEPTH_TEST);
            if (_lastEnableStencilTest) glEnable(GL_STENCIL_TEST); else glDisable(GL_STENCIL_TEST);
            if (_lastEnableScissorTest) glEnable(GL_SCISSOR_TEST); else glDisable(GL_SCISSOR_TEST);
            if (_lastEnablePrimitiveRestart) glEnable(GL_PRIMITIVE_RESTART); else glDisable(GL_PRIMITIVE_RESTART);
            
            glPolygonMode(GL_FRONT_AND_BACK, (uint)_lastPolygonMode);
            glViewport(_lastViewport, _lastViewport, _lastViewport, _lastViewport);
            glScissor(_lastScissorBox, _lastScissorBox, _lastScissorBox, _lastScissorBox);
        }
        
        #endregion

        #region ImGUIController's Internal (GLFW) Callbacks Methods

        /// <inheritdoc cref="GLFW.glfwSetMouseButtonCallback"/>
        private void MouseButtonCallback(IntPtr _, int _button, int _action, int _mods) {
            if (_action == GLFW_PRESS && _button >= 0 && _button < justPressedMouseBtns.Length)
                justPressedMouseBtns[_button] = true;
        }

        /// <inheritdoc cref="GLFW.glfwSetScrollCallback"/>
        private void ScrollCallback(IntPtr _, double _xOffset, double _yOffset) {
            io.MouseWheelH += (float)_xOffset;
            io.MouseWheel += (float)_yOffset;
        }

        /// <inheritdoc cref="GLFW.glfwSetKeyCallback"/>
        private void KeyCallback(IntPtr _, int _key, int _scanCode, int _action, int _mods) {
            if(_key != -1)
            {
                if (_action == GLFW_PRESS) io.KeysDown[_key] = true;
                else if (_action == GLFW_RELEASE) io.KeysDown[_key] = false;
            }
            
            io.KeyCtrl = io.KeysDown[GLFW_KEY_LEFT_CONTROL] || io.KeysDown[GLFW_KEY_RIGHT_CONTROL];
            io.KeyShift = io.KeysDown[GLFW_KEY_LEFT_SHIFT] || io.KeysDown[GLFW_KEY_RIGHT_SHIFT];
            io.KeyAlt = io.KeysDown[GLFW_KEY_LEFT_ALT] || io.KeysDown[GLFW_KEY_RIGHT_ALT];
            io.KeySuper = io.KeysDown[GLFW_KEY_LEFT_SUPER] || io.KeysDown[GLFW_KEY_RIGHT_SUPER];
        }

        /// <inheritdoc cref="GLFW.glfwSetCharCallback"/>
        private void CharCallback(IntPtr _, uint _char) {
            io.AddInputCharacter(_char);
        }
        
        #endregion
        
    }
    
}