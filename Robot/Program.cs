using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;

namespace Robot
{
    class Program : GameWindow
    {
        double radianes = Math.PI / 180;
        double x = 5.0, y = 5.0, z = 5.0, theta = 0.0, beta = 0.0;

        public Program()
            : base(800, 600)
        { }

        [STAThread]
        static void Main(string[] args)
        {
            new Program().Run(60.0);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, (Width - Height) / -2, Width, Height + (Width - Height));

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView((float) (45.0 * Math.PI / 180), Width / Height, 1.0f, 100);
            GL.LoadMatrix(ref matrix);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);

            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 20.0f, 10.0f, 0.0f});
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.1f, 0.1f, 0.1f });
            GL.Enable(EnableCap.Light0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState k = Keyboard.GetState();

            if (k.IsKeyDown(Key.Q))
                if (k.IsKeyDown(Key.ShiftLeft))
                    theta -= 5.0;
                else
                    theta += 5.0;

            if (k.IsKeyDown(Key.E))
                if (k.IsKeyDown(Key.ShiftLeft))
                    beta -= 5.0;
                else
                    beta += 5.0;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Translate(0.0, 0.0, -50.0);
            GL.Rotate(25.0, 1.0, 0.0, 0.0);
            GL.Rotate(-25.0, 0.0, 1.0, 0.0);

            DibujarLineas();

            GL.Rotate(theta, 0.0, 1.0, 0.0);
            GL.Rotate(beta,  1.0, 0.0, 0.0);
            GL.Scale(5.0, 5.0, 5.0);

            //DibujarCubo();

            GL.Color3(Color.White);
            DibujarCono(2);

            SwapBuffers();
        }

        private void DibujarLineas()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Color3(Color.Blue);
                GL.Vertex3(-1.0, 0.0, 0.0);
                GL.Vertex3(Width, 0.0, 0.0);

                GL.Color3(Color.Red);
                GL.Vertex3(0.0, -1.0, 0.0);
                GL.Vertex3(0.0, Height, 0.0);

                GL.Color3(Color.Yellow);
                GL.Vertex3(0.0, 0.0, -1.0);
                GL.Vertex3(0.0, 0.0, 100.0);
            }
            GL.End();
            GL.Enable(EnableCap.Lighting);
        }

        private void DibujarCono(double altura)
        {
            double[] a = { 0.0, altura, 0.0 };
            double[] b = new double[3];
            double[] c = new double[3];

            b[1] = c[1] = 0;

            GL.Begin(PrimitiveType.TriangleFan);
            for (double angulo = 0; angulo <= 360; angulo += 10)
            {
                c[0] = b[0];
                c[2] = b[2];

                b[0] = Math.Cos(angulo * radianes);
                b[2] = Math.Sin(angulo * radianes);

                if (angulo != 0)
                {
                    GL.Normal3(normalesCono(a, b, c));
                    GL.Vertex3(a);
                    GL.Vertex3(b);
                    GL.Vertex3(c);
                }
            }
            GL.End();
        }

        private double[] normalesCono(double[] a, double[] b, double[] c)
        {
            double[] x = {
                b[0] - a[0],
                b[1] - a[1],
                b[2] - a[2]
            };

            double[] y = {
                c[0] - a[0],
                c[1] - a[1],
                c[2] - a[2]
            };

            double[] z = {
                x[1] * y[2] - y[1] * x[2],
              -(x[0] * y[2] - y[0] * x[2]),
                x[0] * y[1] - y[0] * x[1],
            };

            return z;
        }

        private void DibujarCubo()
        {
            GL.Begin(PrimitiveType.Quads);
            {
                // frente
                GL.Color3(1.0, 0.1, 0.1);
                GL.Normal3(0.0, 0.0, 1.0);
                GL.Vertex3(-10.0, -10.0, 10.0);
                GL.Vertex3(-10.0, 10.0, 10.0);
                GL.Vertex3(10.0, 10.0, 10.0);
                GL.Vertex3(10.0, -10.0, 10.0);

                // atras
                GL.Color3(1.0, 1.0, 0.1);
                GL.Normal3(0.0, 0.0, -1.0);
                GL.Vertex3(-10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, 10.0, -10.0);
                GL.Vertex3(10.0, 10.0, -10.0);
                GL.Vertex3(10.0, -10.0, -10.0);

                // arriba
                GL.Color3(0.1, 1.0, 1.0);
                GL.Normal3(0.0, 1.0, 0.0);
                GL.Vertex3(-10.0, 10.0, 10.0);
                GL.Vertex3(-10.0, 10.0, -10.0);
                GL.Vertex3(10.0, 10.0, -10.0);
                GL.Vertex3(10.0, 10.0, 10.0);

                // abajo
                GL.Color3(1.0, 0.1, 1.0);
                GL.Normal3(0.0, -1.0, 0.0);
                GL.Vertex3(-10.0, -10.0, 10.0);
                GL.Vertex3(-10.0, -10.0, -10.0);
                GL.Vertex3(10.0, -10.0, -10.0);
                GL.Vertex3(10.0, -10.0, 10.0);

                // derecha
                GL.Color3(0.1, 1.0, 0.1);
                GL.Normal3(-1.0, 0.0, 0.0);
                GL.Vertex3(-10.0, 10.0, 10.0);
                GL.Vertex3(-10.0, 10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, -10.0);
                GL.Vertex3(-10.0, -10.0, 10.0);

                // izquierda
                GL.Color3(0.1, 0.1, 1.0);
                GL.Normal3(1.0, 0.0, 0.0);
                GL.Vertex3(10.0, 10.0, 10.0);
                GL.Vertex3(10.0, 10.0, -10.0);
                GL.Vertex3(10.0, -10.0, -10.0);
                GL.Vertex3(10.0, -10.0, 10.0);
            }
            GL.End();
        }
    }
}
