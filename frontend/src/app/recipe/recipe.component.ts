import { Component, OnInit } from '@angular/core';
import {Recipe} from '../models/Recipe';
import {RecipeService} from '../services/recipeService';
import {ActivatedRoute} from '@angular/router';
import {Ingredient} from '../models/Ingredient';
import {Step} from '../models/Step';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit {

  constructor(private http: RecipeService, private route: ActivatedRoute) { }

  recipe: Recipe;
  id: number;
  completed = false;
  state: string;
  ngOnInit() {
    this.id = this.route.snapshot.params.id;
    this.http.getRecipeById(this.id).subscribe(
      data => {
        this.recipe = data;
        console.log('Получение рецепта прошло успешно');
      },
      err => console.log('Произошла ошибка при получении рецепта', err)
    );
    // this.recipe = new Recipe(1, 'яйцо', 'масло', 'Россия', 'alex', 30,
    //   [new Ingredient(1, 'масло', '2'), new Ingredient(1, 'Яйцо', '3')],
    //   [new Step(1, 1, 'поджарить'), new Step(2, 3, 'убрать'),
    //     new Step(3, 2, 'полить маслом')]);
    // console.log(this.recipe.ingredients[0].amount);
  }
  done() {
    console.log('рецепт завершен');
  }

}
