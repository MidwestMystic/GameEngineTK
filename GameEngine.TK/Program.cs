using GameEngine.TK.Core;

namespace GameEngine.TK {
    public class Program {
        public static void Main(string[] args) {
            Game game = new MultipleTextures("Test", 800, 600);
            game.Run();
        }
    }
}