### Implementing Auth Guards in Angular

**Overview:**
- **Purpose:** Prevent users from accessing unauthorized routes.
- **Note:** This is a user interface feature, not a security measure. True security must be enforced at the backend API level.

**Steps to Implement Auth Guards:**

1. **Create an Auth Guard:**
   - Open the terminal and use the Angular CLI.
   - Generate a guard:
     ```bash
     ng g guard guards/auth --skip-tests
     ```
   - Select `canActivate`.

2. **Auth Guard Functionality:**
   - Auth Guard uses `canActivate` to determine if a route can be activated.
   - Inject necessary services:
     - `AccountService` to check user authentication.
     - `ToasterService` to display messages.

3. **Auth Guard Implementation:**
   - Define `accountService` and `toaster` using `inject`.
   - Check if the user is authenticated:
     ```typescript
     const accountService = inject(AccountService);
     const toaster = inject(ToasterService);

     return accountService.currentUser() ? true : (
       toaster.error('You shall not pass!'),
       false
     );
     ```

4. **Protect Routes:**
   - Add `canActivate` to routes in `app.routes.ts`:
     ```typescript
     { path: 'members', component: MembersComponent, canActivate: [AuthGuard] }
     ```

5. **Testing the Auth Guard:**
   - Remove any test conditions from `nav.component.html` for testing:
     ```html
     <ng-container *ngIf="true">
       <!-- Navigation Links -->
     </ng-container>
     ```
   - Test by attempting to access protected routes when not logged in and verifying the guard works as expected.

**Important Considerations:**
- Auth guards are only a supplementary measure for user interface flow.
- Real security must be enforced on the backend API.
- Assume all frontend code can be accessed and manipulated by users.
