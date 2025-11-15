# Task Management API (.NET 9)

RESTful API for managing tasks, built with .NET 9 and Swagger UI.


## Authentication

1. **Login as Admin**  
   Use the `/api/login` endpoint to authenticate.


   ```http
   POST /api/login
  
   {
     "email": "admin1@test.com",
     "password": "P@ssw0rd@2025"
   }```

2. **Enable Cookie**
   Set `UseCookies` to **true**
