# Prueba Técnica de Inversiones Regivit

# Descripción
Aplicación web para operadores bancarios que permite:
-Crear y gestionar cuentas de ahorro
-Realizar depósitos y retiros
-Consultar saldos
-Ver historial de transacciones

# Tecnologías Utilizadas
Frontend: Blazor Web App Server + Blazor Bootstrap
Backend: .NET 8 (o superior)
Base de datos: SQL Server (Entity Framework Core)

## Patrones de Diseño Utilizados

### Backend (Web API .NET)
1. **MVC (Model-View-Controller)**  
   - Estructura estándar de controladores API  
   - Separación clara entre lógica y modelos

2. **Repository Pattern**  
   - Abstracción del acceso a datos con Entity Framework Core  
   - Interfaz `IRepository` para operaciones CRUD

3. **DTO Pattern**  
   - Uso de clases `CuentaDto`/`TransaccionDto` para transferencia de datos  
   - Mapeo con AutoMapper para evitar exponer modelos internos

### Frontend (Blazor)

1. **Dependency Injection**  
   - Servicios inyectados (`@inject ITransaccionService`)  
   - HttpClient configurado centralmente

2. **State Management Básico**  
   - Parámetros entre componentes (`[Parameter]`)  
   - CascadingValues para datos compartidos


# Pasos para la Instalación
1.Clonar el repositorio: git clone https://github.com/g-cano/PruebaTecnica_InversionesRegivit.git
2.Restaurar la base de datos usando el script proporcionado.
3.Configurar la cadena de conexión en appsettings.json.
"ConnectionStrings": {
  "DefaultConnection": "Data Source=SERVERNAME=;Initial Catalog=Inversiones_Regivit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
}
4.Visual Studio:
-Click derecho en la solución → Propiedades
-En Proyectos de Inicio:
-Marcar Múltiples proyectos de inicio
-Establecer acciones:
-Server: Iniciar
-Client: Iniciar
-Ordenar con flechas para que el backend inicie primero
5.No olvidar de importar la base de datos.
