using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenGLEngine.Models;
using OpenGLEngine.Entities;
using OpenGLEngine.Shaders;
using OpenTK;
using OpenGLEngine.Tools;

namespace OpenGLEngine
{
    class Renderer
    {
        Matrix4 m_projection;

        public Renderer(StaticShader shader)
        {
            CreateProjectionMatrix();
            shader.Start();
            shader.LoadProjectionMatrix(m_projection);
            shader.Stop();
        }
        public void Prepare()
        {
            GL.ClearColor(Color4.Purple);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Render(RawModel p_model)
        {
            GL.BindVertexArray(p_model.m_vaoID);
            GL.EnableVertexAttribArray(0);
            GL.DrawElements(PrimitiveType.Triangles, p_model.m_vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
        public void Render(TexturedModel p_model)
        {
            RawModel l_rawModel = p_model.m_rawModel;

            GL.BindVertexArray(l_rawModel.m_vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, p_model.m_modelTexture.m_textureID);
            GL.DrawElements(PrimitiveType.Triangles, l_rawModel.m_vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        public void Render(Entity entity, StaticShader shader)
        {
            TexturedModel texModel = entity.m_model;
            RawModel l_rawModel = texModel.m_rawModel;

            GL.BindVertexArray(l_rawModel.m_vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            Matrix4 transformationMatrix = MathUtils.CreateTransformationMatrix(entity.m_position, entity.m_rotation, entity.m_scale);
            shader.LoadTransformationMatrix(transformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texModel.m_modelTexture.m_textureID);
            GL.DrawElements(PrimitiveType.Triangles, l_rawModel.m_vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        private void CreateProjectionMatrix()
        {
            m_projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, MainWindow.m_Width / (float)MainWindow.m_Height, .1f, 1000.0f);
        }
    }
}
