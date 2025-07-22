# Nauther

**Nauther** is a permission-based user access management system. It is designed to provide out-of-the-box user access management for other applications (especially for small to medium projects which has tight deadlines).

You can connect your application (API) to Nauther using HTTP protocol.

Nauther includes a web-based UI panel for:

* Creating, updating, and deleting:

  * Admins
  * Roles
  * Permissions
* Assigning:

  * Permissions to roles
  * Permissions to users
  * Roles to users

---

## Running with Docker

To run the project using Docker, follow the steps below.

### Backend

```bash
cd api
docker compose up
```

### Frontend

```bash
cd ui/nauther_react
docker compose up
```

---

## API Integration

To communicate with Nauther, your application must include the API key in the request headers:

```
X-API-KEY: <your-api-key>
```

---

## Main API Endpoints

### 1. Register a User

```bash
curl -X POST 'http://localhost:7777/api/User/register' \
  -H 'accept: */*' \
  -H 'X-API-KEY: <your-api-key>' \
  -H 'Content-Type: application/json' \
  -d '{
    "id": "<user-id>",
    "password": "<user-password>",
    "confirmPassword": "<user-password-again>",
    "roles": ["<role-id-1>", "<role-id-2>"],
    "permissions": ["<permission-id-1>", "<permission-id-2>"]
}'
```

### 2. Get User Permissions

```bash
curl -X GET 'http://localhost:7777/api/Admin/<USERNAME>/permission' \
  -H 'accept: */*'
```

### 3. Check Password

```bash
curl -X POST 'http://localhost:777/api/Auth/checkPassword' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
    "username": "<USERNAME>",
    "password": "<PASSWORD>"
}'
```

---

ok . now the part that YOU should code

you should create a class which inhertis from the `External_UserModel`

you can check the sample at : `Nauther.Identity.Domain.ExternalContract`

after that you should create a class which implements interface : `IExternalUserDataRepository`

you can checkout the sample code at : `Nauther.Identity.Infrastructure.Services.ExternalUser`

finally , you should register your service at .NET DI . for this  check `InfrastructureServiceRegistration` class at `Nauther.Identity.Infrastructure` 


the beauty of nauther is here : do any shit work you want in the service class . it does not matter what you do ; just implement IExternalUserDataRepository . 
nauther lets you fetch data from anywhere you desire . 
## Database
nauther own db : SQL Server

external user data : as mentioned above , DOES NOT MATTER AT ALL

---

## Future Goals
* Provide support for : PermissionGroup , RoleGroup
* Provide a NuGet package for easy integration with other .NET projects
* provide sdk for Python , JS , Java 
* Improve the user interface design
* Hardening security

---
Feel free to reach me for suggestion,criticize or contributation !