### **Creating a Member Service in Angular**

---

#### **1. Introduction to Member Service**
   - **Objective**: Create a member service in Angular to handle HTTP requests for fetching data from the API server.
   - **Tools**: Use the Angular Command Line Interface (CLI) to generate the service.

#### **2. Creating the Member Service with Angular CLI**
   - **Command**: Use the Angular CLI to generate the service:
     - Command: `ng g s services/members --skip-tests`
     - This creates a service named `members` in the `services` folder, and skips generating a test file.

#### **3. Injecting HTTP Client into the Service**
   - **Purpose**: To make HTTP requests, the `HttpClient` needs to be injected into the service.
   - **Implementation**:
     - Inject `HttpClient` using Angular's `inject()` function:
       ```typescript
       private http = inject(HttpClient);
       ```
   - **Base URL**: Instead of hardcoding the API base URL, use an environment configuration file.

#### **4. Setting Up Environment Configuration Files**
   - **Background**:
     - Angular previously had environment configuration files by default. These were removed but can be regenerated using the CLI.
     - Configuration files allow setting environment-specific variables like API URLs.
   - **Recreating Environment Files**:
     - Command: `ng g environments`
     - This command creates two files: `environment.ts` and `environment.prod.ts`, and updates the `angular.json` file.
   - **Configuration**:
     - **Development File** (`environment.ts`):
       ```typescript
       export const environment = {
         production: false,
         apiUrl: 'https://localhost:5001/api/'
       };
       ```
     - **Production File** (`environment.prod.ts`):
       ```typescript
       export const environment = {
         production: true,
         apiUrl: '/api/'
       };
       ```

#### **5. Using Environment Variables in the Service**
   - **Import Environment Configuration**:
     - Import the correct environment configuration based on the build mode (development or production).
     - Ensure that the correct environment file is selected to avoid issues when deploying to production.
   - **Usage in Service**:
     ```typescript
     const baseUrl = environment.apiUrl;
     ```

#### **6. Updating Other Services and Components**
   - **Account Service**:
     - Update the `AccountService` to use the environment-based API URL instead of a hardcoded one.
   - **Test Errors Component**:
     - Similarly, update the `TestErrorsComponent` to use the environment-based API URL.

#### **7. Creating Methods in Member Service**
   - **Get Members**:
     - Method to fetch the list of members:
       ```typescript
       getMembers(): Observable<Member[]> {
         return this.http.get<Member[]>(`${baseUrl}/users`, this.getHttpOptions());
       }
       ```
   - **Get Member by Username**:
     - Method to fetch a specific member by username:
       ```typescript
       getMember(username: string): Observable<Member> {
         return this.http.get<Member>(`${baseUrl}/users/${username}`, this.getHttpOptions());
       }
       ```

#### **8. Handling Authentication**
   - **Authorize Attribute**:
     - API endpoints are protected by an `Authorize` attribute, so HTTP requests need to include an authorization token.
   - **Get HTTP Options Method**:
     - Method to generate HTTP options including the authorization header:
       ```typescript
       private getHttpOptions() {
         return {
           headers: new HttpHeaders({
             'Authorization': `Bearer ${this.accountService.currentUser?.token}`
           })
         };
       }
       ```
     - **Template Literals**: Use backticks (`) for template literals to ensure proper spacing and formatting in the `Authorization` header.

#### **9. Conclusion and Next Steps**
   - **Service Completion**: The member service is now set up to handle HTTP requests with authentication.
   - **Next Task**: The next step involves fetching and displaying the list of members using this service.

---
