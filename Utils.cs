using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.DevIl;
using Tao.OpenGl;

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

    public enum Phrase
    {
        TALK,
        SHOOT,
        BIG_SHOOT
    }

    public enum State
    {
        IDLE,
        CRASHED_GUN,
    }
    public class Utils
    {
        private static WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();

        public static float[,] initCameraPositions()
        {
            float[,] camera_date = new float[10, 7];

            // Общий вид
            camera_date[0, 0] = 0;
            camera_date[0, 1] = 0;
            camera_date[0, 2] = -90;
            camera_date[0, 3] = -70;
            camera_date[0, 4] = 1f;
            camera_date[0, 5] = 0f;
            camera_date[0, 6] = 0f;
            // Тир
            camera_date[1, 0] = 5;
            camera_date[1, 1] = 2;
            camera_date[1, 2] = -25;
            camera_date[1, 3] = -75;
            camera_date[1, 4] = 1f;
            camera_date[1, 5] = 0.3f;
            camera_date[1, 6] = 0.4f;

            return camera_date;
        }

        public static void frase(Phrase phrase)
        {
            switch (phrase)
            {
                case Phrase.TALK:
                    WMP.URL = @"frase.mp3";
                    WMP.controls.play();
                    break;
                case Phrase.SHOOT:
                    WMP.URL = @"shoot.mp3";
                    WMP.controls.play();
                    break;
                case Phrase.BIG_SHOOT:
                    WMP.URL = @"big-shoot.mp3";
                    WMP.controls.play();
                    break;
            }
            
        }

        public static void SetVolume(int volume)
        {
           WMP.settings.volume = volume;
        }

        public static string state(State state)
        {
            switch (state)
            {
                case State.IDLE:
                    return "wolf_state_idle.png";
                case State.CRASHED_GUN:
                    return "wolf_state_crashed_gun.png";
            }
            return null;

        }

    }
}
