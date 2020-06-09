import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';
import {User} from '../models/User';
import {environment} from '../../environments/environment';
import {Nationality} from '../models/Nationality';
import {Ingredient} from "../models/Ingredient";

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {
  }
  getRecipes(filters: Filter): Observable<Recipe[]> {
    const url = this.baseUrl + '/search';
    return this.http.post<Recipe[]>(url, filters, {
      headers: new HttpHeaders({'Content-Type': 'application/json'
      })
    });
  }
  getRecipeById(id: number): Observable<Recipe> {
    const url = this.baseUrl + '/GetRecipe?idRecipe=' + id;
    return this.http.get<Recipe>(url);
  }
  registerUser(user: User) {
    const url = this.baseUrl + '/Account/register';
    return this.http.post<User>(url, user, {
      headers: new HttpHeaders({'Content-Type': 'application/json'
      })
    });
  }
  loginUser(user: User) {
    const url = this.baseUrl + '/Account/login';
    return this.http.post<User>(url, user, {
      headers: new HttpHeaders({'Content-Type': 'application/json'}),
      observe: 'response'
    });
  }
  getAllNationalities(token) {
    const url = this.baseUrl + '/ReferenceDataManager/searchNationalities';
    const nationality = new Nationality();
    nationality.name = '';
    return this.http.post<Nationality[]>(url, nationality, {
      headers: new HttpHeaders({
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`}
        ),
      observe: 'response'
    });
  }
  getAllIngredients(token) {
    const url = this.baseUrl + '/ReferenceDataManager/searchIngredients';
    const ingredient = new Nationality();
    ingredient.name = '';
    return this.http.post<Ingredient[]>(url, ingredient, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    });
  }

  updateRecipe(recipe: Recipe, token) {
    const url = this.baseUrl + '/RecipeManager/add';
    return this.http.post(url, Recipe, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }
}
