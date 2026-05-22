// =======================================
// LIBRERÍAS
// =======================================

using System;
using System.Collections.Generic;
using System.Globalization;

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
        Console.WriteLine("=======================================");
        Console.WriteLine("        DASHBOARD DE TAREAS");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas");
            PausarPantalla();
            return;
        }

        Console.WriteLine(
            "No.".PadRight(5) +
            "Titulo".PadRight(25) +
            "Prioridad".PadRight(12) +
            "Fecha".PadRight(14) +
            "Estado".PadRight(15) +
            "Categoria".PadRight(20));

        Console.WriteLine(new string('-', 91));

        for (int i = 0; i < titulos.Count; i++)
        {
            Console.WriteLine(
                (i + 1).ToString().PadRight(5) +
                titulos[i].PadRight(25) +
                prioridades[i].PadRight(12) +
                fechasLimite[i].PadRight(14) +
                estados[i].PadRight(15) +
                categorias[i].PadRight(20));
        }

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
        Console.WriteLine("=======================================");
        Console.WriteLine("            AGREGAR TAREA");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        string titulo;
        string prioridad;
        string fechaLimite;
        string estado;
        string categoria;
        string descripcion;

        do
        {
            Console.Write("Titulo: ");
            titulo = Console.ReadLine() ?? "";

            if (!ValidarTitulo(titulo))
            {
                Console.WriteLine("El titulo no puede estar vacio.");
            }
        }
        while (!ValidarTitulo(titulo));

        do
        {
            Console.Write("Prioridad (ALTA, MEDIA, BAJA): ");
            prioridad = Console.ReadLine() ?? "";

            if (!ValidarPrioridad(prioridad))
            {
                Console.WriteLine("La prioridad debe ser ALTA, MEDIA o BAJA.");
            }
        }
        while (!ValidarPrioridad(prioridad));

        prioridad = prioridad.Trim().ToUpper();

        do
        {
            Console.Write("Fecha limite (dd/MM/yyyy o --): ");
            fechaLimite = Console.ReadLine() ?? "";

            if (!ValidarFecha(fechaLimite))
            {
                Console.WriteLine("La fecha debe tener el formato dd/MM/yyyy o ser --.");
            }
        }
        while (!ValidarFecha(fechaLimite));

        fechaLimite = fechaLimite.Trim();

        do
        {
            Console.Write("Estado (Pendiente, En progreso, Completada): ");
            estado = Console.ReadLine() ?? "";

            if (!ValidarEstado(estado))
            {
                Console.WriteLine("El estado debe ser Pendiente, En progreso o Completada.");
            }
        }
        while (!ValidarEstado(estado));

        estado = estado.Trim().ToLower();

        if (estado == "pendiente")
        {
            estado = "Pendiente";
        }
        else if (estado == "en progreso")
        {
            estado = "En progreso";
        }
        else
        {
            estado = "Completada";
        }

        Console.Write("Categoria: ");
        categoria = Console.ReadLine() ?? "";

        Console.Write("Descripcion: ");
        descripcion = Console.ReadLine() ?? "";

        titulos.Add(LimpiarSeparador(titulo.Trim()));
        prioridades.Add(prioridad);
        fechasLimite.Add(fechaLimite);
        estados.Add(estado);
        categorias.Add(LimpiarSeparador(categoria.Trim()));
        descripciones.Add(LimpiarSeparador(descripcion.Trim()));

        Console.WriteLine();
        Console.WriteLine("Tarea agregada correctamente");
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
        return !string.IsNullOrWhiteSpace(titulo);
    }

    static bool ValidarPrioridad(string prioridad)
    {
        prioridad = prioridad.Trim().ToUpper();
        return prioridad == "ALTA" || prioridad == "MEDIA" || prioridad == "BAJA";
    }

    static bool ValidarEstado(string estado)
    {
        estado = estado.Trim().ToLower();
        return estado == "pendiente" || estado == "en progreso" || estado == "completada";
    }

    static bool ValidarFecha(string fecha)
    {
        fecha = fecha.Trim();

        if (fecha == "--")
        {
            return true;
        }

        return DateTime.TryParseExact(
            fecha,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _);
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
        return texto.Replace("|", " ");
    }
}
