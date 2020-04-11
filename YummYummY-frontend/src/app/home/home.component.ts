import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {ShareService} from '../services/shareService';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private http: RecipeService, private shareRecipes: ShareService, private router: Router) {
  }

  name: string;
  ingredient: string;
  origin: string;
  author: string;

  ngOnInit() {
    this.ingredient = null;
    this.origin = null;
    this.author = null;
    this.shareRecipes.filter.reset();
  }

  private searchRecipe() {
    if (this.ingredient || this.origin || this.author || this.name) {
      this.shareRecipes.filter.setValues(this.name, this.ingredient, this.origin, this.author);
      this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
          this.shareRecipes.recipes = data;
          console.log('Recipes: ' + this.shareRecipes.recipes);
          this.shareRecipes.filter.reset();
          this.router.navigate(['all-recipes']).then(nav => console.log(nav), err => console.log(err));
        },
        error => console.log('Error http request on HomePage' + error));
    }
  }
}
