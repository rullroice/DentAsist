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




