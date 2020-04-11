export class Filter {
  name: any = '';
  mainIngredient: any = '';
  nationality: any = '';
  author: any = '';
  constructor() {
  }
  setValues(name: any, mainIngredient: any, nationality: any, author: any) {
    this.name = name;
    this.mainIngredient = mainIngredient;
    this.nationality = nationality;
    this.author = author;
  }
  public reset() {
    this.name = '';
    this.mainIngredient = '';
    this.nationality = '';
    this.author = '';
  }
}
