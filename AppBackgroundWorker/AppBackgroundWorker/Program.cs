using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            var TrabajoPrueba = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            TrabajoPrueba.DoWork += RealizarTrabajo;                // Método que hará el trabajo
           // TrabajoPrueba.DoWork += workProcess;
            
            TrabajoPrueba.ProgressChanged += NotificarProgreso;     // Método donde se notificará el progreso
           // TrabajoPrueba.ProgressChanged += NotificarProgreso2;

            TrabajoPrueba.RunWorkerCompleted += TrabajoCompletado;  // Método que se ejecutará al finalizar el trabajo
           // TrabajoPrueba.RunWorkerCompleted += TrabajoCompletado2;

            TrabajoPrueba.RunWorkerAsync();                         // Se manda a iniciar el trabajo, de forma asíncrona (segundo plano)

            Console.ReadLine();
        }

        public static void RealizarTrabajo(object sender, DoWorkEventArgs e)
        {
            // Cuando se inicia el trabajo, hacer lo siguiente:
            var TotalTareas = 15;                                             // Establecemos el total de tareas a realizar
            var ListaTareas = Enumerable.Range(1, TotalTareas).ToList();     // Simulamos las tareas en una lista

            Console.WriteLine("Se inicia el Trabajo de prueba");             // Mensaje de inicio de Trabajo
            Console.WriteLine();

            foreach (var ProgresoTareas in ListaTareas)                      // El progreso del trabajo irá del 1 al total de tareas
            {
                Console.WriteLine("1");

                if (((BackgroundWorker)sender).CancellationPending) return;  // Se valida si se ha cancelado el trabajo. En caso afirmativo, abandonar la ejecución
                System.Threading.Thread.Sleep(1500);                         // Una pequeña pausa que simula la tarea. además de poder ver el efecto de progreso en la consola
                ((BackgroundWorker)sender).ReportProgress((int)(ProgresoTareas * 100d / TotalTareas));  // Al finalizar cada tarea, reportamos el progreso (Porcentaje, valor entero del 1 al 100)
                if (ProgresoTareas == 2)
                {
                    ((BackgroundWorker)sender).CancelAsync();                // Al llegar a la segunda iteración se cancela el trabajo
                }
            }
          
        }

        public static void NotificarProgreso(object sender, ProgressChangedEventArgs e)
        {
            // Cada vez que se notifica un progreso, hacer lo siguiente:
            Console.WriteLine(string.Format("Se completó el Trabajo de prueba al {0}%", e.ProgressPercentage.ToString()));
        }

        public static void TrabajoCompletado(object sender, RunWorkerCompletedEventArgs e)
        {
            // Si terminó el trabajo (finalización, cancelación, error), hacer lo siguiente:
            Console.WriteLine();
            Console.WriteLine("Finaliza el Trabajo de prueba");
        }


        public static void workProcess(object sender, DoWorkEventArgs e)
        {
            // Cuando se inicia el trabajo, hacer lo siguiente:
            var TotalTareas = 5;                                             // Establecemos el total de tareas a realizar
            var ListaTareas = Enumerable.Range(1, TotalTareas).ToList();     // Simulamos las tareas en una lista

            Console.WriteLine("Se inicia el Trabajo de prueba");             // Mensaje de inicio de Trabajo
            Console.WriteLine();

            foreach (var ProgresoTareas in ListaTareas)                      // El progreso del trabajo irá del 1 al total de tareas
            {
                if (((BackgroundWorker)sender).CancellationPending) return;  // Se valida si se ha cancelado el trabajo. En caso afirmativo, abandonar la ejecución
                System.Threading.Thread.Sleep(500);                         // Una pequeña pausa que simula la tarea. además de poder ver el efecto de progreso en la consola
                ((BackgroundWorker)sender).ReportProgress((int)(ProgresoTareas * 100d / TotalTareas));  // Al finalizar cada tarea, reportamos el progreso (Porcentaje, valor entero del 1 al 100)
                if (ProgresoTareas == 2)
                {
                    ((BackgroundWorker)sender).CancelAsync();                // Al llegar a la segunda iteración se cancela el trabajo
                }
            }
        }
        public static void NotificarProgreso2(object sender, ProgressChangedEventArgs e)
        {
            // Cada vez que se notifica un progreso, hacer lo siguiente:
            Console.WriteLine(string.Format("Se completó el Trabajo de prueba segundo al {0}%", e.ProgressPercentage.ToString()));
        }
        public static void TrabajoCompletado2(object sender, RunWorkerCompletedEventArgs e)
        {
            // Si terminó el trabajo (finalización, cancelación, error), hacer lo siguiente:
            Console.WriteLine();
            Console.WriteLine("Finaliza el Trabajo de prueba segundo");
        }




    }
}
