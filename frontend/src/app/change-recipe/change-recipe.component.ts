import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {CookieService} from 'ngx-cookie-service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-change-recipe',
  templateUrl: './change-recipe.component.html',
  styleUrls: ['./change-recipe.component.scss']
})
export class ChangeRecipeComponent implements OnInit {

  constructor(private http: RecipeService, private cookie: CookieService, private router: Router) { }
  nationalities: any;
  ingredients: any;

  ngOnInit() {
    if (!this.cookie.get('access_token')) {
      this.router.navigate(['/']);
    } else {
    this.http.getAllNationalities(this.cookie.get('access_token')).subscribe(data => {
        this.nationalities = data.body;
        console.log(this.nationalities);
      },
      error => console.log(error));
    this.http.getAllIngredients(this.cookie.get('access_token')).subscribe(data => {
          this.ingredients = data.body;
          console.log(this.nationalities);
        },
        error => console.log(error));
    }
  }

}
