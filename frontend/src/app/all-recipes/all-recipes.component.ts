import { Component, OnInit } from '@angular/core';
import {ShareService} from '../services/shareService';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';
import {CookieService} from "ngx-cookie-service";
import {Filter} from "../models/Filter";

@Component({
  selector: 'app-all-recipes',
  templateUrl: './all-recipes.component.html',
  styleUrls: ['./all-recipes.component.scss']
})
export class AllRecipesComponent implements OnInit {

  role;

  constructor(private shareRecipes: ShareService, private http: RecipeService, private router: Router, private cookie: CookieService) {}

  ngOnInit() {
    this.role = this.cookie.get('role');
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

  private changeRecipe(id: number) {
    this.router.navigate(['change-recipe', id]);
  }

  private deleteRecipe(id: number) {
    this.http.deleteRecipe(id, this.cookie.get('access_token')).subscribe( data => {
        console.log('Удаление прошло успешно');
        const filter = new Filter();
        this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
            this.shareRecipes.recipes = data;
            console.log(this.shareRecipes.recipes[0].duration);
            console.log('Recipes: ' + this.shareRecipes.recipes);
          },
          error => console.log('Error http request on allRecipesPage' + error));
      },
      err => console.log(err));
  }

}
