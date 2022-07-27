using GameEngine.TK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;


namespace GameEngine.TK.Core.Management {
    public static class TextureFactory {
        private static int _textureCursor = 0;
        public static Texture2D Load(string textureName) {
            int handle = GL.GenTexture();
            Enum.TryParse(typeof(TextureUnit), $"Texture{_textureCursor}", out var result);
            if(result == null) {
                throw new Exception($"Exceeded Maximum Texture Slots OpenGL Can Natively Support. Count: {_textureCursor}");
            }
            TextureUnit textureUnit = ((TextureUnit)result);
            GL.ActiveTexture(textureUnit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
            using var image = new Bitmap(textureName);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            _textureCursor++;
            return new Texture2D(handle, image.Width, image.Height, textureUnit);

        }
    }
}
