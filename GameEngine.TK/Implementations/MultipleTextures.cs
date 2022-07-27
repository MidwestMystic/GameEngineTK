using GameEngine.TK.Core;
using GameEngine.TK.Core.Management;
using GameEngine.TK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using GameEngine.TK.Core.Rendering;

namespace GameEngine.TK {
    internal class MultipleTextures : Game {
        public MultipleTextures(string windowTitle, int initialWindowWidth, int initialWIndowHeigh) : base(windowTitle, initialWindowWidth, initialWIndowHeigh) { }

        private readonly float[] _vertices = {
            //Positions         
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, //top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, //bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0.0f, //bottom left - Blue
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f  //top left - White
        };

        private uint[] _indices = {
            0, 1, 3, //First triangle
            1, 2, 3 //Second Triangle
        };

        private VertexBuffer _vertexBuffer;
        //private int _vertexBufferObject;
        private VertexArray _vertexArray;
        //private int _vertexArrayObject;
        private IndexBuffer _indexBuffer;

        private Shader _shader;
        protected override void Initialize() {

        }

        protected override void LoadContent() {
            _shader = new(Shader.ParseShader("Resources/Shaders/TextureWithColorAndTextureSlot.glsl"));
            if (!_shader.CompileShader()) {
                Console.WriteLine("Failed to compile shader");
                return;
            }
            _vertexArray = new();
            _vertexBuffer = new VertexBuffer(_vertices);

            BufferLayout layout = new();
            //Positions
            layout.Add<float>(3);
            //Texture Coords
            layout.Add<float>(2);
            //Color
            layout.Add<float>(3);
            //TextureSlot
            layout.Add<float>(1);

            _vertexArray.AddBuffer(_vertexBuffer, layout);
            _shader.Use();
            _indexBuffer = new IndexBuffer(_indices);
            var textureSampleUniformLocation = _shader.GetUniformLocation("u_Texture[0]");
            int[] samplers = new int[2] { 0, 1 };
            GL.Uniform1(textureSampleUniformLocation, 2, samplers);

            ResourceManager.Instance.LoadTexture("Resources/Textures/wall.jpg");
            ResourceManager.Instance.LoadTexture("Resources/Textures/awesomeface.png");

            //_texture = ResourceManager.Instance.LoadTexture("Resources/Textures/wall.jpg");
            //_texture.Use();

        }
        protected override void Update(GameTime gameTime) {

        }

        protected override void Render(GameTime gameTime) {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);
            _shader.Use();
            _vertexArray.Bind();
            _indexBuffer.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
