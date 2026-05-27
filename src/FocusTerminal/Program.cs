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
                FormatearColumna(titulos[i], 25) +
                prioridades[i].PadRight(12) +
                fechasLimite[i].PadRight(14) +
                estados[i].PadRight(15) +
                FormatearColumna(categorias[i], 20));
        }

        PausarPantalla();
    }

    static void PausarPantalla()
    {
        Console.WriteLine();
        Console.WriteLine("Presione Enter para volver al menu principal...");
        Console.ReadLine();
    }

    static string FormatearColumna(string texto, int ancho)
    {
        texto = texto.Trim();

        if (texto.Length > ancho)
        {
            texto = texto.Substring(0, ancho - 3) + "...";
        }

        return texto.PadRight(ancho);
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

        estado = NormalizarEstado(estado);

        do
        {
            MostrarCategoriasBasicas();
            Console.Write("Categoria: ");
            categoria = Console.ReadLine() ?? "";
            categoria = ObtenerCategoriaBasica(categoria);

            if (string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("La categoria no puede estar vacia.");
            }
        }
        while (string.IsNullOrWhiteSpace(categoria));

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

    static void MostrarCategoriasBasicas()
    {
        Console.WriteLine("Categorias basicas:");
        Console.WriteLine("1. Estudio");
        Console.WriteLine("2. Trabajo");
        Console.WriteLine("3. Personal");
        Console.WriteLine("4. Salud");
        Console.WriteLine("5. Hogar");
        Console.WriteLine("Tambien puede escribir otra categoria.");
    }

    static string ObtenerCategoriaBasica(string categoria)
    {
        string categoriaLimpia = categoria.Trim();

        if (categoriaLimpia == "1")
        {
            return "Estudio";
        }
        else if (categoriaLimpia == "2")
        {
            return "Trabajo";
        }
        else if (categoriaLimpia == "3")
        {
            return "Personal";
        }
        else if (categoriaLimpia == "4")
        {
            return "Salud";
        }
        else if (categoriaLimpia == "5")
        {
            return "Hogar";
        }

        return categoria;
    }

    static void EditarTarea()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("            EDITAR TAREA");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas");
            PausarPantalla();
            return;
        }

        MostrarListaParaEditar();

        int indice = LeerIndiceTarea("Ingrese el numero de la tarea a editar: ");

        if (indice == -2)
        {
            return;
        }

        if (indice == -1)
        {
            PausarPantalla();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Campo a editar:");
        Console.WriteLine("1. Titulo");
        Console.WriteLine("2. Prioridad");
        Console.WriteLine("3. Fecha limite");
        Console.WriteLine("4. Estado");
        Console.WriteLine("5. Categoria");
        Console.WriteLine("6. Descripcion");
        Console.WriteLine("0. Volver al menu principal");

        int opcion = LeerOpcionEntera("Seleccione una opcion: ");
        bool modificado = false;

        switch (opcion)
        {
            case 1:
                modificado = EditarTitulo(indice);
                break;
            case 2:
                modificado = EditarPrioridad(indice);
                break;
            case 3:
                modificado = EditarFechaLimite(indice);
                break;
            case 4:
                modificado = EditarEstado(indice);
                break;
            case 5:
                modificado = EditarCategoria(indice);
                break;
            case 6:
                modificado = EditarDescripcion(indice);
                break;
            case 0:
                return;
            default:
                Console.WriteLine("Opcion no valida.");
                break;
        }

        if (modificado)
        {
            Console.WriteLine("Tarea editada correctamente.");
        }

        PausarPantalla();
    }

    static void CambiarEstado()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("          CAMBIAR ESTADO");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas");
            PausarPantalla();
            return;
        }

        MostrarListaParaEstado();

        int indice = LeerIndiceTarea("Ingrese el numero de la tarea: ");

        if (indice == -2)
        {
            return;
        }

        if (indice == -1)
        {
            PausarPantalla();
            return;
        }

        string nuevoEstado = LeerEstadoPorOpcion();

        if (nuevoEstado == "0")
        {
            return;
        }

        if (nuevoEstado == "")
        {
            Console.WriteLine("Opcion no valida.");
            PausarPantalla();
            return;
        }

        estados[indice] = nuevoEstado;

        Console.WriteLine("Estado actualizado correctamente.");
        PausarPantalla();
    }

    static void EliminarTarea()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("           ELIMINAR TAREA");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas");
            PausarPantalla();
            return;
        }

        MostrarListaParaEliminar();

        int indice = LeerIndiceTarea("Ingrese el numero de la tarea a eliminar: ");

        if (indice == -2)
        {
            return;
        }

        if (indice == -1)
        {
            PausarPantalla();
            return;
        }

        Console.WriteLine("0. Volver al menu principal");
        Console.Write("Esta seguro de eliminar esta tarea? (S/N): ");
        string confirmacion = Console.ReadLine() ?? "";
        confirmacion = confirmacion.Trim().ToUpper();

        if (confirmacion == "0")
        {
            return;
        }

        if (confirmacion == "S")
        {
            titulos.RemoveAt(indice);
            prioridades.RemoveAt(indice);
            fechasLimite.RemoveAt(indice);
            estados.RemoveAt(indice);
            categorias.RemoveAt(indice);
            descripciones.RemoveAt(indice);

            Console.WriteLine("Tarea eliminada correctamente.");
        }
        else
        {
            Console.WriteLine("Operacion cancelada.");
        }

        PausarPantalla();
    }

    static void MostrarListaParaEstado()
    {
        Console.WriteLine(
            "No.".PadRight(5) +
            "Titulo".PadRight(30) +
            "Estado".PadRight(15) +
            "Descripcion".PadRight(30));

        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < titulos.Count; i++)
        {
            Console.WriteLine(
                (i + 1).ToString().PadRight(5) +
                FormatearColumna(titulos[i], 30) +
                estados[i].PadRight(15) +
                FormatearColumna(descripciones[i], 30));
        }
    }

    static void MostrarListaParaEliminar()
    {
        Console.WriteLine(
            "No.".PadRight(5) +
            "Titulo".PadRight(30) +
            "Prioridad".PadRight(12) +
            "Estado".PadRight(15) +
            "Descripcion".PadRight(30));

        Console.WriteLine(new string('-', 92));

        for (int i = 0; i < titulos.Count; i++)
        {
            Console.WriteLine(
                (i + 1).ToString().PadRight(5) +
                FormatearColumna(titulos[i], 30) +
                prioridades[i].PadRight(12) +
                estados[i].PadRight(15) +
                FormatearColumna(descripciones[i], 30));
        }
    }

    static void MostrarListaParaEditar()
    {
        Console.WriteLine(
            "No.".PadRight(5) +
            "Titulo".PadRight(25) +
            "Prioridad".PadRight(12) +
            "Fecha".PadRight(14) +
            "Estado".PadRight(15) +
            "Categoria".PadRight(20) +
            "Descripcion".PadRight(30));

        Console.WriteLine(new string('-', 121));

        for (int i = 0; i < titulos.Count; i++)
        {
            Console.WriteLine(
                (i + 1).ToString().PadRight(5) +
                FormatearColumna(titulos[i], 25) +
                prioridades[i].PadRight(12) +
                fechasLimite[i].PadRight(14) +
                estados[i].PadRight(15) +
                FormatearColumna(categorias[i], 20) +
                FormatearColumna(descripciones[i], 30));
        }
    }

    static int LeerIndiceTarea(string mensaje)
    {
        Console.WriteLine();
        Console.WriteLine("0. Volver al menu principal");

        int numero = LeerOpcionEntera(mensaje);

        if (numero == 0)
        {
            return -2;
        }

        if (numero < 1 || numero > titulos.Count)
        {
            Console.WriteLine("Numero de tarea no valido.");
            return -1;
        }

        return numero - 1;
    }

    static string LeerEstadoPorOpcion()
    {
        Console.WriteLine();
        Console.WriteLine("0. Volver al menu principal");
        Console.WriteLine("1. Pendiente");
        Console.WriteLine("2. En progreso");
        Console.WriteLine("3. Completada");

        int opcion = LeerOpcionEntera("Seleccione el nuevo estado: ");

        if (opcion == 0)
        {
            return "0";
        }
        else if (opcion == 1)
        {
            return "Pendiente";
        }
        else if (opcion == 2)
        {
            return "En progreso";
        }
        else if (opcion == 3)
        {
            return "Completada";
        }

        return "";
    }

    static string NormalizarEstado(string estado)
    {
        estado = estado.Trim().ToLower();

        if (estado == "pendiente")
        {
            return "Pendiente";
        }
        else if (estado == "en progreso")
        {
            return "En progreso";
        }

        return "Completada";
    }

    static bool EditarTitulo(int indice)
    {
        Console.Write("Nuevo titulo: ");
        string titulo = Console.ReadLine() ?? "";

        if (!ValidarTitulo(titulo))
        {
            Console.WriteLine("El titulo no puede estar vacio.");
            return false;
        }

        titulos[indice] = LimpiarSeparador(titulo.Trim());
        return true;
    }

    static bool EditarPrioridad(int indice)
    {
        Console.Write("Nueva prioridad (ALTA, MEDIA, BAJA): ");
        string prioridad = Console.ReadLine() ?? "";

        if (!ValidarPrioridad(prioridad))
        {
            Console.WriteLine("La prioridad debe ser ALTA, MEDIA o BAJA.");
            return false;
        }

        prioridades[indice] = prioridad.Trim().ToUpper();
        return true;
    }

    static bool EditarFechaLimite(int indice)
    {
        Console.Write("Nueva fecha limite (dd/MM/yyyy o --): ");
        string fechaLimite = Console.ReadLine() ?? "";

        if (!ValidarFecha(fechaLimite))
        {
            Console.WriteLine("La fecha debe tener el formato dd/MM/yyyy o ser --.");
            return false;
        }

        fechasLimite[indice] = fechaLimite.Trim();
        return true;
    }

    static bool EditarEstado(int indice)
    {
        Console.Write("Nuevo estado (Pendiente, En progreso, Completada): ");
        string estado = Console.ReadLine() ?? "";

        if (!ValidarEstado(estado))
        {
            Console.WriteLine("El estado debe ser Pendiente, En progreso o Completada.");
            return false;
        }

        estados[indice] = NormalizarEstado(estado);
        return true;
    }

    static bool EditarCategoria(int indice)
    {
        Console.Write("Nueva categoria: ");
        string categoria = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(categoria))
        {
            Console.WriteLine("La categoria no puede estar vacia.");
            return false;
        }

        categorias[indice] = LimpiarSeparador(categoria.Trim());
        return true;
    }

    static bool EditarDescripcion(int indice)
    {
        Console.Write("Nueva descripcion: ");
        string descripcion = Console.ReadLine() ?? "";

        descripciones[indice] = LimpiarSeparador(descripcion.Trim());
        return true;
    }

    // =======================================
    // FUNCIONES DE FILTROS Y REPORTES
    // =======================================

    static void FiltrarTareas()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("           FILTRAR TAREAS");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas");
            PausarPantalla();
            return;
        }

        Console.WriteLine("1. Filtrar por prioridad");
        Console.WriteLine("2. Filtrar por estado");
        Console.WriteLine("3. Filtrar por categoria");
        Console.WriteLine("0. Volver al menu principal");

        int opcion = LeerOpcionEntera("Seleccione una opcion: ");
        Console.WriteLine();

        switch (opcion)
        {
            case 1:
                FiltrarPorPrioridad();
                break;
            case 2:
                FiltrarPorEstado();
                break;
            case 3:
                FiltrarPorCategoria();
                break;
            case 0:
                return;
            default:
                Console.WriteLine("Opcion no valida.");
                break;
        }

        PausarPantalla();
    }

    static void MostrarReporte()
    {
        Console.Clear();
        Console.WriteLine("=======================================");
        Console.WriteLine("          REPORTE DE TAREAS");
        Console.WriteLine("=======================================");
        Console.WriteLine();

        int pendientes = 0;
        int enProgreso = 0;
        int completadas = 0;
        int prioridadAlta = 0;
        int prioridadMedia = 0;
        int prioridadBaja = 0;

        for (int i = 0; i < titulos.Count; i++)
        {
            if (estados[i] == "Pendiente")
            {
                pendientes++;
            }
            else if (estados[i] == "En progreso")
            {
                enProgreso++;
            }
            else if (estados[i] == "Completada")
            {
                completadas++;
            }

            if (prioridades[i] == "ALTA")
            {
                prioridadAlta++;
            }
            else if (prioridades[i] == "MEDIA")
            {
                prioridadMedia++;
            }
            else if (prioridades[i] == "BAJA")
            {
                prioridadBaja++;
            }
        }

        Console.WriteLine("Total de tareas: " + titulos.Count);
        Console.WriteLine("Tareas Pendientes: " + pendientes);
        Console.WriteLine("Tareas En progreso: " + enProgreso);
        Console.WriteLine("Tareas Completadas: " + completadas);
        Console.WriteLine("Tareas con prioridad ALTA: " + prioridadAlta);
        Console.WriteLine("Tareas con prioridad MEDIA: " + prioridadMedia);
        Console.WriteLine("Tareas con prioridad BAJA: " + prioridadBaja);

        PausarPantalla();
    }

    static void FiltrarPorPrioridad()
    {
        Console.Write("Prioridad (ALTA, MEDIA, BAJA): ");
        string prioridad = Console.ReadLine() ?? "";

        if (!ValidarPrioridad(prioridad))
        {
            Console.WriteLine("La prioridad debe ser ALTA, MEDIA o BAJA.");
            return;
        }

        prioridad = prioridad.Trim().ToUpper();
        MostrarResultadosPorPrioridad(prioridad);
    }

    static void FiltrarPorEstado()
    {
        Console.Write("Estado (Pendiente, En progreso, Completada): ");
        string estado = Console.ReadLine() ?? "";

        if (!ValidarEstado(estado))
        {
            Console.WriteLine("El estado debe ser Pendiente, En progreso o Completada.");
            return;
        }

        estado = NormalizarEstado(estado);
        MostrarResultadosPorEstado(estado);
    }

    static void FiltrarPorCategoria()
    {
        Console.Write("Categoria: ");
        string categoria = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(categoria))
        {
            Console.WriteLine("Debe ingresar una categoria para filtrar.");
            return;
        }

        categoria = categoria.Trim().ToLower();

        MostrarResultadosPorCategoria(categoria);
    }

    static void MostrarEncabezadoResultados()
    {
        Console.WriteLine(
            "No.".PadRight(5) +
            "Titulo".PadRight(25) +
            "Prioridad".PadRight(12) +
            "Fecha".PadRight(14) +
            "Estado".PadRight(15) +
            "Categoria".PadRight(20) +
            "Descripcion".PadRight(30));

        Console.WriteLine(new string('-', 121));
    }

    static void MostrarFilaResultado(int indice)
    {
        Console.WriteLine(
            (indice + 1).ToString().PadRight(5) +
            FormatearColumna(titulos[indice], 25) +
            prioridades[indice].PadRight(12) +
            fechasLimite[indice].PadRight(14) +
            estados[indice].PadRight(15) +
            FormatearColumna(categorias[indice], 20) +
            FormatearColumna(descripciones[indice], 30));
    }

    static void MostrarResultadosPorPrioridad(string prioridad)
    {
        bool hayResultados = false;
        MostrarEncabezadoResultados();

        for (int i = 0; i < titulos.Count; i++)
        {
            if (prioridades[i] == prioridad)
            {
                MostrarFilaResultado(i);
                hayResultados = true;
            }
        }

        if (!hayResultados)
        {
            Console.WriteLine("No se encontraron tareas con ese filtro");
        }
    }

    static void MostrarResultadosPorEstado(string estado)
    {
        bool hayResultados = false;
        MostrarEncabezadoResultados();

        for (int i = 0; i < titulos.Count; i++)
        {
            if (estados[i] == estado)
            {
                MostrarFilaResultado(i);
                hayResultados = true;
            }
        }

        if (!hayResultados)
        {
            Console.WriteLine("No se encontraron tareas con ese filtro");
        }
    }

    static void MostrarResultadosPorCategoria(string categoria)
    {
        bool hayResultados = false;
        MostrarEncabezadoResultados();

        for (int i = 0; i < titulos.Count; i++)
        {
            if (categorias[i].Trim().ToLower().Contains(categoria))
            {
                MostrarFilaResultado(i);
                hayResultados = true;
            }
        }

        if (!hayResultados)
        {
            Console.WriteLine("No se encontraron tareas con ese filtro");
        }
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
