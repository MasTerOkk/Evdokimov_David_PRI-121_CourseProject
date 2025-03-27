using Evdokimov_David_PRI_121_CourseProject.particle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Evdokimov_David_PRI_121_CourseProject
{
    public partial class Form1 : Form
    {
        private Surroundings surroundings = new Surroundings();

        private float[,] camera_date = Utils.initCameraPositions();
        double a = 0, b = -0.575, c = -8.5, d = -61, zoom = 0.5;
        double translateX = 0, translateY = 0, translateZ = 0;

        // отсчет времени
        float global_time = 0;
        private Explosion BOOOOM_1 = new Explosion(1, 10, 1, 300, 30);
        float big_shoot_time = 0;
        State wolfState = State.IDLE;

        float xMove = 0f;

        uint wolfSign;
        uint pictureSign;
        uint zayacSign;
        private int imageId;

        float cameraSpeed = 1;

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            int camera = Cam.SelectedIndex;
            if (e.KeyCode == Keys.W)
            {
                camera_date[camera, 2] += cameraSpeed;

            }
            if (e.KeyCode == Keys.S)
            {
                camera_date[camera, 2] -= cameraSpeed;
            }
            if (e.KeyCode == Keys.A)
            {
                camera_date[camera, 0] += cameraSpeed;
            }
            if (e.KeyCode == Keys.D)
            {
                camera_date[camera, 0] -= cameraSpeed;

            }
            if (e.KeyCode == Keys.ControlKey)
            {
                camera_date[camera, 1] += cameraSpeed;

            }
            if (e.KeyCode == Keys.Space)
            {
                camera_date[camera, 1] -= cameraSpeed;
            }
            if (e.KeyCode == Keys.Q)
            {
                camera_date[camera, 4] -= cameraSpeed / 500;
                camera_date[camera, 5] -= cameraSpeed / 500;
                camera_date[camera, 6] -= cameraSpeed / 500;
            }
            if (e.KeyCode == Keys.E)
            {
                camera_date[camera, 4] += cameraSpeed / 500;
                camera_date[camera, 5] += cameraSpeed / 500;
                camera_date[camera, 6] += cameraSpeed / 500;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BOOOOM_1 = new Explosion(1, 10, 1, 300, 1);
            Random rnd = new Random(); 
            BOOOOM_1.SetNewPosition(0, -21.5f, 11.4f); 
            BOOOOM_1.SetNewPower(50); 
            BOOOOM_1.Boooom(global_time);
            Utils.frase(Phrase.SHOOT);
            button1.Enabled = false;
            button2.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1200);
                button1.Enabled = true;
                button2.Enabled = true;
            });
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Utils.SetVolume(trackBar1.Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация glut
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // очистка окна
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соответствии с размерами элемента anT Gl.glViewport(0, 0, AnT.Width, AnT.Height)
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // настройка проекции
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // настройка параметров OpenGL для визуализации
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_NORMALIZE);

            // Звук
            Utils.SetVolume(trackBar1.Value);

            Cam.SelectedIndex = 2;
            RenderTimer.Start();

            wolfSign = genImage(Utils.state(State.IDLE));
            pictureSign = genImageWithEmboss(Utils.SPRITES_PATH + "picture.jpeg");
            zayacSign = genImage(Utils.SPRITES_PATH + "zayac.png");
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            global_time += (float)RenderTimer.Interval / 1000;
            Draw();
            if (global_time > big_shoot_time)
            {
                wolfState = State.IDLE;
                wolfSign = genImage(Utils.state(wolfState));
            }
        }

        private void Draw()
        {
            Gl.glNormal3f(0, 0, 1);

            // очистка буфера цвета и буфера глубины
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            // очищение текущей матрицы
            Gl.glLoadIdentity();

            int camera = Cam.SelectedIndex;
            Gl.glTranslated(camera_date[camera, 0] + translateX, camera_date[camera, 1] + translateY, camera_date[camera, 2] + translateZ);

            // Специальная коррекция для камеры 2
            if (camera == 2)
            {
                Gl.glRotated(-90, 0, 1, 0); // Дополнительный поворот для коррекции
                Gl.glRotated(-15, 0, 0, 1); // Дополнительный поворот для коррекции
            }

            Gl.glRotated(camera_date[camera, 3], camera_date[camera, 4] + a / 500,
                        camera_date[camera, 5] + a / 500, camera_date[camera, 6] + a / 500);

            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);
            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            surroundings.DrawSurroundings();

            Gl.glPopMatrix();
            Gl.glFlush();
            BOOOOM_1.Calculate(global_time);

            //Отрисовка волка
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, wolfSign);
            Gl.glPushMatrix();

            surroundings.DrowWolf(wolfState);

            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            //Отрисовка картины
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, pictureSign);
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);


            Gl.glVertex3d(20.1f, -20, 3);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(20.1f, -20, 20);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(20.1f, 0, 20);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(20.1f, 0, 3);
            Gl.glTexCoord2f(0, 0);

            Gl.glEnd();

            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            //Отрисовка зайца
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, zayacSign);
            Gl.glPushMatrix();

            Gl.glBegin(Gl.GL_QUADS);

            if (global_time % 20 > 10)
            {
                xMove += .05f;
            }
            else
            {
                xMove -= .05f;
            }

            Gl.glVertex3d(10 + xMove * 5, 10, 2);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3d(10 + xMove * 5, 10, 10);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3d(15 + xMove * 5, 10, 10);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3d(15 + xMove * 5, 10, 2);
            Gl.glTexCoord2f(0, 0);

            Gl.glEnd();

            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glColor3f(255, 0, 0);
            Vector3 p1 = new Vector3(0, 29.9f, 0);
            Vector3 p2 = new Vector3(-10, 29.9f, 10);
            Vector3 p3 = new Vector3(10, 29.9f, 10);
            Vector3 p4 = new Vector3(0, 29.9f, 0);

            surroundings.DrawSierpinskiTriangle3D(p1, p2, p3, p4, 3);

            AnT.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BOOOOM_1 = new Explosion(1, 10, 1, 300, 30);
            Random rnd = new Random();
            BOOOOM_1.SetNewPosition(0, -21.5f, 11.4f);
            BOOOOM_1.SetNewPower(50);
            BOOOOM_1.Boooom(global_time);
            Utils.frase(Phrase.BIG_SHOOT);
            button1.Enabled = false;
            button2.Enabled = false;
            
            wolfState = State.CRASHED_GUN;
            wolfSign = genImage(Utils.state(wolfState));
            big_shoot_time = global_time + 4f;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1500);
                Utils.frase(Phrase.TALK);
                Thread.Sleep(3000);
                button1.Enabled = true;
                button2.Enabled = true;
            });
        }

        private uint genImage(string image)
        {
            uint sign = 0;
            Il.ilGenImages(1, out imageId);
            Il.ilBindImage(imageId);
            if (Il.ilLoadImage(image))
            {
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                switch (bitspp)
                {
                    case 24:
                        sign = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        sign = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
            }
            Il.ilDeleteImages(1, ref imageId);
            return sign;
        }

        private uint genImageWithEmboss(string imagePath)
        {
            uint textureId = 0;
            int tempImageId = 0;

            Il.ilGenImages(1, out tempImageId);
            Il.ilBindImage(tempImageId);

            if (!Il.ilLoadImage(imagePath))
            {
                Il.ilDeleteImages(1, ref tempImageId);
                return 0;
            }

            int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
            int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
            int bpp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

            if (bpp == 24)
                Il.ilConvertImage(Il.IL_RGB, Il.IL_UNSIGNED_BYTE);
            else if (bpp == 32)
                Il.ilConvertImage(Il.IL_RGBA, Il.IL_UNSIGNED_BYTE);

            IntPtr imageData = Il.ilGetData();
            byte[] originalBytes = new byte[width * height * (bpp / 8)];
            System.Runtime.InteropServices.Marshal.Copy(imageData, originalBytes, 0, originalBytes.Length);

            byte[] embossedBytes = new byte[width * height * 3]; 
            int offset = 2; 

            for (int y = 0; y < height - offset; y++)
            {
                for (int x = 0; x < width - offset; x++)
                {
                    int origPos = (y * width + x) * (bpp / 8);
                    int offsetPos = ((y + offset) * width + (x + offset)) * (bpp / 8);

                    byte r = (byte)(128 + (originalBytes[origPos] - originalBytes[offsetPos]));
                    byte g = (byte)(128 + (originalBytes[origPos + 1] - originalBytes[offsetPos + 1]));
                    byte b = (byte)(128 + (originalBytes[origPos + 2] - originalBytes[offsetPos + 2]));

                    int embossPos = (y * width + x) * 3;
                    embossedBytes[embossPos] = r;
                    embossedBytes[embossPos + 1] = g;
                    embossedBytes[embossPos + 2] = b;
                }
            }

            Gl.glGenTextures(1, out textureId);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureId);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, width, height, 0,
                           Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, embossedBytes);

            Il.ilDeleteImages(1, ref tempImageId);

            return textureId;
        }

        private uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            uint texObject;
            Gl.glGenTextures(1, out texObject);
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);
            switch (Format)
            {

                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

            }
            Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glAlphaFunc(Gl.GL_GREATER, 0.1f);

            return texObject;
        }
    }
}
