import { Component, OnInit } from '@angular/core';
import {RecipeService} from "../services/recipeService";
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";
import {Nationality} from "../models/Nationality";
import {Ingredient} from "../models/Ingredient";
import {Step} from "../models/Step";
import {SendRecipe} from "../models/SendRecipe";

declare var ym: any;

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss']
})
export class AddRecipeComponent implements OnInit {

  constructor(private http: RecipeService, private cookie: CookieService, private router: Router) { }

  nationalities: Nationality[] = [];
  ingredients: Ingredient[] = [];
  name;
  mainIngredient;
  nationality: Nationality;
  ingredientList: Ingredient[] = [];
  steps: Step[]=[];
  hours: string;
  minutes: string;

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
          console.log(this.ingredients);
        },
        error => console.log(error));
    }
    this.steps.push(new Step(1, 1, ''));
  }


  public addStep() {
    this.steps.push(new Step(1, this.steps[this.steps.length - 1].orderIndex + 1, ""));
  }

  public addRecipe() {
    const duration = this.hours + ':' + this.minutes;
    console.log(this.hours);
    console.log(duration);
    const recipe = new SendRecipe(0, this.name, this.mainIngredient.idIngredient, this.nationality.idNationality,
      duration, this.ingredientList, this.steps);
    recipe.author = this.cookie.get('login');
    console.log(recipe);
    this.http.addRecipe(recipe, this.cookie.get('access_token')).subscribe( data => {
        console.log('Успешно добавлено');
        ym(64770259,'reachGoal','add_recipe');
        this.router.navigate(['/']);
      },
      err => console.log(err)
    );
  }

}
