import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';
import {User} from '../models/User';
import {environment} from '../../environments/environment';
import {Nationality} from '../models/Nationality';
import {Ingredient} from "../models/Ingredient";
import {SendRecipe} from "../models/SendRecipe";
import {ChangeUser} from "../models/changeUser";

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
      headers: new HttpHeaders({'Content-Type': 'application/json'}),
      observe: 'response'
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

  updateRecipe(recipe: SendRecipe, token) {
    const url = this.baseUrl + '/RecipeManager/update';
    return this.http.post<SendRecipe>(url, recipe, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }

  updateUser(user: ChangeUser, token) {
    const url = this.baseUrl + '/Account/change';
    return this.http.post<User>(url, user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }

  addRecipe(recipe: SendRecipe, token) {
    const url = this.baseUrl + '/RecipeManager/add';
    return this.http.post<SendRecipe>(url, recipe, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }

  deleteRecipe(id: number, token){
    const url = this.baseUrl + '/RecipeManager/delete/?idRecipe=' + id;
    return this.http.delete(url, {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }

  getAllUsers(token) {
    const url = this.baseUrl + '/Account/search';
    const user = new User();
    user.login = '';
    return this.http.post<User[]>(url, user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    });
  }

  blockUser(login, token) {
    const url = this.baseUrl + '/Account/block';
    const user = new User();
    user.login = login;
    return this.http.post<User[]>(url, user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    });
  }

  unBlockUser(login, token) {
    const url = this.baseUrl + '/Account/unblock';
    const user = new User();
    user.login = login;
    return this.http.post<User[]>(url, user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    });
  }

  getRandomRecipe() {
    const url = this.baseUrl + '/GetRecipe/random';
    return this.http.get<Recipe>(url);
  }

  deleteIngredient(id: number, token){
    const url = this.baseUrl + '/ReferenceDataManager/deleteIngredient?idIngredient=' + id;
    return this.http.delete(url, {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }
  updateIngredient(ingredient:Ingredient, token) {
    const url = this.baseUrl + '/ReferenceDataManager/updateIngredient';
    return this.http.post(url, ingredient, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }
  addIngredient(ingredient:Ingredient, token) {
    const url = this.baseUrl + '/ReferenceDataManager/addIngredient';
    return this.http.post(url, ingredient, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }

  addNationality(nationality:Nationality, token) {
    const url = this.baseUrl + '/ReferenceDataManager/addNationality';
    return this.http.post(url, nationality, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }

  updateNationality(nationality:Nationality, token) {
    const url = this.baseUrl + '/ReferenceDataManager/updateNationality';
    return this.http.post(url, nationality, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }
  deleteNationality(id: number, token){
    const url = this.baseUrl + '/ReferenceDataManager/deleteNationality?idNationality=' + id;
    return this.http.delete(url, {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`}
      ),
      observe: 'response'
    })
  }
}
