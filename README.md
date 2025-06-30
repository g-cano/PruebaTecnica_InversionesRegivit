# Prueba Técnica de Inversiones Regivit

# Descripción
Aplicación web para operadores bancarios que permite:
- Crear y gestionar cuentas de ahorro
- Realizar depósitos y retiros
- Consultar saldos
- Ver historial de transacciones

# Tecnologías Utilizadas
- Frontend: Blazor Web App Server + Blazor Bootstrap
- Backend: .NET 8 (o superior)
- Base de datos: SQL Server (Entity Framework Core)

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
- Clonar el repositorio: git clone https://github.com/g-cano/PruebaTecnica_InversionesRegivit.git
- Restaurar la base de datos usando el script proporcionado.
- Configurar la cadena de conexión en appsettings.json lo siguiuente: 
```
"ConnectionStrings": {
  "DefaultConnection": "Data Source=SERVERNAME=;Initial Catalog=Inversiones_Regivit;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
}
```
- Visual Studio:
```
--Click derecho en la solución → Propiedades
--En Proyectos de Inicio:
--Marcar Múltiples proyectos de inicio
--Establecer acciones:
--Server: Iniciar
--Client: Iniciar
--Ordenar con flechas para que el backend inicie primero
```

# API utilizadas:

## GET

### /api/Account
```
response body:

  {
    "id": 0,
    "accountNumber": "string",
    "accountName": "string",
    "balance": 0,
    "clientId": 0,
    "createdById": 0,
    "createdAt": "2025-06-30T06:23:26.396Z",
    "createdBy": {
      "id": 0,
      "username": "string",
      "role": "string"
    },
    "client": {
      "id": 0,
      "name": "string",
      "identification": "string",
      "createdAt": "2025-06-30T06:23:26.396Z",
      "createdBy": {
        "id": 0,
        "username": "string",
        "role": "string"
      }
    }
  }
]

```

### /api/Account/{id}
```
response body:
[
  {
    "id": 0,
    "accountNumber": "string",
    "accountName": "string",
    "balance": 0,
    "clientId": 0,
    "createdById": 0,
    "createdAt": "2025-06-30T06:24:51.319Z",
    "createdBy": {
      "id": 0,
      "username": "string",
      "role": "string"
    },
    "client": {
      "id": 0,
      "name": "string",
      "identification": "string",
      "createdAt": "2025-06-30T06:24:51.319Z",
      "createdBy": {
        "id": 0,
        "username": "string",
        "role": "string"
      }
    }
  }
]

```

### /api/Client
```
response body:
[
  {
    "id": 0,
    "name": "string",
    "identification": "string",
    "createdAt": "2025-06-30T06:25:17.544Z",
    "createdBy": {
      "id": 0,
      "username": "string",
      "role": "string"
    }
  }
]
```

### /api/Transaction
```
response body:
[
  {
    "id": 0,
    "amount": 0,
    "transactionDate": "2025-06-30T06:25:52.842Z",
    "recordDate": "2025-06-30T06:25:52.842Z",
    "resultingBalance": 0,
    "transactionTypeId": 0,
    "transactionType": {
      "id": 0,
      "code": "string",
      "name": "string"
    },
    "accountId": 0,
    "account": {
      "id": 0,
      "accountNumber": "string",
      "accountName": "string",
      "balance": 0,
      "clientId": 0,
      "createdById": 0,
      "createdAt": "2025-06-30T06:25:52.842Z",
      "createdBy": {
        "id": 0,
        "username": "string",
        "role": "string"
      },
      "client": {
        "id": 0,
        "name": "string",
        "identification": "string",
        "createdAt": "2025-06-30T06:25:52.842Z",
        "createdBy": {
          "id": 0,
          "username": "string",
          "role": "string"
        }
      }
    },
    "createdById": 0,
    "createdBy": {
      "id": 0,
      "username": "string",
      "role": "string"
    }
  }
]
```

### /api/Transaction/{accountId}/account
```
response body:
[
  {
    "id": 0,
    "amount": 0,
    "transactionDate": "2025-06-30T06:26:24.257Z",
    "recordDate": "2025-06-30T06:26:24.257Z",
    "resultingBalance": 0,
    "transactionTypeId": 0,
    "transactionType": {
      "id": 0,
      "code": "string",
      "name": "string"
    },
    "accountId": 0,
    "account": {
      "id": 0,
      "accountNumber": "string",
      "accountName": "string",
      "balance": 0,
      "clientId": 0,
      "createdById": 0,
      "createdAt": "2025-06-30T06:26:24.257Z",
      "createdBy": {
        "id": 0,
        "username": "string",
        "role": "string"
      },
      "client": {
        "id": 0,
        "name": "string",
        "identification": "string",
        "createdAt": "2025-06-30T06:26:24.257Z",
        "createdBy": {
          "id": 0,
          "username": "string",
          "role": "string"
        }
      }
    },
    "createdById": 0,
    "createdBy": {
      "id": 0,
      "username": "string",
      "role": "string"
    }
  }
]
```

## Post

### /api/Account

```
request body:
{
  "clientId": 0,
  "accountName": "string"
}
```

### /api/Client

```
request body:
{
  "name": "string",
  "identification": "string"
}
```

### /api/Transaction/{accountId}

```
request body:
{
  "amount": 0,
  "transactionDate": "2025-06-30T06:22:07.372Z",
  "transactionTypeId": 0,
  "accountId": 0,
  "createdById": 0
}
```




