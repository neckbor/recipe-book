import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {Filter} from '../models/Filter';
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
  listFilters: Filter[];

  ngOnInit() {
    this.ingredient = null;
    this.origin = null;
    this.author = null;
    this.listFilters = [];
  }

  private searchRecipe() {
    if (this.ingredient || this.origin || this.author || this.name) {
      this.listFilters.push(new Filter( this.name, this.ingredient, this.origin, this.author));
    }
    this.http.getRecipes(this.listFilters).subscribe(data => {
        this.shareRecipes.recipes = data;
        console.log('Recipes: ' + this.shareRecipes.recipes);
        this.listFilters = [];
        this.router.navigate(['all-recipes']).then(nav => console.log(nav), err => console.log(err));
      },
      error => console.log('Error http request on HomePage' + error));
  }
}
