import {Injectable} from '@angular/core';
import {Recipe} from '../models/Recipe';
import {Filter} from '../models/Filter';

@Injectable({
  providedIn: 'root'
})
export class ShareService {
  recipes: Recipe[] = [new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex', '1000'),
    new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex', '34'),
    new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex', '35'),
    new Recipe(1, 'яйцо', 'яйцо ин', 'Россия', 'alex', '35')];
  filter: Filter = new Filter();
}
