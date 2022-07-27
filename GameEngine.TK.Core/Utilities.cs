using OpenTK.Graphics.OpenGL4;

namespace GameEngine.TK.Core {
    public static class Utilities {
        public static int GetSizeOfVertexAttribPointerType(VertexAttribPointerType attribType) {
            switch (attribType) {
                case VertexAttribPointerType.UnsignedByte:
                    return 1;
                    break;
                case VertexAttribPointerType.UnsignedInt:
                    return 4;
                    break;
                case VertexAttribPointerType.Float:
                    return 4;
                    break;
                default:
                    return 0;
            }
        }
    }
}
