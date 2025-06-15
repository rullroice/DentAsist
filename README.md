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

## 🧑‍💼 Módulo de Clientes

El módulo de **Clientes** permite visualizar y administrar la información básica de todas las personas registradas en el sistema.

&nbsp;

![Menu - Pacientes](https://github.com/user-attachments/assets/5616751c-ef16-4bdc-a7c4-573678f1d2ad)

&nbsp;

---








