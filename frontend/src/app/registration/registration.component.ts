import { Component, OnInit } from '@angular/core';
import {User} from '../models/User';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';
import {error} from 'util';
import {CookieService} from 'ngx-cookie-service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(private http: RecipeService, private router: Router, private cookie: CookieService) {}
  user: User = new User();
  repeatPassword: string;
  receivedAndwer: Response;
  check = false;
  ngOnInit() {
    if (this.cookie.get('access_token')) {
      this.router.navigate(['/']);
    }
  }

  private registration() {
    this.http.registerUser(this.user).subscribe(
      (data) => {
        this.user.access_token = data.body.access_token;
        this.user.role = data.body.role;
        this.router.navigate(['/']);
        this.cookie.set('login', this.user.login);
        this.cookie.set('role', this.user.role);
        this.cookie.set('access_token', this.user.access_token);
        console.log('Регистрация прошла успешно');
      }, err => {
        console.log(err);
        this.check = err.status === 409;
      }
    );
  }

}
