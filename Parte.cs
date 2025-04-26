using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea3Grafica;

namespace graficoU3D
{
    class Parte
    {
        private List<Poligono> poligonos = new List<Poligono>();
        private List<int> ids = new List<int>();
        public CentroDeMasa CentroDeMasa { get; private set; }

        private Dictionary<int, float> angulosAcumulados = new Dictionary<int, float>();

        public void Add(int id, Poligono poligono)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                poligonos[index] = poligono;
            }
            else
            {
                ids.Add(id);
                poligonos.Add(poligono);
            }
            CalcularCentroDeMasa();
        }
        
        public Poligono Get(int id)
        {
            int index = ids.IndexOf(id);
            return index >= 0 ? poligonos[index] : null;
        }

        public void Delete(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                ids.RemoveAt(index);
                poligonos.RemoveAt(index);
            }
            CalcularCentroDeMasa();
        }

        public void Draw()
        {
            foreach (var poligono in poligonos)
            {
                poligono.Draw();
            }
        }

        public List<Poligono> GetPoligonos()
        {
            return poligonos;
        }

        private void CalcularCentroDeMasa()
        {
            float sumaX = 0, sumaY = 0, sumaZ = 0;
            int totalVertices = 0;

            foreach (var poligono in poligonos)
            {
                foreach (var vertice in poligono.GetVertices())
                {
                    sumaX += vertice.X;
                    sumaY += vertice.Y;
                    sumaZ += vertice.Z;
                    totalVertices++;
                }
            }

            if (totalVertices > 0)
            {
                CentroDeMasa = new CentroDeMasa(sumaX / totalVertices, sumaY / totalVertices, sumaZ / totalVertices);
            }
        }

        private Vector3 CalcularCentroDeMasaPoligono(Poligono poligono)
        {
            float sumaX = 0, sumaY = 0, sumaZ = 0;
            int totalVertices = 0;

            foreach (var vertice in poligono.GetVertices())
            {
                sumaX += vertice.X;
                sumaY += vertice.Y;
                sumaZ += vertice.Z;
                totalVertices++;
            }

            return totalVertices > 0 ? new Vector3(sumaX / totalVertices, sumaY / totalVertices, sumaZ / totalVertices) : Vector3.Zero;
        }
        public void Rotar(Vector3 centro, float angulo, Vector3 eje)
        {
            foreach (var poligono in poligonos)
            {
                poligono.Rotar(centro, angulo, eje);
            }
        }
        public void Trasladar(Vector3 desplazamiento)
        {
            foreach (var poligono in poligonos)
            {
                for (int i = 0; i < poligono.vertices.Count; i++)
                {
                    Vector3 original = poligono.vertices[i].ToVector3(); // Convertís el vértice a Vector3
                    Vector3 trasladado = original + desplazamiento;
                    poligono.vertices[i].FromVector3(trasladado); // Lo actualizás de vuelta
                }
            }

            // Si la parte tiene centro de masa, también lo movemos
            if (CentroDeMasa != null)
            {
                CentroDeMasa.X += desplazamiento.X;
                CentroDeMasa.Y += desplazamiento.Y;
                CentroDeMasa.Z += desplazamiento.Z;
            }
        }
        public void Escalar(Vector3 centro, float factor)
        {
            foreach (var poligono in poligonos)
            {
                for (int i = 0; i < poligono.vertices.Count; i++)
                {
                    Vector3 original = poligono.vertices[i].ToVector3();
                    Vector3 relativo = original - centro; // desde el centro
                    Vector3 escalado = relativo * factor; // escalarlo
                    Vector3 resultado = escalado + centro; // volver al mundo
                    poligono.vertices[i].FromVector3(resultado);
                }
            }

            if (CentroDeMasa != null)
            {
                Vector3 cm = CentroDeMasa.ToVector3() - centro;
                cm *= factor;
                Vector3 nuevoCentro = cm + centro;
                CentroDeMasa.X = nuevoCentro.X;
                CentroDeMasa.Y = nuevoCentro.Y;
                CentroDeMasa.Z = nuevoCentro.Z;
            }
        }
    }
}
