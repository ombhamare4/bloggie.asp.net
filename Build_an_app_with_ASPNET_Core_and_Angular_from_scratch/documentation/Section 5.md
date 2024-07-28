# Section 5: Client Login and Registration

## Making the Angular Form Functional

### Setting Up the Form in the Component
1. **Add Properties to the Component:**
   - Create a property to store form values:
     ```typescript
     export class NavComponent {
         model: any = {};

         login() {
             console.log(this.model);
         }
     }
     ```

### Using Angular Template-Driven Forms
1. **Template Reference Variable:**
   - In the template, give the form a reference variable and set its type to `NgForm`:
     ```html
     <form #loginForm="ngForm" (ngSubmit)="login()">
     ```

2. **Import FormsModule:**
   - Ensure `FormsModule` is imported and added to your component’s imports array:
     ```typescript
     import { FormsModule } from '@angular/forms';

     @Component({
         selector: 'app-nav',
         templateUrl: './nav.component.html',
         styleUrls: ['./nav.component.css'],
         standalone: true,
         imports: [FormsModule]
     })
     ```

3. **Bind Input Fields:**
   - Use `ngModel` and set the name for the input fields:
     ```html
     <input type="text" name="username" [(ngModel)]="model.username" autocomplete="off" />
     <input type="password" name="password" [(ngModel)]="model.password" autocomplete="off" />
     ```

4. **Form Submission:**
   - Ensure the form uses the `ngSubmit` directive to call the login method:
     ```html
     <form #loginForm="ngForm" (ngSubmit)="login()">
         <input type="text" name="username" [(ngModel)]="model.username" autocomplete="off" />
         <input type="password" name="password" [(ngModel)]="model.password" autocomplete="off" />
         <button type="submit">Login</button>
     </form>
     ```

### Testing the Form
1. **Test Form in the Browser:**
   - Open the form in the browser.
   - Enter values in the input fields and submit the form.
   - Check the console to see the output of the form values:
     ```plaintext
     {username: "enteredUsername", password: "enteredPassword"}
     ```
---
## Angular Service Creation for HTTP Requests

### Generating a Service
1. **Generate Command:**
   - Use `ng g s` to generate a service.
   - Example command: `ng g s _services/accounts --dry-run`
     - `--dry-run` simulates the creation without actually creating files.

2. **File Structure:**
   - Creates a folder `_services` in the `app` directory.
   - Inside `_services`, it creates `account.service.ts` and a test file.
   - The test file can be excluded if not needed by using `--skip-tests`.

3. **Organizing Files:**
   - Use an underscore `_` prefix for non-component folders (e.g., `_services`) to keep them at the top of the directory list.
   - This aids in better organization and easier navigation.

### Service Details
1. **Decorator:**
   - The service file includes the `@Injectable` decorator.
   - This makes the service injectable into components and specifies that it is provided in the root of the application.

2. **Singleton Pattern:**
   - Services in Angular are singletons.
   - Instantiated when the application initializes and disposed of when the application is closed.
   - Useful for sharing data and methods across multiple components.

3. **Creating the Service:**
   - Inject the `HttpClient` service:
     ```typescript
     private http = inject(HttpClient);
     ```
   - Define a base URL (temporarily hard-coded for convenience):
     ```typescript
     private baseUrl = 'https://localhost:5001/api/';
     ```
   - Create a login method:
     ```typescript
     login(model: any) {
       return this.http.post(this.baseUrl + 'accounts/login', model);
     }
     ```
   - The `login` method makes an HTTP POST request with the provided model (username and password).

### Benefits of Using Services
- **Centralized HTTP Requests:**
  - Services provide a central place to make HTTP requests, improving code maintainability and readability.
- **State Management:**
  - Services can store state that needs to be shared across multiple components.
- **Dependency Injection:**
  - Services can be injected into any component that requires their functionality, promoting modular and reusable code.

---
## Using the Angular Service in a Component

#### Steps to Use the Service

1. **Ensure Correct URL in Service:**
   - Verify that the base URL string is accurate and includes the necessary forward slashes.
   - Example:
     ```typescript
     private baseUrl = 'https://localhost:5001/api/';
     ```

2. **Inject Service into Component:**
   - Open the component where you want to use the service (e.g., `nav.component.ts`).
   - Inject the `AccountService` into the component.
   - Example:
     ```typescript
     import { AccountService } from '_services/account.service';

     export class NavComponent {
       private accountService = inject(AccountService);
       loggedIn = false;

       login() {
         this.accountService.login(this.model).subscribe({
           next: response => {
             console.log(response);
             this.loggedIn = true;
           },
           error: error => {
             console.log(error);
           }
         });
       }
     }
     ```
   - Ensure the import statement is correct and the service is imported from the right path.

3. **Call the Login Method:**
   - Use the injected service to call the `login` method.
   - Pass the `model` (containing username and password) to the `login` method.
   - Subscribe to the observable returned by the `login` method to handle the response and error.

4. **Handling the Response:**
   - Log the response to the console.
   - Set a `loggedIn` property to `true` upon a successful login.
   - Log errors to the console for troubleshooting.

5. **Testing the Functionality:**
   - Test logging in with a correct username and password.
   - Test logging in with an incorrect password or username to verify error handling.
   - Observe the responses in the browser console.

#### Example Code for NavComponent

```typescript
import { Component } from '@angular/core';
import { AccountService } from '_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  private accountService = inject(AccountService);
  model: any = {};
  loggedIn = false;

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        this.loggedIn = true;
      },
      error: error => {
        console.log(error);
      }
    });
  }
}
```

### Next Steps

1. **Update the UI Based on Login State:**
   - Hide or show certain elements based on the `loggedIn` state.
   - Use Angular conditionals (`*ngIf`) in the template to conditionally display elements.

2. **Conditional Rendering in Template:**
   - Example:
     ```html
     <div *ngIf="loggedIn; else loginForm">
       <!-- Content for logged-in users -->
     </div>

     <ng-template #loginForm>
       <!-- Login form -->
       <form (ngSubmit)="login()">
         <!-- Username and Password fields -->
         <button type="submit">Login</button>
       </form>
     </ng-template>
     ```
---

## Implementing Conditional Rendering Based on Login Status

To control the visibility of elements based on whether the user is logged in, you can use Angular's structural directives and control flow features. Here are two methods: one compatible with Angular versions prior to 17 and another specific to Angular 17 and above.

### Method 1: Using `*ngIf` (Compatible with All Angular Versions)

1. **Add `*ngIf` Directive:**
   - Use the `*ngIf` directive to conditionally display elements based on the `loggedIn` flag.

2. **Import `NgIf` Directive:**
   - Import `NgIf` into your component if you are using standalone components.

   ```typescript
   import { NgIf } from '@angular/common';

   @Component({
     selector: 'app-nav',
     templateUrl: './nav.component.html',
     styleUrls: ['./nav.component.css'],
     standalone: true,
     imports: [NgIf]
   })
   export class NavComponent {
     private accountService = inject(AccountService);
     model: any = {};
     loggedIn = false;

     login() {
       this.accountService.login(this.model).subscribe({
         next: response => {
           console.log(response);
           this.loggedIn = true;
         },
         error: error => {
           console.log(error);
         }
       });
     }

     logout() {
       this.loggedIn = false;
     }
   }
   ```

3. **Template with `*ngIf`:**
   ```html
   <ul *ngIf="loggedIn">
     <!-- Links for logged-in users -->
   </ul>

   <form *ngIf="!loggedIn" (ngSubmit)="login()">
     <!-- Login form fields -->
     <button type="submit">Login</button>
   </form>
   ```

### Method 2: Using Angular 17 Control Flow Syntax

1. **Use `@if` Directive:**
   - Utilize the new control flow syntax available in Angular 17 and above.

2. **Modify the Template:**
   ```html
   @if (loggedIn) {
     <ul>
       <!-- Links for logged-in users -->
     </ul>
   }

   @if (!loggedIn) {
     <form (ngSubmit)="login()">
       <!-- Login form fields -->
       <button type="submit">Login</button>
     </form>
   }

   @if (loggedIn) {
     <div class="dropdown">
       <a class="dropdown-toggle text-light" role="button">
         Welcome user
       </a>
       <div class="dropdown-menu">
         <a class="dropdown-item" (click)="logout()">Logout</a>
       </div>
     </div>
   }
   ```

### Complete Component Example

```typescript
import { Component } from '@angular/core';
import { AccountService } from '_services/account.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  standalone: true,
  imports: [NgIf]
})
export class NavComponent {
  private accountService = inject(AccountService);
  model: any = {};
  loggedIn = false;

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
        this.loggedIn = true;
      },
      error: error => {
        console.log(error);
      }
    });
  }

  logout() {
    this.loggedIn = false;
  }
}
```

### Testing Conditional Rendering

1. **Initial State (Logged Out):**
   - The login form should be visible.
   - The links and dropdown should be hidden.

2. **Successful Login:**
   - Log in using valid credentials.
   - The login form should be hidden.
   - The links and dropdown should be visible.

3. **Logout:**
   - Click the logout link or button.
   - The login form should be visible again.
   - The links and dropdown should be hidden.

---

