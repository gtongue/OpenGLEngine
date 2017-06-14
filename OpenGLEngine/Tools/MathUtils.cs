using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace OpenGLEngine.Tools
{
    class MathUtils
    {
        public static Matrix4 CreateTransformationMatrix(Vector3 translation, Vector3 rotation, Vector3 scale)
        {
            Matrix4 matrix = new Matrix4();
            matrix = Matrix4.Identity;

            matrix *= Matrix4.CreateTranslation(translation);

            matrix *= Matrix4.CreateRotationX(rotation.X);
            matrix *= Matrix4.CreateRotationY(rotation.Y);
            matrix *= Matrix4.CreateRotationZ(rotation.Z);
            matrix *= Matrix4.CreateScale(scale);

            return matrix;
        }
    }
}
