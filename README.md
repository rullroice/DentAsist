# 🛠️ Cómo instalar y ejecutar

Antes de ejecutar el sistema, asegúrate de que los siguientes programas-extensiones estén instalados en tu equipo:

### ✅ Requisitos

- **.NET SDK**  
  Necesitas tener instalado el SDK de .NET compatible con ASP.NET Core (recomendado: **.NET 6 o superior**).

- **SQL Server**  
  El sistema requiere una instancia de **SQL Server** para gestionar la base de datos de la clínica.

- **wkhtmltopdf**  
  Esta herramienta externa se utiliza para la generación de archivos PDF.  
  Debe estar instalada en tu equipo, y su ruta (especialmente la carpeta `bin`) debe configurarse en el código del programa (`Program.cs`).

  📁 Ejemplo de ruta:  
  `C:\Users\NombreUsuario\OneDrive\Escritorio\PDF\wkhtmltopdf\bin`

---
# 👨‍💻 Guía rápida de uso
Al ingresar al sistema, podrás visualizar el **menú principal**, donde se encuentran las diferentes opciones disponibles para la navegación y operación del sistema.

&nbsp;

![Principal - Menu](https://github.com/user-attachments/assets/c35d7592-796c-4bbd-93a3-786a43ae022a)

&nbsp;

---

Más abajo, tendrás acceso a los distintos **módulos de registro y visualización** que el sistema ofrece para facilitar tus tareas.

&nbsp;

[![Módulos - Menú](https://github.com/user-attachments/assets/c69c61db-4080-428a-9a1d-103af3d85e87)](https://github.com/user-attachments/assets/c69c61db-4080-428a-9a1d-103af3d85e87)

---

## 🧑‍⚕️ Módulo de Odontólogos

Luego de familiarizarte con el menú principal y sus opciones generales, puedes ingresar a los distintos módulos según tu perfil de usuario.

Uno de los módulos más importantes es el de **Odontólogos**, donde se gestiona la información de los profesionales, su especialidad, y se configuran ciertos permisos o accesos.

&nbsp;

![Menu -Lista de Odontologos](https://github.com/user-attachments/assets/cc1ca9e8-049b-4203-b738-e9837cd8c668)

&nbsp;

---
## 📋 Detalles del Turno

Desde el módulo de Odontólogos también es posible acceder a los **detalles de cada turno asignado**. Esta vista permite conocer información clave como la fecha, duración, estado del turno, así como los datos del paciente y del odontólogo correspondiente.

Además, desde esta misma pantalla se pueden realizar acciones como **editar** la información del turno o **volver al listado general**.

&nbsp;

![Opcion - Detalles de turno](https://github.com/user-attachments/assets/f6e511ab-0370-4908-9513-fc105d7bdba9)

&nbsp;

---

## 🧑‍⚕️ Detalles del Odontólogo

Al seleccionar un odontólogo específico desde el listado general, se accede a una **vista detallada** con toda su información profesional, como nombre, matrícula, especialidad y correo electrónico.

Desde esta pantalla, también es posible:

- **Editar los datos del profesional**
- Consultar su **agenda semanal**
- Ver los **pacientes asignados**
- Visualizar los **turnos asociados**
- Acceder a los **planes de tratamiento creados** para cada paciente, con opción de ver detalles, editar o generar el archivo en PDF

&nbsp;

![Opcion - Detalles](https://github.com/user-attachments/assets/3f91e2b5-5d8b-4099-8e73-2a136a4e7c5d)

&nbsp;

---

## ✏️ Editar Odontólogo

Desde la vista de detalles del odontólogo, puedes acceder al formulario de **edición de datos personales y profesionales** del odontólogo seleccionado.

Esta pantalla permite actualizar los siguientes campos:

- **Nombre**
- **Matrícula**
- **Especialidad**
- **Correo electrónico**

Una vez realizada la edición, puedes elegir entre:

- **Guardar cambios** para actualizar la información en el sistema.
- **Cancelar** para descartar cualquier modificación.
- **Volver al listado** para regresar al panel general sin realizar cambios.

&nbsp;

![Opcion - Editar](https://github.com/user-attachments/assets/7b1a1afb-1a32-47e5-b77e-e60cae7d26f2)

&nbsp;

---

## 📆 Agenda Semanal del Odontólogo

Desde la vista de detalles del odontólogo, puedes acceder a su **agenda semanal**, la cual muestra los turnos programados entre dos fechas específicas. Esta pantalla permite:

- Visualizar los turnos por día de la semana.
- Ver detalles clave como la hora, paciente asignado, estado y descripción.
- Acceder rápidamente a las acciones de **Editar** o **ver Detalles** del turno.
- Navegar entre semanas usando los botones: **Semana Anterior**, **Hoy** y **Semana Siguiente**.

Si no hay turnos programados en un día, el sistema lo indicará con claridad.

&nbsp;

![Opcion - Ver agenda](https://github.com/user-attachments/assets/d4fac73c-042e-4ffa-8529-8226156afa16)

&nbsp;

---

## ✅ Fin del Recorrido del Odontólogo

Con esto finaliza el recorrido por el módulo de **Odontólogos**, donde se abordaron las funcionalidades principales como la edición de datos, gestión de turnos, revisión de planes de tratamiento y consulta de la agenda semanal.

---

## 🧑‍🦱 Módulo de Pacientes

En esta sección exploraremos el módulo de **Pacientes**, donde se puede registrar, consultar y gestionar toda la información clínica y personal de los pacientes atendidos en el sistema.

Desde este módulo podrás:

- Registrar un nuevo paciente.
- Editar los datos de un paciente.
- Eliminimar a un pacinte.
- Ver los detalles de un paciente.
- Ver el listado de pacientes.

&nbsp;

---

## 🧑‍💼 Módulo de Listado de Clientes

El módulo de **Clientes** permite visualizar y administrar la información básica de todas las personas registradas en el sistema.

&nbsp;

![Menu - Pacientes](https://github.com/user-attachments/assets/5616751c-ef16-4bdc-a7c4-573678f1d2ad)

&nbsp;

---

## 📝 Crear Nuevo Cliente

El sistema permite registrar nuevos clientes mediante un formulario sencillo y directo. Para acceder a esta vista, se debe hacer clic en el botón **"Crear Nuevo Cliente"** desde el listado principal.

Este formulario solicita los siguientes datos obligatorios para completar el registro:

- **Nombre:** Nombre completo del cliente.
- **RUT:** Número de identificación del cliente.
- **Teléfono:** Número de contacto.
- **Email:** Correo electrónico del cliente.
- **Dirección:** Domicilio o ubicación del cliente.

Una vez ingresada la información, el usuario podrá:

- Presionar el botón **Crear** para guardar los datos en el sistema.
- Usar el botón **Volver al Listado** para regresar sin realizar cambios.

&nbsp;

![Opcion - Crear un paciente](https://github.com/user-attachments/assets/4384ebac-9bab-4cb9-b7cb-7014e3f1026c)

&nbsp;

---
---

## ✏️ Editar Cliente

El sistema permite modificar la información de un cliente previamente registrado mediante el formulario de **edición de datos**.

Esta vista se accede desde el botón **Editar** disponible en el listado de clientes, y muestra un formulario con los campos previamente completados, listos para actualizar:

- **Nombre**
- **RUT**
- **Teléfono**
- **Correo electrónico**
- **Dirección**

Acciones disponibles:

-  **Guardar cambios**: actualiza los datos del cliente en el sistema.
-  **Cancelar**: descarta las modificaciones realizadas.
-  **Volver al listado**: regresa a la vista general de clientes.

&nbsp;

![Opcion - Editar paciente](https://github.com/user-attachments/assets/6327f585-d361-4816-8904-e67cde6b0541)

&nbsp;

---

## 🗑️ Eliminar Cliente

Cuando se selecciona la opción **Eliminar** desde el listado de clientes, el sistema muestra una pantalla de **confirmación**, en la cual se solicita al usuario verificar que realmente desea eliminar ese registro.

### ⚠️ Advertencia Importante

> La eliminación de un cliente puede afectar otros elementos del sistema, como los **planes de tratamiento** o **turnos asociados**.  
> Esta acción es irreversible y requiere confirmación manual.

### Vista previa de los datos mostrados

Antes de ejecutar la eliminación, el sistema presenta los datos del cliente para revisión:

- Nombre  
- RUT  
- Teléfono  
- Correo electrónico  
- Dirección

### Acciones disponibles

- **Eliminar**: borra definitivamente al cliente del sistema.  
- **Cancelar y volver al listado**: abandona la operación sin hacer cambios.

&nbsp;

![Opcion - Eliminar paciente](https://github.com/user-attachments/assets/fb466057-9b92-4dd4-80c2-126dbe8f3a3a)

&nbsp;

---

## 📄 Detalles del Cliente

Desde el listado principal de clientes, es posible acceder a la vista detallada de cada uno para consultar su información general, planes de tratamiento y turnos históricos.

### Información General

Se muestra la información básica del cliente:

- **Nombre**
- **RUT**
- **Teléfono**
- **Correo electrónico**
- **Dirección**

También están disponibles dos botones principales:

-  **Editar Cliente**: para modificar sus datos.
- **Volver al Listado**: para regresar a la vista general.

---

### 🗂️ Planes de Tratamiento

En esta sección se visualiza el plan o los planes asignados al cliente, incluyendo:

- **ID del plan, fecha de creación y profesional que lo creó**
- **Observaciones generales**
- **Pasos del plan**, con detalles como:
  - Número de secuencia
  - Tipo de tratamiento
  - Fecha estimada y fecha realizada
  - Estado del paso (ej. Completado)
  - Observaciones

Acciones disponibles:

-  **Editar Plan**
-  **Exportar PDF** del plan completo

---

### 🕒 Historial de Turnos

Se listan todos los turnos anteriores del cliente, con la siguiente información:

- Fecha y hora
- Duración
- Odontólogo asignado
- Estado del turno
- Descripción
- Acciones: **Editar** o ver **Detalles**

&nbsp;


![Opcion - Detales de Paciente](https://github.com/user-attachments/assets/59c6ffb6-4b22-4dac-b7d8-354faea2733f)

&nbsp;

---

## ✅ Fin del Módulo de Clientes

Con esto concluye el recorrido por el módulo de **Clientes**, donde se explicó cómo registrar, editar, eliminar y consultar a los clientes dentro del sistema, así como su relación con turnos y planes de tratamiento.

---

## 🗂️ Módulo de Planes

El módulo de **Planes** permite gestionar de forma centralizada los distintos **planes de tratamiento** asignados a los clientes. Desde aquí es posible crear nuevos planes, consultar su contenido, editar su progreso y exportarlos en formato PDF.

Cada plan puede incluir:

- Observaciones generales del profesional tratante.
- Fecha de creacion.
- Nombre del paciente.
- Nombre del odontologo.
- acciones que deseas realizar.

&nbsp;

---

## 📋 Listado de Planes de Tratamiento

Desde esta vista se puede consultar, filtrar y gestionar todos los **planes de tratamiento** registrados en el sistema.

### Funcionalidades principales:

- **Crear Nuevo Plan de Tratamiento**
- 📄 **Listado detallado**
  - Paciente
  - Odontólogo asignado
  - Fecha de creación del plan
  - Observaciones generales

### Acciones disponibles por cada registro:

- ✏️ **Editar**: permite modificar el contenido del plan existente.
- 📑 **Detalles**: accede a la vista completa del plan y sus etapas.
- 🗑️ **Eliminar**: permite borrar el plan del sistema (generalmente con confirmación previa).

&nbsp;


![Menu - Plan](https://github.com/user-attachments/assets/1d0c68bc-a42f-4cfb-882b-6fb6b9d64026)

&nbsp;

---

## 🆕 Crear Nuevo Plan de Tratamiento

Esta vista permite registrar un nuevo plan de tratamiento desde cero, asociándolo a un paciente y a un odontólogo específico.

### Campos del formulario:

- **Paciente**: Selección del cliente al que se asignará el plan.
- **Odontólogo**: Profesional responsable del tratamiento.
- **Fecha de creación**: Fecha en la que se genera el plan.
- **Observaciones generales**: Comentarios o notas iniciales sobre el enfoque del tratamiento.

Una vez completados los datos, se puede:

- **Crear plan**: para guardar el nuevo registro.
- **Volver al Listado**: para regresar a la vista general sin crear el plan.


&nbsp;


![Opcion - Crear un plan](https://github.com/user-attachments/assets/3041f1ce-9c57-4ca8-86cc-c463f8f421ce)

&nbsp;

---

## ✏️ Editar Plan de Tratamiento

Desde esta vista es posible modificar los datos principales de un plan de tratamiento ya registrado. Se accede haciendo clic en el botón **Editar** desde el listado general o desde los detalles de un cliente.

### Campos disponibles:

- **Paciente**: Visualiza el nombre del cliente asociado (no editable si ya está vinculado).
- **Odontólogo**: Profesional tratante a cargo del plan.
- **Fecha de creación**: Fecha en que se registró el plan.
- **Observaciones generales**: Comentarios sobre el enfoque, necesidades o estado del tratamiento.

### Acciones:

-  **Guardar cambios**: actualiza los datos del plan en el sistema.
-  **Cancelar**: descarta los cambios realizados.
-  **Volver al listado**: regresa a la vista general sin editar.

&nbsp;

![Opcion - Editar un plan](https://github.com/user-attachments/assets/cdd97d97-417d-4480-acb9-859249e55e5b)


&nbsp;


















