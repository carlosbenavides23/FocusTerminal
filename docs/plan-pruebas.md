# Plan de Pruebas - FocusTerminal

## Objetivo

Verificar que FocusTerminal funcione correctamente en sus funcionalidades principales, persistencia de archivos y mejoras de experiencia de usuario.

## Alcance

Este plan de pruebas cubre la version de avance v0.2.0 del proyecto.

Se evaluan:

- Registro de tareas.
- Edicion de tareas.
- Cambio de estado.
- Eliminacion de tareas.
- Filtros.
- Reportes.
- Persistencia con tareas.txt.
- Recuperacion desde tareas_backup.txt.
- Uso de menus numericos para reducir errores del usuario.

## Casos de prueba

### 1. Inicio sin archivos de datos

Pasos:

1. Eliminar tareas.txt y tareas_backup.txt si existen.
2. Ejecutar el programa.
3. Entrar al dashboard.

Resultado esperado:

- El programa inicia sin errores.
- El dashboard muestra que no hay tareas registradas.

### 2. Agregar tarea valida

Pasos:

1. Elegir la opcion Agregar tarea.
2. Ingresar un titulo valido.
3. Seleccionar prioridad por numero.
4. Ingresar fecha con formato dd/MM/yyyy o --.
5. Seleccionar estado por numero.
6. Seleccionar categoria por numero.
7. Ingresar descripcion.

Resultado esperado:

- La tarea se agrega correctamente.
- La tarea aparece en el dashboard.

### 3. Validar titulo vacio

Pasos:

1. Elegir Agregar tarea.
2. Presionar Enter sin escribir titulo.

Resultado esperado:

- El programa muestra que el titulo no puede estar vacio.
- El programa vuelve a pedir el titulo.

### 4. Validar fecha incorrecta

Pasos:

1. Elegir Agregar tarea.
2. Ingresar una fecha invalida, por ejemplo 40/20/2026.

Resultado esperado:

- El programa muestra mensaje de fecha invalida.
- El programa vuelve a pedir la fecha.

### 5. Categoria personalizada

Pasos:

1. Elegir Agregar tarea.
2. En categoria, seleccionar Otra categoria.
3. Escribir una categoria personalizada.

Resultado esperado:

- La categoria personalizada se guarda correctamente.
- La categoria aparece en dashboard y filtros.

### 6. Editar tarea

Pasos:

1. Elegir Editar tarea.
2. Seleccionar una tarea existente.
3. Elegir un campo editable.
4. Ingresar o seleccionar el nuevo valor.

Resultado esperado:

- El campo seleccionado se actualiza correctamente.
- El dashboard muestra el cambio.

### 7. Cambiar estado

Pasos:

1. Elegir Cambiar estado.
2. Seleccionar una tarea.
3. Elegir un nuevo estado desde el menu numerico.

Resultado esperado:

- El estado cambia correctamente.

### 8. Eliminar tarea

Pasos:

1. Elegir Eliminar tarea.
2. Seleccionar una tarea.
3. Confirmar con S.

Resultado esperado:

- La tarea se elimina.
- Las listas paralelas quedan sincronizadas.
- La tarea ya no aparece en el dashboard.

### 9. Filtrar por prioridad

Pasos:

1. Crear tareas con distintas prioridades.
2. Elegir Filtrar tareas.
3. Seleccionar Filtrar por prioridad.
4. Elegir una prioridad por numero.

Resultado esperado:

- Solo aparecen tareas con la prioridad seleccionada.

### 10. Filtrar por estado

Pasos:

1. Crear tareas con distintos estados.
2. Elegir Filtrar tareas.
3. Seleccionar Filtrar por estado.
4. Elegir un estado por numero.

Resultado esperado:

- Solo aparecen tareas con el estado seleccionado.

### 11. Filtrar por categoria existente

Pasos:

1. Crear tareas con categorias diferentes.
2. Elegir Filtrar tareas.
3. Seleccionar Filtrar por categoria.
4. Elegir una categoria existente por numero.

Resultado esperado:

- Solo aparecen tareas con esa categoria.

### 12. Busqueda manual por categoria

Pasos:

1. Elegir Filtrar por categoria.
2. Seleccionar M para busqueda manual.
3. Escribir parte del nombre de una categoria.

Resultado esperado:

- El sistema muestra coincidencias parciales.

### 13. Mostrar reporte

Pasos:

1. Crear varias tareas con distintos estados y prioridades.
2. Elegir Mostrar reporte.

Resultado esperado:

- El reporte muestra conteos correctos por estado y prioridad.

### 14. Guardar al salir

Pasos:

1. Crear una tarea.
2. Salir con la opcion 0.
3. Verificar que se crea tareas.txt.

Resultado esperado:

- El archivo tareas.txt se crea y contiene los datos guardados.

### 15. Cargar al iniciar

Pasos:

1. Ejecutar el programa despues de haber guardado tareas.
2. Entrar al dashboard.

Resultado esperado:

- Las tareas guardadas aparecen correctamente.

### 16. Recuperar desde backup

Pasos:

1. Crear y guardar tareas.
2. Verificar que exista tareas_backup.txt.
3. Eliminar tareas.txt.
4. Ejecutar el programa otra vez.

Resultado esperado:

- El programa carga los datos desde tareas_backup.txt.
- El programa vuelve a crear tareas.txt.

### 17. Lineas mal formadas en archivo

Pasos:

1. Agregar manualmente una linea incompleta o mal formada en tareas.txt.
2. Ejecutar el programa.

Resultado esperado:

- El programa ignora la linea incorrecta.
- El programa no se detiene ni muestra errores criticos.

## Observaciones pendientes

- Evaluar la implementacion de un identificador unico por tarea.
- Realizar pruebas finales con varios usuarios.
- Revisar mensajes de error y claridad de instrucciones.
- Preparar pruebas para la defensa final del proyecto.
