export class Todo {
  id: number;
  title: string = '';
  complete: boolean = false;
  priority: number = 0;

  constructor(values: Object = {}) {
    Object.assign(this, values);
  }
}
