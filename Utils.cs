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

        public static string SOUNDS_PATH = "./assets/sounds/";
        public static string SPRITES_PATH = "./assets/sprites/";

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

            camera_date[2, 0] = 7;   // X позиция (отрицательное - левее)
            camera_date[2, 1] = -5;   // Y позиция (высота)
            camera_date[2, 2] = -30;    // Z позиция
            camera_date[2, 3] = -90;   // Основной поворот влево (по оси Y)
            camera_date[2, 4] = 1.0f;  // Ось X поворота (добавляем для коррекции)
            camera_date[2, 5] = 0.0f;  // Ось Y поворота
            camera_date[2, 6] = 0.0f;  // Ось Z поворота

            return camera_date;
        }

        public static void frase(Phrase phrase)
        {
            switch (phrase)
            {
                case Phrase.TALK:
                    WMP.URL = @SOUNDS_PATH + "frase.mp3";
                    WMP.controls.play();
                    break;
                case Phrase.SHOOT:
                    WMP.URL = @SOUNDS_PATH + "shoot.mp3";
                    WMP.controls.play();
                    break;
                case Phrase.BIG_SHOOT:
                    WMP.URL = @SOUNDS_PATH + "big-shoot.mp3";
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
                    return SPRITES_PATH + "wolf_state_idle.png";
                case State.CRASHED_GUN:
                    return SPRITES_PATH + "wolf_state_crashed_gun.png";
            }
            return null;

        }

    }
}
