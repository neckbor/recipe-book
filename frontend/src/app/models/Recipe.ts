export class Recipe {
  idRecipe: number;
  name: string;
  ingredient: string;
  nationality: string;
  author: string;
  duration: string;
  constructor(id: number, name: string, mainIngredient: string, nationality: string, author: string, duration: string) {
    this.idRecipe = id;
    this.name = name;
    this.ingredient = mainIngredient;
    this.nationality = nationality;
    this.author = author;
    this.duration = duration;
  }
}
