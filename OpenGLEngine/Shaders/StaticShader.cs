using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace OpenGLEngine.Shaders
{
    class StaticShader : ShaderProgram
    {
        //TODO not set in stone shaders
        private static String VERTEX_FILE = "vertexShader.glsl";
        private static String FRAGMENT_FILE = "fragmentShader.glsl";


        private int m_locationTransformationMatrix;
        private int m_locationProjectionMatrix;

        public StaticShader() : base(VERTEX_FILE, FRAGMENT_FILE)
        {

        }

        protected override void BindAttributes()
        {
            base.BindAttribute(0, "position");
            base.BindAttribute(1, "textureCoords");
        }

        protected override void GetAllUniformLocations()
        {
            m_locationTransformationMatrix = base.GetUniformLocation("transformationMatrix");
            m_locationProjectionMatrix = base.GetUniformLocation("projectionMatrix");
        }

        public void LoadTransformationMatrix(Matrix4 matrix)
        {
            base.LoadMatrix(m_locationTransformationMatrix, matrix);
        }

        public void LoadProjectionMatrix(Matrix4 matrix)
        {
            base.LoadMatrix(m_locationProjectionMatrix, matrix);
        }
    }
}
