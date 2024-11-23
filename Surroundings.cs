using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace Evdokimov_David_PRI_121_CourseProject
{
    // Класс отвечающий за окружение
    internal class Surroundings
    {
        public void DrawSurroundings()
        {
            DrowPlane(new Point(-100, -100, -2), new Point(100, 100, -2), new RGB(0, 0.7f, 0.3f));
            DrowParalelepiped(new Point(-50, -50, 0), new Point(50, 50, -2), new RGB(1, 1, 1));
        }

        // Функция отрисовки плоской поверхности
        private void DrowPlane(Point a, Point b, RGB rgb)
        {
            Gl.glPushMatrix();
            Gl.glColor3f(rgb.getR(), rgb.getG(), rgb.getB());
            Gl.glBegin(Gl.GL_QUADS);

            Gl.glVertex3d(a.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), b.getY(), b.getZ());
            Gl.glVertex3d(a.getX(), b.getY(), b.getZ());

            Gl.glEnd();
            Gl.glPopMatrix();
        }

        // Функция отрисовки паралелепипеда
        private void DrowParalelepiped(Point a, Point b, RGB rgb)
        {

            Gl.glPushMatrix();
            //Цвета
            Gl.glColor3f(rgb.getR(), rgb.getG(), rgb.getB());
            Gl.glBegin(Gl.GL_QUADS);
            //Верхняя грань
            Gl.glVertex3d(a.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), b.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), b.getY(), a.getZ());
            //Нижняя грань
            Gl.glVertex3d(a.getX(), a.getY(), b.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), b.getZ());
            Gl.glVertex3d(b.getX(), b.getY(), b.getZ());
            Gl.glVertex3d(a.getX(), b.getY(), b.getZ());

            //Боковые грани
            //1-2
            Gl.glVertex3d(a.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), b.getX(), a.getZ());
            Gl.glVertex3d(a.getX(), b.getX(), b.getZ());
            Gl.glVertex3d(a.getX(), a.getY(), b.getZ());
            //2-3
            Gl.glVertex3d(a.getX(), b.getX(), a.getZ());
            Gl.glVertex3d(b.getX(), b.getX(), a.getZ());
            Gl.glVertex3d(b.getX(), b.getX(), b.getZ());
            Gl.glVertex3d(a.getX(), b.getX(), b.getZ());
            //3-4
            Gl.glVertex3d(b.getX(), b.getX(), a.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), b.getZ());
            Gl.glVertex3d(b.getX(), b.getX(), b.getZ());
            //4-1
            Gl.glVertex3d(b.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), a.getY(), b.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), b.getZ());

            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }
    }
}
