export class Filter {
  key: string;
  operation: string;
  value;
  constructor(key: string, operation: string, value) {
    this.key = key;
    this.operation = operation;
    this.value = value;
  }
}
