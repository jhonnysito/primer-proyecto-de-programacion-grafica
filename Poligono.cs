using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using Tarea3Grafica;

namespace graficoU3D
{
    public class Poligono
    {
        // Propiedad para almacenar los vértices

        
        public Dictionary<int, Vertice> vertices = new Dictionary<int, Vertice>();
        public Color4 Color { get; set; }

        public void Add(int id, Vertice vertice)
        {
            vertices[id] = vertice;
        }
        public void Rotar(Vector3 centro, float angulo, Vector3 eje)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Vector3 original = vertices[i].ToVector3();
                Vector3 rotado = Transformacion.Rotar(original, centro, angulo, eje);
                vertices[i] = new Vertice(rotado.X, rotado.Y, rotado.Z);
            }
        }
        public Vertice Get(int id)
        {
            return vertices.ContainsKey(id) ? vertices[id] : null;
        }

        public void Delete(int id)
        {
            vertices.Remove(id);
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(Color);
            foreach (var vertice in vertices.Values)
            {
                GL.Vertex3(vertice.X, vertice.Y, vertice.Z);
            }
            GL.End();
        }

        // Método público para obtener todos los vértices
        public IEnumerable<Vertice> GetVertices()
        {
            return vertices.Values;
        }
      
    }
}