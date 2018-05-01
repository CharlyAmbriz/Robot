using System;
using System.Collections.Generic;
using System.Text;

using OpenTK;

namespace Robot
{
    class Camara
    {
        public Vector3 posicion = Vector3.Zero;
        public Vector3 orientacion = new Vector3((float)Math.PI, 0.0f, 0.0f);
        public float velocidad = 0.2f, sensibilidad = 0.007f;

        public Matrix4 GetMatrizVista()
        {
            Vector3 mirarA = new Vector3();

            mirarA.X = (float)(Math.Sin(orientacion.X) * Math.Cos(orientacion.Y));
            mirarA.Y = (float)(Math.Sin(orientacion.Y));
            mirarA.Z = (float)(Math.Cos(orientacion.X) * Math.Cos(orientacion.Y));

            return Matrix4.LookAt(posicion, posicion + mirarA, Vector3.UnitY);
        }

        public void Mover(float x, float y, float z, float plus)
        {
            Vector3 offset = new Vector3();
            Vector3 forward = new Vector3((float)Math.Sin(orientacion.X), 0, (float)Math.Cos(orientacion.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, velocidad + plus);

            posicion += offset;
        }

        public void AgregarRotacion(float x, float y)
        {
            x *= sensibilidad;
            y *= sensibilidad;

            orientacion.X = (orientacion.X + x) % ((float)Math.PI * 2.0f);
            orientacion.Y = Math.Max(Math.Min(orientacion.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }
    }
}
