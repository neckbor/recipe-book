import {Step} from './Step';

export class SendRecipe {
  idRecipe: number;
  name: string;
  idIngredient: number;
  idNationality: number;
  author: string;
  duration: string;
  ingredients: any[] = [];
  steps: Step[] = [];
  constructor(id: number, name: string, idIngredient: number,
              idNationality: number, author: string, duration: any, ingredients: any[], steps: Step[]) {
    this.idRecipe = id;
    this.name = name;
    this.idIngredient = idIngredient;
    this.idNationality = idNationality;
    this.author = author;
    this.duration = duration;
    this.ingredients = ingredients;
    this.steps = steps.sort(this.sortSteps);
  }
  private sortSteps(a, b) {
    return a.orderIndex - b.orderIndex;
  }
}
