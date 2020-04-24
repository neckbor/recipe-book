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

  ngOnInit() {
    this.shareRecipes.filter.reset();
  }

  private searchRecipe() {
    if (this.shareRecipes.filter.mainIngredient ||
      this.shareRecipes.filter.nationality ||
      this.shareRecipes.filter.author ||
      this.shareRecipes.filter.name) {
      this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
          this.shareRecipes.recipes = data;
          console.log('Recipes: ' + this.shareRecipes.recipes);
          this.router.navigate(['all-recipes']).then(nav => console.log(nav), err => console.log(err));
        },
        error => console.log('Error http request on HomePage' + error));
    }
  }
}
