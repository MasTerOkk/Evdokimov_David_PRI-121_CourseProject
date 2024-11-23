using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evdokimov_David_PRI_121_CourseProject
{
    // Класс хранящий значение цветов RGB.
    public class RGB
    {
        private float R;
        private float G;
        private float B;

        public RGB(float R, float G, float B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public float getR()
        {
            return R;
        }

        public float getG()
        {
            return G;
        }

        public float getB()
        {
            return B;
        }
    }

    //Класс хранящий параметры точки в пространстве
    public class Point
    {
        private double x;
        private double y;
        private double z;

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public double getZ()
        {
            return z;
        }
    }
}
