using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

//TODO fix namespace
namespace OpenGLEngine
{
    class Loader
    {
        /*TODO
         Store list to delete all vbos and vaos
         GL.DeleteVertexArrays();
         GL.DeleteBuffers();
         GL.DeleteTextures();
        */

        public RawModel LoadToVAO(float[] p_positions,float[] p_textureCoords, int[] p_indices)
        {
            uint l_vaoID = CreateVAO();
            BindIndicesBuffer(p_indices);
            StoreDataInAttributeList(0,3, p_positions);
            StoreDataInAttributeList(1, 2, p_textureCoords);

            UnbindVAO();
            return new RawModel(l_vaoID, p_indices.Length);
        }

        public int LoadTexture(string p_filename)
        {
            //TODO understand this method
            Bitmap bitmap = new Bitmap(p_filename);

            int l_textureID;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out l_textureID);
            GL.BindTexture(TextureTarget.Texture2D, l_textureID);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return l_textureID;
        }

        private uint CreateVAO()
        {
            uint l_vaoID = 0;
            GL.GenVertexArrays(1, out l_vaoID);
            GL.BindVertexArray(l_vaoID);
            return l_vaoID;
        }

        private void StoreDataInAttributeList(int p_attributeNumber, int p_coordinateSize, float[] p_data)
        {
            uint l_vboID = 0;
            GL.GenBuffers(1, out l_vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, l_vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(p_data.Length * sizeof(float)), p_data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(p_attributeNumber, p_coordinateSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }

        private void BindIndicesBuffer(int[] p_indices)
        {
            uint l_vboID = 0;
            GL.GenBuffers(1, out l_vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, l_vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(p_indices.Length * sizeof(int)), p_indices, BufferUsageHint.StaticDraw);
        }
    }
}
