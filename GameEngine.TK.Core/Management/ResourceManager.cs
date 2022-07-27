using GameEngine.TK.Core.Rendering;

namespace GameEngine.TK.Core.Management {
    public sealed class ResourceManager {
        private static ResourceManager _instance = null;
        private static readonly object _loc = new();
        private IDictionary<string, Texture2D> _textureCache = new Dictionary<string, Texture2D>();

        public static ResourceManager Instance {
            get {
                lock(_loc) {
                    if(_instance == null) {
                        _instance = new ResourceManager();
                    }
                    return _instance;
                }                
            }
        }

        public Texture2D LoadTexture(string textureName) {
            _textureCache.TryGetValue(textureName, out var value);
            if(value is not null) {
                return value;
            }
            value = TextureFactory.Load(textureName);
            _textureCache.Add(textureName, value);
            return value;
        }

    }
}
