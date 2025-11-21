import { Component, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { AppUser } from '../../types/appUser';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  @Input({required: true}) membersFromApp: AppUser[] = [];
  protected registerMode = signal(false);
  
  showRegister() {
    this.registerMode.set(true);
  }
}
