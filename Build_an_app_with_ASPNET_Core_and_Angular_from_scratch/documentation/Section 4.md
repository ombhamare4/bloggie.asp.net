# Storing Passwords Securely: Concepts and Implementation

## Overview
Storing passwords securely is critical to protecting user data in case of a database breach. This guide covers the concepts and implementation of secure password storage using hashing and salting techniques.

## Options for Storing Passwords

### Option 1: Storing Passwords in Clear Text
- **Description**: Passwords are stored as entered by users.
- **Disadvantages**:
  - If the database is compromised, all passwords are exposed.
  - Users must change passwords if compromised.
  - Clear text storage is highly insecure and not recommended.

### Option 2: Hashing Passwords
- **Description**: Passwords are hashed using a hashing algorithm.
- **Process**:
  - User enters password.
  - Password is hashed and stored in the database.
  - During login, the entered password is hashed and compared with the stored hash.
- **Disadvantages**:
  - Identical passwords result in identical hashes.
  - Vulnerable to dictionary attacks using precomputed hash tables.

### Option 3: Hashing and Salting Passwords
- **Description**: Adds a unique salt to each password before hashing.
- **Process**:
  - User enters password.
  - A random salt is generated.
  - Password and salt are concatenated and hashed.
  - Both the salt and hash are stored in the database.
  - During login, the entered password and stored salt are concatenated, hashed, and compared with the stored hash.
- **Advantages**:
  - Identical passwords result in different hashes due to unique salts.
  - Makes precomputed dictionary attacks ineffective.

## Implementation Steps

### Step 1: Setup and Install Dependencies
Ensure you have the necessary libraries installed for hashing (e.g., bcrypt).

```bash
npm install bcrypt
```

### Step 2: Hashing and Salting Passwords
Here’s an example using bcrypt in a Node.js environment:

```javascript
const bcrypt = require('bcrypt');

// Function to hash a password
async function hashPassword(password) {
    const saltRounds = 10; // Number of rounds to generate salt
    const salt = await bcrypt.genSalt(saltRounds);
    const hashedPassword = await bcrypt.hash(password, salt);
    return { salt, hashedPassword };
}

// Function to compare password with stored hash
async function verifyPassword(inputPassword, storedHash) {
    const match = await bcrypt.compare(inputPassword, storedHash);
    return match;
}
```

### Step 3: Storing Passwords in the Database
Store both the hashed password and the salt in your user table:

```sql
CREATE TABLE Users (
    id INT PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    passwordHash VARCHAR(255) NOT NULL,
    salt VARCHAR(255) NOT NULL
);
```

### Step 4: Registering a User
During user registration, hash the password and store the hash and salt in the database:

```javascript
async function registerUser(username, password) {
    const { salt, hashedPassword } = await hashPassword(password);
    // Save username, hashedPassword, and salt in the database
}
```

### Step 5: Authenticating a User
During login, retrieve the stored hash and salt, then verify the entered password:

```javascript
async function loginUser(username, inputPassword) {
    const user = await getUserByUsername(username); // Retrieve user from database
    if (!user) {
        throw new Error('User not found');
    }
    const match = await verifyPassword(inputPassword, user.passwordHash);
    if (match) {
        console.log('Authentication successful');
    } else {
        console.log('Authentication failed');
    }
}
```

## Summary
- Storing passwords securely is essential to protect user data.
- Clear text storage is unacceptable.
- Hashing alone is insufficient due to dictionary attacks.
- Hashing with salting is a better approach, ensuring unique hashes for identical passwords.
- Use established libraries like bcrypt for hashing and salting.
- In production, consider using frameworks like ASP.Net Identity for robust and tested solutions.

By following these steps, you can implement a secure password storage system that protects against common attacks and enhances the security of your application.

# Creating a Token Service in .NET

1. **Introduction to the Token Service**:
   - The goal is to pass a JSON Web Token (JWT) to the user upon successful login.
   - This token will be used to authenticate requests to protected API endpoints.

2. **Service Creation**:
   - Services in .NET are injectable components that can be used across the application.
   - We'll create a service specifically for generating JWTs.

3. **Interface Creation**:
   - Interfaces in .NET provide an abstraction, defining what a service will do without specifying how.
   - Create an interface named `ITokenService` in an `Interfaces` folder within the API folder.
   - The interface method `CreateToken` will return a string (the token) and take an `AppUser` as a parameter.

4. **Service Implementation**:
   - Create a `TokenService` class in a `Services` folder within the API folder.
   - Implement the `ITokenService` interface in this class.
   - Implement the `CreateToken` method to generate the token.

5. **Dependency Injection Setup**:
   - Register the `TokenService` in the `Program.cs` file.
   - Use `AddScoped` to specify the service's lifetime. This means a new instance of `TokenService` will be created per HTTP request.

6. **Service Lifetime Options**:
   - **Singleton**: One instance for the entire application lifetime.
   - **Transient**: A new instance each time the service is requested.
   - **Scoped**: A new instance per client request (suitable for our token service).

7. **Interface vs. Implementation**:
   - Using an interface provides abstraction and decoupling, ensuring that changes in implementation do not affect the consumers of the service.
   - Interfaces make services more testable by allowing easy creation of mocks.

8. **Implementation and Testing**:
   - The `TokenService` implementation will be done in subsequent steps.
   - Using interfaces facilitates unit testing by allowing the creation of mock implementations without relying on actual service logic.

### Example Code

**ITokenService.cs**:
```csharp
public interface ITokenService
{
    string CreateToken(AppUser user);
}
```

**TokenService.cs**:
```csharp
public class TokenService : ITokenService
{
    public string CreateToken(AppUser user)
    {
        // Implementation logic to create a token
    }
}
```

**Program.cs**:
```csharp
builder.Services.AddScoped<ITokenService, TokenService>();
```

This setup allows for scalable and maintainable code by leveraging dependency injection, abstraction, and clean separation of concerns.

---

# Configuring Token Service Logic

1. **Objective**:
   - Create a JSON Web Token (JWT) and return it as a string.
   - Inject necessary dependencies into the token service.

2. **Token Configuration**:
   - Inject `IConfiguration` to access configuration settings, such as the token key.

3. **Steps to Configure Token Service**:
   - Inject `IConfiguration` in the `TokenService` constructor.
   - Retrieve the token key from configuration settings.
   - Validate the token key's existence and length (must be at least 64 characters).

4. **NuGet Package for JWT**:
   - Install `System.IdentityModel.Tokens.Jwt` package for handling JWT operations.

5. **Key and Claims**:
   - Create a symmetric security key using the token key.
   - Define claims for the token, which are assertions about the user.

6. **Token Descriptor**:
   - Define the token's properties such as subject, expiration, and signing credentials.
   - Use HMAC SHA-512 for signing the token.

7. **Token Creation**:
   - Use `JwtSecurityTokenHandler` to create the token.
   - Return the serialized token as a string.

### Example Code

**TokenService.cs**:
```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from appsettings");
        if (tokenKey.Length < 64) throw new Exception("Your Token Key must be at least 64 characters");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
          new(ClaimTypes.NameIdentifier, user.Username)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}
```

### Additional Information

**Terms and Concepts**:

1. **JWT (JSON Web Token)**:
   - A compact, URL-safe means of representing claims to be transferred between two parties. The token is digitally signed, which ensures its integrity and authenticity.

2. **IConfiguration**:
   - An interface in .NET Core used to read configuration settings. It abstracts the configuration sources such as JSON files, environment variables, and more.

3. **Symmetric Key**:
   - A type of encryption where the same key is used for both encryption and decryption. It's simpler and faster but requires secure key management.

4. **Claims**:
   - Statements about an entity (typically, the user) that are included in the token. Examples include user ID, username, and roles.

5. **SecurityTokenDescriptor**:
   - A class used to define the structure and settings of the token being created, including claims, expiry, and signing credentials.

6. **JwtSecurityTokenHandler**:
   - A class that provides methods to create, validate, and write JWT tokens.

7. **HMAC SHA-512**:
   - A cryptographic algorithm used for signing the token. HMAC (Hash-based Message Authentication Code) is a specific construction for creating a MAC involving a cryptographic hash function and a secret cryptographic key.
