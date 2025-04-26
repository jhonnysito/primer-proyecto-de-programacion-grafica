using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graficoU3D
{
    public class Vertice
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vertice(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public void FromVector3(Vector3 vec)
        {
            X = vec.X;
            Y = vec.Y;
            Z = vec.Z;
        }
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
        private static Poligono CrearPoligonoConColor(Color4 color, params Vertice[] vertices)
        {
            var poligono = new Poligono();
            for (int i = 0; i < vertices.Length; i++)
            {
                poligono.Add(i, vertices[i]);
            }
            poligono.Color = color;
            return poligono;
        }
        public static Dictionary<string, Poligono> CrearPoligonos()
        {
            var poligonos = new Dictionary<string, Poligono>();

            // Colores
            var amarillo = new Color4(1.0f, 1.0f, 0.0f, 1.0f);  // Parte inferior
            var rojo = new Color4(1.0f, 0.0f, 0.0f, 1.0f);     // Lado izquierdo
            var verde = new Color4(0.0f, 1.0f, 0.0f, 1.0f);     // Lado derecho
            var azul = new Color4(0.0f, 0.0f, 1.0f, 1.0f);      // Base de la "U"
            var blanco = new Color4(1.0f, 1.0f, 1.0f, 1.0f);    // Cara frontal
            var gris = new Color4(0.5f, 0.5f, 0.5f, 1.0f);      // Cara trasera
            var magenta = new Color4(1.0f, 0.0f, 1.0f, 1.0f);   // Cara superior

            // Base de la U
            poligonos["base"] = CrearPoligonoConColor(
                azul,
                new Vertice(-0.5f, -0.5f, 0.5f),
                new Vertice(-0.5f, -0.5f, -0.5f),
                new Vertice(0.5f, -0.5f, -0.5f),
                new Vertice(0.5f, -0.5f, 0.5f)
            );

            // Izquierda
            poligonos["izquierdo"] = CrearPoligonoConColor(
                rojo,
                new Vertice(-0.5f, 2.5f, -0.5f),
                new Vertice(-0.5f, -0.5f, -0.5f),
                new Vertice(-0.5f, -0.5f, 0.5f),
                new Vertice(-0.5f, 2.5f, 0.5f)
            );

            // Derecha
            poligonos["derecho"] = CrearPoligonoConColor(
                verde,
                new Vertice(0.5f, 2.5f, 0.5f),
                new Vertice(0.5f, -0.5f, 0.5f),
                new Vertice(0.5f, -0.5f, -0.5f),
                new Vertice(0.5f, 2.5f, -0.5f)
            );

            // Frontal
            poligonos["frontal"] = CrearPoligonoConColor(
                blanco,
                new Vertice(-0.5f, 2.5f, 0.5f),
                new Vertice(-0.5f, -0.5f, 0.5f),
                new Vertice(0.5f, -0.5f, 0.5f),
                new Vertice(0.5f, 2.5f, 0.5f)
            );

            // Trasera
            poligonos["trasera"] = CrearPoligonoConColor(
                gris,
                new Vertice(0.5f, 2.5f, -0.5f),
                new Vertice(0.5f, -0.5f, -0.5f),
                new Vertice(-0.5f, -0.5f, -0.5f),
                new Vertice(-0.5f, 2.5f, -0.5f)
            );

            // Superior
            poligonos["superior"] = CrearPoligonoConColor(
                magenta,
                new Vertice(-0.5f, 2.5f, -0.5f),
                new Vertice(-0.5f, 2.5f, 0.5f),
                new Vertice(0.5f, 2.5f, 0.5f),
                new Vertice(0.5f, 2.5f, -0.5f)
            );
            //base de la letra U
            poligonos["base2"] = CrearPoligonoConColor(
               azul,
               new Vertice(-0.5f, -1.5f, 0.5f),
               new Vertice(-0.5f, -1.5f, -0.5f),
               new Vertice(2.5f, -1.5f, -0.5f),
               new Vertice(2.5f, -1.5f, 0.5f)
           );
            // Izquierda
            poligonos["izquierdo2"] = CrearPoligonoConColor(
                rojo,
                new Vertice(-0.5f, -0.5f, -0.5f),
                new Vertice(-0.5f, -1.5f, -0.5f),
                new Vertice(-0.5f, -1.5f, 0.5f),
                new Vertice(-0.5f, -0.5f, 0.5f)
            );

            // Derecha
            poligonos["derecho2"] = CrearPoligonoConColor(
                verde,
                new Vertice(2.5f, -0.5f, 0.5f),
                new Vertice(2.5f, -1.5f, 0.5f),
                new Vertice(2.5f, -1.5f, -0.5f),
                new Vertice(2.5f, -0.5f, -0.5f)
            );

            // Frontal
            poligonos["frontal2"] = CrearPoligonoConColor(
                blanco,
                new Vertice(-0.5f, -0.5f, 0.5f),
                new Vertice(-0.5f, -1.5f, 0.5f),
                new Vertice(2.5f, -1.5f, 0.5f),
                new Vertice(2.5f, -0.5f, 0.5f)
            );

            // Trasera
            poligonos["trasera2"] = CrearPoligonoConColor(
                gris,
                new Vertice(0.5f, -0.5f, -0.5f),
                new Vertice(0.5f, -1.5f, -0.5f),
                new Vertice(2.5f, -1.5f, -0.5f),
                new Vertice(2.5f, -0.5f, -0.5f)
            );
            // Superior
            poligonos["superior2"] = CrearPoligonoConColor(
                magenta,
                new Vertice(-0.5f, -0.5f, 0.5f),
                new Vertice(-0.5f, -0.5f, -0.5f),
                new Vertice(2.5f, -0.5f, -0.5f),
                new Vertice(2.5f, -0.5f, 0.5f)
            );
            //base de la letra U
            poligonos["base3"] = CrearPoligonoConColor(
               azul,
               new Vertice(2.5f, -1.5f, 0.5f),
               new Vertice(2.5f, -1.5f, -0.5f),
               new Vertice(3.5f, -1.5f, -0.5f),
               new Vertice(3.5f, -1.5f, 0.5f)
           );
            // Izquierda
            poligonos["izquierdo3"] = CrearPoligonoConColor(
                rojo,
                new Vertice(2.5f, 2.5f, -0.5f),
                new Vertice(2.5f, -1.5f, -0.5f),
                new Vertice(2.5f, -1.5f, 0.5f),
                new Vertice(2.5f, 2.5f, 0.5f)
            );

            // Derecha
            poligonos["derecho3"] = CrearPoligonoConColor(
                verde,
                new Vertice(3.5f, 2.5f, 0.5f),
                new Vertice(3.5f, -1.5f, 0.5f),
                new Vertice(3.5f, -1.5f, -0.5f),
                new Vertice(3.5f, 2.5f, -0.5f)
            );

            // Frontal
            poligonos["frontal3"] = CrearPoligonoConColor(
                blanco,
                new Vertice(2.5f, 2.5f, 0.5f),
                new Vertice(2.5f, -1.5f, 0.5f),
                new Vertice(3.5f, -1.5f, 0.5f),
                new Vertice(3.5f, 2.5f, 0.5f)
            );

            // Trasera
            poligonos["trasera3"] = CrearPoligonoConColor(
                gris,
                new Vertice(2.5f, 2.5f, -0.5f),
                new Vertice(2.5f, -1.5f, -0.5f),
                new Vertice(3.5f, -1.5f, -0.5f),
                new Vertice(3.5f, 2.5f, -0.5f)
            );
            // Superior
            poligonos["superior3"] = CrearPoligonoConColor(
                magenta,
                new Vertice(2.5f, 2.5f, 0.5f),
                new Vertice(2.5f, 2.5f, -0.5f),
                new Vertice(3.5f, 2.5f, -0.5f),
                new Vertice(3.5f, 2.5f, 0.5f)
            );
            return poligonos;
        }

        public static Dictionary<string, Poligono> CrearPoligonos(float x, float y, float z)
        {
            var poligonos = new Dictionary<string, Poligono>();

            // Colores
            var amarillo = new Color4(1.0f, 1.0f, 0.0f, 1.0f);
            var rojo = new Color4(1.0f, 0.0f, 0.0f, 1.0f);
            var verde = new Color4(0.0f, 1.0f, 0.0f, 1.0f);
            var azul = new Color4(0.0f, 0.0f, 1.0f, 1.0f);
            var blanco = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            var gris = new Color4(0.5f, 0.5f, 0.5f, 1.0f);
            var magenta = new Color4(1.0f, 0.0f, 1.0f, 1.0f);

            // Función local para trasladar vértices
            Vertice Trasladar(Vertice v) => new Vertice(v.X + x, v.Y + y, v.Z + z);

            // Base
            poligonos["base"] = CrearPoligonoConColor(
                azul,
                Trasladar(new Vertice(-0.5f, -0.5f, 0.5f)),
                Trasladar(new Vertice(-0.5f, -0.5f, -0.5f)),
                Trasladar(new Vertice(0.5f, -0.5f, -0.5f)),
                Trasladar(new Vertice(0.5f, -0.5f, 0.5f))
            );

            // Izquierda
            poligonos["izquierdo"] = CrearPoligonoConColor(
                rojo,
                Trasladar(new Vertice(-0.5f, 2.5f, -0.5f)),
                Trasladar(new Vertice(-0.5f, -0.5f, -0.5f)),
                Trasladar(new Vertice(-0.5f, -0.5f, 0.5f)),
                Trasladar(new Vertice(-0.5f, 2.5f, 0.5f))
            );

            // Derecha
            poligonos["derecho"] = CrearPoligonoConColor(
                verde,
                Trasladar(new Vertice(0.5f, 2.5f, 0.5f)),
                Trasladar(new Vertice(0.5f, -0.5f, 0.5f)),
                Trasladar(new Vertice(0.5f, -0.5f, -0.5f)),
                Trasladar(new Vertice(0.5f, 2.5f, -0.5f))
            );

            // Frontal
            poligonos["frontal"] = CrearPoligonoConColor(
                blanco,
                Trasladar(new Vertice(-0.5f, 2.5f, 0.5f)),
                Trasladar(new Vertice(-0.5f, -0.5f, 0.5f)),
                Trasladar(new Vertice(0.5f, -0.5f, 0.5f)),
                Trasladar(new Vertice(0.5f, 2.5f, 0.5f))
            );

            // Trasera
            poligonos["trasera"] = CrearPoligonoConColor(
                gris,
                Trasladar(new Vertice(0.5f, 2.5f, -0.5f)),
                Trasladar(new Vertice(0.5f, -1.5f, -0.5f)),
                Trasladar(new Vertice(-0.5f, -1.5f, -0.5f)),
                Trasladar(new Vertice(-0.5f, 2.5f, -0.5f))
            );

            // Superior
            poligonos["superior"] = CrearPoligonoConColor(
                magenta,
                Trasladar(new Vertice(-0.5f, 2.5f, -0.5f)),
                Trasladar(new Vertice(-0.5f, 2.5f, 0.5f)),
                Trasladar(new Vertice(0.5f, 2.5f, 0.5f)),
                Trasladar(new Vertice(0.5f, 2.5f, -0.5f))
            );
            //base de la letra U
            poligonos["base2"] = CrearPoligonoConColor(
               azul,
              Trasladar (new Vertice(-0.5f, -1.5f, 0.5f)),
              Trasladar (new Vertice(-0.5f, -1.5f, -0.5f)),
              Trasladar (new Vertice(2.5f, -1.5f, -0.5f)),
              Trasladar (new Vertice(2.5f, -1.5f, 0.5f))
           );
            // Izquierda
            poligonos["izquierdo2"] = CrearPoligonoConColor(
                rojo,
               Trasladar(new Vertice(-0.5f, -0.5f, -0.5f)),
               Trasladar(new Vertice(-0.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(-0.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(-0.5f, -0.5f, 0.5f))
            );

            // Derecha
            poligonos["derecho2"] = CrearPoligonoConColor(
                verde,
               Trasladar(new Vertice(2.5f, -0.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -0.5f, -0.5f))
            );

            // Frontal
            poligonos["frontal2"] = CrearPoligonoConColor(
                blanco,
               Trasladar(new Vertice(-0.5f, -0.5f, 0.5f)),
               Trasladar(new Vertice(-0.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, -0.5f, 0.5f))
            );

            // Trasera
            poligonos["trasera2"] = CrearPoligonoConColor(
                gris,
               Trasladar(new Vertice(0.5f, -0.5f, -0.5f)),
               Trasladar(new Vertice(0.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -0.5f, -0.5f))
            );
            // Superior
            poligonos["superior2"] = CrearPoligonoConColor(
                magenta,
               Trasladar(new Vertice(-0.5f, -0.5f, 0.5f)),
               Trasladar(new Vertice(-0.5f, -0.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -0.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -0.5f, 0.5f))
            );
            //base de la letra U
            poligonos["base3"] = CrearPoligonoConColor(
               azul,
              Trasladar(new Vertice(2.5f, -1.5f, 0.5f)),
              Trasladar(new Vertice(2.5f, -1.5f, -0.5f)),
              Trasladar(new Vertice(3.5f, -1.5f, -0.5f)),
              Trasladar(new Vertice(3.5f, -1.5f, 0.5f))
           );
            // Izquierda
            poligonos["izquierdo3"] = CrearPoligonoConColor(
                rojo,
               Trasladar(new Vertice(2.5f, 2.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, 2.5f, 0.5f))
            );

            // Derecha
            poligonos["derecho3"] = CrearPoligonoConColor(
                verde,
               Trasladar(new Vertice(3.5f, 2.5f, 0.5f)),
               Trasladar(new Vertice(3.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(3.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(3.5f, 2.5f, -0.5f))
            );

            // Frontal
            poligonos["frontal3"] = CrearPoligonoConColor(
                blanco,
               Trasladar(new Vertice(2.5f, 2.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(3.5f, -1.5f, 0.5f)),
               Trasladar(new Vertice(3.5f, 2.5f, 0.5f))
            );

            // Trasera
            poligonos["trasera3"] = CrearPoligonoConColor(
                gris,
               Trasladar(new Vertice(2.5f, 2.5f, -0.5f)),
               Trasladar(new Vertice(2.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(3.5f, -1.5f, -0.5f)),
               Trasladar(new Vertice(3.5f, 2.5f, -0.5f))
            );
            // Superior
            poligonos["superior3"] = CrearPoligonoConColor(
                magenta,
               Trasladar(new Vertice(2.5f, 2.5f, 0.5f)),
               Trasladar(new Vertice(2.5f, 2.5f, -0.5f)),
               Trasladar(new Vertice(3.5f, 2.5f, -0.5f)),
               Trasladar(new Vertice(3.5f, 2.5f, 0.5f))
            );
            return poligonos;
        }
    }
}
