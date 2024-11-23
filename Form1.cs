using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Evdokimov_David_PRI_121_CourseProject
{
    public partial class Form1 : Form
    {
        private Surroundings surroundings = new Surroundings();

        private float[,] camera_date = Utils.initCameraPositions();
        //private float[,] camera_date = new float[5, 7];
        double a = 0, b = -0.575, c = -8.5, d = -61, zoom = 0.5;
        double translateX = 0, translateY = 0, translateZ = 0;
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация glut
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

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

            Cam.SelectedIndex = 0;
            RenderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Draw();
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
            Gl.glRotated(camera_date[camera, 3], camera_date[camera, 4] + a / 500, camera_date[camera, 5] + a / 500, camera_date[camera, 6] + a / 500);
            // и масштабирование объекта
            Gl.glScaled(zoom, zoom, zoom);
            // помещаем состояние матрицы в стек матриц, дальнейшие трансформации затронут только визуализацию объекта
            Gl.glPushMatrix();

            surroundings.DrawSurroundings();

            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }
    }
}
