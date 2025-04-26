using graficoU3D;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Tarea3Grafica;

namespace graficoU3D
{
    class Game : GameWindow
    {
        private KeyboardState tecladoAnterior;
        private Controlador controlador;
        private Vector3 camaraVista;  
        private Vector3 cameraFront; 
        private Vector3 cameraUp; // Y positivo es "arriba"
        private int contObjetos;
        private Objeto nuevoObjeto;
        private Escenario escenario;
        private float angulo = 0.0f;
        private bool enModoEscenario;
        private bool enModoPartes;
        private bool enModoEscalado;
        private bool enModoObjeto;
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {
            camaraVista = new Vector3(0,0,0);//hacia donde mira la camara
            cameraFront = new Vector3(0, 0, -3);//posicion de donde esta ubicada la camara
            cameraUp = Vector3.UnitY;
            contObjetos = 1;
            enModoEscenario=false;
            enModoPartes =false;
            enModoEscalado = false;
            enModoObjeto = false;
            escenario = new Escenario();
            var poligonos = Vertice.CrearPoligonos();
            List<Vertice> verticesParaSerializar = new List<Vertice>();
            // Extraer los vértices de cada polígono
            foreach (KeyValuePair<string, Poligono> poligono in poligonos)
            {
                verticesParaSerializar.AddRange(poligono.Value.GetVertices());
            }
            //Serializar a JSON 
            string json = JsonConvert.SerializeObject(verticesParaSerializar, Formatting.Indented);
            // Guardar en archivo
            File.WriteAllText("vertices.json", json);
            InicializarEscenario();
            
        }
        private void InicializarEscenario()
        {
            JSON_serializador serializador = new JSON_serializador();
            nuevoObjeto = serializador.DibujarLetraConJson(7f, 0f, 0f);
            escenario.Add(contObjetos, nuevoObjeto);
            contObjetos++;
            nuevoObjeto = serializador.DibujarLetraConJson(0f, 0f, 0f);
            escenario.Add(contObjetos, nuevoObjeto);
            contObjetos++;
            nuevoObjeto = serializador.DibujarLetraConJson(0f, 5, 0f);
            escenario.Add(contObjetos, nuevoObjeto);
            contObjetos++;
            //DibujarLetraConJson(7,0,0);
        }
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            controlador = new Controlador(escenario);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-15, 15, -15, 15, -15, 15);
           
        }
       
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Matrix4 view = Matrix4.LookAt(camaraVista, camaraVista + cameraFront, cameraUp);
            GL.LoadMatrix(ref view); // ESTA LÍNEA APLICA LA MATRIZ DE CÁMARA
            // 🔄 Aplica rotación antes de dibujar
            //GL.Rotate(angulo, 0.0f, 1.0f, 0.0f); // Gira sobre el eje Y
            //DibujarEjes();
            escenario.Draw();
            SwapBuffers();
            // 🔁 Aumenta el ángulo para el próximo frame
            angulo += 1.0f;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }
       
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState teclado = Keyboard.GetState();
            // Activa el modo por partes
            if (teclado.IsKeyDown(Key.L) && !tecladoAnterior.IsKeyDown(Key.L))
            {
                enModoEscenario=true;
                enModoPartes = false;
                enModoEscalado = false;
                enModoObjeto = false;
                Console.WriteLine("Entrando al modo mover escenario.");

            }
            // Activa el modo por partes
            if (teclado.IsKeyDown(Key.P) && !tecladoAnterior.IsKeyDown(Key.P))
            {
                enModoEscenario = false;
                enModoPartes = true;
                enModoEscalado = false;
                enModoObjeto = false;
                Console.WriteLine("Entrando al modo por partes.");
               
            }
            // Activa el modo de escalado
            if (teclado.IsKeyDown(Key.E) && !tecladoAnterior.IsKeyDown(Key.E))
            {
                enModoEscenario = false;
                enModoPartes = false;  // Desactiva el modo partes
                enModoEscalado = true;
                enModoObjeto = false;
                Console.WriteLine("Entrando al modo de escalado.");
            }
            // Activa el modo por objetos
            if (teclado.IsKeyDown(Key.O) && !tecladoAnterior.IsKeyDown(Key.O))
            {
                enModoEscenario = false;
                enModoPartes = false;
                enModoEscalado = false;
                enModoObjeto = true;
                Console.WriteLine("Entrando al modo por objetos.");
            }
            // Llamada al método de escenario si estamos en el modo de mover todo el escenario
            if (enModoEscenario)
            {
                controlador.escenarios(teclado, (float)e.Time);
            }
            // Llamada al método de partes si estamos en el modo de partes
            if (enModoPartes)
            {
                controlador.partes(teclado, tecladoAnterior, contObjetos, (float)e.Time);
            }

            // Llamada al método de escalado si estamos en el modo de escalado
            if (enModoEscalado)
            {
                controlador.escalarObjeto(teclado, tecladoAnterior, contObjetos);
            }
            // Llamada al método de objeto si estamos en el modo de objeto
            if (enModoObjeto)
            {
                controlador.objetos(teclado, tecladoAnterior, contObjetos, (float)e.Time);
            }
            // Llamás al método en cada frame, pero adentro se controla el estado paso a paso

            tecladoAnterior = teclado;
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            // Aquí puedo liberar cualquier recurso si los estuvieras usando, por ejemplo:
            // GL.DeleteBuffers(...);
            // GL.DeleteVertexArrays(...);
            // GL.DeleteTextures(...);

            // Si tengo objetos que implementan IDisposable, liberarlos también
            escenario = null;
        }
        
    }

}
