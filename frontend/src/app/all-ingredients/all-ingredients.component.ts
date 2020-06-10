import { Component, OnInit } from '@angular/core';
import {CookieService} from "ngx-cookie-service";
import {Router} from "@angular/router";
import {RecipeService} from "../services/recipeService";
import {Ingredient} from "../models/Ingredient";

@Component({
  selector: 'app-all-ingredients',
  templateUrl: './all-ingredients.component.html',
  styleUrls: ['./all-ingredients.component.scss']
})
export class AllIngredientsComponent implements OnInit {

  constructor(private cookie: CookieService, private router: Router, private http: RecipeService) { }

  ingredients;
  nameIngredient = '';

  ngOnInit() {
    if (this.cookie.get('role') != 'admin') {
      this.router.navigate(['/']);
    } else {
      this.http.getAllIngredients(this.cookie.get('access_token')).subscribe( data => {
        this.ingredients = data.body;
      },
        err => console.log(err));
    }
  }
  public deleteIngredient(id) {
    this.http.deleteIngredient(id, this.cookie.get('access_token')).subscribe( data => {
      console.log('Успешно удален');
      this.http.getAllIngredients(this.cookie.get('access_token')).subscribe( data => {
          this.ingredients = data.body;
        },
        err => {
        console.log(err);
        });
    },
      err => {
        console.log(err);
        if(err.status === 500) {
          alert('Этот ингридиент используется в рецепте. Его нельзя удалить. Сначала удалите рецепт.')
        }})
  }

  public updateIngredient(ingredient) {
    this.http.updateIngredient(ingredient, this.cookie.get('access_token')).subscribe( data => {
      console.log('Умпешно изменен');
      this.http.getAllIngredients(this.cookie.get('access_token')).subscribe( data => {
          this.ingredients = data.body;
        },
        err => {
          console.log(err);
        },);
    },
      err => console.log(err))
  }

  public addIngredient() {
    if (this.nameIngredient == ''){
      alert('Ингридент не должен быть пустым')
    } else{
      const ing = new Ingredient(0, this.nameIngredient, '', 0);
      this.http.addIngredient(ing, this.cookie.get('access_token')).subscribe( data => {
        console.log('Успешно добавлен');
        this.http.getAllIngredients(this.cookie.get('access_token')).subscribe( data => {
            this.ingredients = data.body;
          },
          err => {
            console.log(err);
          });
      },
        err => console.log(err));
    }
  }

}
