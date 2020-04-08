import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {Filter} from '../models/Filter';
import {ShareService} from '../services/shareService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private http: RecipeService, private shareRecipes: ShareService) {
  }

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
    if (this.ingredient) {
      this.listFilters.push(new Filter('ingredient', ':', this.ingredient));
    }
    if (this.origin) {
      this.listFilters.push(new Filter('origin', ':', this.origin));
    }
    if (this.author) {
      this.listFilters.push(new Filter('author', ':', this.author));
    }
    this.http.getRecipes(this.listFilters).subscribe(data => {
        this.shareRecipes.recipes = data;
        console.log('Recipes: ' + this.shareRecipes.recipes);
        this.listFilters = [];
      },
      error => console.log('Error http request on HomePage'));
  }
}
