using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Robot
{
    class Robot
    {
        public void Dibujar()
        {
            DibujarCabeza();
            DibujarBrazos();
            DibujarTorso();
            DibujarRueda();
        }

        private void DibujarCabeza()
        {
            double[,] coords =
            {
                { -3.0, -3.0,  3.0 }, //0
                { -2.5, -2.0, -3.0 }, //1
                {  2.5, -2.0, -3.0 }, //2
                {  3.0, -3.0,  3.0 }, //3
                { -3.0,  3.0,  3.0 }, //4
                { -2.5,  2.0, -3.0 }, //5
                {  2.5,  2.0, -3.0 }, //6
                {  3.0,  3.0,  3.0 }  //7
            };

            GL.PushMatrix();
            GL.Translate(0.0, 3.8, 0.0);
            DibujarCubo(coords);

            GL.Translate(-1.2, 0.8, 3.0);
            GL.Scale(1.1, 1.2, 1.0);
            DibujarOjo();
            
            GL.Translate(2.4, 0.0, 0.0);
            DibujarOjo();
            GL.PopMatrix();
        }

        private void DibujarTorso()
        {
            GL.Color3(0.3, 0.3, 0.3);
            DibujarEsfera();
        }

        private void DibujarBrazos()
        {
        }

        private void DibujarRueda()
        {
        }

        private void DibujarOjo()
        {
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            GL.PushMatrix();
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Color3(1.0, 1.0, 1.0);
                for (double i = 0; i <= 360; i++)
                {
                    x2 = x1;
                    y2 = y1;

                    x1 = Math.Cos(i * Math.PI / 180);
                    y1 = Math.Sin(i * Math.PI / 180);

                    if (i != 0)
                    {
                        GL.Vertex3(x1, y1, 0);
                        GL.Vertex3(x1, y1, 0.2);
                        GL.Vertex3(x2, y2, 0.2);
                        GL.Vertex3(x2, y2, 0);
                    }
                }
            }
            GL.End();

            for (double j = 0; j < 0.3; j += 0.1)
            {
                GL.Begin(PrimitiveType.Polygon);
                {
                    GL.Color3(1.0, 1.0, 1.0);
                    for (double i = 0; i <= 360; i++)
                    {
                        x1 = Math.Cos(i * Math.PI / 180);
                        y1 = Math.Sin(i * Math.PI / 180);

                        if (i != 0)
                        {
                            GL.Vertex3(x1, y1, j);
                            GL.Vertex3(x1, y1, j);
                        }
                    }
                }
                GL.End();
            }
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Lighting);
            GL.PopMatrix();
        }

        private void DibujarCubo(double[,] coords)
        {
            GL.PushMatrix();
            GL.Enable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);

            GL.Color3(0.0, 0.3, 0.7);
            GL.Begin(PrimitiveType.Quads);
            {
                //Caras
                {
                    // frente
                    GL.Normal3(0.0, 0.0, 1.0);

                    GL.Vertex3(coords[0, 0] * 0.9, coords[0, 1] * 0.9, coords[0, 2]);
                    GL.Vertex3(coords[4, 0] * 0.9, coords[4, 1] * 0.9, coords[4, 2]);
                    GL.Vertex3(coords[7, 0] * 0.9, coords[7, 1] * 0.9, coords[7, 2]);
                    GL.Vertex3(coords[3, 0] * 0.9, coords[3, 1] * 0.9, coords[3, 2]);

                    // atras
                    GL.Normal3(0.0, 0.0, -1.0);

                    GL.Vertex3(coords[1, 0] * 0.9, coords[1, 1] * 0.9, coords[1, 2]);
                    GL.Vertex3(coords[5, 0] * 0.9, coords[5, 1] * 0.9, coords[5, 2]);
                    GL.Vertex3(coords[6, 0] * 0.9, coords[6, 1] * 0.9, coords[6, 2]);
                    GL.Vertex3(coords[2, 0] * 0.9, coords[2, 1] * 0.9, coords[2, 2]);

                    // arriba
                    GL.Normal3(0.0, 1.0, 0.0);

                    GL.Vertex3(coords[4, 0] * 0.9, coords[4, 1], coords[4, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 0.9, coords[5, 1], coords[5, 2] * 0.9);
                    GL.Vertex3(coords[6, 0] * 0.9, coords[6, 1], coords[6, 2] * 0.9);
                    GL.Vertex3(coords[7, 0] * 0.9, coords[7, 1], coords[7, 2] * 0.9);

                    // abajo
                    GL.Normal3(0.0, -1.0, 0.0);

                    GL.Vertex3(coords[0, 0] * 0.9, coords[0, 1], coords[0, 2] * 0.9);
                    GL.Vertex3(coords[1, 0] * 0.9, coords[1, 1], coords[1, 2] * 0.9);
                    GL.Vertex3(coords[2, 0] * 0.9, coords[2, 1], coords[2, 2] * 0.9);
                    GL.Vertex3(coords[3, 0] * 0.9, coords[3, 1], coords[3, 2] * 0.9);

                    // derecha
                    GL.Normal3(-1.0, 0.0, 0.0);

                    GL.Vertex3(coords[4, 0], coords[4, 1] * 0.9, coords[4, 2] * 0.9);
                    GL.Vertex3(coords[5, 0], coords[5, 1] * 0.9, coords[5, 2] * 0.9);
                    GL.Vertex3(coords[1, 0], coords[1, 1] * 0.9, coords[1, 2] * 0.9);
                    GL.Vertex3(coords[0, 0], coords[0, 1] * 0.9, coords[0, 2] * 0.9);

                    // izquierda
                    GL.Normal3(1.0, 0.0, 0.0);

                    GL.Vertex3(coords[7, 0], coords[7, 1] * 0.9, coords[7, 2] * 0.9);
                    GL.Vertex3(coords[6, 0], coords[6, 1] * 0.9, coords[6, 2] * 0.9);
                    GL.Vertex3(coords[2, 0], coords[2, 1] * 0.9, coords[2, 2] * 0.9);
                    GL.Vertex3(coords[3, 0], coords[3, 1] * 0.9, coords[3, 2] * 0.9);
                }

                //Bordes
                {
                    // arriba-frente
                    GL.Normal3(0.0, 0.5, 1.0);
                    GL.Vertex3(coords[4, 0] * 0.9, coords[4, 1] * 0.9, coords[4, 2] * 1.0);
                    GL.Vertex3(coords[4, 0] * 0.9, coords[4, 1] * 1.0, coords[4, 2] * 0.9);
                    GL.Vertex3(coords[7, 0] * 0.9, coords[7, 1] * 1.0, coords[7, 2] * 0.9);
                    GL.Vertex3(coords[7, 0] * 0.9, coords[7, 1] * 0.9, coords[7, 2] * 1.0);

                    // arriba-derecha
                    GL.Normal3(-1.0, 0.5, 0.0);
                    GL.Vertex3(coords[4, 0] * 1.0, coords[4, 1] * 0.9, coords[4, 2] * 0.9);
                    GL.Vertex3(coords[4, 0] * 0.9, coords[4, 1] * 1.0, coords[4, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 0.9, coords[5, 1] * 1.0, coords[5, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 1.0, coords[5, 1] * 0.9, coords[5, 2] * 0.9);

                    // arriba-atras
                    GL.Normal3(0.0, 0.5, -1.0);
                    GL.Vertex3(coords[6, 0] * 0.9, coords[6, 1] * 0.9, coords[6, 2] * 1.0);
                    GL.Vertex3(coords[6, 0] * 0.9, coords[6, 1] * 1.0, coords[6, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 0.9, coords[5, 1] * 1.0, coords[5, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 0.9, coords[5, 1] * 0.9, coords[5, 2] * 1.0);

                    // arriba-izquierda
                    GL.Normal3(1.0, 0.5, 0.0);
                    GL.Vertex3(coords[7, 0] * 1.0, coords[7, 1] * 0.9, coords[7, 2] * 0.9);
                    GL.Vertex3(coords[7, 0] * 0.9, coords[7, 1] * 1.0, coords[7, 2] * 0.9);
                    GL.Vertex3(coords[6, 0] * 0.9, coords[6, 1] * 1.0, coords[6, 2] * 0.9);
                    GL.Vertex3(coords[6, 0] * 1.0, coords[6, 1] * 0.9, coords[6, 2] * 0.9);

                    // abajo-frente
                    GL.Normal3(0.0, -0.5, 1.0);
                    GL.Vertex3(coords[0, 0] * 0.9, coords[0, 1] * 1.0, coords[0, 2] * 0.9);
                    GL.Vertex3(coords[0, 0] * 0.9, coords[0, 1] * 0.9, coords[0, 2] * 1.0);
                    GL.Vertex3(coords[3, 0] * 0.9, coords[3, 1] * 0.9, coords[3, 2] * 1.0);
                    GL.Vertex3(coords[3, 0] * 0.9, coords[3, 1] * 1.0, coords[3, 2] * 0.9);

                    // abajo-derecha
                    GL.Normal3(-1.0, -0.5, 0.0);
                    GL.Vertex3(coords[1, 0] * 0.9, coords[1, 1] * 1.0, coords[1, 2] * 0.9);
                    GL.Vertex3(coords[1, 0] * 1.0, coords[1, 1] * 0.9, coords[1, 2] * 0.9);
                    GL.Vertex3(coords[0, 0] * 1.0, coords[0, 1] * 0.9, coords[0, 2] * 0.9);
                    GL.Vertex3(coords[0, 0] * 0.9, coords[0, 1] * 1.0, coords[0, 2] * 0.9);

                    // abajo-atras
                    GL.Normal3(0.0, -0.5, -1.0);
                    GL.Vertex3(coords[2, 0] * 0.9, coords[2, 1] * 1.0, coords[2, 2] * 0.9);
                    GL.Vertex3(coords[2, 0] * 0.9, coords[2, 1] * 0.9, coords[2, 2] * 1.0);
                    GL.Vertex3(coords[1, 0] * 0.9, coords[1, 1] * 0.9, coords[1, 2] * 1.0);
                    GL.Vertex3(coords[1, 0] * 0.9, coords[1, 1] * 1.0, coords[1, 2] * 0.9);

                    // abajo-izquierda
                    GL.Normal3(1.0, -0.5, 0.0);
                    GL.Vertex3(coords[3, 0] * 0.9, coords[3, 1] * 1.0, coords[3, 2] * 0.9);
                    GL.Vertex3(coords[3, 0] * 1.0, coords[3, 1] * 0.9, coords[3, 2] * 0.9);
                    GL.Vertex3(coords[2, 0] * 1.0, coords[2, 1] * 0.9, coords[2, 2] * 0.9);
                    GL.Vertex3(coords[2, 0] * 0.9, coords[2, 1] * 1.0, coords[2, 2] * 0.9);

                    // frente-izquierda
                    GL.Normal3(0.5, 0.0, 0.5);
                    GL.Vertex3(coords[3, 0] * 0.9, coords[3, 1] * 0.9, coords[3, 2] * 1.0);
                    GL.Vertex3(coords[3, 0] * 1.0, coords[3, 1] * 0.9, coords[3, 2] * 0.9);
                    GL.Vertex3(coords[7, 0] * 1.0, coords[7, 1] * 0.9, coords[7, 2] * 0.9);
                    GL.Vertex3(coords[7, 0] * 0.9, coords[7, 1] * 0.9, coords[7, 2] * 1.0);

                    // frente-derecha
                    GL.Normal3(-0.5, 0.0, 0.5);
                    GL.Vertex3(coords[0, 0] * 0.9, coords[0, 1] * 0.9, coords[0, 2] * 1.0);
                    GL.Vertex3(coords[0, 0] * 1.0, coords[0, 1] * 0.9, coords[0, 2] * 0.9);
                    GL.Vertex3(coords[4, 0] * 1.0, coords[4, 1] * 0.9, coords[4, 2] * 0.9);
                    GL.Vertex3(coords[4, 0] * 0.9, coords[4, 1] * 0.9, coords[4, 2] * 1.0);

                    // atras-derecha
                    GL.Normal3(-0.5, 0.0, -0.5);
                    GL.Vertex3(coords[1, 0] * 0.9, coords[1, 1] * 0.9, coords[1, 2] * 1.0);
                    GL.Vertex3(coords[1, 0] * 1.0, coords[1, 1] * 0.9, coords[1, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 1.0, coords[5, 1] * 0.9, coords[5, 2] * 0.9);
                    GL.Vertex3(coords[5, 0] * 0.9, coords[5, 1] * 0.9, coords[5, 2] * 1.0);

                    // atras-izquierda
                    GL.Normal3(0.5, 0.0, -0.5);
                    GL.Vertex3(coords[2, 0] * 0.9, coords[2, 1] * 0.9, coords[2, 2] * 1.0);
                    GL.Vertex3(coords[2, 0] * 1.0, coords[2, 1] * 0.9, coords[2, 2] * 0.9);
                    GL.Vertex3(coords[6, 0] * 1.0, coords[6, 1] * 0.9, coords[6, 2] * 0.9);
                    GL.Vertex3(coords[6, 0] * 0.9, coords[6, 1] * 0.9, coords[6, 2] * 1.0);
                }
            }
            GL.End();

            //Vertices
            GL.Begin(PrimitiveType.Triangles);
            {
                double[,] normales =
                {
                    { -0.5, -0.5,  0.5 },
                    { -0.5, -0.5, -0.5 },
                    {  0.5, -0.5, -0.5 },
                    {  0.5, -0.5,  0.5 },
                    { -0.5,  0.5,  0.5 },
                    { -0.5,  0.5, -0.5 },
                    {  0.5,  0.5, -0.5 },
                    {  0.5,  0.5,  0.5 },

                };
                for (int i = 0; i < 8; i++)
                {
                    GL.Normal3(normales[i, 0], normales[i, 1], normales[i, 2]);
                    GL.Vertex3(coords[i, 0] * 1.0, coords[i, 1] * 0.9, coords[i, 2] * 0.9);
                    GL.Vertex3(coords[i, 0] * 0.9, coords[i, 1] * 1.0, coords[i, 2] * 0.9);
                    GL.Vertex3(coords[i, 0] * 0.9, coords[i, 1] * 0.9, coords[i, 2] * 1.0);
                }
            }
            GL.End();

            GL.Disable(EnableCap.Lighting);
            GL.Enable(EnableCap.Texture2D);
            GL.PopMatrix();
        }

        private void DibujarEsfera(float radius = 2.0f, int latitudes = 30, int longitudes = 20)
        {
            float latitude_increment = 360.0f / latitudes;
            float longitude_increment = 180.0f / longitudes;

            GL.PushMatrix();
            GL.Enable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);

            GL.Begin(PrimitiveType.TriangleStrip);
            for (float u = 0; u < 360.0f; u += latitude_increment)
            {
                for (float t = 0; t < 180.0f; t += longitude_increment)
                {
                    float rad = radius;

                    float x = (float)(rad * Math.Sin(t * Math.PI / 180) * Math.Sin(u * Math.PI / 180));
                    float y = (float)(rad * Math.Cos(t * Math.PI / 180));
                    float z = (float)(rad * Math.Sin(t * Math.PI / 180) * Math.Cos(u * Math.PI / 180));

                    GL.Vertex3(x, y, z);
                    GL.Normal3(x, y, z);

                    float x1 = (float)(rad * Math.Sin((t + longitude_increment) * Math.PI / 180) * Math.Sin((u + latitude_increment) * Math.PI / 180));
                    float y1 = (float)(rad * Math.Cos((t + longitude_increment) * Math.PI / 180));
                    float z1 = (float)(rad * Math.Sin((t + longitude_increment) * Math.PI / 180) * Math.Cos((u + latitude_increment) * Math.PI / 180));

                    GL.Vertex3(x1, y1, z1);
                }
            }
            GL.End();

            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Lighting);
            GL.PopMatrix();
        }
    }
}
