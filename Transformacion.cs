using OpenTK;
using System;

namespace Tarea3Grafica
{
    public static class Transformacion
    {


        public static Vector3 Trasladar(Vector3 vertice, Vector3 desplazamiento)
        {
            return vertice + desplazamiento;
        }



        public static Vector3 Rotar(Vector3 vertice, Vector3 centro, float angulo, Vector3 eje)
        {
            Vector3 verticeRelativo = vertice - centro;

            Matrix3 rotacion = Matrix3.CreateFromAxisAngle(eje, MathHelper.DegreesToRadians(angulo));

            Vector3 verticeRotado = Vector3.Transform(verticeRelativo, rotacion);

            return verticeRotado + centro;
        }

        // Método para realizar un escalado
        public static Vector3 Escalar(Vector3 vertice, Vector3 centro, Vector3 factorEscalado)
        {
            // Mover el vértice al origen usando el centro como referencia
            Vector3 verticeRelativo = vertice - centro;

            // Aplicar el escalado
            Vector3 verticeEscalado = new Vector3(
                verticeRelativo.X * factorEscalado.X,
                verticeRelativo.Y * factorEscalado.Y,
                verticeRelativo.Z * factorEscalado.Z
            );

            return verticeEscalado + centro;
        }
    }
}