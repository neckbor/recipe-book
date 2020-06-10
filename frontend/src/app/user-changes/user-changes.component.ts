import { Component, OnInit } from '@angular/core';
import {CookieService} from "ngx-cookie-service";
import {RecipeService} from "../services/recipeService";
import {ChangeUser} from "../models/changeUser";
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-changes',
  templateUrl: './user-changes.component.html',
  styleUrls: ['./user-changes.component.scss']
})
export class UserChangesComponent implements OnInit {

  constructor(private cookie: CookieService, private http: RecipeService, private router: Router) { }

  login = this.cookie.get('login');
  password = '';

  ngOnInit() {
    if (!this.cookie.get('login')) {
      this.router.navigate(['/']);
    } else {
      this.login = this.cookie.get('login');
    }
  }

  updateUser() {
    if(this.login == '') {
      alert('Логин должен быть заполнен');
    }
    if (this.login == this.cookie.get('login') && this.password == '') {
      this.router.navigate(['/']);
    } else {
      const user = new ChangeUser();
      user.login = this.login;
      user.oldLogin = this.cookie.get('login');
      user.password = this.password;
      this.http.updateUser(user, this.cookie.get('access_token')).subscribe(data => {
          console.log('Успешно обновлено');
          this.cookie.set('login', data.body.login);
          this.cookie.set('access_token', data.body.access_token);
          this.router.navigate(['/']);
        },
        err => console.log(err));
    }
  }

}
