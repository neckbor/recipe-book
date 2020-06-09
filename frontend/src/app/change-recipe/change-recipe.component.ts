import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {CookieService} from 'ngx-cookie-service';
import {ActivatedRoute, Router} from '@angular/router';
import {Subscription} from 'rxjs';
import {Recipe} from '../models/Recipe';
import {SelectedValue} from "../models/SelectedValue";
import {Ingredient} from "../models/Ingredient";
import {Nationality} from "../models/Nationality";

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
  ingredients: Ingredient[] = [];
  id: number;
  recipe: Recipe;
  selectedValue;

  ngOnInit() {
    if (!this.cookie.get('access_token')) {
      this.router.navigate(['/']);
    } else {
      this.http.getRecipeById(this.id).subscribe(data => {
        this.recipe = data;
        this.selectedValue = this.recipe.ingredients;
      },
        err => console.log(err));
      this.http.getAllNationalities(this.cookie.get('access_token')).subscribe(data => {
        this.nationalities = data.body;
        console.log(this.nationalities);
      },
      error => console.log(error));
      this.http.getAllIngredients(this.cookie.get('access_token')).subscribe(data => {
          this.ingredients = data.body;
          console.log(this.ingredients);
        },
        error => console.log(error));
    }
  }

  public dependAmount() {
    console.log(this.selectedValue);
    // this.currentIngredients = [];
    // for (const i = 0; i < this.selectedValue.length; i++) {
    //   this.currentIngredients.push
    // }
    // this.amount = this.selectedValue ? [this.selectedValue.length] : [];
    // console.log('Размер' + this.amount.length);
  }

  public compareFn(ingredient1: Ingredient, ingredient2: Ingredient) {
    return ingredient1 && ingredient2 ? ingredient1.name === ingredient2.name : ingredient1 === ingredient2;
  }

  // public compareNationalitites(str1: String, nation: Nationality) {
  //   return str1 && nation ? str1 === nation.name : str1 === nation;
  // }

}
