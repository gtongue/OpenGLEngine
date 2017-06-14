using OpenGLEngine.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLEngine.Models
{
    class TexturedModel
    {
        public RawModel m_rawModel;
        public ModelTexture m_modelTexture;

        public TexturedModel(RawModel p_rawModel, ModelTexture p_modelTexture)
        {
            m_rawModel = p_rawModel;
            m_modelTexture = p_modelTexture;
        }
    }
}
