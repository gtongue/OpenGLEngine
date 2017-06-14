using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace OpenGLEngine.Shaders
{
    abstract class ShaderProgram
    {
        private int m_programID;
        private int m_vertexShaderID;
        private int m_fragmentShaderID;

        public ShaderProgram(String p_vertexFile, String p_fragmentFile)
        {
            m_vertexShaderID = LoadShader(p_vertexFile, ShaderType.VertexShader);
            m_fragmentShaderID = LoadShader(p_fragmentFile, ShaderType.FragmentShader);
            m_programID = GL.CreateProgram();

            GL.AttachShader(m_programID, m_vertexShaderID);
            GL.AttachShader(m_programID, m_fragmentShaderID);

            BindAttributes();

            GL.LinkProgram(m_programID);
            GL.ValidateProgram(m_programID);

            GetAllUniformLocations();
        }
        protected abstract void BindAttributes();
        protected void BindAttribute(int p_attribute, String p_variableName)
        {
            GL.BindAttribLocation(m_programID, p_attribute, p_variableName);
        }
        public void Start()
        {
            GL.UseProgram(m_programID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        protected void LoadFloat(int location, float value)
        {
            GL.Uniform1(location, value);
        }

        protected void LoadVector3(int location, Vector3 value)
        {
            GL.Uniform3(location, ref value);
        }

        protected void LoadMatrix(int location, Matrix4 matrix)
        {
            GL.UniformMatrix4(location, false, ref matrix);
        }
        protected int GetUniformLocation(String p_uniformName)
        {
            return GL.GetUniformLocation(m_programID, p_uniformName);
        }

        protected abstract void GetAllUniformLocations();

        //TODO IDisposable???
        public void CleanUp()
        {
            Stop();
            GL.DetachShader(m_programID, m_vertexShaderID);
            GL.DetachShader(m_programID, m_fragmentShaderID);
            GL.DeleteShader(m_vertexShaderID);
            GL.DeleteShader(m_fragmentShaderID);
            GL.DeleteProgram(m_programID);
        }

        private static int LoadShader(String p_filename, ShaderType p_shaderType)
        {
            String l_textSource = System.IO.File.ReadAllText("..//..//Shaders//" + p_filename);
            int l_shaderID = GL.CreateShader(p_shaderType);
            GL.ShaderSource(l_shaderID, l_textSource);
            GL.CompileShader(l_shaderID);

            int l_status;
            GL.GetShader(l_shaderID, ShaderParameter.CompileStatus, out l_status);

            if (l_status == 0)
                throw new GraphicsException(String.Format("Error compiling {0} shader: {1}", p_shaderType.ToString(), GL.GetShaderInfoLog(l_shaderID)));

            return l_shaderID;
        }
    }
}
