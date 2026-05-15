// =======================================
// LIBRERÍAS
// =======================================

using System;
using System.Collections.Generic;

class Program
{
    // =======================================
    // LISTAS PARALELAS Y CONSTANTES
    // =======================================

    static List<string> titulos = new List<string>();
    static List<string> prioridades = new List<string>();
    static List<string> fechasLimite = new List<string>();
    static List<string> estados = new List<string>();
    static List<string> categorias = new List<string>();
    static List<string> descripciones = new List<string>();

    const string ArchivoTareas = "tareas.txt";

    // =======================================
    // MAIN
    // =======================================

    static void Main()
    {
        int opcion;

        CargarTareas();

        do
        {
            MostrarMenuPrincipal();
            opcion = LeerOpcionEntera("Seleccione una opción: ");

            switch (opcion)
            {
                case 1:
                    MostrarDashboard();
                    break;
                case 2:
                    AgregarTarea();
                    break;
                case 3:
                    EditarTarea();
                    break;
                case 4:
                    CambiarEstado();
                    break;
                case 5:
                    EliminarTarea();
                    break;
                case 6:
                    FiltrarTareas();
                    break;
                case 7:
                    MostrarReporte();
                    break;
                case 0:
                    GuardarTareas();
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    PausarPantalla();
                    break;
            }
        }
        while (opcion != 0);
    }

    // =======================================
    // FUNCIONES DE INTERFAZ
    // =======================================

    static void MostrarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("           FOCUSTERMINAL");
        Console.WriteLine("=======================================");
        Console.WriteLine("1. Mostrar dashboard simple");
        Console.WriteLine("2. Agregar tarea");
        Console.WriteLine("3. Editar tarea");
        Console.WriteLine("4. Cambiar estado");
        Console.WriteLine("5. Eliminar tarea");
        Console.WriteLine("6. Filtrar tareas");
        Console.WriteLine("7. Mostrar reporte");
        Console.WriteLine("0. Salir");
        Console.WriteLine("=======================================");
    }

    static void MostrarDashboard()
    {
        Console.Clear();
        Console.WriteLine("Mostrar dashboard: función pendiente de implementación");
        PausarPantalla();
    }

    static void PausarPantalla()
    {
        Console.WriteLine();
        Console.WriteLine("Presione Enter para continuar...");
        Console.ReadLine();
    }

    // =======================================
    // FUNCIONES DE GESTIÓN DE TAREAS
    // =======================================

    static void AgregarTarea()
    {
        Console.Clear();
        Console.WriteLine("Agregar tarea: función pendiente de implementación");
        PausarPantalla();
    }

    static void EditarTarea()
    {
        Console.Clear();
        Console.WriteLine("Editar tarea: función pendiente de implementación");
        PausarPantalla();
    }

    static void CambiarEstado()
    {
        Console.Clear();
        Console.WriteLine("Cambiar estado: función pendiente de implementación");
        PausarPantalla();
    }

    static void EliminarTarea()
    {
        Console.Clear();
        Console.WriteLine("Eliminar tarea: función pendiente de implementación");
        PausarPantalla();
    }

    // =======================================
    // FUNCIONES DE FILTROS Y REPORTES
    // =======================================

    static void FiltrarTareas()
    {
        Console.Clear();
        Console.WriteLine("Filtrar tareas: función pendiente de implementación");
        PausarPantalla();
    }

    static void MostrarReporte()
    {
        Console.Clear();
        Console.WriteLine("Mostrar reporte: función pendiente de implementación");
        PausarPantalla();
    }

    // =======================================
    // FUNCIONES DE ARCHIVO
    // =======================================

    static void GuardarTareas()
    {
        // Función pendiente de implementación
    }

    static void CargarTareas()
    {
        // Función pendiente de implementación
    }

    // =======================================
    // FUNCIONES DE VALIDACIÓN
    // =======================================

    static bool ValidarTitulo(string titulo)
    {
        // Función pendiente de implementación
        return false;
    }

    static bool ValidarPrioridad(string prioridad)
    {
        // Función pendiente de implementación
        return false;
    }

    static bool ValidarEstado(string estado)
    {
        // Función pendiente de implementación
        return false;
    }

    static bool ValidarFecha(string fecha)
    {
        // Función pendiente de implementación
        return false;
    }

    static int LeerOpcionEntera(string mensaje)
    {
        int opcion;
        string entrada;

        Console.Write(mensaje);
        entrada = Console.ReadLine() ?? "";

        if (int.TryParse(entrada, out opcion))
        {
            return opcion;
        }

        return -1;
    }

    static string LimpiarSeparador(string texto)
    {
        // Función pendiente de implementación
        return texto;
    }
}
