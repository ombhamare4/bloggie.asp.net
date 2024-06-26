# Dating App Project Setup

## Introduction

In this training course, we will be using the .NET command-line interface (CLI) to create our project. While you can use Visual Studio or Ryder, I recommend using the command line for consistency and to familiarize yourself with the available options.

## Step 1: Set Up Project Folder

1. Open a terminal.
2. Navigate to the directory where you want to create your project.
3. Use the following command to create a new directory:
   ```
   mkdir DatingApp
   ```
4. Change into the `dating-app` directory:
   ```
   cd DatingApp
   ```

## Step 2: Check .NET SDK Version

1. Check the installed .NET SDK version by running:
   ```
   dotnet --info
   ```
   Note: In this case, the version is `7.0.100 RC 2`.

## Step 3: Create Solution and Project

1. Create a solution file:
   ```
   dotnet new sln
   ```
2. Create a Web API project:
   ```
   dotnet new webapi -n API
   ```
   if you using .Net 8.0 then use following command
   ```
   dotnet new webapi -n API --use-controllers
   ```

## Step 4: Add Project to Solution

1. Add the API project to the solution:
   ```
   dotnet sln add API
   ```

## Step 5: Open Project in Visual Studio Code

1. Open Visual Studio Code:
   ```
   code .
   ```
2. If you encounter an issue, follow these steps:
   - Open the command palette (Shift + Command + P on Mac).
   - Search for "path".
   - Choose "Shell Command: Install 'code' command in PATH".
   - If access is denied, uninstall and reinstall the command in PATH.
   - Once installed successfully, close and reopen Visual Studio Code.
3. Confirm trust for the project files when prompted.

## Conclusion

Now that your project is set up, you can begin developing your .NET Web API application.


# Project Overview

In this section, we'll take a closer look at the structure of our project and ensure that our application is running correctly. We'll also make some adjustments to the project settings.

## Viewing Project Structure

### Solution Explorer
1. Open Visual Studio Code.
2. Use the Solution Explorer view to navigate through the project.

### Terminal
3. Open the terminal (Ctrl + `).
4. Navigate to the API project:
    ```
    cd API
    ```

### Running the Application
5. Run the application:
    ```
    dotnet run
    ```

### Testing the Application
6. Open your browser and navigate to the URL provided in the terminal.

### Exploring Project Files
- **Properties**
    - `launchSettings.json`: Contains configuration settings for running the application.

- **App Settings**
    - `appsettings.json`: General configuration settings.
    - `appsettings.Development.json`: Development-specific configuration settings.

- **Program.cs**
    - Main entry point into the application.
    - Contains configuration for services and the HTTP request pipeline.

## Adjusting Project Settings

### `launchSettings.json`
- Remove unnecessary profiles.
- Set the application URL to `https://localhost:5001`.

### `Program.cs`
- Remove Swagger-related middleware.

## Conclusion
Now that we have explored our project structure and adjusted the necessary settings, our application is ready for further development. In the next section, we will create our first class and begin building our application.

# Creating Our First Entity

In this section, we'll create our first entity, `AppUser`, which will represent users in our dating application.

## Creating the Entity

1. Inside the `API` project, create a new folder named `Entities`.

    - Right-click on the `API` project.
    - Select **Add** > **New Folder**.
    - Name the folder `Entities`.

2. Inside the `Entities` folder, add a new C# class file.

    - Right-click on the `Entities` folder.
    - Select **Add** > **New File...**.
    - Choose **Class** from the list of options.
    - Name the class `AppUser`.

3. Add the following code to the `AppUser` class:

    ```csharp
    namespace API.Entities
    {
        public class AppUser
        {
            public int Id { get; set; }
            public string Username { get; set; }
        }
    }
    ```

    - We've created two properties for our `AppUser` entity: `Id` and `Username`.
    - `Id` will serve as the unique identifier for each user.
    - `Username` will store the username of the user.

## Adjusting Project Settings

### `launchSettings.json`
- Remove unnecessary profiles.
- Set the application URL to `https://localhost:5001`.

### `API.csproj`
- Set the `Nullable` flag to `disable`.

    ```xml
    <Nullable>disable</Nullable>
    ```

    - This will ensure that strings are non-nullable by default.

    ```xml
    <Project Sdk="Microsoft.NET.Sdk.Web">

      <PropertyGroup>
        <TargetFramework>net7.0</TargetFramework>
        <Nullable>disable</Nullable>
      </PropertyGroup>

      <ItemGroup>
        <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.0" />
        <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="6.0.0" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.0-rc.2.12526" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.0-rc.2.12526" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.0-rc.2.12526" />
        <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="7.0.0-rc.2.12526" />
      </ItemGroup>

    </Project>
    ```

    - Ensure that the necessary packages are installed.

## Conclusion

With our `AppUser` entity created, we're ready to move on to the next step: setting up Entity Framework to interact with our database. In the next section, we'll explore what Entity Framework is and how to use it in our project.

# Introduction to Entity Framework

In this section, we'll get a brief introduction to Entity Framework, an object-relational mapper that automates database-related activities in our application.

## What is Entity Framework?

Entity Framework (EF) is an object-relational mapper responsible for translating our code into SQL commands that interact with our database.

Before the introduction of EF, developers had to write database access code manually, which was often cumbersome and error-prone.

EF automates these database-related activities, making it easier and more efficient to interact with databases.

## Setting up Entity Framework

To use Entity Framework in our application, we need to:

1. Create a class that derives from the `DbContext` class provided by EF.
2. Configure our entity classes to map to database tables.

## Key Features of Entity Framework

### LINQ Queries

Entity Framework allows us to write LINQ queries to interact with our database.

```csharp
// LINQ query to add a new user
var user = new AppUser { Id = 4, Username = "John" };
_context.Users.Add(user);
await _context.SaveChangesAsync();
```

### Database Providers

EF works with database providers to translate LINQ queries into SQL commands. We'll use SQLite for development, as it doesn't require a database server and is portable across different platforms.

### Change Tracking

EF keeps track of changes made to entities and automatically submits these changes to the database.

### Saving Changes

EF provides a `SaveChanges` method to execute insert, update, and delete commands on the database.

### Concurrency

EF uses optimistic concurrency to prevent overwriting changes made by other users.

### Transactions

EF manages transactions automatically while querying or saving data.

### Caching

EF includes first-level caching, which returns data from the cache instead of hitting the database for repeated queries.

### Conventions and Configuration

EF follows default conventions for configuring the database schema based on entity classes. We can also configure entities and override conventions if needed.

### Migrations

EF provides migrations to automatically generate database schemas based on our code. We can use migrations to create, update, and revert database schemas.

## Conclusion

Entity Framework simplifies database interactions in our application by automating database-related tasks. In the upcoming sections, we'll set up Entity Framework in our project and explore its features in more detail.

# Adding Entity Framework to the Project

To add Entity Framework to our project, we'll need to install the required NuGet packages. If you're using Visual Studio Code, you'll need to install an extension called NuGet Gallery to manage the packages.

## Installing NuGet Gallery Extension

1. Open the Extensions view in Visual Studio Code.
2. Search for "NuGet Gallery" and install it.

## Installing Entity Framework Core Packages

Once you have the NuGet Gallery extension installed, follow these steps to add the required packages:

1. Open the Command Palette (Ctrl+Shift+P).
2. Search for "NuGet" and select "NuGet: Open NuGet Gallery".
3. Search for the following packages and install them:

   - **Microsoft.EntityFrameworkCore.Sqlite**: Provides SQLite support for Entity Framework Core. Use this package for development purposes only.
   - **Microsoft.EntityFrameworkCore.Design**: Required to use EF Core commands like migrations.

   **Note:** Make sure to select the appropriate version that matches the major version of the .NET runtime you're using. For example, if you're using .NET 7, choose version 7.x of these packages.

## Updating the Project File

After installing the packages, ensure that your `API.csproj` file looks similar to this:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>disable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.x" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="7.0.x" />
  </ItemGroup>

</Project>
```

## Conclusion

With Entity Framework Core added to our project, we're now ready to create a `DbContext` class, which acts as a bridge between our entity classes and the database. Next, we'll create our `DbContext` class and configure it to work with our entities.

Sure, here's a README.md file based on the provided transcript:

---

# Setting up DbContext for Database Operations

## Creating DbContext Class

1. Inside the Solution Explorer, right-click on the API project, select **New Folder**, and name it **Data**. This folder will hold all our data access logic.

2. Inside the **Data** folder, add a new class file and name it **DataContext.cs**.

   ```csharp
   namespace API.Data
   {
       public class DataContext : DbContext
       {
           public DataContext(DbContextOptions<DataContext> options) : base(options)
           {
           }

           public DbSet<AppUser> Users { get; set; }
       }
   }
   ```

3. Make sure to add the necessary using statements at the top of the file.

   ```csharp
   using Microsoft.EntityFrameworkCore;
   ```

## Setting Up DbContext in Program Class

4. Navigate to the **Program.cs** class.

5. Inside the `ConfigureServices` method in the **Program.cs** class, add the following code to register the **DataContext** class as a service:

   ```csharp
   public void ConfigureServices(IServiceCollection services)
   {
       services.AddDbContext<DataContext>(options =>
       {
           options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
       });
   }
   ```

   Make sure to import the necessary namespace:

   ```csharp
   using Microsoft.EntityFrameworkCore;
   ```

## Adding Connection String to Configuration

6. Open the **appsettings.json** file and add a connection string named **DefaultConnection**:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "DataSource=app.db"
     }
   }
   ```

   Adjust the connection string according to your database configuration.

## Creating Migrations

7. To create a migration, open a terminal window in the root directory of your project and run the following command:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

8. To apply the migration and create the database, run:

   ```bash
   dotnet ef database update
   ```

This will create the necessary tables in your database based on the entities defined in your code.

---
