import { Component, input } from '@angular/core';
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
  protected creds = {} as RegisterCreds

  register() {
    console.log(this.creds);
  }

  cancel() {
    console.log("cancelled!");
  }
}
