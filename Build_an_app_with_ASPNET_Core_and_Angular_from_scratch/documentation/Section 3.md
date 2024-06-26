# Angular Project Setup Notes

## 1. Project Initialization

### Packages and Git Setup

- Ensure packages are installed successfully (green ticks).
- If the directory is already under version control, git initialization will be skipped.

### Directory Structure

- Use VS Code's File Explorer to manage files.
- Ignore Solution Explorer for client-side projects not registered with the .NET solution folder.
- Focus on the client-side inside the `dating-app` folder.

## 2. Running the Angular App

### Starting the App

1. Open a terminal tab in VS Code.
2. Navigate to the client folder: `cd client`.
3. Run the Angular application: `ng serve`.

### Troubleshooting

- Ensure no other instances are running to avoid conflicts.
- If errors occur, stop any other running applications and try again.

### Verification

- Open a browser and go to `http://localhost:4200`.
- The Angular Welcome page should be displayed.

## 3. Project Structure and Files

### Standalone Components (Angular 16+)

- Angular has shifted from modular architecture to standalone components.
- No `App.module.ts` file in the new project structure.
- Standalone components import necessary modules directly in the component.

### Key Files

- `index.html`: Single page application entry point containing `<app-root>`.
- `main.ts`: Bootstraps the application with the root component.
- `app.component.ts`: Root component with `standalone: true` in the decorator.

### Component Decorator

- Decorator specifies properties like `selector`, `templateUrl`, and `imports`.

### Template and Class Properties

- Use interpolation `{{ title }}` to access class properties in the template.
- Hot module reloading reflects changes immediately in the browser.

## 4. Component Files

### Structure

- Each component consists of:
  - TypeScript file (`.ts`): Component logic.
  - HTML template (`.html`): Component view.
  - CSS file (`.css`): Component-specific styles.
  - Test file (`.spec.ts`): (Optional) Component tests.

### Removing Unused Files

- Test files can be removed if not used in the training course.

## 5. Configuration Files

### Application Configuration

- `app.config.ts`: Application-wide configurations and providers.

### Routing

- `app-routing.module.ts`: Define application routes.

### Global Styles

- `styles.css`: Define global styles for the application.

### Build and Serve Configuration

- `angular.json`: Configuration for building and serving the application.

### Dependencies

- `package.json`: Lists all dependencies and scripts for the project.

### TypeScript Configuration

- `tsconfig.json`: Configures TypeScript compiler settings.

## 6. Setting Up Extensions

### Angular Code Assistance

- Add extensions in VS Code to help write Angular code efficiently.

## Example Code Modifications

### Updating `app.component.html`

1. Delete existing content:
   ```html
   <h1>{{ title }}</h1>
   ```
2. Change the `title` property in `app.component.ts`:
   ```typescript
   title = "Dating App";
   ```

### Verifying Changes

- Changes should reflect immediately due to hot module reloading.
- Use browser developer tools (Chrome recommended) to inspect elements and debug.

## Conclusion

- This guide outlines the basics of setting up and running an Angular project.
- It covers the new standalone component structure, key configuration files, and running the application.
- For development, using Chrome and relevant VS Code extensions is recommended.

---

# Angular & .NET Project Setup and HTTP Client Integration

## Project Initialization

### Angular Project Creation

1. Ensure successful creation of the Angular project with confirmation of packages installed.
2. Skip git initialization if the directory is already under version control.

### VS Code Setup

1. Open the project in VS Code.
2. Use the folder explorer to manage files inside the client-side project.

### Running the Angular Application

1. Navigate to the client folder:
   ```sh
   cd client
   ```
2. Start the Angular application:
   ```sh
   ng serve
   ```
3. Open a browser and navigate to `http://localhost:4200` to see the Angular Welcome page.

## Understanding Angular Project Structure

### Key Files and Directories

- **`index.html`**: Entry point of the application.
- **`main.ts`**: Bootstraps the Angular application.
- **App Components**:
  - **`app.component.ts`**: Main application component.
  - **`app.component.html`**: HTML template for the app component.
  - **`app.component.css`**: CSS styles for the app component.
- **Configuration Files**:
  - **`angular.json`**: Angular project configuration.
  - **`package.json`**: Lists project dependencies.
  - **`tsconfig.json`**: TypeScript configuration.

### Standalone Components in Angular 16+

- Angular components are now created as standalone components, eliminating the need for module files.

## Making HTTP Requests in Angular

### Configuring HTTP Client

1. Add HTTP Client to providers in `App.config`:

   ```typescript
   import { provideHttpClient } from "@angular/common/http";

   providers: [provideHttpClient()];
   ```

### Dependency Injection in Angular

1. Inject the HTTP client using Angular's inject function:

   ```typescript
   import { inject } from "@angular/core";
   import { HttpClient } from "@angular/common/http";

   export class AppComponent implements OnInit {
     private http = inject(HttpClient);

     users: any;

     ngOnInit() {
       this.http.get("https://localhost:5001/api/users").subscribe(
         (response) => {
           this.users = response;
         },
         (error) => {
           console.log(error);
         },
         () => {
           console.log("Request has completed");
         }
       );
     }
   }
   ```

### Handling Lifecycle Hooks

1. Implement `OnInit` interface:

   ```typescript
   import { OnInit } from "@angular/core";

   export class AppComponent implements OnInit {
     ngOnInit() {
       // Initialization code
     }
   }
   ```

### Understanding Observables

- Angular uses observables to handle asynchronous operations.
- Observables are lazy and require subscription to execute.

### Error Handling with CORS

- Ensure proper handling of CORS (Cross-Origin Resource Sharing) issues.
- Example error:
  ```
  Access to XMLHttpRequest at 'https://localhost:5001/api/users' from origin 'http://localhost:4200' has been blocked by CORS policy.
  ```
- Inspect network requests using browser developer tools to diagnose and fix CORS issues.

### Summary

This guide covers the setup and basic structure of an Angular project, the integration of an HTTP client, and handling HTTP requests to communicate with a .NET API endpoint. It also includes an overview of key project files and directories, the introduction of standalone components in Angular 16+, and best practices for dependency injection and handling CORS issues.

---
# Handling CORS in ASP.NET Core and Angular

## Overview
Cross-Origin Resource Sharing (CORS) is a security feature implemented in browsers to restrict web pages from making requests to a different domain than the one that served the initial web page. This helps protect against security risks such as cross-site scripting and data theft.

In this example, our Angular application (`localhost:4200`) is trying to access an API hosted on a different server (`localhost:5001`), which requires proper CORS configuration on the server.

## Configuring CORS in ASP.NET Core

### Adding CORS as a Service and Middleware

1. **Open `Program.cs`**:
    ```csharp
    // Add CORS as a service
    builder.Services.AddCors();

    // Add CORS as middleware
    app.UseCors(x => x
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200", "https://localhost:4200"));
    ```

2. **Order of Middleware**:
    - Place the CORS middleware before mapping controller endpoints.
    - Correct order ensures the CORS policy is applied before any endpoints are handled.

3. **Restart the API Server**:
    - After making changes to middleware, fully restart the API server to ensure the changes take effect.
    - Use `dotnet watch` for the restart:
      ```sh
      dotnet watch run
      ```

## Testing the CORS Configuration

1. **Open the Browser and Refresh**:
    - Ensure no CORS errors are present.
    - The request should complete without errors.

2. **Check Network Requests**:
    - Verify that the response headers from the API server include `Access-Control-Allow-Origin` set to `http://localhost:4200`.
    - Confirm that the API response contains the expected data.

## JSON Data Conventions

- **Casing Conventions**:
    - .NET API uses Pascal casing for class properties.
    - JSON responses are converted to camel casing by default.

- **Angular Considerations**:
    - Angular is case-sensitive. Ensure consistency in data handling between backend and frontend.

## Example Code

**Program.cs**:
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.Run();
```

**Angular HTTP Request**:
```typescript
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http.get('https://localhost:5001/api/users').subscribe(
      response => {
        console.log('Request completed', response);
      },
      error => {
        console.error('Request failed', error);
      }
    );
  }
}
```

By following these steps, you can successfully configure CORS in your ASP.NET Core API and test it with an Angular frontend, ensuring smooth and secure cross-origin requests.

---

## Displaying Users in Angular

### Navigating in VS Code
- **Accessing Templates**:
  - Right-click the template URL and select "Go to Definition" or press F12.
- **Angular Components**:
  - Angular components typically have a component file, a template file, and a CSS file.

### Displaying User List
- **HTML Structure**:
  - Use `<ul>` for an unordered list.
  - Use `<li>` for list items.
- **Looping Through Users**:
  - Use Angular's structural directive `*ngFor` to loop through the user array.

### Handling `*ngFor` Directive
- **Error Handling**:
  - Import `NgFor` directive if you see warnings.
  - Alternatively, import the `CommonModule` if using multiple features from it.

### Example of `*ngFor` Usage
```html
<ul>
  <li *ngFor="let user of users">{{ user.id }} - {{ user.username }}</li>
</ul>
```
- **Case Sensitivity**:
  - Ensure casing matches what is returned from the API.

### Alternative Loop Syntax
- **Using `@for` Control Flow**:
  - New Angular 17 way of looping.
  - Example:
  ```html
  <ng-container *ngFor="let user of users; trackBy: user.id">
    <li>{{ user.id }} - {{ user.username }}</li>
  </ng-container>
  ```
  - This method does not require importing `NgFor`.

### Next Steps
- **User Interface Component Library**:
  - Explore adding a UI component library to the project.
- **Icon Set**:
  - Look into adding an icon set for enhanced UI.

These notes outline how to display user data in an Angular application, navigate through files in VS Code, and handle structural directives and control flow for listing users.