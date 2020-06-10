import { Component, OnInit } from '@angular/core';
import {faBars} from '@fortawesome/free-solid-svg-icons';
import {faTimes} from '@fortawesome/free-solid-svg-icons';
import {User} from '../models/User';
import {CookieService} from 'ngx-cookie-service';
import {RecipeService} from "../services/recipeService";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  faBars = faBars;
  faTimes = faTimes;
  login: string;
  constructor(private cookie: CookieService, private http: RecipeService, private router: Router) {
    this.login = cookie.get('login');
  }

  ngOnInit() {
  }
  private adaptiveHeader() {
    document.getElementById('menu-toggle').classList.toggle('active');
    document.getElementById('nav').classList.toggle('active');
  }
  private exit() {
    this.cookie.delete('login');
    this.cookie.delete('access_token');
    this.cookie.delete('role');
  }

  public getRandom() {
    this.http.getRandomRecipe().subscribe( data => {
        this.router.navigate(['recipe', data.idRecipe]);
      },
      error => console.log(error));
  }

}
