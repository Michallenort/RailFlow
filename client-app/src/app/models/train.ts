export interface Train {
  id: string;
  number: number;
  maxSpeed: number | null;
  capacity: number;
}

export interface TrainFormValues {
  number: number;
  maxSpeed: number | null;
  capacity: number;
}

export class Train implements Train {
  constructor(init?: TrainFormValues) {
    Object.assign(this, init);
  }
}