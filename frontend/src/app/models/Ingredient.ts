export class Ingredient {
  idIngredientList: number;
  name: string;
  amount: string;
  constructor(idIngredientList: number, ingredient: string, amount: string) {
    this.idIngredientList = idIngredientList;
    this.name = ingredient;
    this.amount = amount;
  }
}
