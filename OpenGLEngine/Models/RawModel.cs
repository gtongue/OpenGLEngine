using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLEngine
{
    class RawModel
    {
        public uint m_vaoID;
        public int m_vertexCount;

        public RawModel(uint p_vaoID, int p_vertexCount)
        {
            m_vaoID = p_vaoID;
            m_vertexCount = p_vertexCount;
        }


    }
}
