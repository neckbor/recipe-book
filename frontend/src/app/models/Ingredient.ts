export class Ingredient {
  idIngredientList: number;
  ingredient: string;
  amount: string;
  constructor(idIngredientList: number, ingredient: string, amount: string) {
    this.idIngredientList = idIngredientList;
    this.ingredient = ingredient;
    this.amount = amount;
  }
}
