import {Step} from './Step';

export class SendRecipe {
  idRecipe: number;
  name: string;
  idIngredient: number;
  idNationality: number;
  duration: string;
  ingredientList: any[] = [];
  steps: Step[] = [];
  constructor(id: number, name: string, idIngredient: number,
              idNationality: number,duration: any, ingredients: any[], steps: Step[]) {
    this.idRecipe = id;
    this.name = name;
    this.idIngredient = idIngredient;
    this.idNationality = idNationality;
    this.duration = this.formatTime(duration);
    this.ingredientList = ingredients;
    this.steps = steps.sort(this.sortSteps);
  }
  private sortSteps(a, b) {
    return a.orderIndex - b.orderIndex;
  }

  private formatTime (duration) {
    return duration.value.hours + ':' + duration.value.minutes;
  }
}
