import { Component, OnInit } from '@angular/core';
import {User} from '../models/User';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor() {}
  user: User = new User();
  repeatPassword: string;
  ngOnInit() {
  }

  private registration() {
    console.log(this.user.email);
  }

}
