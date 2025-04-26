using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graficoU3D
{

    class Controlador
    {
        private int estadoEscalado;
        private Boolean enModoEscalado;
        private int parteSeleccionada;
        private int estado = 0;
        private Escenario escenario;
        private float velocidadTraslacion;
        private float velocidadRotacion;

        public int Transformacion { get; private set; } = 1;
        public int ObjetoSeleccionado { get; private set; } = 1;
        public Controlador(Escenario escenario, float velocidadTraslacion = 2.5f, float velocidadRotacion = 45f)
        {
            this.escenario = escenario;
            this.velocidadTraslacion = velocidadTraslacion;
            this.velocidadRotacion = velocidadRotacion;
            parteSeleccionada = -1;
        }
       
        public void partes(KeyboardState teclado, KeyboardState tecladoAnterior, int contObjetos, float deltaTime)
        {
            Parte parte=new Parte();
            Objeto objeto;
            switch (estado)
            {
                case 0: // Esperando selección de transformación
                    if (teclado.IsKeyDown(Key.T))
                    {
                        Transformacion = 1;
                        Console.WriteLine("Transformación: Traslación");
                        estado = 1;
                    }
                    else if (teclado.IsKeyDown(Key.R))
                    {
                        Transformacion = 2;
                        Console.WriteLine("Transformación: Rotación");
                        estado = 1;
                    }
                    break;
                
                case 1: // Esperando selección de objeto
                    for (int i = 1; i <= contObjetos; i++)
                    {
                        Key key = Key.Number1 + (i - 1);
                        if (teclado.IsKeyDown(key))
                        {
                            ObjetoSeleccionado = i;
                            Console.WriteLine($"Objeto {i} seleccionado.");
                            estado = 2; // Pasamos al siguiente paso
                            break;
                        }
                    }

                    break;
                case 2: // Aplicar transformación
                    objeto = escenario.Get(ObjetoSeleccionado);
                    // Esperando selección de parte
                    if (teclado.IsKeyDown(Key.A) && !tecladoAnterior.IsKeyDown(Key.A))
                    {
                        parteSeleccionada = 1;
                        Console.WriteLine($"Parte A (1) del Objeto {ObjetoSeleccionado} seleccionada.");
                    }
                    else if (teclado.IsKeyDown(Key.S) && !tecladoAnterior.IsKeyDown(Key.S))
                    {
                        parteSeleccionada = 2;
                        Console.WriteLine($"Parte S (2) del Objeto {ObjetoSeleccionado} seleccionada.");
                    }
                    else if (teclado.IsKeyDown(Key.D) && !tecladoAnterior.IsKeyDown(Key.D))
                    {
                        parteSeleccionada = 3;
                        Console.WriteLine($"Parte D (3) del Objeto {ObjetoSeleccionado} seleccionada.");
                    }
                    if (parteSeleccionada > 0)
                    {
                        parte = objeto.partes[parteSeleccionada - 1];
                    }
                    if (objeto == null) return;
                    if (parteSeleccionada == -1) return;
                    switch (Transformacion)
                    {
                        case 1: // Traslación
                            if (teclado.IsKeyDown(Key.Left)) parte.Trasladar(new Vector3(-velocidadTraslacion * deltaTime, 0, 0));
                            if (teclado.IsKeyDown(Key.Right)) parte.Trasladar(new Vector3(velocidadTraslacion * deltaTime, 0, 0));
                            if (teclado.IsKeyDown(Key.Up)) parte.Trasladar(new Vector3(0, velocidadTraslacion * deltaTime, 0));
                            if (teclado.IsKeyDown(Key.Down)) parte.Trasladar(new Vector3(0, -velocidadTraslacion * deltaTime, 0));
                            break;

                        case 2: // Rotación
                            if (teclado.IsKeyDown(Key.Left)) parte.Rotar(parte.CentroDeMasa.ToVector3(), velocidadRotacion * deltaTime, Vector3.UnitX);
                            if (teclado.IsKeyDown(Key.Right)) parte.Rotar(parte.CentroDeMasa.ToVector3(), -velocidadRotacion * deltaTime, Vector3.UnitX);
                            if (teclado.IsKeyDown(Key.Up)) parte.Rotar(parte.CentroDeMasa.ToVector3(), velocidadRotacion * deltaTime, Vector3.UnitY);
                            if (teclado.IsKeyDown(Key.Down)) parte.Rotar(parte.CentroDeMasa.ToVector3(), -velocidadRotacion * deltaTime, Vector3.UnitY);
                            break;
                    }
                    // para volver a empezar la accion
                    if (teclado.IsKeyDown(Key.BackSpace)) // Por ejemplo, presionar "Backspace" para reiniciar
                    {
                        Console.WriteLine("Reiniciando selección.");
                        estado = 0;
                    }
                    break;
                
            }
        }
        public void objetos(KeyboardState teclado, KeyboardState tecladoAnterior, int contObjetos, float deltaTime)
        {
            Objeto objeto;
            switch (estado)
            {
                case 0: // Esperando selección de transformación
                    if (teclado.IsKeyDown(Key.T))
                    {
                        Transformacion = 1;
                        Console.WriteLine("Transformación: Traslación");
                        estado = 1;
                    }
                    else if (teclado.IsKeyDown(Key.R))
                    {
                        Transformacion = 2;
                        Console.WriteLine("Transformación: Rotación");
                        estado = 1;
                    }
                    break;

                case 1: // Esperando selección de objeto
                    for (int i = 1; i <= contObjetos; i++)
                    {
                        Key key = Key.Number1 + (i - 1);
                        if (teclado.IsKeyDown(key))
                        {
                            ObjetoSeleccionado = i;
                            Console.WriteLine($"Objeto {i} seleccionado.");
                            estado = 2; // Pasamos al siguiente paso
                            break;
                        }
                    }
                    break;

                case 2: // Aplicar transformación al objeto completo
                    objeto = escenario.Get(ObjetoSeleccionado);
                    if (objeto == null) return;
                    Vector3 desplazamiento = Vector3.Zero;
                    // Verificando si se debe aplicar transformación al objeto completo
                    switch (Transformacion)
                    {
                        case 1: // Traslación
                            if (teclado.IsKeyDown(Key.Left)) {
                                desplazamiento = new Vector3(-velocidadTraslacion * deltaTime, 0f, 0f); // mover en eje X
                                objeto.Trasladar(desplazamiento);
                            }
                            if (teclado.IsKeyDown(Key.Right))
                            {
                                desplazamiento = new Vector3(velocidadTraslacion * deltaTime, 0f, 0f); // mover en eje X
                                objeto.Trasladar(desplazamiento);
                            }
                            if (teclado.IsKeyDown(Key.Up))
                            {
                                desplazamiento = new Vector3(0f, velocidadTraslacion * deltaTime,  0f); // mover en eje X
                                objeto.Trasladar(desplazamiento);
                            }
                            if (teclado.IsKeyDown(Key.Down))
                            {
                                desplazamiento = new Vector3(0f, -velocidadTraslacion * deltaTime,  0f); // mover en eje X
                                objeto.Trasladar(desplazamiento);
                            }
                            break;

                        case 2: // Rotación
                            if (teclado.IsKeyDown(Key.Left)) objeto.Rotar(10f, Vector3.UnitX);
                            if (teclado.IsKeyDown(Key.Right)) objeto.Rotar(10f, Vector3.UnitX);
                            if (teclado.IsKeyDown(Key.Up)) objeto.Rotar(10f, Vector3.UnitY);
                            if (teclado.IsKeyDown(Key.Down)) objeto.Rotar(10f, Vector3.UnitY);
                            break;
                    }

                    // Para volver a empezar la acción
                    if (teclado.IsKeyDown(Key.BackSpace)) // Por ejemplo, presionar "Backspace" para reiniciar
                    {
                        Console.WriteLine("Reiniciando selección.");
                        estado = 0;
                    }
                    break;
            }
        }

        public void escenarios(KeyboardState teclado, float time)
        {
            Vector3 desplazamiento = Vector3.Zero;
            if (teclado.IsKeyDown(Key.Left))
            {
                desplazamiento = new Vector3(-velocidadTraslacion * time, 0f, 0f); // mover en eje X
                escenario.Trasladar(desplazamiento);
            }
            if (teclado.IsKeyDown(Key.Right))
            {
                desplazamiento = new Vector3(velocidadTraslacion * time, 0f, 0f); // mover en eje X
                escenario.Trasladar(desplazamiento);
            }
            if (teclado.IsKeyDown(Key.Up))
            {
                desplazamiento = new Vector3(0f, velocidadTraslacion * time, 0f); // mover en eje X
                escenario.Trasladar(desplazamiento);
            }
            if (teclado.IsKeyDown(Key.Down))
            {
                desplazamiento = new Vector3(0f, -velocidadTraslacion * time, 0f); // mover en eje X
                escenario.Trasladar(desplazamiento);
            }
        }

        public void escalarObjeto(KeyboardState teclado, KeyboardState tecladoAnterior, int contObjetos)
        {
            if (teclado.IsKeyDown(Key.E) && !tecladoAnterior.IsKeyDown(Key.E))  // Cambiar a E para escalado
            {
                Console.WriteLine("Modo de escalado activado.");
                enModoEscalado = true;  // Cambiar a true cuando se activa el modo escalado
            }

            if (enModoEscalado)
            {
                // Esperando selección del objeto para escalado
                for (int i = 1; i <= contObjetos; i++)
                {
                    Key key = Key.Number1 + (i - 1);  // Usando las teclas de número para seleccionar el objeto
                    if (teclado.IsKeyDown(key) && !tecladoAnterior.IsKeyDown(key))
                    {
                        ObjetoSeleccionado = i;
                        Console.WriteLine($"Objeto {i} seleccionado para escalado.");
                        estadoEscalado = 1; // Vamos al paso de escalado
                        break;
                    }
                }
            }

            if (estadoEscalado == 1) // Aplicando el escalado
            {
                Objeto objeto = escenario.Get(ObjetoSeleccionado);
                if (objeto == null) return;

                if (teclado.IsKeyDown(Key.Left)) objeto.Escalar(1.01f);
                if (teclado.IsKeyDown(Key.Right)) objeto.Escalar(0.99f);

                if (teclado.IsKeyDown(Key.BackSpace))
                {
                    Console.WriteLine("Reiniciando escalado.");
                    enModoEscalado = false;
                    estadoEscalado = 0;
                }
            }
        }
    }
}
