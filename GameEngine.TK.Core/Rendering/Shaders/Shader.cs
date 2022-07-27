using GameEngine.TK.Core.Rendering.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace GameEngine.TK.Core.Rendering {
    public class Shader {
        public int ProgramId { get; private set; }
        private ShaderProgramSource _shaderProgramSource { get; }
        public bool Compiled { get; private set; }
        private readonly IDictionary<string, int> _uniforms = new Dictionary<string, int>();
        public Shader(ShaderProgramSource shaderProgramSource, bool compile = false) {
            _shaderProgramSource = shaderProgramSource;
            if(compile) {
                CompileShader();
            }
        }

        public bool CompileShader() {
            if(_shaderProgramSource == null) {
                Console.WriteLine("Shader Program Source is Null");
                return false;
            }
            if(Compiled) {
                Console.WriteLine("Shader is already compiled");
                return false;
            }
            int vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, _shaderProgramSource.VertexShaderSource);
            GL.CompileShader(vertexShaderId);
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if (vertexShaderCompilationCode != (int)All.True) {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
                return false;
            }

            int fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, _shaderProgramSource.FragmentShaderSource);
            GL.CompileShader(fragmentShaderId);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmentShaderCompilationCode);
            if (fragmentShaderCompilationCode != (int)All.True) {
                Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderId));
                return false;
            }

            ProgramId = GL.CreateProgram();
            GL.AttachShader(ProgramId, vertexShaderId);
            GL.AttachShader(ProgramId, fragmentShaderId);
            GL.LinkProgram(ProgramId);

            GL.DetachShader(ProgramId, vertexShaderId);
            GL.DetachShader(ProgramId, fragmentShaderId);

            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            GL.GetProgram(ProgramId, GetProgramParameterName.ActiveUniforms, out var totalUniforms);
            for(int i = 0; i < totalUniforms; i++) {
                string key = GL.GetActiveUniform(ProgramId, i, out _, out _);
                int location = GL.GetUniformLocation(ProgramId, key);
                _uniforms.Add(key, location);
            }

            Compiled = true;
            return true;
        }

        public int GetUniformLocation(string uniformName) => _uniforms[uniformName];

        public void Use() {
            if (Compiled) {
                GL.UseProgram(ProgramId);
            }
            else {
                Console.WriteLine("Shader has not been compiled!");
            }
        }

        public static ShaderProgramSource ParseShader(string filePath) {
            string[] shaderSource = new string[2];
            eShaderType shaderType = eShaderType.NONE;
            var allLines = File.ReadAllLines(filePath);
            for(int i = 0; i < allLines.Length; i++) {
                string current = allLines[i];
                if(current.ToLower().Contains("#shader")) {
                    if(current.ToLower().Contains("vertex")) {
                        shaderType = eShaderType.VERTEX;
                    } else if (current.ToLower().Contains("fragment")) {
                        shaderType = eShaderType.FRAGMENT;
                    } else {
                        Console.WriteLine("Error. no shader type has been supplied");
                    }
                } else {
                    shaderSource[(int)shaderType] += current + Environment.NewLine;
                }
            }
            return new ShaderProgramSource(shaderSource[(int)eShaderType.VERTEX], shaderSource[(int)eShaderType.FRAGMENT]);
        }
    }
}
