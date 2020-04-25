export class Step {
  idStep: number;
  orderIndex: number;
  description: string;
  constructor(idStep: number, orderIndex: number, description: string) {
    this.idStep = idStep;
    this.orderIndex = orderIndex;
    this.description = description;
  }
}
