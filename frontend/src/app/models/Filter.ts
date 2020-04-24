export class Filter {
  recipeName: any;
  ingredient: any;
  nationality: any;
  author: any;
  constructor() {
    this.recipeName = '';
    this.ingredient = '';
    this.nationality = '';
    this.author = '';
  }
  setValues(name: any, mainIngredient: any, nationality: any, author: any) {
    this.recipeName = name;
    this.ingredient = mainIngredient;
    this.nationality = nationality;
    this.author = author;
  }
  public reset() {
    this.recipeName = '';
    this.ingredient = '';
    this.nationality = '';
    this.author = '';
  }
}
