using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;
using Tarea3Grafica;

namespace graficoU3D
{
    class JSON_serializador
    {
        // Serializar
        public static void Serializar(List<Vertice> vertices, string rutaArchivo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(vertices, Formatting.Indented);
                File.WriteAllText(rutaArchivo, json);
                Console.WriteLine("Vértices serializados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al serializar los vértices: {ex.Message}");
            }
        }

        // Deserializar
        public static List<Vertice> Deserializar(string rutaArchivo)
        {
            try
            {
                string json = File.ReadAllText(rutaArchivo);
                var vertices = JsonConvert.DeserializeObject<List<Vertice>>(json);
                Console.WriteLine("Vértices deserializados correctamente.");
                return vertices;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al deserializar los vértices: {ex.Message}");
                return null;
            }
        }

        public Objeto DibujarLetraConJson(float v1, float v2, float v3)
        {    Objeto nuevoObjeto;
            List<Poligono> poligonos = new List<Poligono>();
            List<Parte> partes = new List<Parte>();
            var verticesDeserializados = JSON_serializador.Deserializar("vertices.json");
            int id = 0;
            int contador = 0;
            Poligono nuevoPoligono = new Poligono();
            Vector3 desplazamiento = new Vector3(v1, v2, v3);

            foreach (var vertice in verticesDeserializados)
            {
                Vector3 v = new Vector3(vertice.X, vertice.Y, vertice.Z);
                v = Transformacion.Trasladar(v, desplazamiento);
                nuevoPoligono.Add(id++, new Vertice(v.X, v.Y, v.Z));
                contador++;
                if (contador % 4 == 0)
                {
                    nuevoPoligono.Color = new Color4(1.0f, 1.0f, 0.0f, 1.0f);
                    poligonos.Add(nuevoPoligono);
                    nuevoPoligono = new Poligono();
                    id = 0;
                }
            }
            int c = 0;
            Parte nuevaParte = new Parte();
            for (int j = 0; j < poligonos.Count; j++)
            {
                nuevaParte.Add(j, poligonos[j]);
                c++;
                if (c % 6 == 0)
                {
                    partes.Add(nuevaParte);
                    nuevaParte = new Parte();
                    c = 0;
                }
            }
            nuevoObjeto = new Objeto();
            for (int j = 0; j < partes.Count; j++)
            {
                nuevoObjeto.Add(j, partes[j]);
            }
            nuevoObjeto.CalcularCentroDeMasa();
            //escenario.Add(contObjetos, nuevoObjeto);
            //contObjetos++;
            return nuevoObjeto;
        }
    }
}
