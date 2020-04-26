import { Component, OnInit } from '@angular/core';
import {ShareService} from '../services/shareService';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';

@Component({
  selector: 'app-all-recipes',
  templateUrl: './all-recipes.component.html',
  styleUrls: ['./all-recipes.component.scss']
})
export class AllRecipesComponent implements OnInit {

  constructor(private shareRecipes: ShareService, private http: RecipeService, private router: Router) {}

  ngOnInit() {
    this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
        this.shareRecipes.recipes = data;
        console.log(this.shareRecipes.recipes[0].duration);
        console.log('Recipes: ' + this.shareRecipes.recipes);
      },
      error => console.log('Error http request on allRecipesPage' + error));
  }

  private searchRecipe() {
    if (this.shareRecipes.filter.ingredient ||
      this.shareRecipes.filter.nationality ||
      this.shareRecipes.filter.author ||
      this.shareRecipes.filter.recipeName) {
      this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
          this.shareRecipes.recipes = data;
          console.log('Recipes: ' + this.shareRecipes.recipes);
        },
        error => console.log('Error http request on allRecipesPage' + error));
    }
  }
  private reset() {
    this.shareRecipes.filter.reset();
    this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
        this.shareRecipes.recipes = data;
        console.log('Recipes: ' + this.shareRecipes.recipes);
      },
      error => console.log('Error http request on allRecipesPage' + error));
  }

  private seeRecipe(id: number) {
    this.router.navigate(['recipe', id]);
  }

}
