import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {CookieService} from 'ngx-cookie-service';
import {ActivatedRoute, Router} from '@angular/router';
import {Subscription} from 'rxjs';
import {Recipe} from '../models/Recipe';

@Component({
  selector: 'app-change-recipe',
  templateUrl: './change-recipe.component.html',
  styleUrls: ['./change-recipe.component.scss']
})
export class ChangeRecipeComponent implements OnInit {

  constructor(private http: RecipeService, private cookie: CookieService, private router: Router, private route: ActivatedRoute) {
    this.routeSubscription = route.params.subscribe(params => this.id = params.id);
  }
  private routeSubscription: Subscription;
  nationalities: any;
  ingredients: any;
  id: number;
  recipe: Recipe;

  ngOnInit() {
    if (!this.cookie.get('access_token')) {
      this.router.navigate(['/']);
    } else {
      this.http.getRecipeById(this.id).subscribe(data => {
        this.recipe = data;
      },
        err => console.log(err));
      // this.http.getAllNationalities(this.cookie.get('access_token')).subscribe(data => {
      //   this.nationalities = data.body;
      //   console.log(this.nationalities);
      // },
      // error => console.log(error));
      // this.http.getAllIngredients(this.cookie.get('access_token')).subscribe(data => {
      //     this.ingredients = data.body;
      //     console.log(this.nationalities);
      //   },
      //   error => console.log(error));
    }
  }

}
