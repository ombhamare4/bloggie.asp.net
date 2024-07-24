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