# Task Management API (.NET 9)

RESTful API for managing tasks, built with .NET 9 and Swagger UI.

## Run Application 

1. Open TaskManagementSystem Solution By Visual Studio or VS Code.

2. Run TaskManagementSystem.API .

3. After Run Swagger UI will Launching.

## Authentication

1. **Login as Admin**  
   Use the `/api/login` endpoint to authenticate.


   ```http
   POST /api/login
  
   {
     "email": "admin1@test.com",
     "password": "P@ssw0rd@2025"
   }

2. **Enable Cookie**
   Set `UseCookies` to **true**.

3. Then You Can Access Endpoints as your Permission.

4. To **Login as User**
   Same as Above But You Use Below Schema.

   ```http
   POST /api/login
  
   {
     "email": "test1@test.com",
     "password": "P@ssw0rd@2025"
   }

5. Also **Enable Cookie**
   Set `UseCookies` to **true**.

📝**Note:** The User & Admin Can Access Endpoints as Asked in Assignment.

## Unit Test

 1. **Explore Unit Tests 🔍 :** 
 Click on Test Tab in Navbar then Test Explorer or by Right Click On **TaskManagementSystem.Tests** Project inside the ***tests*** Folder in Solution Explorer Then Inside Test Explorer Click on `TaskManagementSystem.Tests` in Test Section on the right side to list all tests. 

 2. **Run Tests 🧪 :**

  **Test Controller Action** (HTTP GET `Task/GetTasks` Action ):
  Click Run on `TaskManagementSystem.Tests.PresentationLayer` test.

   **Test Service Layer Method** (TaskService `CreateAsync` Method):
  Click Run on `TaskManagementSystem.Tests.ServiceLayer.Tasks` test.

