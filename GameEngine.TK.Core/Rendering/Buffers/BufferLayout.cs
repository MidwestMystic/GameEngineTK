using OpenTK.Graphics.OpenGL4;

namespace GameEngine.TK.Core.Rendering {
    public class BufferLayout {
        private List<BufferElement> _elements = new();
        private int _stride;
        public BufferLayout() {
            _stride = 0;
        }

        public List<BufferElement> GetBufferElements() => _elements;
        public int GetStride() => _stride;

        public void Add<T>(int count, bool normalized = false) where T : struct {
            VertexAttribPointerType type;
            if(typeof(float) == typeof(T)) {
                type = VertexAttribPointerType.Float;
                _stride += sizeof(float) * count;
            }
            else if(typeof(uint) == typeof(T)) {
                type = VertexAttribPointerType.UnsignedByte;
                _stride += sizeof(uint) * count;
            }
            else if(typeof(byte) == typeof(T)) {
                type = VertexAttribPointerType.UnsignedByte;
                _stride += sizeof(byte) * count;
            }
            else {
                throw new ArgumentException($"{typeof(T)} is Not a Valid Type");
            }
            _elements.Add(new BufferElement { Type = type, Count = count, Normalized = normalized });

        }

    }
}
