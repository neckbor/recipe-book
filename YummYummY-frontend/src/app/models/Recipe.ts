export class Recipe {
  IDRecipe: number;
  Name: string;
  MainIngredient: string;
  Nationality: string;
  Author: string;
  constructor(id: number, name: string, mainIngredient: string, nationality: string, author: string) {
    this.IDRecipe = id;
    this.Name = name;
    this.MainIngredient = mainIngredient;
    this.Nationality = nationality;
    this.Author = author;
  }
}
