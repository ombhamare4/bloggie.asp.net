# Extending the user entity

### Repository Pattern Introduction

#### Description

- **Martin Fowler's Definition**: A repository mediates between the domain and the data mapping layers, acting like an in-memory domain object collection.

#### Current Architecture

- **Web Server**: Requests come into the controller endpoint.
- **DbContext**: Represents a session with the database, translating controller logic into queries and returning data.

#### Proposed Change: Introduction of the Repository Layer

- **New Structure**:
  - Controllers will use repositories instead of directly accessing the DbContext.
  - The repository will internally use the DbContext to execute logic.
  - This adds a layer of abstraction, encapsulating the database logic.

#### Reasons for Using the Repository Pattern

1. **Encapsulation**:
   - Encapsulates the complex logic inside the DbContext.
   - Limits the controller's access to specific methods (e.g., getUser, getAllUsers, updateUser, saveChanges).
2. **Reduce Duplicate Query Logic**:

   - Centralizes logic that might be needed across multiple controllers, reducing code duplication.

3. **Promotes Testability**:

   - Easier to test against a repository interface than against a DbContext.
   - Allows mocking of repositories in unit tests  `(Unit testing is a software testing technique where individual components or units of a software application are tested in isolation to ensure they perform as expected. These “units” can be functions, methods, objects, or other entities in the application’s source code. The goal is to verify the functionality of each unit independently, typically using automated testing frameworks)`.
   - If using testing later, it simplifies testing processes.

4. **Advantages**:
   - Minimizes duplicate query logic.
   - Decouples the application from the persistence framework.
   - Centralizes query logic, preventing it from being scattered across the application.
   - Allows easy change of the Object-Relational Mapper (ORM), although this is rarely done.
   - Promotes testability through easier mocking of the repository interface.

#### Disadvantages of the Repository Pattern

1. **Abstraction Overload**:

   - Entity Framework itself is an abstraction from the database, and a repository is another abstraction on top of that. Some argue this is unnecessary.

2. **Increased Code**:

   - Each root entity needs its own repository, leading to more code.

3. **Need for Unit of Work Pattern**:
   - To manage different instances of DbContext injected into various repositories, the Unit of Work pattern will be required later.

#### Conclusion

- While the repository pattern introduces additional abstraction, the benefits (encapsulation, reduced duplication, improved testability) outweigh the disadvantages for this application. The pattern makes the code more understandable and reusable.

#### **Repository pattern**

**Repository pattern** is a powerful design pattern that plays hide-and-seek with your data. Well, not literally, but it does help keep your persistence concerns neatly tucked away from your domain model. Let's dive into it!

1. **What's the Repository Pattern?**

   - Imagine you're building a web application, and you need to interact with a database. You've got your business logic (the cool stuff your app does) and your data access logic (the not-so-cool-but-necessary stuff that talks to the database). Now, you don't want your business logic to be cluttered with SQL queries, right? That's where the Repository pattern steps in.
   - The Repository pattern provides an abstraction layer between your business logic and the data access layer. It acts as a bridge, shielding your domain model from the nitty-gritty details of how data is stored or retrieved.
   - In a nutshell, it's like having a friendly concierge who handles all your database requests while you focus on running your hotel (or, in this case, your app).

2. **How Does It Work?**

   - You define one or more **repository interfaces** in your domain model. These interfaces declare the methods your business logic needs to interact with the data (e.g., `GetById`, `Add`, `Update`, `Delete`).
   - Then, you create **repository implementations** elsewhere in your application. These implementations talk to the actual data store (like a database) using specific adapters (e.g., Entity Framework, Dapper, or any other ORM).
   - The cool part? Your business logic doesn't care which implementation is used—it only knows about the interface. So, you can swap out the underlying data access technology without rewriting your entire app. Flexibility, my friend!

3. **Why Use It?**

   - **Separation of Concerns**: By isolating data access logic, you keep your domain model clean and focused on its core responsibilities.
   - **Testability**: Repositories make unit testing a breeze. You can easily mock or stub them for testing purposes.
   - **Decoupling**: Your business logic doesn't need to know about the database schema, SQL syntax, or any of that jazz. It just speaks the repository's language.
   - **Consistency**: If you have multiple parts of your app accessing the same data, a repository ensures they all follow the same rules.

4. **Example in ASP.NET Core**:

   - In an ASP.NET Core MVC application, you'd create repository interfaces (like `IStudentRepository`) and their corresponding implementations (e.g., `StudentRepository`). These implementations would use Entity Framework Core or another data access library to fetch or store data.
   - Here's a snippet of what it might look like:

     ```csharp
     public interface IStudentRepository
     {
         Student GetById(int studentId);
         void Add(Student student);
         // Other methods...
     }

     public class StudentRepository : IStudentRepository
     {
         private readonly YourDbContext _dbContext;

         public StudentRepository(YourDbContext dbContext)
         {
             _dbContext = dbContext;
         }

         public Student GetById(int studentId)
         {
             return _dbContext.Students.Find(studentId);
         }

         public void Add(Student student)
         {
             _dbContext.Students.Add(student);
             _dbContext.SaveChanges();
         }
         // Other implementations...
     }
     ```

   - And then, in your controller, you'd inject the `IStudentRepository` and use its methods. Voilà!

Remember, the Repository pattern isn't just for .NET—it's a universal superhero that fights data access chaos across different programming languages and frameworks! 🦸‍♂️¹²³

Got any more questions or need help with anything else? Feel free to ask! 😊

Source: Conversation with Copilot, 18/8/2024
(1) Implementing the Repository and Unit of Work Patterns in an ASP.NET MVC .... https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application.
(2) Designing the infrastructure persistence layer - .NET. https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design.
(3) Repository Design Pattern in ASP.NET Core MVC using EF Core. https://dotnettutorials.net/lesson/repository-design-pattern-in-asp-net-core-mvc/.
(4) Repository Pattern in ASP.NET Core - Ultimate Guide. https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/.

### Implementation of the Repository Pattern

#### Step 1: Create an Interface
1. **File Creation**:
   - Create a new file named `IUserRepository.cs` of type interface.
   - This interface will define the contract for the repository implementation.

2. **Interface Definition**:
   - **Methods**:
     - `void Update(AppUser user)`: Updates a user entity.
     - `Task<bool> SaveAllAsync()`: Saves changes to the database and returns a boolean indicating success.
     - `Task<IEnumerable<AppUser>> GetUsersAsync()`: Retrieves all users.
     - `Task<AppUser?> GetUserByIdAsync(int id)`: Retrieves a user by ID.
     - `Task<AppUser?> GetUserByUsernameAsync(string username)`: Retrieves a user by username.

3. **Null Handling**:
   - Methods like `GetUserByIdAsync` and `GetUserByUsernameAsync` are optional and can return `null`. This allows the consumer to handle the null value appropriately.

#### Step 2: Create the Implementation Class
1. **File Creation**:
   - Create a new file named `UserRepository.cs` in the `Data` folder.

2. **Class Definition**:
   - **Inherits**: Implements the `IUserRepository` interface.
   - **DbContext Injection**: Inject the data context to execute database queries.

3. **Method Implementations**:
   - **`GetUserByIdAsync`**:
     ```csharp
     public async Task<AppUser?> GetUserByIdAsync(int id)
     {
         return await _context.Users.FindAsync(id);
     }
     ```
     - Uses `FindAsync` to retrieve a user by ID. Returns `null` if not found.

   - **`GetUserByUsernameAsync`**:
     ```csharp
     public async Task<AppUser?> GetUserByUsernameAsync(string username)
     {
         return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
     }
     ```
     - Uses `SingleOrDefaultAsync` to retrieve a user by username. Returns `null` if not found.

   - **`GetUsersAsync`**:
     ```csharp
     public async Task<IEnumerable<AppUser>> GetUsersAsync()
     {
         return await _context.Users.ToListAsync();
     }
     ```
     - Retrieves all users using `ToListAsync`.

   - **`SaveAllAsync`**:
     ```csharp
     public async Task<bool> SaveAllAsync()
     {
         return await _context.SaveChangesAsync() > 0;
     }
     ```
     - Saves changes to the database and checks if any changes were made.

   - **`Update`**:
     ```csharp
     public void Update(AppUser user)
     {
         _context.Entry(user).State = EntityState.Modified;
     }
     ```
     - Marks an entity as modified. While Entity Framework tracks changes automatically, this method can be used to explicitly set an entity as modified.

#### Step 3: Register the Repository in the Service Container
1. **ApplicationServiceExtensions**:
   - Open the `ApplicationServiceExtensions` class.
   - Register the repository with the service container:
     ```csharp
     services.AddScoped<IUserRepository, UserRepository>();
     ```

#### Summary
- The `IUserRepository` interface defines methods for data access and saving entities.
- The `UserRepository` class implements these methods, using Entity Framework's `DbContext` to interact with the database.
- The repository is registered in the service container, making it available for injection into controllers or other services.