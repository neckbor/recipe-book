import { Component, OnInit } from '@angular/core';
import {Recipe} from '../models/Recipe';
import {RecipeService} from '../services/recipeService';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit {

  constructor(private http: RecipeService, private route: ActivatedRoute) { }

  recipe: Recipe;
  id: number;
  ngOnInit() {
    this.id = this.route.snapshot.params.id;
    this.http.getRecipeById(this.id).subscribe(
      data => {
        this.recipe = data;
        console.log('Получение рецепта прошло успешно');
      },
      err => console.log('Произошла ошибка при получении рецепта', err)
    );
  }
  done() {
    console.log('рецепт завершен');
  }

}
