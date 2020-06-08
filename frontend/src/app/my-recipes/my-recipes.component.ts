import { Component, OnInit } from '@angular/core';
import {RecipeService} from '../services/recipeService';
import {Router} from '@angular/router';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';
import {CookieService} from 'ngx-cookie-service';

@Component({
  selector: 'app-my-recipes',
  templateUrl: './my-recipes.component.html',
  styleUrls: ['./my-recipes.component.scss']
})
export class MyRecipesComponent implements OnInit {

  constructor(private http: RecipeService, private router: Router, private cookie: CookieService) { }
  myRecipes: Recipe[];
  checkCreate = false;

  ngOnInit() {
    if (!this.cookie.get('login')) {
      this.router.navigate(['/']);
    }
    const filter = new Filter();
    this.checkCreate = !!(this.cookie.get('role') === 'open' || 'admin');
    filter.author = this.cookie.get('login');
    this.http.getRecipes(filter).subscribe(data => {
      this.myRecipes = data;
    },
      error => console.log(error));
  }

  private seeRecipe(id: number) {
    this.router.navigate(['recipe', id]);
  }

  private changeRecipe(id: number) {
    this.router.navigate(['change-recipe', id]);
  }

  private deleteRecipe(id: number) {

  }

}
