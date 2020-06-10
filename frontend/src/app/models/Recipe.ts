import {Ingredient} from './Ingredient';
import {Step} from './Step';
import {TimeSpan} from 'ts-timespan/dist/timespan';

export class Recipe {
  idRecipe: number;
  name: string;
  mainIngredient: string;
  nationality: string;
  author: string;
  duration: TimeSpan;
  ingredients: Ingredient[] = [];
  steps: Step[] = [];
  constructor(id: number, name: string, mainIngredient: string,
              nationality: string, author: string, duration: any, ingredients: Ingredient[], steps: Step[]) {
    this.idRecipe = id;
    this.name = name;
    this.mainIngredient = mainIngredient;
    this.nationality = nationality;
    this.author = author;
    this.duration = duration;
    this.ingredients = ingredients;
    this.steps = steps.sort(this.sortSteps);
  }
  private sortSteps(a, b) {
    return a.orderIndex - b.orderIndex;
  }
}
