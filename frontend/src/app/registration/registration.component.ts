import { Component, OnInit } from '@angular/core';
import {User} from '../models/User';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';
import {error} from 'util';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(private http: RecipeService, private router: Router) {}
  user: User = new User();
  repeatPassword: string;
  receivedAndwer: Response;
  ngOnInit() {
  }

  private registration() {
    console.log(this.user.email);
    this.http.registerUser(this.user).subscribe(
      (data) => {
        this.user.access_token = data.access_token;
        this.router.navigate(['/']);
        console.log('Регистрация прошла успешно');
      }, err => console.log(err)
    );
  }

}
