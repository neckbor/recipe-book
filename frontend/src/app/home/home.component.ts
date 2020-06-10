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
      this.http.getRecipes(this.shareRecipes.filter).subscribe(data => {
          this.shareRecipes.recipes = data;
          console.log('Recipes: ' + this.shareRecipes.recipes);
          this.router.navigate(['all-recipes']).then(nav => console.log(nav), err => console.log(err));
        },
        error => console.log('Error http request on HomePage' + error));
  }

  public getRandomRecipe() {
    this.http.getRandomRecipe().subscribe( data => {
      this.router.navigate(['recipe', data.idRecipe]);
    },
      error => console.log(error));
  }
}
