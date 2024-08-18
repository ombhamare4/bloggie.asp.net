We add DBSet when we want to query that Entity

Certainly! Let's delve into the intriguing world of **shadow foreign keys** in the context of **Entity Framework**.

1. **What Are Shadow Properties?**
   - **Shadow properties** are properties that exist in the EF Core model but are not explicitly defined in your .NET entity classes.
   - They are maintained purely within the **Change Tracker** and are not part of your domain model.
   - Essentially, they're like hidden variables that EF uses internally to manage certain aspects of your data.

2. **Why Use Shadow Properties?**
   - One common use case for shadow properties is when you want to hide certain database-related details from your application code or business logic.
   - By using shadow properties, you can manage relationships entirely through navigation properties without exposing the underlying foreign key properties.

3. **Shadow Foreign Keys:**
   - Shadow properties are often employed for foreign key properties.
   - Imagine you have two related entities (let's say, `Blog` and `Post`). The relationship is represented by navigation properties (e.g., a `Blog` has many `Posts`).
   - In the database, this relationship is enforced by a foreign key constraint, but you might not want to expose the actual foreign key property in your .NET classes.
   - Instead, EF introduces a shadow property to store the foreign key value in the database.
   - The naming convention for such shadow foreign keys is usually `<navigation property name><principal key property name>`.
     - If the principal key property name starts with the name of the navigation property, the name will be just `<principal key property name>`.
     - If there's no navigation property on the dependent entity, the naming convention becomes `<principal type name><principal key property name>`.

4. **Example:**
   Let's say we have a `Blog` and a `Post` class:
   ```csharp
   public class Blog
   {
       public int BlogId { get; set; }
       public string Url { get; set; }
       public List<Post> Posts { get; set; }
   }

   public class Post
   {
       public int PostId { get; set; }
       public string Title { get; set; }
       public string Content { get; set; }
       public Blog Blog { get; set; } // No explicit foreign key property
   }
   ```
   - Since there's no CLR property explicitly holding the foreign key for the relationship between `Blog` and `Post`, a shadow property (let's call it `BlogId`) is created to store the foreign key value in the database.

5. **Configuring Shadow Properties:**
   - You can use the Fluent API to configure shadow properties.
   - For example, if you want to configure a shadow property named `LastUpdated`:
     ```csharp
     modelBuilder.Entity<Blog>()
         .Property<DateTime>("LastUpdated");
     ```

So, in summary, shadow foreign keys allow you to maintain relationships without exposing the actual foreign key properties in your domain model. It's like having a secret passage in your database schema! 🕵️‍♂️¹²³
