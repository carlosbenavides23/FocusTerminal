# Manual de Usuario - FocusTerminal

## 1. Inicio del programa

FocusTerminal es una aplicacion de consola para gestionar tareas academicas.

Al iniciar, el programa carga automaticamente las tareas guardadas en tareas.txt. Si tareas.txt no existe, intenta recuperar la informacion desde tareas_backup.txt. Si no existe ningun archivo, el programa inicia sin tareas registradas.

## 2. Menu principal

Al abrir el programa se muestra el menu principal:

1. Mostrar dashboard simple
2. Agregar tarea
3. Editar tarea
4. Cambiar estado
5. Eliminar tarea
6. Filtrar tareas
7. Mostrar reporte
0. Salir

El usuario debe escribir el numero de la opcion que desea usar.

## 3. Mostrar dashboard

La opcion 1 muestra una tabla con las tareas registradas.

El dashboard incluye:

- Numero de tarea
- Titulo
- Prioridad
- Fecha limite
- Estado
- Categoria
- Descripcion

Si no hay tareas registradas, el programa muestra un mensaje indicandolo.

## 4. Registrar tarea

La opcion 2 permite agregar una nueva tarea.

El programa solicita:

- Titulo
- Prioridad
- Fecha limite
- Estado
- Categoria
- Descripcion

Para reducir errores, la prioridad, el estado y la categoria se eligen mediante menus numericos.

### Prioridad

1. ALTA
2. MEDIA
3. BAJA
0. Cancelar

### Estado

1. Pendiente
2. En progreso
3. Completada
0. Cancelar

### Categoria

1. Estudio
2. Trabajo
3. Personal
4. Salud
5. Hogar
6. Otra categoria
0. Cancelar

Si se elige Otra categoria, el usuario puede escribir una categoria personalizada.

## 5. Editar tarea

La opcion 3 permite modificar una tarea existente.

Primero se muestra la lista de tareas. Luego el usuario elige el numero de la tarea que desea editar.

Campos editables:

1. Titulo
2. Prioridad
3. Fecha limite
4. Estado
5. Categoria
6. Descripcion
0. Volver al menu principal

La prioridad, el estado y la categoria se editan usando menus numericos.

## 6. Cambiar estado

La opcion 4 permite cambiar rapidamente el estado de una tarea.

El usuario selecciona la tarea por numero y luego elige el nuevo estado:

1. Pendiente
2. En progreso
3. Completada
0. Cancelar

## 7. Eliminar tarea

La opcion 5 permite eliminar una tarea.

El programa muestra las tareas registradas, solicita el numero de tarea y luego pide confirmacion:

- S: confirmar eliminacion
- N: cancelar operacion
- 0: volver al menu principal

Al eliminar una tarea, el programa elimina el mismo indice en todas las listas paralelas para mantener los datos sincronizados.

## 8. Filtrar tareas

La opcion 6 permite consultar tareas segun diferentes criterios.

Filtros disponibles:

1. Filtrar por prioridad
2. Filtrar por estado
3. Filtrar por categoria
0. Volver al menu principal

### Filtrar por prioridad

El usuario selecciona la prioridad desde un menu numerico.

### Filtrar por estado

El usuario selecciona el estado desde un menu numerico.

### Filtrar por categoria

El programa muestra las categorias existentes en las tareas registradas. El usuario puede elegir una categoria por numero.

Tambien existe la opcion M para realizar una busqueda manual por texto. Esta busqueda permite coincidencias parciales.

Ejemplo: si existe la categoria Programacion, una busqueda por progra puede encontrarla.

## 9. Ver reporte

La opcion 7 muestra estadisticas basicas:

- Total de tareas
- Tareas pendientes
- Tareas en progreso
- Tareas completadas
- Tareas con prioridad ALTA
- Tareas con prioridad MEDIA
- Tareas con prioridad BAJA

## 10. Guardar y cargar informacion

La informacion se guarda automaticamente al salir del programa con la opcion 0.

El sistema utiliza:

- tareas.txt: archivo principal de tareas.
- tareas_backup.txt: copia de respaldo.

Si tareas.txt se borra, el programa intenta cargar la informacion desde tareas_backup.txt y vuelve a crear el archivo principal.

## 11. Recomendaciones de uso

- Usar la opcion 0 para salir correctamente y guardar los cambios.
- No editar manualmente tareas.txt si no es necesario.
- Mantener tareas_backup.txt como respaldo.
- Usar categorias claras para facilitar los filtros.
