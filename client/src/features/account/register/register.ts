import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppUser, RegisterCreds } from '../../../types/appUser';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  public membersFromHome = input.required<AppUser[]>();
  public cancelRegister = output<boolean>();
  protected creds = {} as RegisterCreds

  register() {
    console.log(this.creds);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
