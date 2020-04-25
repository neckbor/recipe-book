import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private baseUrl = 'https://localhost:44383/api';
  constructor(private http: HttpClient) {
  }
  getRecipes(filters: Filter): Observable<Recipe[]> {
    const url = this.baseUrl + '/search'
    return this.http.post<Recipe[]>(url, filters, {
      headers: new HttpHeaders({'Content-Type': 'application/json'
      })
    });
  }
  getRecipeById(id: number): Observable<Recipe> {
    const url = this.baseUrl + '/GetRecipe?idRecipe=' + id;
    return this.http.get<Recipe>(url);
  }
}
