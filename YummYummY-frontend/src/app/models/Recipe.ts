export class Recipe {
  id: number;
  name: string;
  idIngredient: number;
  nationality: string;
  constructor(id: number, name: string, idIngredient: number, nationality: string) {
    this.id = id;
    this.name = name;
    this.idIngredient = idIngredient;
    this.nationality = nationality;
  }
}
