using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenGLEngine.Shaders;
using OpenGLEngine.Textures;
using OpenGLEngine.Models;
using OpenGLEngine.Entities;

namespace OpenGLEngine
{
    class MainWindow: OpenTK.GameWindow
    {
        public static int m_Width;
        public static int m_Height;

        public MainWindow():base(1280, 720, OpenTK.Graphics.GraphicsMode.Default, "Initial Window", OpenTK.GameWindowFlags.Default,
            OpenTK.DisplayDevice.Default, 3, 0, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
            Console.WriteLine("gl version: " + GL.GetString(StringName.Version));
            m_Width = Width;
            m_Height = Height;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
            m_Width = Width;
            m_Height = Height;

            base.OnResize(e);
          /*  GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);*/

        }
   
        protected override void OnUpdateFrame(FrameEventArgs e)

        {
            // this is called every frame, put game logic here
            base.OnUpdateFrame(e);
            if (Keyboard[Key.Escape])
                Exit();
        }
        Loader m_loader = new Loader();
        Renderer m_renderer;
        StaticShader m_shader;

        float[] vertices = {
        -.5f, .5f, 0,
        -.5f, -.5f, 0,
        .5f, -.5f, 0,
        .5f, .5f, 0 };

        int[] indices =
        {
            0,1,3,
            3,1,2
        };

        float[] textureCoords =
        {
            0,0,
            0,1,
            1,1,
            1,0
        };

        RawModel m_model;
        ModelTexture m_modelTexture;
        TexturedModel m_texturedModel;
        Entity m_entity;

        protected override void OnLoad(EventArgs e)

        {
            // this is called when the window starts running
            base.OnLoad(e);

            GL.ClearColor(Color4.Purple);
            GL.Enable(EnableCap.DepthTest);
            m_model = m_loader.LoadToVAO(vertices,textureCoords, indices);
            m_shader = new StaticShader();
            m_renderer = new Renderer(m_shader);
            m_modelTexture = new ModelTexture(m_loader.LoadTexture("..//..//res//texture.png"));
            m_texturedModel = new TexturedModel(m_model, m_modelTexture);
            m_entity = new Entity(m_texturedModel, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1));
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            m_renderer.Prepare();

            m_shader.Start();
            //m_renderer.Render(m_texturedModel);
            m_renderer.Render(m_entity, m_shader);
            m_entity.Move(new Vector3(0, 0, -1));
            //m_entity.m_rotation.Y += .1f;
            m_shader.Stop();

            SwapBuffers();
        }
    }
}
