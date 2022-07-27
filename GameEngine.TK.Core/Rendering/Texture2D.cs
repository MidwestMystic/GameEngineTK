using OpenTK.Graphics.OpenGL4;

namespace GameEngine.TK.Core.Rendering {
    public class Texture2D : IDisposable {
        private bool _disposed;
        public int Handle { get; private set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public TextureUnit TextureSlot { get; set; } = TextureUnit.Texture0;
        public Texture2D(int handle) {
            Handle = handle;
        }

        public Texture2D(int handle, int width, int height) : this(handle) {
            Width = width;
            Height = height;
        }

        public Texture2D(int handle, int width, int heigh, TextureUnit textureSlot) : this(handle, width, heigh) {
            TextureSlot = textureSlot;
        }

        ~Texture2D() {
            Dispose(false);
        }

        public void Use() {
            GL.ActiveTexture(TextureSlot);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Dispose(bool disposing) {
            if(!_disposed) {
                GL.DeleteTexture(Handle);
                _disposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
