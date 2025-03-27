using System;
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
            DrowPlane(new Point(-1000, -1000, -2), new Point(1000, 1000, -2), new RGB(0, 0.7f, 0.3f));
            // Платформа
            DrowParalelepiped(new Point(-50, -50, 0), new Point(50, 50, -2), new RGB(1, 1, 1));
            // Тир
            DrowShootingGallery();
            DrawTreesAndBushes();
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
                Gl.glVertex3f(p1.X, p1.Y, p1.Z);
                Gl.glVertex3f(p2.X, p2.Y, p2.Z);
                Gl.glVertex3f(p3.X, p3.Y, p3.Z);

                Gl.glVertex3f(p1.X, p1.Y, p1.Z);
                Gl.glVertex3f(p2.X, p2.Y, p2.Z);
                Gl.glVertex3f(p4.X, p4.Y, p4.Z);

                Gl.glVertex3f(p1.X, p1.Y, p1.Z);
                Gl.glVertex3f(p3.X, p3.Y, p3.Z);
                Gl.glVertex3f(p4.X, p4.Y, p4.Z);

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

        public void DrawTreesAndBushes()
        {
            // Зона 1: x от -95 до 95, y от 70 до 95
            DrawTree(new Point(-90, 90, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(-70, 85, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(-50, 92, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));
            DrawTree(new Point(-30, 80, -2), 21, 1.7f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.4f, 0));
            DrawTree(new Point(-10, 88, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(10, 83, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(30, 91, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));
            DrawTree(new Point(50, 82, -2), 21, 1.7f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.4f, 0));
            DrawTree(new Point(70, 89, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(90, 84, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));

            DrawBush(new Point(-80, 75, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-60, 78, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-40, 76, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-20, 79, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(0, 77, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(20, 80, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(40, 75, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(60, 78, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(80, 76, -2), 4, new RGB(0, 0.55f, 0));

            // Зона 2: x от -95 до -70, y от -95 до 95
            DrawTree(new Point(-92, -80, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(-85, -50, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(-88, -20, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));
            DrawTree(new Point(-82, 10, -2), 21, 1.7f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.4f, 0));
            DrawTree(new Point(-89, 40, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(-84, 70, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(-91, 90, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));

            DrawBush(new Point(-78, -60, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-76, -30, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-79, 0, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-75, 30, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-77, 60, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(-74, 85, -2), 5, new RGB(0, 0.55f, 0));

            // Зона 3: x от 70 до 95, y от -95 до 95
            DrawTree(new Point(72, -85, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(75, -55, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(78, -25, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));
            DrawTree(new Point(82, 5, -2), 21, 1.7f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.4f, 0));
            DrawTree(new Point(79, 35, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(84, 65, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(91, 85, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));
            DrawTree(new Point(88, -15, -2), 21, 1.7f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.4f, 0));
            DrawTree(new Point(85, 15, -2), 19, 1.5f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.5f, 0));
            DrawTree(new Point(92, 45, -2), 20, 1.6f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.45f, 0));
            DrawTree(new Point(89, 75, -2), 18, 1.4f, new RGB(0.5f, 0.35f, 0.05f), new RGB(0, 0.55f, 0));

            DrawBush(new Point(74, -70, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(76, -40, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(79, -10, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(75, 20, -2), 5, new RGB(0, 0.55f, 0));
            DrawBush(new Point(77, 50, -2), 4, new RGB(0, 0.55f, 0));
            DrawBush(new Point(74, 80, -2), 5, new RGB(0, 0.55f, 0));
        }

        private void DrawTree(Point position, float height, float trunkWidth, RGB trunkColor, RGB foliageColor)
        {
            // Рисуем ствол
            DrowParalelepiped(
                new Point((float) position.getX() - trunkWidth / 2, (float) position.getY() - trunkWidth / 2, (float) position.getZ()),
                new Point((float) position.getX() + trunkWidth / 2, (float) position.getY() + trunkWidth / 2, (float) position.getZ() + height * 0.4f),
                trunkColor);

            // Рисуем крону (как конус)
            Gl.glPushMatrix();
            Gl.glColor3f(foliageColor.getR(), foliageColor.getG(), foliageColor.getB());
            Gl.glTranslated(position.getX(), position.getY(), position.getZ() + height * 0.4f);
            Glu.GLUquadric quad = Glu.gluNewQuadric();
            Glu.gluCylinder(quad, height * 0.3f, 0, height * 0.6f, 20, 20);
            Glu.gluDisk(quad, 0, height * 0.3f, 20, 20);
            Glu.gluDeleteQuadric(quad);
            Gl.glPopMatrix();
        }

        private void DrawBush(Point position, float size, RGB color)
        {
            Gl.glPushMatrix();
            Gl.glColor3f(color.getR(), color.getG(), color.getB());
            Gl.glTranslated(position.getX(), position.getY(), position.getZ());

            int segments = 20;
            int layers = 10;

            // Создаем перевернутый профиль куста с использованием полинома Эрмита
            Vector3[] profile = new Vector3[layers];
            for (int i = 0; i < layers; i++)
            {
                float t = i / (float)(layers - 1); // Нормализованный параметр [0..1]

                // Точки и касательные для полинома Эрмита
                Vector3 p0 = new Vector3(0, 0, size * 0.7f);  // Верхняя точка
                Vector3 p1 = new Vector3(size * 0.3f, 0, 0);  // Нижняя точка
                Vector3 m0 = new Vector3(size * 0.5f, 0, size * 0.5f);
                Vector3 m1 = new Vector3(-size * 0.2f, 0, size * 0.3f);

                // Вычисляем точку на кривой Эрмита
                float h1 = 2 * t * t * t - 3 * t * t + 1;
                float h2 = -2 * t * t * t + 3 * t * t;
                float h3 = t * t * t - 2 * t * t + t;
                float h4 = t * t * t - t * t;

                profile[i] = h1 * p0 + h2 * p1 + h3 * m0 + h4 * m1;
            }

            // Создаем поверхность вращения
            Gl.glBegin(Gl.GL_QUADS);
            for (int i = 0; i < layers - 1; i++)
            {
                for (int j = 0; j < segments; j++)
                {
                    float angle1 = (float)j / segments * 2 * (float)Math.PI;
                    float angle2 = (float)(j + 1) / segments * 2 * (float)Math.PI;

                    Vector3 p1 = profile[i];
                    Vector3 p2 = profile[i + 1];

                    // Верхняя точка текущего слоя
                    Vector3 v1 = new Vector3(
                        (float)(p1.X * Math.Cos(angle1)),
                        (float)(p1.X * Math.Sin(angle1)),
                        p1.Z);

                    Vector3 v2 = new Vector3(
                        (float)(p1.X * Math.Cos(angle2)),
                        (float)(p1.X * Math.Sin(angle2)),
                        p1.Z);

                    // Нижняя точка следующего слоя
                    Vector3 v3 = new Vector3(
                        (float)(p2.X * Math.Cos(angle2)),
                        (float)(p2.X * Math.Sin(angle2)),
                        p2.Z);

                    Vector3 v4 = new Vector3(
                        (float)(p2.X * Math.Cos(angle1)),
                        (float)(p2.X * Math.Sin(angle1)),
                        p2.Z);


                    // Вершины квадрата (порядок изменен для корректного отображения перевернутой формы)
                    Gl.glVertex3f(v1.X, v1.Y, v1.Z);
                    Gl.glVertex3f(v4.X, v4.Y, v4.Z);
                    Gl.glVertex3f(v3.X, v3.Y, v3.Z);
                    Gl.glVertex3f(v2.X, v2.Y, v2.Z);
                }
            }
            Gl.glEnd();

            Gl.glPopMatrix();
        }
    }
}
