using GameEngine.TK.Core.Management;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GameEngine.TK.Core {
    public abstract class Game {

        protected string WindowTitle { get; set; }
        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }

        private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
        private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

        public Game(string windowTitle, int initialWindowWidth, int initialWIndowHeight) {
            WindowTitle = windowTitle;
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWIndowHeight;
            _nativeWindowSettings.Size = new Vector2i(initialWindowWidth, initialWIndowHeight);
            _nativeWindowSettings.Title = windowTitle;
            _nativeWindowSettings.API = ContextAPI.OpenGL;

            _gameWindowSettings.RenderFrequency = 60.0;
            _gameWindowSettings.UpdateFrequency = 60.0;
        }

        public void Run() {
            Initialize();
            using GameWindow gameWindow = DisplayManager.Instance.CreateWindow(_gameWindowSettings, _nativeWindowSettings);
            GameTime gameTime = new();
            gameWindow.Load += LoadContent;
            gameWindow.UpdateFrame += (FrameEventArgs eventArgs) => {
                double time = eventArgs.Time;
                gameTime.ElapsedGameTime = TimeSpan.FromMilliseconds(time);
                gameTime.TotalGameTime += TimeSpan.FromMilliseconds(time);
                Update(gameTime);
            };
            gameWindow.RenderFrame += (FrameEventArgs eventArgs) => {
                Render(gameTime);
                gameWindow.SwapBuffers();
            };
            gameWindow.Resize += (ResizeEventArgs) => {
                GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
            };
            gameWindow.Run();
        }

        protected abstract void Initialize();
        protected abstract void LoadContent();
        protected abstract void Update(GameTime gameTime);
        protected abstract void Render(GameTime gameTime);
    }
}