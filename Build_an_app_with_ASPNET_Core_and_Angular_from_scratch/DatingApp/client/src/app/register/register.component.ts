import { Component, input, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  accountService = inject(AccountService);

  userFromHomeComponent = input.required<any>();
  cancelRegister = output<boolean>();
  model: any = {};
  user: any = {};

  register() {
    this.accountService.register(this.model).subscribe({
      next: (response: any) => {
        this.user = response.username;
        this.cancel()
      },
      error: (error) => {
        console.log("🌋🌋",error);
      },
    });
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
