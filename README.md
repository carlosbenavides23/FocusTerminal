# FocusTerminal

FocusTerminal es una aplicacion de consola desarrollada en C# para registrar, organizar y consultar tareas academicas mediante programacion estructurada y archivos de texto.

## Objetivo

Apoyar la organizacion academica de estudiantes universitarios mediante un gestor de tareas local, ligero y facil de usar desde la terminal.

## Estado actual

Version actual de avance: v0.2.0.

El proyecto ya cuenta con gestion principal de tareas, persistencia local y mejoras de experiencia de usuario para reducir la escritura manual innecesaria.

## Caracteristicas principales

- Registro de tareas academicas con titulo, prioridad, fecha limite, estado, categoria y descripcion.
- Dashboard simple con informacion principal de las tareas registradas.
- Edicion de tareas existentes.
- Cambio de estado de tareas.
- Eliminacion de tareas.
- Filtros por prioridad, estado y categoria.
- Filtro por categoria usando categorias existentes o busqueda manual.
- Reporte basico de tareas por estado y prioridad.
- Guardado y carga de datos mediante archivo de texto.
- Copia de respaldo con tareas_backup.txt.
- Recuperacion desde respaldo si falta tareas.txt.

## Mejoras de experiencia de usuario

Para reducir la carga cognitiva del usuario, el programa utiliza menus numericos en lugar de exigir que el usuario recuerde nombres exactos.

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

## Tecnologias utilizadas

- C#
- .NET
- Visual Studio 2026

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

Nota: tareas.txt y tareas_backup.txt son archivos generados por la ejecucion del programa. No forman parte del codigo fuente principal del repositorio.

## Ejecucion

Desde la raiz del repositorio:

dotnet run --project src\FocusTerminal\FocusTerminal.csproj

Para compilar:

dotnet build src\FocusTerminal\FocusTerminal.csproj

## Proximas mejoras

- Pruebas finales y correcciones.
- Evaluar identificador unico por tarea.
- Mejoras finales de presentacion visual en consola.
- Preparacion de documentacion final y defensa del proyecto.
