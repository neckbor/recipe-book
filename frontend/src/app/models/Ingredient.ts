export class Ingredient {
  idIngredientList: any;
  name: string;
  amount: string;
  constructor(idIngredientList: any, ingredient: string, amount: string) {
    this.idIngredientList = idIngredientList;
    this.name = ingredient;
    this.amount = amount;
  }
}
