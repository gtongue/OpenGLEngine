using OpenGLEngine.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLEngine.Entities
{
    class Entity
    {
        public TexturedModel m_model;
        public Vector3 m_position;
        public Vector3 m_rotation;
        public Vector3 m_scale;

        public Entity(TexturedModel model, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            m_model = model;
            m_position = position;
            m_rotation = rotation;
            m_scale = scale;
        }

        public void Move(Vector3 move)
        {
            m_position += move;
        }
        public void Rotate(Vector3 rotate)
        {
            m_rotation += rotate;
        }
        public void Scale(Vector3 scale)
        {
            m_scale += scale;
        }
    }
}
