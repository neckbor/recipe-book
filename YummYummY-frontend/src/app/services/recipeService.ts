import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  // private baseUrl = 'http://localhost:8089/recipes';
  private baseUrl = 'https://ptsv2.com/t/0szvs-1586612145';
  constructor(private http: HttpClient) {
  }
  getRecipes(filters: Filter[]): Observable<Recipe[]> {
    return this.http.post<Recipe[]>(this.baseUrl, filters, {
      headers: new HttpHeaders({'Content-Type': 'application/json'
      })
    });
  }
}
