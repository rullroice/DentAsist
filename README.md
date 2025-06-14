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
