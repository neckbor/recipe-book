import {Injectable} from '@angular/core';
import {Recipe} from '../models/Recipe';

@Injectable({
  providedIn: 'root'
})
export class ShareService {
  recipes: Recipe[] = [];
}
