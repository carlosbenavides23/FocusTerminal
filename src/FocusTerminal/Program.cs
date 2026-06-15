// =======================================
// LIBRERÍAS
// =======================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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
    const string ArchivoBackup = "tareas_backup.txt";

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
                    ConsultarTareas();
                    break;
                case 2:
                    AgregarTarea();
                    break;
                case 3:
                    EditarTarea();
                    break;
                case 4:
                    EliminarTarea();
                    break;
                case 5:
                    FiltrarTareas();
                    break;
                case 6:
                    MostrarReporte();
                    break;
                case 0:
                    GuardarTareas();
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
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
        LimpiarPantalla();

        MostrarEncabezadoPrincipal();
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            MostrarCajaSinTareas("VISTA RAPIDA");
        }
        else
        {
            MostrarTablaDashboard(5);

            if (titulos.Count > 5)
            {
                Console.WriteLine();
                Console.WriteLine("Mostrando 5 de " + titulos.Count + " tareas registradas.");
                Console.WriteLine("Use [1] Consultar tareas para ver el listado completo.");
            }
        }
        Console.WriteLine();

        MostrarResumenDashboard();
        Console.WriteLine();

        MostrarPanelAcciones();
        Console.WriteLine();
    }

    static void ConsultarTareas()
    {
        LimpiarPantalla();

        MostrarEncabezadoDoble("CONSULTAR TAREAS");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            MostrarCajaSinTareas("TAREAS REGISTRADAS");
        }
        else
        {
            MostrarTablaDashboard(0);
        }

        Console.WriteLine();
        PausarPantalla();
    }

    // =======================================
    // FUNCIONES AUXILIARES DEL DASHBOARD
    // =======================================

    static void MostrarEncabezadoPrincipal()
    {
        int anchoInterior = 78;
        string titulo = "FOCUSTERMINAL";
        string subtitulo = "Gestor academico de tareas";

        Console.WriteLine("\u2554" + new string('\u2550', anchoInterior) + "\u2557");
        Console.WriteLine("\u2551" + CentrarTexto(titulo, anchoInterior) + "\u2551");
        Console.WriteLine("\u2551" + CentrarTexto(subtitulo, anchoInterior) + "\u2551");
        Console.WriteLine("\u255A" + new string('\u2550', anchoInterior) + "\u255D");
    }

    static void MostrarResumenDashboard()
    {
        int pendientes = 0;
        int enProgreso = 0;
        int completadas = 0;

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
        }

        int anchoInterior = 78;
        string etiqueta = " RESUMEN ";
        string contenido = " Total: " + titulos.Count
            + "        Pendientes: " + pendientes
            + "        En progreso: " + enProgreso
            + "        Completadas: " + completadas;

        int espacioTotal = anchoInterior - etiqueta.Length;
        int izquierda = espacioTotal / 2;
        int derecha = espacioTotal - izquierda;
        Console.WriteLine("\u250C" + new string('\u2500', izquierda) + etiqueta + new string('\u2500', derecha) + "\u2510");

        if (contenido.Length > anchoInterior)
        {
            contenido = contenido.Substring(0, anchoInterior);
        }

        Console.WriteLine("\u2502" + contenido.PadRight(anchoInterior) + "\u2502");
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static void MostrarTablaDashboard(int limite)
    {
        List<int> indices = new List<int>();
        int totalMostrar = titulos.Count;

        if (limite > 0 && totalMostrar > limite)
        {
            totalMostrar = limite;
        }

        for (int i = 0; i < totalMostrar; i++)
        {
            indices.Add(i);
        }

        string titulo = limite > 0 ? "VISTA RAPIDA" : "TAREAS REGISTRADAS";
        MostrarTablaTareas(indices, titulo);
    }

    static void MostrarTablaTareas(List<int> indices, string titulo)
    {
        int anchoNo = 4;
        int anchoTitulo = 18;
        int anchoPrioridad = 10;
        int anchoEstado = 12;
        int anchoDescripcion = 20;
        int anchoFecha = 10;
        int anchoInterior = (anchoNo + 2) + (anchoTitulo + 2)
            + (anchoPrioridad + 2) + (anchoEstado + 2)
            + (anchoDescripcion + 2) + (anchoFecha + 2) + 5;

        string etiqueta = " " + titulo + " ";
        int espacioEtiqueta = anchoInterior - etiqueta.Length;
        int etiquetaIzq = espacioEtiqueta / 2;
        int etiquetaDer = espacioEtiqueta - etiquetaIzq;

        Console.WriteLine("\u250C" + new string('\u2500', etiquetaIzq) + etiqueta + new string('\u2500', etiquetaDer) + "\u2510");

        string encabezado = "\u2502"
            + " " + AjustarTexto("No.", anchoNo) + " "
            + "\u2502" + " " + AjustarTexto("Titulo", anchoTitulo) + " "
            + "\u2502" + " " + AjustarTexto("Prioridad", anchoPrioridad) + " "
            + "\u2502" + " " + AjustarTexto("Estado", anchoEstado) + " "
            + "\u2502" + " " + AjustarTexto("Descripcion", anchoDescripcion) + " "
            + "\u2502" + " " + AjustarTexto("Fecha", anchoFecha) + " "
            + "\u2502";
        Console.WriteLine(encabezado);

        Console.WriteLine("\u251C"
            + new string('\u2500', anchoNo + 2) + "\u253C"
            + new string('\u2500', anchoTitulo + 2) + "\u253C"
            + new string('\u2500', anchoPrioridad + 2) + "\u253C"
            + new string('\u2500', anchoEstado + 2) + "\u253C"
            + new string('\u2500', anchoDescripcion + 2) + "\u253C"
            + new string('\u2500', anchoFecha + 2)
            + "\u2524");

        for (int i = 0; i < indices.Count; i++)
        {
            int indice = indices[i];

            Console.Write("\u2502"
                + " " + AjustarTexto((indice + 1).ToString(), anchoNo) + " "
                + "\u2502" + " " + AjustarTexto(titulos[indice], anchoTitulo) + " "
                + "\u2502" + " ");

            EscribirPrioridadConColor(prioridades[indice], anchoPrioridad);

            Console.WriteLine(" "
                + "\u2502" + " " + AjustarTexto(estados[indice], anchoEstado) + " "
                + "\u2502" + " " + AjustarTexto(descripciones[indice], anchoDescripcion) + " "
                + "\u2502" + " " + AjustarTexto(fechasLimite[indice], anchoFecha) + " "
                + "\u2502");
        }

        Console.WriteLine("\u2514"
            + new string('\u2500', anchoNo + 2) + "\u2534"
            + new string('\u2500', anchoTitulo + 2) + "\u2534"
            + new string('\u2500', anchoPrioridad + 2) + "\u2534"
            + new string('\u2500', anchoEstado + 2) + "\u2534"
            + new string('\u2500', anchoDescripcion + 2) + "\u2534"
            + new string('\u2500', anchoFecha + 2)
            + "\u2518");
    }

    static void MostrarCajaSinTareas(string titulo)
    {
        int anchoInterior = 78;
        string etiqueta = " " + titulo + " ";
        int espacioEtiqueta = anchoInterior - etiqueta.Length;
        int etiquetaIzq = espacioEtiqueta / 2;
        int etiquetaDer = espacioEtiqueta - etiquetaIzq;

        string mensaje = " No hay tareas registradas.";

        Console.WriteLine("\u250C" + new string('\u2500', etiquetaIzq) + etiqueta + new string('\u2500', etiquetaDer) + "\u2510");
        Console.WriteLine("\u2502" + mensaje.PadRight(anchoInterior) + "\u2502");
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static void MostrarPanelAcciones()
    {
        int anchoInterior = 78;
        string etiqueta = " ACCIONES ";
        int espacioEtiqueta = anchoInterior - etiqueta.Length;
        int etiquetaIzq = espacioEtiqueta / 2;
        int etiquetaDer = espacioEtiqueta - etiquetaIzq;

        Console.WriteLine("\u250C" + new string('\u2500', etiquetaIzq) + etiqueta + new string('\u2500', etiquetaDer) + "\u2510");
        MostrarFilaAcciones("[1] Consultar tareas", "[2] Agregar tarea", "[3] Editar tarea");
        MostrarFilaAcciones("[4] Eliminar tarea", "[5] Filtrar tareas", "[6] Mostrar reporte");
        MostrarFilaAcciones("[0] Guardar y salir", "", "");
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static void MostrarFilaAcciones(string accion1, string accion2, string accion3)
    {
        Console.WriteLine("\u2502 "
            + accion1.PadRight(25)
            + accion2.PadRight(25)
            + accion3.PadRight(26)
            + " \u2502");
    }

    static void EscribirPrioridadConColor(string prioridad, int ancho)
    {
        ConsoleColor colorOriginal = Console.ForegroundColor;

        try
        {
            if (prioridad == "ALTA")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (prioridad == "MEDIA")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (prioridad == "BAJA")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.Write(AjustarTexto(prioridad, ancho));
        }
        finally
        {
            Console.ForegroundColor = colorOriginal;
        }
    }

    // =======================================
    // FUNCIONES AUXILIARES VISUALES GENERALES
    // =======================================

    static void MostrarEncabezadoDoble(string titulo)
    {
        int anchoInterior = 60;

        Console.WriteLine("\u2554" + new string('\u2550', anchoInterior) + "\u2557");
        Console.WriteLine("\u2551" + CentrarTexto(titulo, anchoInterior) + "\u2551");
        Console.WriteLine("\u255A" + new string('\u2550', anchoInterior) + "\u255D");
    }

    static void MostrarEncabezadoSeccion(string titulo)
    {
        int anchoInterior = 60;
        string etiqueta = " " + titulo + " ";
        int espacioEtiqueta = anchoInterior - etiqueta.Length;
        int etiquetaIzq = espacioEtiqueta / 2;
        int etiquetaDer = espacioEtiqueta - etiquetaIzq;

        Console.WriteLine("\u250C" + new string('\u2500', etiquetaIzq) + etiqueta + new string('\u2500', etiquetaDer) + "\u2510");
    }

    static void MostrarLineaCaja(string contenido)
    {
        int anchoInterior = 60;
        string linea = " " + contenido;

        if (linea.Length > anchoInterior)
        {
            linea = linea.Substring(0, anchoInterior);
        }

        Console.WriteLine("\u2502" + linea.PadRight(anchoInterior) + "\u2502");
    }

    static void MostrarCierreCaja()
    {
        int anchoInterior = 60;
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static void MostrarMensajeExito(string mensaje)
    {
        int anchoInterior = 60;
        string contenido = " [OK] " + mensaje;

        if (contenido.Length > anchoInterior)
        {
            contenido = contenido.Substring(0, anchoInterior);
        }

        Console.WriteLine("\u250C" + new string('\u2500', anchoInterior) + "\u2510");
        Console.WriteLine("\u2502" + contenido.PadRight(anchoInterior) + "\u2502");
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static void MostrarMensajeError(string mensaje)
    {
        int anchoInterior = 60;
        string contenido = " [ERROR] " + mensaje;

        if (contenido.Length > anchoInterior)
        {
            contenido = contenido.Substring(0, anchoInterior);
        }

        Console.WriteLine("\u250C" + new string('\u2500', anchoInterior) + "\u2510");
        Console.WriteLine("\u2502" + contenido.PadRight(anchoInterior) + "\u2502");
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static void MostrarMensajeAdvertencia(string mensaje)
    {
        int anchoInterior = 60;
        string contenido = " [!] " + mensaje;

        if (contenido.Length > anchoInterior)
        {
            contenido = contenido.Substring(0, anchoInterior);
        }

        Console.WriteLine("\u250C" + new string('\u2500', anchoInterior) + "\u2510");
        Console.WriteLine("\u2502" + contenido.PadRight(anchoInterior) + "\u2502");
        Console.WriteLine("\u2514" + new string('\u2500', anchoInterior) + "\u2518");
    }

    static string CentrarTexto(string texto, int ancho)
    {
        if (texto.Length >= ancho)
        {
            return texto.Substring(0, ancho);
        }

        int espacioTotal = ancho - texto.Length;
        int izquierda = espacioTotal / 2;
        int derecha = espacioTotal - izquierda;

        return new string(' ', izquierda) + texto + new string(' ', derecha);
    }

    static string AjustarTexto(string texto, int ancho)
    {
        texto = texto.Trim();

        if (texto.Length > ancho)
        {
            return texto.Substring(0, ancho - 3) + "...";
        }

        return texto.PadRight(ancho);
    }

    static void PausarPantalla()
    {
        Console.WriteLine();
        Console.WriteLine("Presione Enter para volver al menu principal...");
        Console.ReadLine();
    }

    static void LimpiarPantalla()
    {
        try
        {
            Console.Clear();
        }
        catch
        {
            Console.WriteLine();
        }
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
        LimpiarPantalla();

        MostrarEncabezadoDoble("AGREGAR TAREA");
        Console.WriteLine();

        string titulo;
        string prioridad;
        string fechaLimite;
        string estado;
        string categoria;
        string descripcion;

        do
        {
            Console.WriteLine("Titulo:");
            Console.Write("> ");
            titulo = Console.ReadLine() ?? "";

            if (!ValidarTitulo(titulo))
            {
                MostrarMensajeError("El titulo no puede estar vacio.");
            }
        }
        while (!ValidarTitulo(titulo));

        do
        {
            prioridad = SeleccionarPrioridad();

            if (prioridad == "")
            {
                MostrarMensajeError("Opcion no valida.");
            }
        }
        while (prioridad == "");

        if (prioridad == "0")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            PausarPantalla();
            return;
        }

        do
        {
            Console.Write("Fecha limite (dd/MM/yyyy o --): ");
            fechaLimite = Console.ReadLine() ?? "";

            if (!ValidarFecha(fechaLimite))
            {
                MostrarMensajeError("La fecha debe tener formato dd/MM/yyyy o ser --.");
            }
        }
        while (!ValidarFecha(fechaLimite));

        fechaLimite = fechaLimite.Trim();

        do
        {
            estado = SeleccionarEstado();

            if (estado == "")
            {
                MostrarMensajeError("Opcion no valida.");
            }
        }
        while (estado == "");

        if (estado == "0")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            PausarPantalla();
            return;
        }

        do
        {
            categoria = SeleccionarCategoria();

            if (categoria == "")
            {
                MostrarMensajeError("Opcion no valida.");
            }
        }
        while (categoria == "");

        if (categoria == "CANCELAR")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            PausarPantalla();
            return;
        }

        Console.Write("Descripcion: ");
        descripcion = Console.ReadLine() ?? "";

        titulos.Add(LimpiarSeparador(titulo.Trim()));
        prioridades.Add(prioridad);
        fechasLimite.Add(fechaLimite);
        estados.Add(estado);
        categorias.Add(LimpiarSeparador(categoria.Trim()));
        descripciones.Add(LimpiarSeparador(descripcion.Trim()));

        Console.WriteLine();
        MostrarMensajeExito("Tarea agregada correctamente.");
        PausarPantalla();
    }

    static string SeleccionarPrioridad()
    {
        Console.WriteLine();
        Console.WriteLine("Prioridades:");
        Console.WriteLine("1. ALTA");
        Console.WriteLine("2. MEDIA");
        Console.WriteLine("3. BAJA");
        Console.WriteLine("0. Cancelar");

        int opcion = LeerOpcionEntera("Seleccione una prioridad: ");

        if (opcion == 1)
        {
            return "ALTA";
        }
        else if (opcion == 2)
        {
            return "MEDIA";
        }
        else if (opcion == 3)
        {
            return "BAJA";
        }
        else if (opcion == 0)
        {
            return "0";
        }

        return "";
    }

    static string SeleccionarEstado()
    {
        Console.WriteLine();
        Console.WriteLine("Estados:");
        Console.WriteLine("1. Pendiente");
        Console.WriteLine("2. En progreso");
        Console.WriteLine("3. Completada");
        Console.WriteLine("0. Cancelar");

        int opcion = LeerOpcionEntera("Seleccione un estado: ");

        if (opcion == 1)
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
        else if (opcion == 0)
        {
            return "0";
        }

        return "";
    }

    static string SeleccionarCategoria()
    {
        Console.WriteLine();
        Console.WriteLine("Categorias:");
        Console.WriteLine("1. Estudio");
        Console.WriteLine("2. Trabajo");
        Console.WriteLine("3. Personal");
        Console.WriteLine("4. Salud");
        Console.WriteLine("5. Hogar");
        Console.WriteLine("6. Otra categoria");
        Console.WriteLine("0. Cancelar");

        int opcion = LeerOpcionEntera("Seleccione una categoria: ");

        if (opcion == 1)
        {
            return "Estudio";
        }
        else if (opcion == 2)
        {
            return "Trabajo";
        }
        else if (opcion == 3)
        {
            return "Personal";
        }
        else if (opcion == 4)
        {
            return "Salud";
        }
        else if (opcion == 5)
        {
            return "Hogar";
        }
        else if (opcion == 6)
        {
            Console.Write("Nueva categoria: ");
            string categoria = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("La categoria no puede estar vacia.");
                return "";
            }

            return LimpiarSeparador(categoria.Trim());
        }
        else if (opcion == 0)
        {
            return "CANCELAR";
        }

        return "";
    }

    static void EditarTarea()
    {
        LimpiarPantalla();

        MostrarEncabezadoDoble("EDITAR TAREA");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            MostrarCajaSinTareas("EDITAR TAREA");
            PausarPantalla();
            return;
        }

        MostrarTablaDashboard(0);

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
        MostrarEncabezadoSeccion("CAMPO A EDITAR");
        MostrarLineaCaja("[1] Titulo");
        MostrarLineaCaja("[2] Prioridad");
        MostrarLineaCaja("[3] Estado");
        MostrarLineaCaja("[4] Categoria");
        MostrarLineaCaja("[5] Descripcion");
        MostrarLineaCaja("[6] Fecha limite");
        MostrarLineaCaja("[0] Volver");
        MostrarCierreCaja();

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
                modificado = EditarEstado(indice);
                break;
            case 4:
                modificado = EditarCategoria(indice);
                break;
            case 5:
                modificado = EditarDescripcion(indice);
                break;
            case 6:
                modificado = EditarFechaLimite(indice);
                break;
            case 0:
                MostrarMensajeAdvertencia("Operacion cancelada.");
                PausarPantalla();
                return;
            default:
                MostrarMensajeError("Opcion no valida.");
                break;
        }

        if (modificado)
        {
            MostrarMensajeExito("Tarea editada correctamente.");
        }

        PausarPantalla();
    }

    static void EliminarTarea()
    {
        LimpiarPantalla();

        MostrarEncabezadoDoble("ELIMINAR TAREA");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            MostrarCajaSinTareas("ELIMINAR TAREA");
            PausarPantalla();
            return;
        }

        MostrarTablaDashboard(0);

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

        Console.WriteLine();
        MostrarEncabezadoSeccion("CONFIRMAR ELIMINACION");
        MostrarLineaCaja("S: Confirmar");
        MostrarLineaCaja("N: Cancelar");
        MostrarLineaCaja("0: Volver");
        MostrarCierreCaja();
        Console.Write("Seleccione una opcion: ");
        string confirmacion = Console.ReadLine() ?? "";
        confirmacion = confirmacion.Trim().ToUpper();

        if (confirmacion == "0")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            PausarPantalla();
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

            MostrarMensajeExito("Tarea eliminada correctamente.");
        }
        else if (confirmacion == "N")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
        }
        else
        {
            MostrarMensajeError("Opcion no valida.");
        }

        PausarPantalla();
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
        string prioridad = SeleccionarPrioridad();

        if (prioridad == "0")
        {
            Console.WriteLine("Operacion cancelada.");
            return false;
        }

        if (prioridad == "")
        {
            Console.WriteLine("Opcion no valida.");
            return false;
        }

        prioridades[indice] = prioridad;
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
        string estado = SeleccionarEstado();

        if (estado == "0")
        {
            Console.WriteLine("Operacion cancelada.");
            return false;
        }

        if (estado == "")
        {
            Console.WriteLine("Opcion no valida.");
            return false;
        }

        estados[indice] = estado;
        return true;
    }

    static bool EditarCategoria(int indice)
    {
        string categoria = SeleccionarCategoria();

        if (categoria == "CANCELAR")
        {
            Console.WriteLine("Operacion cancelada.");
            return false;
        }

        if (categoria == "")
        {
            Console.WriteLine("Opcion no valida.");
            return false;
        }

        categorias[indice] = categoria.Trim();
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
        LimpiarPantalla();

        MostrarEncabezadoDoble("FILTRAR TAREAS");
        Console.WriteLine();

        if (titulos.Count == 0)
        {
            MostrarCajaSinTareas("FILTRAR TAREAS");
            PausarPantalla();
            return;
        }

        MostrarEncabezadoSeccion("TIPO DE FILTRO");
        MostrarLineaCaja("[1] Por prioridad");
        MostrarLineaCaja("[2] Por estado");
        MostrarLineaCaja("[3] Por categoria");
        MostrarLineaCaja("[4] Por fecha limite");
        MostrarLineaCaja("[0] Volver");
        MostrarCierreCaja();

        int opcion = LeerOpcionEntera("Seleccione una opcion: ");

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
            case 4:
                FiltrarPorFecha();
                break;
            case 0:
                return;
            default:
                MostrarMensajeError("Opcion no valida.");
                break;
        }

        PausarPantalla();
    }

    static void MostrarReporte()
    {
        LimpiarPantalla();

        MostrarEncabezadoDoble("REPORTE");
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

        // Caja: Resumen general
        MostrarEncabezadoSeccion("RESUMEN GENERAL");
        MostrarLineaCaja("Total de tareas: " + titulos.Count);
        MostrarCierreCaja();
        Console.WriteLine();

        // Caja: Por estado
        MostrarEncabezadoSeccion("POR ESTADO");
        MostrarLineaCaja("Pendientes:     " + pendientes);
        MostrarLineaCaja("En progreso:    " + enProgreso);
        MostrarLineaCaja("Completadas:    " + completadas);
        MostrarCierreCaja();
        Console.WriteLine();

        // Caja: Por prioridad
        MostrarEncabezadoSeccion("POR PRIORIDAD");
        MostrarLineaCaja("ALTA:           " + prioridadAlta);
        MostrarLineaCaja("MEDIA:          " + prioridadMedia);
        MostrarLineaCaja("BAJA:           " + prioridadBaja);
        MostrarCierreCaja();

        PausarPantalla();
    }

    static void FiltrarPorPrioridad()
    {
        LimpiarPantalla();
        MostrarEncabezadoDoble("FILTRAR POR PRIORIDAD");

        string prioridad = SeleccionarPrioridad();

        if (prioridad == "0")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            return;
        }

        if (prioridad == "")
        {
            MostrarMensajeError("Opcion no valida.");
            return;
        }

        MostrarResultadosPorPrioridad(prioridad);
    }

    static void FiltrarPorEstado()
    {
        LimpiarPantalla();
        MostrarEncabezadoDoble("FILTRAR POR ESTADO");

        string estado = SeleccionarEstado();

        if (estado == "0")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            return;
        }

        if (estado == "")
        {
            MostrarMensajeError("Opcion no valida.");
            return;
        }

        MostrarResultadosPorEstado(estado);
    }

    static void FiltrarPorCategoria()
    {
        LimpiarPantalla();
        MostrarEncabezadoDoble("FILTRAR POR CATEGORIA");
        Console.WriteLine();

        List<string> categoriasExistentes = ObtenerCategoriasExistentes();

        if (categoriasExistentes.Count == 0)
        {
            MostrarMensajeAdvertencia("No hay categorias registradas.");
            return;
        }

        MostrarEncabezadoSeccion("CATEGORIAS DISPONIBLES");

        for (int i = 0; i < categoriasExistentes.Count; i++)
        {
            MostrarLineaCaja("[" + (i + 1) + "] " + categoriasExistentes[i]);
        }

        MostrarLineaCaja("[M] Busqueda manual");
        MostrarLineaCaja("[0] Volver");
        MostrarCierreCaja();
        Console.Write("Seleccione una opcion: ");

        string entrada = (Console.ReadLine() ?? "").Trim();

        if (entrada == "0")
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            return;
        }

        if (entrada.ToUpper() == "M")
        {
            Console.Write("Texto de busqueda: ");
            string busqueda = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                MostrarMensajeError("Debe ingresar una categoria para filtrar.");
                return;
            }

            MostrarResultadosPorCategoria(busqueda, false);
            return;
        }

        int opcion;

        if (!int.TryParse(entrada, out opcion) ||
            opcion < 1 ||
            opcion > categoriasExistentes.Count)
        {
            MostrarMensajeError("Opcion no valida.");
            return;
        }

        MostrarResultadosPorCategoria(categoriasExistentes[opcion - 1], true);
    }

    static void FiltrarPorFecha()
    {
        LimpiarPantalla();
        MostrarEncabezadoDoble("FILTRAR POR FECHA");
        Console.WriteLine();

        MostrarEncabezadoSeccion("TIPO DE BUSQUEDA");
        MostrarLineaCaja("[1] Fecha especifica");
        MostrarLineaCaja("[2] Tareas sin fecha");
        MostrarLineaCaja("[0] Volver");
        MostrarCierreCaja();

        int opcion = LeerOpcionEntera("Seleccione una opcion: ");

        if (opcion == 0)
        {
            MostrarMensajeAdvertencia("Operacion cancelada.");
            return;
        }

        if (opcion == 1)
        {
            Console.Write("Fecha limite (dd/MM/yyyy): ");
            string fecha = (Console.ReadLine() ?? "").Trim();

            if (fecha == "--" || !ValidarFecha(fecha))
            {
                MostrarMensajeError("La fecha debe tener el formato dd/MM/yyyy.");
                return;
            }

            MostrarResultadosPorFecha(fecha);
        }
        else if (opcion == 2)
        {
            MostrarResultadosPorFecha("--");
        }
        else
        {
            MostrarMensajeError("Opcion no valida.");
        }
    }

    static List<string> ObtenerCategoriasExistentes()
    {
        List<string> categoriasExistentes = new List<string>();

        for (int i = 0; i < categorias.Count; i++)
        {
            string categoria = categorias[i].Trim();

            if (string.IsNullOrWhiteSpace(categoria))
            {
                continue;
            }

            bool repetida = false;

            for (int j = 0; j < categoriasExistentes.Count; j++)
            {
                if (string.Equals(
                    categoriasExistentes[j],
                    categoria,
                    StringComparison.OrdinalIgnoreCase))
                {
                    repetida = true;
                    break;
                }
            }

            if (!repetida)
            {
                categoriasExistentes.Add(categoria);
            }
        }

        return categoriasExistentes;
    }

    static void MostrarResultadosPorPrioridad(string prioridad)
    {
        List<int> resultados = new List<int>();

        for (int i = 0; i < titulos.Count; i++)
        {
            if (prioridades[i] == prioridad)
            {
                resultados.Add(i);
            }
        }

        MostrarResultadosFiltro(resultados);
    }

    static void MostrarResultadosPorEstado(string estado)
    {
        List<int> resultados = new List<int>();

        for (int i = 0; i < titulos.Count; i++)
        {
            if (estados[i] == estado)
            {
                resultados.Add(i);
            }
        }

        MostrarResultadosFiltro(resultados);
    }

    static void MostrarResultadosPorCategoria(string categoria, bool coincidenciaExacta)
    {
        List<int> resultados = new List<int>();

        for (int i = 0; i < titulos.Count; i++)
        {
            string categoriaTarea = categorias[i].Trim();
            bool coincide = coincidenciaExacta
                ? string.Equals(categoriaTarea, categoria, StringComparison.OrdinalIgnoreCase)
                : categoriaTarea.ToLower().Contains(categoria.ToLower());

            if (coincide)
            {
                resultados.Add(i);
            }
        }

        MostrarResultadosFiltro(resultados);
    }

    static void MostrarResultadosPorFecha(string fecha)
    {
        List<int> resultados = new List<int>();

        for (int i = 0; i < titulos.Count; i++)
        {
            if (fechasLimite[i] == fecha)
            {
                resultados.Add(i);
            }
        }

        MostrarResultadosFiltro(resultados);
    }

    static void MostrarResultadosFiltro(List<int> resultados)
    {
        Console.WriteLine();

        if (resultados.Count == 0)
        {
            MostrarMensajeAdvertencia("No se encontraron tareas con ese filtro.");
            return;
        }

        MostrarTablaTareas(resultados, "RESULTADOS");
    }

    // =======================================
    // FUNCIONES DE ARCHIVO
    // =======================================

    static void GuardarTareas()
    {
        try
        {
            if (File.Exists(ArchivoTareas))
            {
                File.Copy(ArchivoTareas, ArchivoBackup, true);
            }

            using (StreamWriter escritor = new StreamWriter(ArchivoTareas))
            {
                for (int i = 0; i < titulos.Count; i++)
                {
                    escritor.WriteLine(CrearLineaTarea(i));
                }
            }
        }
        catch
        {
            Console.WriteLine("Error al guardar las tareas.");
        }
    }

    static void CargarTareas()
    {
        LimpiarListasTareas();

        string rutaArchivo;
        bool cargarDesdeBackup = false;

        if (File.Exists(ArchivoTareas))
        {
            rutaArchivo = ArchivoTareas;
        }
        else if (File.Exists(ArchivoBackup))
        {
            rutaArchivo = ArchivoBackup;
            cargarDesdeBackup = true;
        }
        else
        {
            return;
        }

        try
        {
            CargarDesdeArchivo(rutaArchivo);

            if (cargarDesdeBackup)
            {
                File.Copy(ArchivoBackup, ArchivoTareas, true);
            }
        }
        catch
        {
            Console.WriteLine("Error al cargar las tareas.");
        }
    }

    static string CrearLineaTarea(int indice)
    {
        return LimpiarSeparador(titulos[indice].Trim()) + "|" +
               LimpiarSeparador(prioridades[indice].Trim()) + "|" +
               LimpiarSeparador(fechasLimite[indice].Trim()) + "|" +
               LimpiarSeparador(estados[indice].Trim()) + "|" +
               LimpiarSeparador(categorias[indice].Trim()) + "|" +
               LimpiarSeparador(descripciones[indice].Trim());
    }

    static void CargarDesdeArchivo(string rutaArchivo)
    {
        using (StreamReader lector = new StreamReader(rutaArchivo))
        {
            string? linea;

            while ((linea = lector.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                string[] partes = linea.Split('|');

                if (partes.Length != 6)
                {
                    continue;
                }

                string titulo = partes[0].Trim();
                string prioridad = partes[1].Trim();
                string fechaLimite = partes[2].Trim();
                string estado = partes[3].Trim();
                string categoria = partes[4].Trim();
                string descripcion = partes[5].Trim();

                if (!ValidarTitulo(titulo) ||
                    !ValidarPrioridad(prioridad) ||
                    !ValidarFecha(fechaLimite) ||
                    !ValidarEstado(estado))
                {
                    continue;
                }

                titulos.Add(titulo);
                prioridades.Add(prioridad.ToUpper());
                fechasLimite.Add(fechaLimite);
                estados.Add(NormalizarEstado(estado));
                categorias.Add(categoria);
                descripciones.Add(descripcion);
            }
        }
    }

    static void LimpiarListasTareas()
    {
        titulos.Clear();
        prioridades.Clear();
        fechasLimite.Clear();
        estados.Clear();
        categorias.Clear();
        descripciones.Clear();
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
