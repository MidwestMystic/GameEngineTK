using GameEngine.TK.Core;
using GameEngine.TK.Core.Management;
using GameEngine.TK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameEngine.TK {
    internal class TextureTest : Game {
        public TextureTest(string windowTitle, int initialWindowWidth, int initialWIndowHeigh) : base(windowTitle, initialWindowWidth, initialWIndowHeigh) { }

        private readonly float[] _vertices = {
            //Positions         //Colors
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, //top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, //bottom left - Blue
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  //top left - White
        };

        private uint[] _indices = {
            0, 1, 3, //First triangle
            1, 2, 3 //Second Triangle
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        private Shader _shader;
        private Texture2D _texture;

        protected override void Initialize() {

        }

        protected override void LoadContent() {
            _shader = new(Shader.ParseShader("Resources/Shaders/Texture.glsl"));
            _shader.CompileShader();
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            _texture = ResourceManager.Instance.LoadTexture("Resources/Textures/wall.jpg");
            _texture.Use();

        }
        protected override void Update(GameTime gameTime) {

        }

        protected override void Render(GameTime gameTime) {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}
