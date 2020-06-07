import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';
import {CookieService} from 'ngx-cookie-service';
import {User} from '../models/User';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private http: RecipeService, private router: Router, private cookie: CookieService) {}
  user: User = new User();
  check = false;

  ngOnInit() {
  }

  private loginUser() {
    this.http.loginUser(this.user).subscribe(
      (response) => {
        this.user.access_token = response.body.access_token;
        this.router.navigate(['/']);
        this.cookie.set('login', this.user.login);
        // this.cookie.set('role', this.user.role);
        this.cookie.set('access_token', this.user.access_token);
        console.log('Авторизация прошла успешно');
      }, err => {
        this.check = err.status === 400;
        console.log(err);
      }
    );
  }


}
