import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {CookieService} from 'ngx-cookie-service';
import {ActivatedRoute, Router} from '@angular/router';
import {Subscription} from 'rxjs';
import {Recipe} from '../models/Recipe';
import {SelectedValue} from "../models/SelectedValue";
import {Ingredient} from "../models/Ingredient";
import {Nationality} from "../models/Nationality";
import {Step} from "../models/Step";
import {SendRecipe} from "../models/SendRecipe";

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
  nationalities: Nationality[] = [];
  ingredients: Ingredient[] = [];
  id: number;
  recipe: Recipe;
  selectedValue;
  currentNationality: Nationality;
  currentMainIngredient: Ingredient;

  ngOnInit() {
    if (!this.cookie.get('access_token')) {
      this.router.navigate(['/']);
    } else {
      this.http.getRecipeById(this.id).subscribe(data => {
        this.recipe = data;
        console.log(this.recipe);
        this.selectedValue = this.recipe.ingredients;
      },
        err => console.log(err));
      this.http.getAllNationalities(this.cookie.get('access_token')).subscribe(data => {
        this.nationalities = data.body;
        console.log(this.nationalities);
          this.currentNationality = this.nationalities.find( x => x.name == this.recipe.nationality);
          console.log(this.currentNationality);
      },
      error => console.log(error));
      this.http.getAllIngredients(this.cookie.get('access_token')).subscribe(data => {
          this.ingredients = data.body;
          console.log(this.ingredients);
          this.currentMainIngredient = this.ingredients.find(x => x.name == this.recipe.mainIngredient);
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

  public addStep() {
    this.recipe.steps.push(new Step(0, this.recipe.steps[this.recipe.steps.length - 1].orderIndex + 1, ""));
  }

  public updateRecipe() {
    const recipe = new SendRecipe(this.recipe.idRecipe, this.recipe.name, this.currentMainIngredient.idIngredient, this.currentNationality.idNationality,
      this.recipe.duration, this.selectedValue, this.recipe.steps);
    console.log(recipe);
    this.http.updateRecipe(recipe, this.cookie.get('access_token')).subscribe( data => {
      console.log('Успешно обновлено');
    },
      err => console.log(err)
    );
  }

  public compareNationalitites(nat1: Nationality, nat2: Nationality) {
    return nat1 && nat2 ? nat1.name === nat2.name : nat1 === nat2;
  }

}
