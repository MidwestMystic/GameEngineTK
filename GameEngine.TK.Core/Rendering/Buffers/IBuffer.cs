namespace GameEngine.TK.Core.Rendering {
    public interface IBuffer {
        int BufferId { get; }
        void Bind();
        void Unbind();
    }
}
