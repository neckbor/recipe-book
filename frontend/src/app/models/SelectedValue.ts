import {Ingredient} from "./Ingredient";

export class SelectedValue {
  ingredient: Ingredient;
  count: any;

  constructor(ingredient) {
    this.ingredient = ingredient;
    this.count = '';
  }

}
