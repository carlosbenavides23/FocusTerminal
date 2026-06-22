# FocusTerminal

FocusTerminal es una aplicacion de consola desarrollada en C# para registrar, organizar, consultar y dar seguimiento a tareas academicas mediante programacion estructurada, listas paralelas y archivos de texto.

## Objetivo

Apoyar la organizacion academica de estudiantes universitarios mediante un gestor de tareas local, ligero, claro y facil de usar desde la terminal.

## Estado actual

Version final de entrega: v1.0.

El proyecto cuenta con gestion completa de tareas, persistencia local, respaldo automatico, filtros, reportes, interfaz TUI en consola e instalador para Windows.

## Caracteristicas principales

- Registro de tareas academicas con titulo, prioridad, fecha limite, estado, categoria y descripcion.
- Pantalla principal con interfaz TUI, vista rapida de tareas, resumen general y acciones principales.
- Consulta completa de tareas registradas.
- Edicion de tareas existentes, incluyendo prioridad, estado, categoria, descripcion y fecha limite.
- Eliminacion de tareas con confirmacion.
- Filtros por prioridad, estado, categoria y fecha limite.
- Busqueda manual por categoria.
- Consulta de tareas sin fecha asignada.
- Reporte general por estado y prioridad.
- Guardado y carga de datos mediante archivo de texto.
- Copia de respaldo automatica con tareas_backup.txt.
- Recuperacion desde respaldo si falta tareas.txt.
- Instalador para Windows generado con Inno Setup.
- Version portable en archivo comprimido.

## Interfaz de usuario

FocusTerminal utiliza una interfaz TUI construida directamente en consola. La interfaz se organiza mediante encabezados, tablas, cajas y paneles de acciones.

La pantalla principal muestra:

- Vista rapida de tareas registradas.
- Resumen general de tareas.
- Acciones principales del sistema.

La vista rapida muestra un maximo de 5 tareas para evitar saturar la pantalla. Para revisar el listado completo, el usuario puede usar la opcion Consultar tareas.

La prioridad de las tareas se representa con colores:

- ALTA: rojo.
- MEDIA: amarillo.
- BAJA: verde.

El resto de la interfaz mantiene un estilo sobrio para priorizar la legibilidad.

## Mejoras de experiencia de usuario

El programa busca reducir la carga cognitiva del usuario mediante menus numericos, pantallas limpias y mensajes claros.

Actualmente se seleccionan por numero:

- Prioridades: ALTA, MEDIA, BAJA.
- Estados: Pendiente, En progreso, Completada.
- Categorias basicas: Estudio, Trabajo, Personal, Salud, Hogar.

El usuario solo escribe manualmente cuando es necesario, por ejemplo:

- Titulo de la tarea.
- Fecha limite.
- Descripcion.
- Categoria personalizada.
- Busqueda manual por categoria.

## Persistencia de datos

FocusTerminal guarda la informacion localmente usando archivos de texto.

- tareas.txt: archivo principal de datos.
- tareas_backup.txt: copia de seguridad.

Al iniciar, el programa intenta cargar primero tareas.txt. Si no existe, intenta recuperar los datos desde tareas_backup.txt. Al salir, las tareas se guardan usando StreamWriter, y la carga se realiza usando StreamReader.

Nota: tareas.txt y tareas_backup.txt son archivos generados durante la ejecucion del programa. No forman parte del codigo fuente principal del repositorio.

## Tecnologias utilizadas

- C#
- .NET
- Aplicacion de consola
- Programacion estructurada
- Listas paralelas
- Archivos de texto
- StreamWriter
- StreamReader
- Visual Studio
- Inno Setup
- Git y GitHub

## Estructura del proyecto

FocusTerminal/
- docs/
  - manual-usuario.md
  - plan-pruebas.md
- src/
  - FocusTerminal/
    - FocusTerminal.csproj
    - Program.cs
- .gitignore
- README.md

## Descarga

La version final del sistema se encuentra disponible en GitHub Releases:

https://github.com/carlosbenavides23/FocusTerminal/releases/tag/v1.0

La release incluye:

- Instalador para Windows (.exe).
- Version portable (.zip).
- Codigo fuente del proyecto.

## Instalacion

Opcion recomendada:

1. Descargar el instalador desde GitHub Releases.
2. Ejecutar FocusTerminal-Setup-v1.0.exe.
3. Seguir el asistente de instalacion.
4. Abrir FocusTerminal desde el acceso directo o desde la carpeta instalada.

Opcion portable:

1. Descargar el archivo .zip desde GitHub Releases.
2. Descomprimir el archivo.
3. Ejecutar FocusTerminal.exe.

## Ejecucion desde codigo fuente

Desde la raiz del repositorio:

dotnet run --project src\FocusTerminal\FocusTerminal.csproj

Para compilar:

dotnet build src\FocusTerminal\FocusTerminal.csproj

## Publicacion

Para generar una version publicable desde terminal:

dotnet publish src\FocusTerminal\FocusTerminal.csproj -c Release -r win-x64 --self-contained true

El instalador de Windows se genera posteriormente usando Inno Setup a partir de la carpeta publish.

## Estado final

FocusTerminal queda como una aplicacion de consola funcional para la gestion academica de tareas, con interfaz TUI, persistencia de datos, respaldo local, filtros, reportes, instalador y version portable para Windows.
