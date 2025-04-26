using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graficoU3D
{
    class Objeto
    {
        public List<Parte> partes = new List<Parte>();
        private List<int> ids = new List<int>();
        public CentroDeMasa CentroDeMasa { get; private set; }

        public Objeto()
        {
            CentroDeMasa = new CentroDeMasa(0f, 0f, 0f);
        }

        public void Add(int id, Parte parte)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                partes[index] = parte;
            }
            else
            {
                ids.Add(id);
                partes.Add(parte);
            }
        }

        public Parte Get(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                return partes[index];
            }
            return null;
        }

        public void Delete(int id)
        {
            int index = ids.IndexOf(id);
            if (index >= 0)
            {
                ids.RemoveAt(index);
                partes.RemoveAt(index);
            }
        }

        public void Draw()
        {
            foreach (var parte in partes)
            {
                parte.Draw();
            }
        }

     
        // Método para calcular el centro de masa del objeto completo
        public void CalcularCentroDeMasa()
        {
            float sumaX = 0, sumaY = 0, sumaZ = 0;
            int totalPartes = partes.Count;
            float minX = 100f;
            float maxX = -100f;
            float minY = 100f;
            float maxY = -100f;
            float minZ = 100f;
            float maxZ = -100f;
            if (totalPartes > 0)
            {
                foreach (var parte in partes)
                {
                    if (parte.CentroDeMasa != null)
                    {
                        if (parte.CentroDeMasa.X < minX)
                        {
                            minX = parte.CentroDeMasa.X;
                        }
                        if (parte.CentroDeMasa.X > maxX)
                        {
                            maxX = parte.CentroDeMasa.X;
                        }
                        if (parte.CentroDeMasa.Y < minY)
                        {
                            minY = parte.CentroDeMasa.Y;
                        }
                        if (parte.CentroDeMasa.Y > maxY)
                        {
                            maxY = parte.CentroDeMasa.Y;
                        }
                        if (parte.CentroDeMasa.Z < minZ)
                        {
                            minZ = parte.CentroDeMasa.Z;
                        }
                        if (parte.CentroDeMasa.Z > maxZ)
                        {
                            maxZ = parte.CentroDeMasa.Z;
                        }
                        sumaX += parte.CentroDeMasa.X;
                        sumaY += parte.CentroDeMasa.Y;
                        sumaZ += parte.CentroDeMasa.Z;
                    }
                }
                CentroDeMasa = new CentroDeMasa((minX+maxX)/2, (minY + maxY) / 2, (minZ + maxZ) / 2);
            }
        }
        public void Trasladar(Vector3 desplazamiento)
        {
            foreach (var parte in partes)
            {
                parte.Trasladar(desplazamiento);
            }
        }
        public void Rotar(float angulo, Vector3 eje)
        {
            foreach (var parte in partes)
            {
                parte.Rotar(CentroDeMasa.ToVector3(), angulo, eje);
            }
        }
        public void Escalar(float factor)
        {
            foreach (var parte in partes)
            {
                parte.Escalar(CentroDeMasa.ToVector3(), factor);
            }
        }
    }
}
