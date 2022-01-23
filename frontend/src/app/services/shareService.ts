import {Injectable} from '@angular/core';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';

@Injectable({
  providedIn: 'root'
})
export class ShareService {
  recipes: Recipe[] = [];
  filter: Filter = new Filter();
  randomRecipe: Recipe;
}
