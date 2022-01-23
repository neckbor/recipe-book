export class Ingredient {
  idIngredientList: any;
  idIngredient: any;
  name: string;
  amount: string;
  constructor(idIngredientList: any, ingredient: string, amount: string, idIngredient: any) {
    this.idIngredientList = idIngredientList;
    this.name = ingredient;
    this.amount = amount;
    this.idIngredient = idIngredient;
  }
}
