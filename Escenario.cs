using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graficoU3D
{
    class Escenario
    {
        private List<Objeto> objetos = new List<Objeto>();
        private List<int> ids = new List<int>();

        public void Add(int id, Objeto objeto)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                objetos[index] = objeto;
            }
            else
            {
                ids.Add(id);
                objetos.Add(objeto);
            }
        }

        public Objeto Get(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                return objetos[index];
            }
            return null;
        }

        public void Delete(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                ids.RemoveAt(index);
                objetos.RemoveAt(index);
            }
        }
        public void Trasladar(Vector3 desplazamiento)
        {
            foreach (var objeto in objetos)
            {
                objeto.Trasladar(desplazamiento);
            }
        }
        public void Draw()
        {
            foreach (var objeto in objetos)
            {
                objeto.Draw();
            }
        }

        public List<Objeto> GetObjetos()
        {
            return objetos;
        }
    }
}
