import {Injectable} from '@angular/core';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';

@Injectable({
  providedIn: 'root'
})
export class ShareService {
  recipes: Recipe[] = [new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex'),
    new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex'),
    new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex'),
    new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex')];
  filter: Filter = new Filter();
}
