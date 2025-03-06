using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Tao.OpenGl;

namespace Evdokimov_David_PRI_121_CourseProject
{
    // Класс отвечающий за окружение
    internal class Surroundings
    {
        public void DrawSurroundings()
        {
            // Трава
            DrowPlane(new Point(-100, -100, -2), new Point(100, 100, -2), new RGB(0, 0.7f, 0.3f));
            // Платформа
            DrowParalelepiped(new Point(-50, -50, 0), new Point(50, 50, -2), new RGB(1, 1, 1));
            // Тир
            DrowShootingGallery();
        }

        public void DrowWolf(State state)
        {
            switch (state)
            {
                case State.IDLE:
                    Gl.glBegin(Gl.GL_QUADS);


                    Gl.glVertex3d(0, -21, -4);
                    Gl.glTexCoord2f(0, 1);
                    Gl.glVertex3d(0, -21, 15);
                    Gl.glTexCoord2f(1, 1);
                    Gl.glVertex3d(0, -40, 15);
                    Gl.glTexCoord2f(1, 0);
                    Gl.glVertex3d(0, -40, -3);
                    Gl.glTexCoord2f(0, 0);

                    Gl.glEnd();
                    break;
                case State.CRASHED_GUN:
                    Gl.glBegin(Gl.GL_QUADS);


                    Gl.glVertex3d(0, -22, 0);
                    Gl.glTexCoord2f(0, 1);
                    Gl.glVertex3d(0, -22, 15);
                    Gl.glTexCoord2f(1, 1);
                    Gl.glVertex3d(0, -41, 15);
                    Gl.glTexCoord2f(1, 0);
                    Gl.glVertex3d(0, -41, 0);
                    Gl.glTexCoord2f(0, 0);

                    Gl.glEnd();
                    break;
            }
        }

        private void DrowShootingGallery()
        {
            // Стены
            DrowParalelepiped(new Point(-20, 30, 0), new Point(20, 32, 20), new RGB(1, 0.5f, 0));
            DrowParalelepiped(new Point(20, 30, 0), new Point(18, -30, 20), new RGB(1, 0.5f, 0));
            DrowParalelepiped(new Point(-20, 30, 0), new Point(-18, -30, 20), new RGB(1, 0.5f, 0));
            // Ограждение
            DrowParalelepiped(new Point(-20, -30, 0), new Point(10, -25, 4), new RGB(1, 0.5f, 0));
            // Крыша
            DrowParalelepiped(new Point(-20, -30, 20), new Point(20, 32, 22), new RGB(1, 0.4f, 0));
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
            Gl.glVertex3d(a.getX(), b.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), b.getY(), b.getZ());
            Gl.glVertex3d(a.getX(), a.getY(), b.getZ());
            //2-3
            Gl.glVertex3d(a.getX(), b.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), b.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), b.getY(), b.getZ());
            Gl.glVertex3d(a.getX(), b.getY(), b.getZ());
            //3-4
            Gl.glVertex3d(b.getX(), b.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), b.getZ());
            Gl.glVertex3d(b.getX(), b.getY(), b.getZ());
            //4-1
            Gl.glVertex3d(b.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), a.getY(), a.getZ());
            Gl.glVertex3d(a.getX(), a.getY(), b.getZ());
            Gl.glVertex3d(b.getX(), a.getY(), b.getZ());

            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glPopMatrix();
        }

        public void DrawSierpinskiTriangle3D(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, int depth)
        {
            if (depth == 0)
            {
                Gl.glBegin(Gl.GL_TRIANGLES);
                // First face
                Gl.glVertex3f(p1.X, p1.Y, p1.Z);
                Gl.glVertex3f(p2.X, p2.Y, p2.Z);
                Gl.glVertex3f(p3.X, p3.Y, p3.Z);

                // Second face
                Gl.glVertex3f(p1.X, p1.Y, p1.Z);
                Gl.glVertex3f(p2.X, p2.Y, p2.Z);
                Gl.glVertex3f(p4.X, p4.Y, p4.Z);

                // Third face
                Gl.glVertex3f(p1.X, p1.Y, p1.Z);
                Gl.glVertex3f(p3.X, p3.Y, p3.Z);
                Gl.glVertex3f(p4.X, p4.Y, p4.Z);

                // Fourth face
                Gl.glVertex3f(p2.X, p2.Y, p2.Z);
                Gl.glVertex3f(p3.X, p3.Y, p3.Z);
                Gl.glVertex3f(p4.X, p4.Y, p4.Z);

                Gl.glEnd();
            }
            else
            {
                Vector3 mid12 = (p1 + p2) / 2;
                Vector3 mid13 = (p1 + p3) / 2;
                Vector3 mid14 = (p1 + p4) / 2;
                Vector3 mid23 = (p2 + p3) / 2;
                Vector3 mid24 = (p2 + p4) / 2;
                Vector3 mid34 = (p3 + p4) / 2;

                DrawSierpinskiTriangle3D(p1, mid12, mid13, mid14, depth - 1);
                DrawSierpinskiTriangle3D(mid12, p2, mid23, mid24, depth - 1);
                DrawSierpinskiTriangle3D(mid13, mid23, p3, mid34, depth - 1);
                DrawSierpinskiTriangle3D(mid14, mid24, mid34, p4, depth - 1);
            }
        }

    }
}
