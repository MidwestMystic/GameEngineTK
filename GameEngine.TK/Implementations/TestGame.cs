using GameEngine.TK.Core;
using GameEngine.TK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameEngine.TK {
    internal class TestGame : Game {
        public TestGame(string windowTitle, int initialWindowWidth, int initialWIndowHeigh) : base(windowTitle, initialWindowWidth, initialWIndowHeigh) { }

        private readonly float[] _vertices = {
            //Positions x,y,z   //Colors
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, //Bottom left - Red
             0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, //Bottom right - Green
             0.0f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f  //Top vertex - Blue
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader;

        protected override void Initialize() {

        }

        protected override void LoadContent() {
            _shader = new(Shader.ParseShader("Resources/Shaders/Default.glsl"));
            _shader.CompileShader();
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

        }
        protected override void Update(GameTime gameTime) {
            
        }

        protected override void Render(GameTime gameTime) {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}
