using OpenTK.Graphics.OpenGL4;

namespace GameEngine.TK.Core.Rendering {
    public struct BufferElement {
        public VertexAttribPointerType Type;
        public int Count;
        public bool Normalized;
    }
}
