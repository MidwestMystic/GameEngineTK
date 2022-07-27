using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System.Drawing;

namespace GameEngine.TK.Core.Management {
    public sealed class DisplayManager {
        private static DisplayManager _instance = null;
        private static readonly object _lock = new();
        public GameWindow GameWindow;

        public static DisplayManager Instance {
            get {
                lock(_lock) {
                    if(_instance is null) {
                        _instance = new DisplayManager();
                    }
                    return _instance;
                }
            }
        }

        public unsafe GameWindow CreateWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) {
            GameWindow = new GameWindow(gameWindowSettings, nativeWindowSettings);
            int x, y;
            MonitorInfo currentMonitor = Monitors.GetMonitorFromWindow(GameWindow.WindowPtr);
            Rectangle monitorRectangle = new Rectangle(0, 0, currentMonitor.ClientArea.Size.X, currentMonitor.ClientArea.Size.Y);
            x = (monitorRectangle.Right + monitorRectangle.Left - nativeWindowSettings.Size.X) / 2;
            y = (monitorRectangle.Bottom + monitorRectangle.Top - nativeWindowSettings.Size.Y) / 2;
            if(x < monitorRectangle.Left) {
                x = monitorRectangle.Left;
            }
            if(y < monitorRectangle.Top) {
                y = monitorRectangle.Top;
            }
            GameWindow.ClientRectangle = new Box2i(x, y, x + nativeWindowSettings.Size.X, y + nativeWindowSettings.Size.Y);
            return GameWindow;
        }
    }
}
